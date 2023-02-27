// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyLocalVariableInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyLocalVariableInfo : LocalVariableInfo
  {
    public MetadataOnlyLocalVariableInfo(int index, Type type, bool fPinned)
    {
      this.LocalType = type;
      this.LocalIndex = index;
      this.IsPinned = fPinned;
    }

    public virtual bool IsPinned { get; }

    public virtual int LocalIndex { get; }

    public virtual Type LocalType { get; }
  }
}
