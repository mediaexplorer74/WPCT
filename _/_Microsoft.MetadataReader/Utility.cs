// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.Utility
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using Microsoft.Tools.IO;
using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal static class Utility
  {
    public static bool Compare(string string1, string string2, bool ignoreCase) => string.Equals(string1, string2, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);

    public static bool IsBindingFlagsMatching(
      MethodBase method,
      bool isInherited,
      BindingFlags bindingFlags)
    {
      return Utility.IsBindingFlagsMatching((MemberInfo) method, method.IsStatic, method.IsPublic, isInherited, bindingFlags);
    }

    public static bool IsBindingFlagsMatching(
      FieldInfo fieldInfo,
      bool isInherited,
      BindingFlags bindingFlags)
    {
      return Utility.IsBindingFlagsMatching((MemberInfo) fieldInfo, fieldInfo.IsStatic, fieldInfo.IsPublic, isInherited, bindingFlags);
    }

    public static bool IsBindingFlagsMatching(
      MemberInfo memberInfo,
      bool isStatic,
      bool isPublic,
      bool isInherited,
      BindingFlags bindingFlags)
    {
      if ((bindingFlags & BindingFlags.DeclaredOnly) != 0 & isInherited)
        return false;
      if (isPublic)
      {
        if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
          return false;
      }
      else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
        return false;
      if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
      {
        if (isStatic)
        {
          if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default & isInherited || (bindingFlags & BindingFlags.Static) == BindingFlags.Default)
            return false;
        }
        else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
          return false;
      }
      return true;
    }

    internal static string GetNamespaceHelper(string fullName)
    {
      if (!fullName.Contains("."))
        return (string) null;
      int length = fullName.LastIndexOf('.');
      return fullName.Substring(0, length);
    }

    internal static string GetTypeNameFromFullNameHelper(string fullName, bool isNested)
    {
      if (isNested)
      {
        int num = fullName.LastIndexOf('+');
        return fullName.Substring(num + 1);
      }
      int num1 = fullName.LastIndexOf('.');
      return fullName.Substring(num1 + 1);
    }

    internal static void VerifyNotByRef(MetadataOnlyCommonType type)
    {
      if (type.IsByRef)
        throw new TypeLoadException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CannotFindTypeInModule, new object[2]
        {
          (object) (((MemberInfo) type).Name + "&"),
          (object) type.Resolver
        }));
    }

    internal static bool IsValidPath(string modulePath)
    {
      if (string.IsNullOrEmpty(modulePath))
        return false;
      foreach (char invalidPathChar in LongPathPath.GetInvalidPathChars())
      {
        foreach (char ch in modulePath)
        {
          if ((int) invalidPathChar == (int) ch)
            return false;
        }
      }
      try
      {
        if (!LongPathPath.IsPathRooted(modulePath))
          return false;
      }
      catch (Exception ex)
      {
        throw;
      }
      return true;
    }
  }
}
