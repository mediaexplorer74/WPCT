// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashFlags
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

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
