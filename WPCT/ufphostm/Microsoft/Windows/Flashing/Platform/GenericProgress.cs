// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.GenericProgress
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public abstract class GenericProgress
  {
    public abstract void RegisterProgress([In] uint Progress);
  }
}
