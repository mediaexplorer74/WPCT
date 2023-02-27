// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ArrayFabricatedGetMethodInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class ArrayFabricatedGetMethodInfo : ArrayFabricatedMethodInfo
  {
    public ArrayFabricatedGetMethodInfo(Type arrayType)
      : base(arrayType)
    {
    }

    public virtual string Name => "Get";

    public virtual ParameterInfo[] GetParameters() => this.MakeParameterHelper(0);

    public virtual Type ReturnType => this.GetElementType();
  }
}
