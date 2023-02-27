// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyTypeVariable
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyTypeVariable : MetadataOnlyCommonType
  {
    private readonly int _ownerMethodToken;
    private readonly int _ownerTypeToken;
    private readonly string _name;
    private readonly uint _position;
    private readonly MetadataOnlyModule _resolver;
    private readonly int _token;
    private readonly GenericParameterAttributes _gpAttributes;

    internal MetadataOnlyTypeVariable(MetadataOnlyModule resolver, Token token)
    {
      this._token = ((Token) ref token).Value;
      this._resolver = resolver;
      this._resolver.GetGenericParameterProps(this._token, out this._ownerTypeToken, out this._ownerMethodToken, out this._name, out this._gpAttributes, out this._position);
    }

    public virtual string FullName => (string) null;

    internal override MetadataOnlyModule Resolver => this._resolver;

    public virtual Type BaseType
    {
      get
      {
        foreach (Type parameterConstraint in base.GetGenericParameterConstraints())
        {
          if (parameterConstraint.IsClass)
            return parameterConstraint;
        }
        return this._resolver.AssemblyResolver.GetBuiltInType((CorElementType) 28);
      }
    }

    public virtual bool Equals(Type txOther)
    {
      switch (txOther)
      {
        case MetadataOnlyTypeVariableRef _:
          return this._ownerMethodToken != 0 && (long) this._position == (long) txOther.GenericParameterPosition;
        case MetadataOnlyTypeVariable onlyTypeVariable:
          return !(((MemberInfo) this).Name != ((MemberInfo) onlyTypeVariable).Name) && this._ownerTypeToken == onlyTypeVariable._ownerTypeToken && this._ownerMethodToken == onlyTypeVariable._ownerMethodToken && ((object) ((MemberInfo) this).Module).Equals((object) ((MemberInfo) onlyTypeVariable).Module);
        default:
          return false;
      }
    }

    public virtual bool IsAssignableFrom(Type c) => MetadataOnlyTypeDef.IsAssignableFromHelper((Type) this, c);

    public virtual Type UnderlyingSystemType => (Type) this;

    public virtual Type GetElementType() => (Type) null;

    public virtual int MetadataToken => this._token;

    public override MethodInfo[] GetMethods(BindingFlags flags) => new MethodInfo[0];

    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => new ConstructorInfo[0];

    public virtual FieldInfo[] GetFields(BindingFlags flags) => new FieldInfo[0];

    public virtual FieldInfo GetField(string name, BindingFlags flags) => (FieldInfo) null;

    public override PropertyInfo[] GetProperties(BindingFlags flags) => new PropertyInfo[0];

    public virtual EventInfo[] GetEvents(BindingFlags flags) => new EventInfo[0];

    public virtual EventInfo GetEvent(string name, BindingFlags flags) => (EventInfo) null;

    public override Type MakeGenericType(params Type[] argTypes) => throw new InvalidOperationException();

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr) => (Type) null;

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => Type.EmptyTypes;

    protected virtual TypeAttributes GetAttributeFlagsImpl() => TypeAttributes.Public;

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

    public override bool IsGenericParameter => true;

    public virtual Type[] GetGenericArguments() => Type.EmptyTypes;

    public virtual Type[] GetGenericParameterConstraints() => new List<Type>(this._resolver.GetConstraintTypes(this._token)).ToArray();

    public virtual Type GetGenericTypeDefinition() => throw new InvalidOperationException();

    protected override ConstructorInfo GetConstructorImpl(
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return (ConstructorInfo) null;
    }

    public virtual Type[] GetInterfaces() => MetadataOnlyTypeDef.GetAllInterfacesHelper((MetadataOnlyCommonType) this);

    public virtual Type GetInterface(string name, bool ignoreCase) => MetadataOnlyModule.GetInterfaceHelper(base.GetInterfaces(), name, ignoreCase);

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => MetadataOnlyTypeDef.GetMembersHelper((Type) this, bindingAttr);

    public virtual Guid GUID => Guid.Empty;

    protected virtual bool HasElementTypeImpl() => false;

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

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this.Resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);

    public virtual GenericParameterAttributes GenericParameterAttributes => this._gpAttributes;

    public virtual int GenericParameterPosition => (int) this._position;

    public virtual MemberTypes MemberType => MemberTypes.TypeInfo;

    public virtual Type DeclaringType
    {
      get
      {
        if (this._ownerTypeToken != 0)
          return this._resolver.ResolveType(this._ownerTypeToken);
        return base.DeclaringMethod != null ? ((MemberInfo) base.DeclaringMethod).DeclaringType : (Type) null;
      }
    }

    public override MethodBase DeclaringMethod => this._ownerMethodToken != 0 ? this._resolver.ResolveMethod(this._ownerMethodToken) : (MethodBase) null;

    public virtual string Name => this._name;

    public virtual string Namespace
    {
      get
      {
        if (((MemberInfo) this).DeclaringType != null)
          return ((MemberInfo) this).DeclaringType.Namespace;
        return base.DeclaringMethod != null ? ((MemberInfo) base.DeclaringMethod).DeclaringType.Namespace : (string) null;
      }
    }

    public virtual Assembly Assembly => ((Module) this._resolver).Assembly;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    public override Type ReflectedType => throw new NotSupportedException();

    public virtual string ToString() => ((MemberInfo) this).Name;

    protected virtual TypeCode GetTypeCodeImpl() => TypeCode.Object;
  }
}
