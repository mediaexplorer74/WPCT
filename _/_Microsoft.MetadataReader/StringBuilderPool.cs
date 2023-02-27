// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.StringBuilderPool
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Text;

namespace Microsoft.MetadataReader
{
  internal static class StringBuilderPool
  {
    private const int DefaultCapacity = 128;
    private const int MaxListSize = 5;
    private const int MaxCapacity = 4096;
    private static readonly StringBuilder[] s_pool = new StringBuilder[5];
    private static readonly object s_synclock = new object();

    public static StringBuilder Get() => StringBuilderPool.Get(128);

    public static StringBuilder Get(int capacity)
    {
      StringBuilder stringBuilder = (StringBuilder) null;
      lock (StringBuilderPool.s_synclock)
      {
        for (int index = 0; index < StringBuilderPool.s_pool.Length; ++index)
        {
          if (StringBuilderPool.s_pool[index] != null)
          {
            stringBuilder = StringBuilderPool.s_pool[index];
            StringBuilderPool.s_pool[index] = (StringBuilder) null;
            break;
          }
        }
      }
      if (stringBuilder == null)
        stringBuilder = new StringBuilder(capacity);
      stringBuilder.Length = 0;
      stringBuilder.EnsureCapacity(capacity);
      return stringBuilder;
    }

    public static void Release(ref StringBuilder builder)
    {
      if (builder != null && builder.Capacity < 4096)
      {
        lock (StringBuilderPool.s_synclock)
        {
          for (int index = 0; index < StringBuilderPool.s_pool.Length; ++index)
          {
            if (StringBuilderPool.s_pool[index] == null)
            {
              StringBuilderPool.s_pool[index] = builder;
              break;
            }
          }
        }
      }
      builder = (StringBuilder) null;
    }
  }
}
