// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.CorCallingConvention
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

namespace Microsoft.MetadataReader
{
  internal enum CorCallingConvention
  {
    Default = 0,
    VarArg = 5,
    Field = 6,
    LocalSig = 7,
    Property = 8,
    Unmanaged = 9,
    GenericInst = 10, // 0x0000000A
    NativeVarArg = 11, // 0x0000000B
    Mask = 15, // 0x0000000F
    Generic = 16, // 0x00000010
    HasThis = 32, // 0x00000020
    ExplicitThis = 64, // 0x00000040
  }
}
