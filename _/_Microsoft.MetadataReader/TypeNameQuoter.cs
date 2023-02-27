// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.TypeNameQuoter
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Text;

namespace Microsoft.MetadataReader
{
  internal static class TypeNameQuoter
  {
    private static readonly char[] s_specialCharacters = new char[7]
    {
      '\\',
      '[',
      ']',
      ',',
      '+',
      '&',
      '*'
    };

    internal static string GetQuotedTypeName(string name)
    {
      if (name.IndexOfAny(TypeNameQuoter.s_specialCharacters) == -1)
        return name;
      StringBuilder builder = StringBuilderPool.Get();
      for (int index = 0; index < name.Length; ++index)
      {
        if (TypeNameQuoter.Contains(TypeNameQuoter.s_specialCharacters, name[index]))
          builder.Append('\\');
        builder.Append(name[index]);
      }
      string quotedTypeName = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return quotedTypeName;
    }

    private static bool Contains(char[] This, char ch)
    {
      foreach (int thi in This)
      {
        if (thi == (int) ch)
          return true;
      }
      return false;
    }
  }
}
