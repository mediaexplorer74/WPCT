// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MethodSignatureDescriptor
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

namespace Microsoft.MetadataReader
{
  internal class MethodSignatureDescriptor
  {
    public CorCallingConvention CallingConvention { get; set; }

    public int GenericParameterCount { get; set; }

    public TypeSignatureDescriptor ReturnParameter { get; set; }

    public TypeSignatureDescriptor[] Parameters { get; set; }
  }
}
