// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.SimpleParameterInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class SimpleParameterInfo : ParameterInfo
  {
    internal SimpleParameterInfo(MemberInfo member, Type paramType, int position)
    {
      this.Member = member;
      this.ParameterType = paramType;
      this.Position = position;
    }

    public virtual string ToString()
    {
      StringBuilder builder = StringBuilderPool.Get();
      MetadataOnlyCommonType.TypeSigToString(base.ParameterType, builder);
      builder.Append(' ');
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return str;
    }

    public virtual int Position { get; }

    public virtual Type ParameterType { get; }

    public virtual MemberInfo Member { get; }

    public virtual int MetadataToken => 134217728;

    public virtual ParameterAttributes Attributes => ParameterAttributes.None;

    public virtual string Name => string.Empty;

    public virtual object DefaultValue => (object) null;

    public virtual object RawDefaultValue => (object) null;

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => (IList<CustomAttributeData>) new CustomAttributeData[0];

    public virtual Type[] GetOptionalCustomModifiers() => Type.EmptyTypes;

    public virtual Type[] GetRequiredCustomModifiers() => Type.EmptyTypes;
  }
}
