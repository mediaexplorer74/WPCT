// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ArrayFabricatedSetMethodInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class ArrayFabricatedSetMethodInfo : ArrayFabricatedMethodInfo
  {
    public ArrayFabricatedSetMethodInfo(Type arrayType)
      : base(arrayType)
    {
    }

    public virtual string Name => "Set";

    public virtual ParameterInfo[] GetParameters()
    {
      ParameterInfo[] parameters = this.MakeParameterHelper(1);
      int rank = this.Rank;
      Type elementType = this.GetElementType();
      parameters[rank] = this.MakeParameterInfo(elementType, rank);
      return parameters;
    }

    public virtual Type ReturnType => this.Universe.GetBuiltInType((CorElementType) 1);
  }
}
