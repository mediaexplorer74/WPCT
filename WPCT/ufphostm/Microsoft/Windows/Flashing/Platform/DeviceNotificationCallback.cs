// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.DeviceNotificationCallback
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public abstract class DeviceNotificationCallback
  {
    public abstract void Connected([In] string DevicePath);

    public abstract void Disconnected([In] string DevicePath);
  }
}
