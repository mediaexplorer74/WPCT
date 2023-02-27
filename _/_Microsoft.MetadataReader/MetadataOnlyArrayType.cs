// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyArrayType
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyArrayType : MetadataOnlyCommonArrayType
  {
    private readonly int _rank;

    public MetadataOnlyArrayType(MetadataOnlyCommonType elementType, int rank)
      : base(elementType)
    {
      this._rank = rank;
    }

    public override string FullName
    {
      get
      {
        string fullName = base.GetElementType().FullName;
        return fullName == null || base.GetElementType().IsGenericTypeDefinition ? (string) null : fullName + "[" + MetadataOnlyArrayType.GetDimensionString(this._rank) + "]";
      }
    }

    private static string GetDimensionString(int rank)
    {
      if (rank == 1)
        return "*";
      StringBuilder builder = StringBuilderPool.Get();
      for (int index = 1; index < rank; ++index)
        builder.Append(',');
      string dimensionString = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return dimensionString;
    }

    public override int GetArrayRank() => this._rank;

    public override bool Equals(Type t) => t != null && t is MetadataOnlyArrayType && t.GetArrayRank() == base.GetArrayRank() && base.GetElementType().Equals(t.GetElementType());

    public override bool IsAssignableFrom(Type c)
    {
      if (c == null)
        return false;
      if (!c.IsArray)
        return MetadataOnlyTypeDef.IsAssignableFromHelper((Type) this, c);
      if (c.GetArrayRank() != this._rank)
        return false;
      Type elementType = c.GetElementType();
      return elementType.IsValueType ? base.GetElementType().Equals(elementType) : base.GetElementType().IsAssignableFrom(elementType);
    }

    public override string Name => ((MemberInfo) base.GetElementType()).Name + "[" + MetadataOnlyArrayType.GetDimensionString(this._rank) + "]";

    public virtual string ToString() => base.GetElementType().ToString() + "[" + MetadataOnlyArrayType.GetDimensionString(this._rank) + "]";
  }
}
