// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.SignatureComparer
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal static class SignatureComparer
  {
    private const BindingFlags MembersDeclaredOnTypeOnly = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public static IEnumerable<MethodBase> FilterMethods(
      MethodFilter filter,
      MethodInfo[] allMethods)
    {
      List<MethodBase> methodBaseList = new List<MethodBase>();
      CallingConventions callingConvention = SignatureUtil.GetReflectionCallingConvention(filter.CallingConvention);
      foreach (MethodInfo allMethod in allMethods)
      {
        if (((MemberInfo) allMethod).Name.Equals(filter.Name, StringComparison.Ordinal) && SignatureUtil.IsCallingConventionMatch((MethodBase) allMethod, callingConvention) && SignatureUtil.IsGenericParametersCountMatch(allMethod, filter.GenericParameterCount) && ((MethodBase) allMethod).GetParameters().Length == filter.ParameterCount)
          methodBaseList.Add((MethodBase) allMethod);
      }
      return (IEnumerable<MethodBase>) methodBaseList;
    }

    public static IEnumerable<MethodBase> FilterConstructors(
      MethodFilter filter,
      ConstructorInfo[] allConstructors)
    {
      List<MethodBase> methodBaseList = new List<MethodBase>();
      CallingConventions callingConvention = SignatureUtil.GetReflectionCallingConvention(filter.CallingConvention);
      foreach (ConstructorInfo allConstructor in allConstructors)
      {
        if (((MemberInfo) allConstructor).Name.Equals(filter.Name, StringComparison.Ordinal) && SignatureUtil.IsCallingConventionMatch((MethodBase) allConstructor, callingConvention) && ((MethodBase) allConstructor).GetParameters().Length == filter.ParameterCount)
          methodBaseList.Add((MethodBase) allConstructor);
      }
      return (IEnumerable<MethodBase>) methodBaseList;
    }

    internal static bool IsParametersTypeMatch(
      MethodBase templateMethod,
      TypeSignatureDescriptor[] parameters)
    {
      ParameterInfo[] parameters1 = templateMethod.GetParameters();
      int length = parameters1.Length;
      for (int index = 0; index < length; ++index)
      {
        if (!parameters[index].Type.Equals(parameters1[index].ParameterType))
          return false;
      }
      return true;
    }

    public static MethodBase FindMatchingMethod(
      string methodName,
      Type typeToInspect,
      MethodSignatureDescriptor expectedSignature,
      GenericContext context)
    {
      bool flag1 = methodName.Equals(".ctor", StringComparison.Ordinal) || methodName.Equals(".cctor", StringComparison.Ordinal);
      int genericParameterCount = expectedSignature.GenericParameterCount;
      MethodFilter filter = new MethodFilter(methodName, genericParameterCount, expectedSignature.Parameters.Length, expectedSignature.CallingConvention);
      IEnumerable<MethodBase> methodBases = !flag1 ? SignatureComparer.FilterMethods(filter, typeToInspect.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) : SignatureComparer.FilterConstructors(filter, typeToInspect.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
      MethodBase matchingMethod = (MethodBase) null;
      bool flag2 = false;
      foreach (MethodBase methodBase1 in methodBases)
      {
        MethodBase methodBase2 = methodBase1;
        bool flag3 = false;
        if (genericParameterCount > 0 && context.MethodArgs.Length != 0)
        {
          methodBase2 = (MethodBase) (methodBase1 as MethodInfo).MakeGenericMethod(context.MethodArgs);
          flag3 = true;
        }
        MethodBase templateMethod = !typeToInspect.IsGenericType ? (!flag3 ? methodBase2 : methodBase1) : SignatureComparer.GetTemplateMethod(typeToInspect, ((MemberInfo) methodBase2).MetadataToken);
        if ((flag1 || expectedSignature.ReturnParameter.Type.Equals((templateMethod as MethodInfo).ReturnType)) && SignatureComparer.IsParametersTypeMatch(templateMethod, expectedSignature.Parameters))
        {
          if (flag2)
            throw new AmbiguousMatchException();
          matchingMethod = methodBase2;
          flag2 = true;
        }
      }
      return matchingMethod;
    }

    private static MethodBase GetTemplateMethod(Type typeToInspect, int methodToken) => ((MemberInfo) typeToInspect.GetGenericTypeDefinition()).Module.ResolveMethod(methodToken);
  }
}
