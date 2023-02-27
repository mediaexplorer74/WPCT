﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MethodFilter
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

namespace Microsoft.MetadataReader
{
  internal class MethodFilter
  {
    public MethodFilter(
      string name,
      int genericParameterCount,
      int parameterCount,
      CorCallingConvention callingConvention)
    {
      this.Name = name;
      this.GenericParameterCount = genericParameterCount;
      this.ParameterCount = parameterCount;
      this.CallingConvention = callingConvention;
    }

    public string Name { get; set; }

    public int GenericParameterCount { get; set; }

    public int ParameterCount { get; set; }

    public CorCallingConvention CallingConvention { get; set; }
  }
}
