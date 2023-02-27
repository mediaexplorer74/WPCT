// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.UnusedIntPtr
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;

namespace Microsoft.MetadataReader
{
  internal struct UnusedIntPtr
  {
    private IntPtr _zeroPtr;

    public static UnusedIntPtr Zero => new UnusedIntPtr()
    {
      _zeroPtr = IntPtr.Zero
    };
  }
}
