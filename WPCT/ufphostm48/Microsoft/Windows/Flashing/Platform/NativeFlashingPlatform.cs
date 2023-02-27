// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.NativeFlashingPlatform
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

using FlashingPlatform1;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class NativeFlashingPlatform
  {
    [DllImport("ufphost.dll")]
    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe int GetFlashingPlatformVersion(uint* A_0, uint* A_1);

    [DllImport("ufphost.dll")]
    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe int CreateFlashingPlatform(ushort* A_0, IFlashingPlatform** A_1);
  }
}
