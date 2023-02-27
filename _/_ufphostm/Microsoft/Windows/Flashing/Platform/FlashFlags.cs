// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashFlags
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using System;

namespace Microsoft.Windows.Flashing.Platform
{
  [Flags]
  public enum FlashFlags : uint
  {
    Normal = 0,
    SkipPlatformIDCheck = 1,
    SkipSignatureCheck = 2,
    SkipRootKeyHashCheck = 4,
    SkipHash = 8,
    VerifyWrite = 16, // 0x00000010
  }
}
