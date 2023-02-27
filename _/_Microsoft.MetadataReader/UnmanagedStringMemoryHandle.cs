// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.UnmanagedStringMemoryHandle
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.MetadataReader
{
  [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
  internal sealed class UnmanagedStringMemoryHandle : SafeHandleZeroOrMinusOneIsInvalid
  {
    internal UnmanagedStringMemoryHandle()
      : base(true)
    {
    }

    internal UnmanagedStringMemoryHandle(int countBytes)
      : base(true)
    {
      if (countBytes == 0)
        return;
      this.SetHandle(Marshal.AllocHGlobal(countBytes));
    }

    protected override bool ReleaseHandle()
    {
      if (!(this.handle != IntPtr.Zero))
        return false;
      Marshal.FreeHGlobal(this.handle);
      this.handle = IntPtr.Zero;
      return true;
    }

    public string GetAsString(int countCharsNoNull) => Marshal.PtrToStringUni(this.handle, countCharsNoNull);
  }
}
