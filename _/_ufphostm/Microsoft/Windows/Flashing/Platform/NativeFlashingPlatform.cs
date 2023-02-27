// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.NativeFlashingPlatform
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using FlashingPlatform;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  internal class NativeFlashingPlatform
  {
    [DllImport("ufphost.dll")]
    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe int GetFlashingPlatformVersion(uint* A_0, uint* A_1);

    [DllImport("ufphost.dll")]
    [MethodImpl(MethodImplOptions.ForwardRef)]
    public static extern unsafe int CreateFlashingPlatform(ushort* A_0, IFlashingPlatform** A_1);
  }
}
