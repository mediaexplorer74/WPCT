// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.SignatureUtil
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal static class SignatureUtil
  {
    private static readonly uint[] s_tkCorEncodeToken = new uint[4]
    {
      33554432U,
      16777216U,
      452984832U,
      1912602624U
    };
    private const byte FieldId = 83;
    private const byte PropertyId = 84;
    private const CorElementType BoxedValue = (CorElementType) 81;

    internal static CorElementType ExtractElementType(byte[] sig, ref int index) => (CorElementType) SignatureUtil.ExtractInt(sig, ref index);

    internal static CorCallingConvention ExtractCallingConvention(
      byte[] sig,
      ref int index)
    {
      return (CorCallingConvention) SignatureUtil.ExtractInt(sig, ref index);
    }

    internal static CustomModifiers ExtractCustomModifiers(
      byte[] sig,
      ref int index,
      MetadataOnlyModule resolver,
      GenericContext context)
    {
      int num = index;
      CorElementType elementType = SignatureUtil.ExtractElementType(sig, ref index);
      if (elementType == 32 || elementType == 31)
      {
        List<Type> optModifiers = new List<Type>();
        List<Type> reqModifiers = new List<Type>();
        for (; elementType == 32 || elementType == 31; elementType = SignatureUtil.ExtractElementType(sig, ref index))
        {
          Token token = SignatureUtil.ExtractToken(sig, ref index);
          Type type = resolver.ResolveTypeTokenInternal(token, context);
          if (elementType == 32)
            optModifiers.Add(type);
          else
            reqModifiers.Add(type);
          num = index;
        }
        index = num;
        return new CustomModifiers(optModifiers, reqModifiers);
      }
      index = num;
      return (CustomModifiers) null;
    }

    internal static Type ExtractType(
      byte[] sig,
      ref int index,
      MetadataOnlyModule resolver,
      GenericContext context)
    {
      return SignatureUtil.ExtractType(sig, ref index, resolver, context, false).Type;
    }

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    internal static TypeSignatureDescriptor ExtractType(
      byte[] sig,
      ref int index,
      MetadataOnlyModule resolver,
      GenericContext context,
      bool fAllowPinned)
    {
      TypeSignatureDescriptor type1 = new TypeSignatureDescriptor();
      type1.IsPinned = false;
      CorElementType elementType = SignatureUtil.ExtractElementType(sig, ref index);
      switch ((int) elementType)
      {
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
        case 10:
        case 11:
        case 12:
        case 13:
        case 14:
        case 24:
        case 25:
        case 28:
          type1.Type = resolver.AssemblyResolver.GetBuiltInType(elementType);
          break;
        case 15:
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context).MakePointerType();
          break;
        case 16:
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context).MakeByRefType();
          break;
        case 17:
        case 18:
          Token token1 = SignatureUtil.ExtractToken(sig, ref index);
          type1.Type = resolver.ResolveTypeTokenInternal(token1, context);
          break;
        case 19:
          int index1 = SignatureUtil.ExtractInt(sig, ref index);
          if (GenericContext.IsNullOrEmptyTypeArgs(context))
            throw new ArgumentException(Resources.TypeArgumentCannotBeResolved);
          type1.Type = context.TypeArgs[index1];
          break;
        case 20:
          Type type2 = SignatureUtil.ExtractType(sig, ref index, resolver, context);
          int num1 = SignatureUtil.ExtractInt(sig, ref index);
          int num2 = SignatureUtil.ExtractInt(sig, ref index);
          for (int index2 = 0; index2 < num2; ++index2)
            SignatureUtil.ExtractInt(sig, ref index);
          int num3 = SignatureUtil.ExtractInt(sig, ref index);
          for (int index3 = 0; index3 < num3; ++index3)
            SignatureUtil.ExtractInt(sig, ref index);
          type1.Type = type2.MakeArrayType(num1);
          break;
        case 21:
          int num4 = index;
          Type type3 = SignatureUtil.ExtractType(sig, ref index, resolver, (GenericContext) null);
          Type[] typeArray = new Type[SignatureUtil.ExtractInt(sig, ref index)];
          for (int index4 = 0; index4 < typeArray.Length; ++index4)
            typeArray[index4] = SignatureUtil.ExtractType(sig, ref index, resolver, context);
          type1.Type = type3.MakeGenericType(typeArray);
          break;
        case 22:
          type1.Type = resolver.AssemblyResolver.GetTypeXFromName("System.TypedReference");
          break;
        case 27:
          int callingConvention = (int) SignatureUtil.ExtractCallingConvention(sig, ref index);
          int num5 = SignatureUtil.ExtractInt(sig, ref index);
          SignatureUtil.ExtractType(sig, ref index, resolver, context);
          for (int index5 = 0; index5 < num5; ++index5)
            SignatureUtil.ExtractType(sig, ref index, resolver, context);
          type1.Type = resolver.AssemblyResolver.GetBuiltInType((CorElementType) 24);
          break;
        case 29:
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context).MakeArrayType();
          break;
        case 30:
          int index6 = SignatureUtil.ExtractInt(sig, ref index);
          if (GenericContext.IsNullOrEmptyMethodArgs(context))
            throw new ArgumentException(Resources.TypeArgumentCannotBeResolved);
          type1.Type = context.MethodArgs[index6];
          break;
        case 31:
          Token token2 = SignatureUtil.ExtractToken(sig, ref index);
          resolver.ResolveTypeTokenInternal(token2, context);
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context);
          break;
        case 32:
          Token token3 = SignatureUtil.ExtractToken(sig, ref index);
          resolver.ResolveTypeTokenInternal(token3, context);
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context);
          break;
        case 69:
          type1.IsPinned = true;
          type1.Type = SignatureUtil.ExtractType(sig, ref index, resolver, context);
          break;
        default:
          throw new ArgumentException(Resources.IncorrectElementTypeValue);
      }
      return type1;
    }

    internal static void ExtractCustomAttributeArgumentType(
      ITypeUniverse universe,
      Module module,
      byte[] customAttributeBlob,
      ref int index,
      out CorElementType argumentTypeId,
      out Type argumentType)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref argumentTypeId = (int) SignatureUtil.ExtractElementType(customAttributeBlob, ref index);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      SignatureUtil.VerifyElementType((CorElementType) ^(int&) ref argumentTypeId);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(int&) ref argumentTypeId == 29)
      {
        CorElementType elementType = SignatureUtil.ExtractElementType(customAttributeBlob, ref index);
        SignatureUtil.VerifyElementType(elementType);
        if (elementType == 81)
          argumentType = universe.GetBuiltInType((CorElementType) 28).MakeArrayType();
        else if (elementType == 85)
        {
          argumentType = SignatureUtil.ExtractTypeValue(universe, module, customAttributeBlob, ref index);
          argumentType = argumentType.MakeArrayType();
        }
        else
          argumentType = universe.GetBuiltInType(elementType).MakeArrayType();
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref argumentTypeId == 85)
        {
          argumentType = SignatureUtil.ExtractTypeValue(universe, module, customAttributeBlob, ref index);
          if (argumentType == null)
            throw new ArgumentException(Resources.InvalidCustomAttributeFormatForEnum);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          argumentType = ^(int&) ref argumentTypeId != 81 ? universe.GetBuiltInType((CorElementType) ^(int&) ref argumentTypeId) : (Type) null;
        }
      }
    }

    internal static bool IsVarArg(CorCallingConvention conv) => (conv & CorCallingConvention.Mask) == CorCallingConvention.VarArg;

    internal static int ExtractInt(byte[] sig, ref int index)
    {
      int num;
      if (((int) sig[index] & 128) == 0)
      {
        num = (int) sig[index];
        ++index;
      }
      else if (((int) sig[index] & 192) == 128)
      {
        num = ((int) sig[index] & 63) << 8 | (int) sig[index + 1];
        index += 2;
      }
      else
      {
        if (((int) sig[index] & 224) != 192)
          throw new ArgumentException(Resources.InvalidMetadataSignature);
        num = ((int) sig[index] & 31) << 24 | (int) sig[index + 1] << 16 | (int) sig[index + 2] << 8 | (int) sig[index + 3];
        index += 4;
      }
      return num;
    }

    internal static Token ExtractToken(byte[] sig, ref int index)
    {
      uint num = (uint) SignatureUtil.ExtractInt(sig, ref index);
      uint tkType = SignatureUtil.s_tkCorEncodeToken[(int) num & 3];
      return new Token(SignatureUtil.TokenFromRid(num >> 2, tkType));
    }

    internal static CorElementType GetTypeId(Type type)
    {
      if (type.IsEnum)
        return SignatureUtil.GetTypeId(MetadataOnlyModule.GetUnderlyingType(type));
      if (type.IsArray)
        return (CorElementType) 29;
      CorElementType result;
      if (SignatureUtil.TypeMapForAttributes.LookupPrimitive(type, out result))
        return result;
      throw new ArgumentException(Resources.UnsupportedTypeInAttributeSignature);
    }

    internal static string ExtractStringValue(byte[] blob, ref int index) => (string) SignatureUtil.ExtractValue((CorElementType) 14, blob, ref index);

    internal static uint ExtractUIntValue(byte[] blob, ref int index) => (uint) SignatureUtil.ExtractValue((CorElementType) 9, blob, ref index);

    internal static Type ExtractTypeValue(
      ITypeUniverse universe,
      Module module,
      byte[] blob,
      ref int index)
    {
      Type typeValue = (Type) null;
      string stringValue = SignatureUtil.ExtractStringValue(blob, ref index);
      if (!string.IsNullOrEmpty(stringValue))
      {
        bool flag = false;
        typeValue = TypeNameParser.ParseTypeName(universe, module, stringValue, flag);
        if (typeValue == null)
        {
          module = universe.GetSystemAssembly().ManifestModule;
          typeValue = TypeNameParser.ParseTypeName(universe, module, stringValue);
        }
      }
      return typeValue;
    }

    internal static object ExtractValue(CorElementType typeId, byte[] blob, ref int index)
    {
      switch (typeId - 2)
      {
        case 0:
          object boolean = (object) BitConverter.ToBoolean(blob, index);
          ++index;
          return boolean;
        case 1:
          object obj1 = (object) BitConverter.ToChar(blob, index);
          index += 2;
          return obj1;
        case 2:
          object obj2 = (object) (sbyte) blob[index];
          ++index;
          return obj2;
        case 3:
          object obj3 = (object) blob[index];
          ++index;
          return obj3;
        case 4:
          object int16 = (object) BitConverter.ToInt16(blob, index);
          index += 2;
          return int16;
        case 5:
          object uint16 = (object) BitConverter.ToUInt16(blob, index);
          index += 2;
          return uint16;
        case 6:
          object int32 = (object) BitConverter.ToInt32(blob, index);
          index += 4;
          return int32;
        case 7:
          object uint32 = (object) BitConverter.ToUInt32(blob, index);
          index += 4;
          return uint32;
        case 8:
          object int64 = (object) BitConverter.ToInt64(blob, index);
          index += 8;
          return int64;
        case 9:
          object uint64 = (object) BitConverter.ToUInt64(blob, index);
          index += 8;
          return uint64;
        case 10:
          object single = (object) BitConverter.ToSingle(blob, index);
          index += 4;
          return single;
        case 11:
          object obj4 = (object) BitConverter.ToDouble(blob, index);
          index += 8;
          return obj4;
        case 12:
          object obj5;
          if (blob[index] == byte.MaxValue)
          {
            ++index;
            obj5 = (object) null;
          }
          else
          {
            int count = SignatureUtil.ExtractInt(blob, ref index);
            obj5 = (object) Encoding.UTF8.GetString(blob, index, count);
            index += count;
          }
          return obj5;
        default:
          throw new InvalidOperationException(Resources.IncorrectElementTypeValue);
      }
    }

    internal static IList<CustomAttributeTypedArgument> ExtractListOfValues(
      Type elementType,
      ITypeUniverse universe,
      Module module,
      uint size,
      byte[] blob,
      ref int index)
    {
      CorElementType typeId = SignatureUtil.GetTypeId(elementType);
      List<CustomAttributeTypedArgument> attributeTypedArgumentList = new List<CustomAttributeTypedArgument>((int) size);
      if (typeId == 28)
      {
        for (int index1 = 0; (long) index1 < (long) size; ++index1)
        {
          CorElementType elementType1 = SignatureUtil.ExtractElementType(blob, ref index);
          SignatureUtil.VerifyElementType(elementType1);
          if (elementType1 == 29)
            throw new NotImplementedException(Resources.ArrayInsideArrayInAttributeNotSupported);
          Type enumType;
          object obj;
          if (elementType1 == 85)
          {
            enumType = SignatureUtil.ExtractTypeValue(universe, module, blob, ref index);
            if (enumType == null)
              throw new ArgumentException(Resources.InvalidCustomAttributeFormatForEnum);
            obj = SignatureUtil.ExtractValue(SignatureUtil.GetTypeId(MetadataOnlyModule.GetUnderlyingType(enumType)), blob, ref index);
          }
          else
          {
            enumType = universe.GetBuiltInType(elementType1);
            obj = SignatureUtil.ExtractValue(elementType1, blob, ref index);
          }
          attributeTypedArgumentList.Add(new CustomAttributeTypedArgument(enumType, obj));
        }
      }
      else if (typeId == 80)
      {
        for (int index2 = 0; (long) index2 < (long) size; ++index2)
        {
          object typeValue = (object) SignatureUtil.ExtractTypeValue(universe, module, blob, ref index);
          attributeTypedArgumentList.Add(new CustomAttributeTypedArgument(elementType, typeValue));
        }
      }
      else
      {
        if (typeId == 29)
          throw new ArgumentException(Resources.JaggedArrayInAttributeNotSupported);
        for (int index3 = 0; (long) index3 < (long) size; ++index3)
        {
          object obj = SignatureUtil.ExtractValue(typeId, blob, ref index);
          attributeTypedArgumentList.Add(new CustomAttributeTypedArgument(elementType, obj));
        }
      }
      return (IList<CustomAttributeTypedArgument>) attributeTypedArgumentList.AsReadOnly();
    }

    internal static bool IsValidCustomAttributeElementType(CorElementType elementType) => SignatureUtil.TypeMapForAttributes.IsValidCustomAttributeElementType(elementType);

    internal static void VerifyElementType(CorElementType elementType)
    {
      if (elementType != 2 && elementType != 3 && elementType != 4 && elementType != 5 && elementType != 6 && elementType != 7 && elementType != 8 && elementType != 9 && elementType != 10 && elementType != 11 && elementType != 12 && elementType != 13 && elementType != 14 && elementType != 80 && elementType != 29 && elementType != 85 && elementType != 81)
        throw new ArgumentException(Resources.InvalidElementTypeInAttribute);
    }

    internal static NamedArgumentType ExtractNamedArgumentType(
      byte[] customAttributeBlob,
      ref int index)
    {
      switch ((byte) SignatureUtil.ExtractValue((CorElementType) 5, customAttributeBlob, ref index))
      {
        case 83:
          return NamedArgumentType.Field;
        case 84:
          return NamedArgumentType.Property;
        default:
          throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
          {
            (object) Resources.InvalidCustomAttributeFormat,
            (object) Resources.ExpectedPropertyOrFieldId
          }));
      }
    }

    internal static MethodSignatureDescriptor ExtractMethodSignature(
      SignatureBlob methodSignatureBlob,
      MetadataOnlyModule resolver,
      GenericContext context)
    {
      byte[] signatureAsByteArray = methodSignatureBlob.GetSignatureAsByteArray();
      int index1 = 0;
      MethodSignatureDescriptor methodSignature = new MethodSignatureDescriptor();
      methodSignature.ReturnParameter = new TypeSignatureDescriptor();
      methodSignature.GenericParameterCount = 0;
      methodSignature.CallingConvention = SignatureUtil.ExtractCallingConvention(signatureAsByteArray, ref index1);
      bool flag = (methodSignature.CallingConvention & CorCallingConvention.ExplicitThis) != 0;
      if ((methodSignature.CallingConvention & CorCallingConvention.Generic) != 0)
      {
        int expectedNumberOfMethodArgs = SignatureUtil.ExtractInt(signatureAsByteArray, ref index1);
        context = expectedNumberOfMethodArgs > 0 ? context.VerifyAndUpdateMethodArguments(expectedNumberOfMethodArgs) : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
        {
          (object) Resources.InvalidMetadataSignature,
          (object) Resources.ExpectedPositiveNumberOfGenericParameters
        }));
        methodSignature.GenericParameterCount = expectedNumberOfMethodArgs;
      }
      int length = SignatureUtil.ExtractInt(signatureAsByteArray, ref index1);
      bool fAllowPinned = false;
      CustomModifiers customModifiers1 = SignatureUtil.ExtractCustomModifiers(signatureAsByteArray, ref index1, resolver, context);
      methodSignature.ReturnParameter = SignatureUtil.ExtractType(signatureAsByteArray, ref index1, resolver, context, fAllowPinned);
      methodSignature.ReturnParameter.CustomModifiers = customModifiers1;
      if (flag)
      {
        SignatureUtil.ExtractType(signatureAsByteArray, ref index1, resolver, context);
        --length;
      }
      methodSignature.Parameters = new TypeSignatureDescriptor[length];
      for (int index2 = 0; index2 < length; ++index2)
      {
        CustomModifiers customModifiers2 = SignatureUtil.ExtractCustomModifiers(signatureAsByteArray, ref index1, resolver, context);
        methodSignature.Parameters[index2] = SignatureUtil.ExtractType(signatureAsByteArray, ref index1, resolver, context, fAllowPinned);
        methodSignature.Parameters[index2].CustomModifiers = customModifiers2;
      }
      if (index1 != signatureAsByteArray.Length)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
        {
          (object) Resources.InvalidMetadataSignature,
          (object) Resources.ExtraInformationAfterLastParameter
        }));
      return methodSignature;
    }

    internal static CallingConventions GetReflectionCallingConvention(
      CorCallingConvention callConvention)
    {
      CallingConventions callingConventions = (CallingConventions) 0;
      if ((callConvention & CorCallingConvention.Mask) == CorCallingConvention.HasThis)
        callingConventions |= CallingConventions.HasThis;
      else if ((callConvention & CorCallingConvention.Mask) == CorCallingConvention.ExplicitThis)
        callingConventions |= CallingConventions.ExplicitThis;
      return !SignatureUtil.IsVarArg(callConvention) ? callingConventions | CallingConventions.Standard : callingConventions | CallingConventions.VarArgs;
    }

    internal static bool IsCallingConventionMatch(
      MethodBase method,
      CallingConventions callConvention)
    {
      return (callConvention & CallingConventions.Any) != (CallingConventions) 0 || ((callConvention & CallingConventions.VarArgs) == (CallingConventions) 0 || (method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions) 0) && ((callConvention & CallingConventions.Standard) == (CallingConventions) 0 || (method.CallingConvention & CallingConventions.Standard) != (CallingConventions) 0);
    }

    internal static bool IsGenericParametersCountMatch(
      MethodInfo method,
      int expectedGenericParameterCount)
    {
      int num = 0;
      if (((MethodBase) method).IsGenericMethod)
        num = ((MethodBase) method).GetGenericArguments().Length;
      return num == expectedGenericParameterCount;
    }

    internal static bool IsParametersTypeMatch(MethodBase method, Type[] parameterTypes)
    {
      if (parameterTypes == null)
        return true;
      ParameterInfo[] parameters = method.GetParameters();
      if (parameters.Length != parameterTypes.Length)
        return false;
      int length = parameters.Length;
      for (int index = 0; index < length; ++index)
      {
        if (!parameters[index].ParameterType.Equals(parameterTypes[index]))
          return false;
      }
      return true;
    }

    private static uint TokenFromRid(uint rid, uint tkType) => rid | tkType;

    internal static StringComparison GetStringComparison(BindingFlags flags) => (flags & BindingFlags.IgnoreCase) == 0 ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

    public static class TypeMapForAttributes
    {
      private static readonly Dictionary<string, CorElementType> s_typeNameMapForAttributes;

      public static bool IsValidCustomAttributeElementType(CorElementType elementType) => SignatureUtil.TypeMapForAttributes.s_typeNameMapForAttributes.ContainsValue(elementType);

      public static bool LookupPrimitive(Type type, out CorElementType result)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref result = 0;
        ITypeUniverse itypeUniverse = Helpers.Universe(type);
        return (itypeUniverse == null || ((object) itypeUniverse.GetSystemAssembly()).Equals((object) type.Assembly)) && SignatureUtil.TypeMapForAttributes.s_typeNameMapForAttributes.TryGetValue(type.FullName, out result);
      }

      static TypeMapForAttributes()
      {
        Dictionary<string, CorElementType> dictionary = new Dictionary<string, CorElementType>();
        dictionary.Add("System.Boolean", (CorElementType) 2);
        dictionary.Add("System.Char", (CorElementType) 3);
        dictionary.Add("System.SByte", (CorElementType) 4);
        dictionary.Add("System.Byte", (CorElementType) 5);
        dictionary.Add("System.Int16", (CorElementType) 6);
        dictionary.Add("System.UInt16", (CorElementType) 7);
        dictionary.Add("System.Int32", (CorElementType) 8);
        dictionary.Add("System.UInt32", (CorElementType) 9);
        dictionary.Add("System.Int64", (CorElementType) 10);
        dictionary.Add("System.UInt64", (CorElementType) 11);
        dictionary.Add("System.Single", (CorElementType) 12);
        dictionary.Add("System.Double", (CorElementType) 13);
        dictionary.Add("System.IntPtr", (CorElementType) 24);
        dictionary.Add("System.UIntPtr", (CorElementType) 25);
        dictionary.Add("System.Array", (CorElementType) 29);
        dictionary.Add("System.String", (CorElementType) 14);
        dictionary.Add("System.Type", (CorElementType) 80);
        dictionary.Add("System.Object", (CorElementType) 28);
        SignatureUtil.TypeMapForAttributes.s_typeNameMapForAttributes = dictionary;
      }
    }
  }
}
