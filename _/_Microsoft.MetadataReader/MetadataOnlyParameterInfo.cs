// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyParameterInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyParameterInfo : ParameterInfo
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly int _parameterToken;
    private readonly CustomModifiers _customModifiers;
    private string _name;
    private readonly uint _nameLength;
    private readonly int _parentMemberToken;

    internal MetadataOnlyParameterInfo(
      MetadataOnlyModule resolver,
      Token parameterToken,
      Type paramType,
      CustomModifiers customModifiers)
    {
      this._resolver = resolver;
      this._parameterToken = Token.op_Implicit(parameterToken);
      this.ParameterType = paramType;
      this._customModifiers = customModifiers;
      uint pulSequence;
      uint pdwAttr;
      this._resolver.RawImport.GetParamProps(this._parameterToken, out this._parentMemberToken, out pulSequence, (StringBuilder) null, 0U, out this._nameLength, out pdwAttr, out uint _, out UnusedIntPtr _, out uint _);
      this.Position = (int) pulSequence - 1;
      this.Attributes = (ParameterAttributes) pdwAttr;
    }

    private void InitializeName()
    {
      if (!string.IsNullOrEmpty(this._name))
        return;
      IMetadataImport rawImport = this._resolver.RawImport;
      StringBuilder builder = StringBuilderPool.Get((int) this._nameLength);
      rawImport.GetParamProps(this._parameterToken, out int _, out uint _, builder, (uint) builder.Capacity, out uint _, out uint _, out uint _, out UnusedIntPtr _, out uint _);
      this._name = builder.ToString();
      StringBuilderPool.Release(ref builder);
    }

    public virtual ParameterAttributes Attributes { get; }

    public virtual Type[] GetOptionalCustomModifiers() => this._customModifiers == null ? Type.EmptyTypes : this._customModifiers.OptionalCustomModifiers;

    public virtual Type[] GetRequiredCustomModifiers() => this._customModifiers == null ? Type.EmptyTypes : this._customModifiers.RequiredCustomModifiers;

    public virtual string Name
    {
      get
      {
        this.InitializeName();
        return this._name;
      }
    }

    public virtual MemberInfo Member => (MemberInfo) this._resolver.ResolveMethod(this._parentMemberToken);

    public virtual int Position { get; }

    public virtual Type ParameterType { get; }

    public virtual int MetadataToken => this._parameterToken;

    public virtual object DefaultValue => throw new InvalidOperationException();

    public virtual object RawDefaultValue => throw new NotImplementedException();

    public virtual bool Equals(object obj) => obj is MetadataOnlyParameterInfo onlyParameterInfo && ((object) onlyParameterInfo._resolver).Equals((object) this._resolver) && onlyParameterInfo._parameterToken.Equals(this._parameterToken);

    public virtual int GetHashCode() => ((object) this._resolver).GetHashCode() * (int) short.MaxValue + this._parameterToken.GetHashCode();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this._resolver.GetCustomAttributeData(base.MetadataToken);

    public virtual string ToString() => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
    {
      (object) MetadataOnlyCommonType.TypeSigToString(base.ParameterType),
      (object) base.Name
    });
  }
}
