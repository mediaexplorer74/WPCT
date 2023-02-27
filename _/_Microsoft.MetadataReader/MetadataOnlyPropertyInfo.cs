// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyPropertyInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyPropertyInfo : PropertyInfo
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly Token _propertyToken;
    private readonly Token _declaringClassToken;
    private readonly Type _propertyType;
    private readonly GenericContext _context;
    private string _name;
    private int _nameLength;
    private readonly Token _setterToken;
    private readonly Token _getterToken;

    public MetadataOnlyPropertyInfo(
      MetadataOnlyModule resolver,
      Token propToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      this._resolver = resolver;
      this._propertyToken = propToken;
      this._context = new GenericContext(typeArgs, methodArgs);
      PropertyAttributes pdwPropFlags;
      EmbeddedBlobPointer ppvSig;
      int pbSig;
      Token pmdSetter;
      Token pmdGetter;
      this._resolver.RawImport.GetPropertyProps(this._propertyToken, out this._declaringClassToken, (StringBuilder) null, 0, out this._nameLength, out pdwPropFlags, out ppvSig, out pbSig, out int _, out UnusedIntPtr _, out int _, out pmdSetter, out pmdGetter, out Token _, 1U, out uint _);
      this.Attributes = pdwPropFlags;
      byte[] sig = this._resolver.ReadEmbeddedBlob(ppvSig, pbSig);
      int index = 0;
      SignatureUtil.ExtractCallingConvention(sig, ref index);
      SignatureUtil.ExtractInt(sig, ref index);
      this._propertyType = SignatureUtil.ExtractType(sig, ref index, this._resolver, this._context);
      this._setterToken = pmdSetter;
      this._getterToken = pmdGetter;
    }

    private void InitializeName()
    {
      if (!string.IsNullOrEmpty(this._name))
        return;
      IMetadataImport rawImport = this._resolver.RawImport;
      StringBuilder builder = StringBuilderPool.Get(this._nameLength);
      rawImport.GetPropertyProps(this._propertyToken, out Token _, builder, builder.Capacity, out this._nameLength, out PropertyAttributes _, out EmbeddedBlobPointer _, out int _, out int _, out UnusedIntPtr _, out int _, out Token _, out Token _, out Token _, 1U, out uint _);
      this._name = builder.ToString();
      StringBuilderPool.Release(ref builder);
    }

    public virtual string ToString() => ((MemberInfo) this).DeclaringType.ToString() + "." + ((MemberInfo) this).Name;

    public virtual PropertyAttributes Attributes { get; }

    public virtual MemberTypes MemberType => MemberTypes.Property;

    public virtual string Name
    {
      get
      {
        this.InitializeName();
        return this._name;
      }
    }

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual Type PropertyType => this._propertyType;

    public virtual Type DeclaringType => this._resolver.GetGenericType(this._declaringClassToken, this._context);

    public virtual object GetConstantValue() => throw new NotImplementedException();

    public virtual int MetadataToken => Token.op_Implicit(this._propertyToken);

    public virtual bool CanRead
    {
      get
      {
        Token getterToken = this._getterToken;
        return !((Token) ref getterToken).IsNil;
      }
    }

    public virtual bool CanWrite
    {
      get
      {
        Token setterToken = this._setterToken;
        return !((Token) ref setterToken).IsNil;
      }
    }

    public virtual MethodInfo[] GetAccessors(bool nonPublic)
    {
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      MethodInfo getMethod = base.GetGetMethod(nonPublic);
      if (getMethod != null)
        methodInfoList.Add(getMethod);
      MethodInfo setMethod = base.GetSetMethod(nonPublic);
      if (setMethod != null)
        methodInfoList.Add(setMethod);
      return methodInfoList.ToArray();
    }

    public virtual MethodInfo GetGetMethod(bool nonPublic)
    {
      Token getterToken = this._getterToken;
      if (((Token) ref getterToken).IsNil)
        return (MethodInfo) null;
      MethodInfo genericMethodInfo = this._resolver.GetGenericMethodInfo(this._getterToken, this._context);
      return nonPublic || ((MethodBase) genericMethodInfo).IsPublic ? genericMethodInfo : (MethodInfo) null;
    }

    public virtual MethodInfo GetSetMethod(bool nonPublic)
    {
      Token setterToken = this._setterToken;
      if (((Token) ref setterToken).IsNil)
        return (MethodInfo) null;
      MethodInfo genericMethodInfo = this._resolver.GetGenericMethodInfo(this._setterToken, this._context);
      return nonPublic || ((MethodBase) genericMethodInfo).IsPublic ? genericMethodInfo : (MethodInfo) null;
    }

    public virtual ParameterInfo[] GetIndexParameters()
    {
      MethodInfo getMethod = base.GetGetMethod(true);
      return getMethod != null ? ((MethodBase) getMethod).GetParameters() : new ParameterInfo[0];
    }

    public virtual object GetValue(
      object obj,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual void SetValue(
      object obj,
      object value,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual Module Module => (Module) this._resolver;

    public virtual bool Equals(object obj) => obj is MetadataOnlyPropertyInfo onlyPropertyInfo && ((object) onlyPropertyInfo._resolver).Equals((object) this._resolver) && onlyPropertyInfo._propertyToken.Equals((object) this._propertyToken) && ((MemberInfo) this).DeclaringType.Equals(((MemberInfo) onlyPropertyInfo).DeclaringType);

    public virtual int GetHashCode() => ((object) this._resolver).GetHashCode() * (int) short.MaxValue + this._propertyToken.GetHashCode();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this._resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);
  }
}
