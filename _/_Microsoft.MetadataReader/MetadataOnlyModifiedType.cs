// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyModifiedType
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

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyModifiedType : MetadataOnlyCommonType
  {
    private readonly MetadataOnlyCommonType _type;
    private readonly string _mod;

    public MetadataOnlyModifiedType(MetadataOnlyCommonType type, string mod)
    {
      this._type = type;
      this._mod = mod;
    }

    public virtual string FullName
    {
      get
      {
        string fullName = this._type.FullName;
        return fullName == null || this._type.IsGenericTypeDefinition ? (string) null : fullName + this._mod;
      }
    }

    internal override MetadataOnlyModule Resolver => this._type.Resolver;

    public virtual Type BaseType => (Type) null;

    public virtual bool Equals(Type t)
    {
      if (t == null)
        return false;
      if (this.IsByRef)
      {
        if (!t.IsByRef)
          return false;
      }
      else if (this.IsPointer && !t.IsPointer)
        return false;
      return this._type.Equals(t.GetElementType());
    }

    protected override bool IsByRefImpl() => this._mod.Equals("&");

    protected override bool IsPointerImpl() => this._mod.Equals("*");

    public virtual bool IsAssignableFrom(Type c)
    {
      if (c == null)
        return false;
      if (this.IsPointer && c.IsPointer || this.IsByRef && c.IsByRef)
      {
        Type elementType = c.GetElementType();
        if (this._type.IsAssignableFrom(elementType) && !elementType.IsValueType)
          return true;
      }
      return MetadataOnlyTypeDef.IsAssignableFromHelper((Type) this, c);
    }

    public virtual Type UnderlyingSystemType => (Type) this;

    public virtual Type GetElementType() => (Type) this._type;

    public override MethodInfo[] GetMethods(BindingFlags flags) => new MethodInfo[0];

    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => new ConstructorInfo[0];

    public virtual FieldInfo[] GetFields(BindingFlags flags) => new FieldInfo[0];

    public override PropertyInfo[] GetProperties(BindingFlags flags) => new PropertyInfo[0];

    public virtual EventInfo[] GetEvents(BindingFlags flags) => new EventInfo[0];

    public virtual EventInfo GetEvent(string name, BindingFlags flags) => (EventInfo) null;

    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr) => (FieldInfo) null;

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr) => (Type) null;

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => Type.EmptyTypes;

    protected virtual TypeAttributes GetAttributeFlagsImpl() => TypeAttributes.NotPublic;

    protected override MethodInfo GetMethodImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return (MethodInfo) null;
    }

    protected override PropertyInfo GetPropertyImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      Type returnType,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return (PropertyInfo) null;
    }

    protected override ConstructorInfo GetConstructorImpl(
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return (ConstructorInfo) null;
    }

    public virtual Type[] GetInterfaces() => new Type[0];

    public virtual Type GetInterface(string name, bool ignoreCase)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return (Type) null;
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => MetadataOnlyTypeDef.GetMembersHelper((Type) this, bindingAttr);

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual Guid GUID => Guid.Empty;

    protected virtual bool HasElementTypeImpl() => true;

    public virtual object InvokeMember(
      string name,
      BindingFlags invokeAttr,
      Binder binder,
      object target,
      object[] args,
      ParameterModifier[] modifiers,
      CultureInfo culture,
      string[] namedParameters)
    {
      throw new NotSupportedException();
    }

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => (IList<CustomAttributeData>) new CustomAttributeData[0];

    public virtual string ToString() => this._type.ToString() + this._mod;

    public virtual int MetadataToken => 33554432;

    public virtual Type[] GetGenericArguments() => this._type.GetGenericArguments();

    public virtual Type GetGenericTypeDefinition() => throw new InvalidOperationException();

    public virtual GenericParameterAttributes GenericParameterAttributes => throw new InvalidOperationException(Resources.ValidOnGenericParameterTypeOnly);

    public virtual MemberTypes MemberType => MemberTypes.TypeInfo;

    public virtual Type DeclaringType => (Type) null;

    public virtual string Name => ((MemberInfo) this._type).Name + this._mod;

    public virtual Assembly Assembly => this._type.Assembly;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public override Type ReflectedType => throw new NotSupportedException();

    public virtual string Namespace => this._type.Namespace;

    protected virtual TypeCode GetTypeCodeImpl() => TypeCode.Object;
  }
}
