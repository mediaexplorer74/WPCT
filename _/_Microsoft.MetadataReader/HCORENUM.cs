// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.HCORENUM
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;

namespace Microsoft.MetadataReader
{
  public struct HCORENUM
  {
    private IntPtr _hEnum;

    public void Close(IMetadataImport import)
    {
      if (!(this._hEnum != IntPtr.Zero))
        return;
      import.CloseEnum(this._hEnum);
      this._hEnum = IntPtr.Zero;
    }

    public void Close(IMetadataImport2 import)
    {
      if (!(this._hEnum != IntPtr.Zero))
        return;
      import.CloseEnum(this._hEnum);
      this._hEnum = IntPtr.Zero;
    }

    public void Close(IMetadataAssemblyImport import)
    {
      if (!(this._hEnum != IntPtr.Zero))
        return;
      import.CloseEnum(this._hEnum);
      this._hEnum = IntPtr.Zero;
    }
  }
}
