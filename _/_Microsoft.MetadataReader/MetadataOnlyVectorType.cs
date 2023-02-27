// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyVectorType
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyVectorType : MetadataOnlyCommonArrayType
  {
    public MetadataOnlyVectorType(MetadataOnlyCommonType elementType)
      : base(elementType)
    {
    }

    public override string FullName
    {
      get
      {
        string fullName = base.GetElementType().FullName;
        return fullName == null || base.GetElementType().IsGenericTypeDefinition ? (string) null : fullName + "[]";
      }
    }

    public override int GetArrayRank() => 1;

    protected override bool IsArrayImpl() => true;

    public override bool Equals(Type t) => t != null && t is MetadataOnlyVectorType && t.GetArrayRank() == 1 && base.GetElementType().Equals(t.GetElementType());

    public override bool IsAssignableFrom(Type c)
    {
      if (c == null)
        return false;
      if (!c.IsArray || c.GetArrayRank() != 1 || !(c is MetadataOnlyVectorType))
        return MetadataOnlyTypeDef.IsAssignableFromHelper((Type) this, c);
      Type elementType = c.GetElementType();
      return elementType.IsValueType ? base.GetElementType().Equals(elementType) : base.GetElementType().IsAssignableFrom(elementType);
    }

    public override string Name => ((MemberInfo) base.GetElementType()).Name + "[]";

    public virtual string ToString() => base.GetElementType().ToString() + "[]";
  }
}
