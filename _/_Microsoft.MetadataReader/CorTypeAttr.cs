// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.CorTypeAttr
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;

namespace Microsoft.MetadataReader
{
  [Flags]
  internal enum CorTypeAttr
  {
    tdVisibilityMask = 7,
    tdNotPublic = 0,
    tdPublic = 1,
    tdNestedPublic = 2,
    tdNestedPrivate = tdNestedPublic | tdPublic, // 0x00000003
    tdNestedFamily = 4,
    tdNestedAssembly = tdNestedFamily | tdPublic, // 0x00000005
    tdNestedFamANDAssem = tdNestedFamily | tdNestedPublic, // 0x00000006
    tdNestedFamORAssem = tdNestedFamANDAssem | tdPublic, // 0x00000007
    tdLayoutMask = 24, // 0x00000018
    tdAutoLayout = 0,
    tdSequentialLayout = 8,
    tdExplicitLayout = 16, // 0x00000010
    tdClassSemanticsMask = 32, // 0x00000020
    tdClass = 0,
    tdInterface = tdClassSemanticsMask, // 0x00000020
    tdAbstract = 128, // 0x00000080
    tdSealed = 256, // 0x00000100
    tdSpecialName = 1024, // 0x00000400
    tdImport = 4096, // 0x00001000
    tdSerializable = 8192, // 0x00002000
    tdStringFormatMask = 196608, // 0x00030000
    tdAnsiClass = 0,
    tdUnicodeClass = 65536, // 0x00010000
    tdAutoClass = 131072, // 0x00020000
    tdCustomFormatClass = tdAutoClass | tdUnicodeClass, // 0x00030000
    tdCustomFormatMask = 12582912, // 0x00C00000
    tdBeforeFieldInit = 1048576, // 0x00100000
    tdForwarder = 2097152, // 0x00200000
    tdReservedMask = 264192, // 0x00040800
    tdRTSpecialName = 2048, // 0x00000800
    tdHasSecurity = 262144, // 0x00040000
  }
}
