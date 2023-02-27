// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyConstructorInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyConstructorInfo : ConstructorInfo
  {
    private readonly MethodBase _method;

    public MetadataOnlyConstructorInfo(MethodBase method) => this._method = method;

    public virtual int MetadataToken => ((MemberInfo) this._method).MetadataToken;

    public virtual string ToString() => ((object) this._method).ToString();

    public virtual Module Module => ((MemberInfo) this._method).Module;

    public virtual Type DeclaringType => ((MemberInfo) this._method).DeclaringType;

    public virtual string Name => ((MemberInfo) this._method).Name;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual ParameterInfo[] GetParameters() => this._method.GetParameters();

    public virtual MethodAttributes Attributes => this._method.Attributes;

    public virtual CallingConventions CallingConvention => this._method.CallingConvention;

    public virtual MemberTypes MemberType => MemberTypes.Constructor;

    public virtual bool IsGenericMethodDefinition => this._method.IsGenericMethodDefinition;

    public virtual MethodBody GetMethodBody() => this._method.GetMethodBody();

    public virtual object Invoke(
      BindingFlags invokeAttr,
      Binder binder,
      object[] parameters,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual MethodImplAttributes GetMethodImplementationFlags() => this._method.GetMethodImplementationFlags();

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual RuntimeMethodHandle MethodHandle => throw new NotSupportedException();

    public virtual object Invoke(
      object obj,
      BindingFlags invokeAttr,
      Binder binder,
      object[] parameters,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => ((MemberInfo) this._method).GetCustomAttributesData();

    public virtual bool Equals(object obj) => obj is MetadataOnlyConstructorInfo onlyConstructorInfo && ((object) this._method).Equals((object) onlyConstructorInfo._method);

    public virtual int GetHashCode() => ((object) this._method).GetHashCode();
  }
}
