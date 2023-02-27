// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.TypeProxy
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("TypeProxy")]
  internal abstract class TypeProxy : MetadataOnlyCommonType, ITypeProxy
  {
    protected readonly MetadataOnlyModule m_resolver;
    private Type _cachedResolvedType;

    protected TypeProxy(MetadataOnlyModule resolver) => this.m_resolver = resolver;

    internal override MetadataOnlyModule Resolver => this.m_resolver;

    public ITypeUniverse TypeUniverse => this.m_resolver.AssemblyResolver;

    public virtual Type GetResolvedType()
    {
      if (this._cachedResolvedType == null)
        this._cachedResolvedType = this.GetResolvedTypeWorker();
      return this._cachedResolvedType;
    }

    protected abstract Type GetResolvedTypeWorker();

    public virtual string ToString() => ((object) this.GetResolvedType()).ToString();

    public virtual string FullName => this.GetResolvedType().FullName;

    public virtual string Namespace => this.GetResolvedType().Namespace;

    public virtual string Name => ((MemberInfo) this.GetResolvedType()).Name;

    public override string AssemblyQualifiedName => this.GetResolvedType().AssemblyQualifiedName;

    public override int GetHashCode() => ((object) this.GetResolvedType()).GetHashCode();

    public override bool Equals(object objOther) => objOther is Type type && base.Equals(type);

    public virtual bool Equals(Type t) => t != null && this.GetResolvedType().Equals(t);

    public override Type MakeByRefType() => (Type) this.Resolver.Factory.CreateByRefType((MetadataOnlyCommonType) this);

    public override Type MakePointerType() => (Type) this.Resolver.Factory.CreatePointerType((MetadataOnlyCommonType) this);

    public override int GetArrayRank() => this.GetResolvedType().GetArrayRank();

    public override Type MakeGenericType(params Type[] args) => (Type) new ProxyGenericType(this, args);

    public override Type MakeArrayType() => (Type) this.Resolver.Factory.CreateVectorType((MetadataOnlyCommonType) this);

    public override Type MakeArrayType(int rank) => (Type) this.Resolver.Factory.CreateArrayType((MetadataOnlyCommonType) this, rank);

    public override Module Module => ((MemberInfo) this.GetResolvedType()).Module;

    public virtual Type BaseType => this.GetResolvedType().BaseType;

    protected override bool IsArrayImpl() => this.GetResolvedType().IsArray;

    protected override bool IsByRefImpl() => this.GetResolvedType().IsByRef;

    protected override bool IsPointerImpl() => this.GetResolvedType().IsPointer;

    public override bool IsEnum => this.GetResolvedType().IsEnum;

    protected virtual bool IsValueTypeImpl() => this.GetResolvedType().IsValueType;

    protected override bool IsPrimitiveImpl() => this.GetResolvedType().IsPrimitive;

    public virtual Type GetElementType() => this.GetResolvedType().GetElementType();

    public virtual int MetadataToken => ((MemberInfo) this.GetResolvedType()).MetadataToken;

    public virtual Type[] GetGenericArguments() => this.GetResolvedType().GetGenericArguments();

    public virtual Type GetGenericTypeDefinition() => this.GetResolvedType().GetGenericTypeDefinition();

    public override bool ContainsGenericParameters => this.GetResolvedType().ContainsGenericParameters;

    public override MethodInfo[] GetMethods(BindingFlags flags) => this.GetResolvedType().GetMethods(flags);

    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => this.GetResolvedType().GetConstructors(bindingAttr);

    public virtual bool IsAssignableFrom(Type c) => this.GetResolvedType().IsAssignableFrom(c);

    protected override bool IsContextfulImpl() => this.GetResolvedType().IsContextful;

    protected override bool IsMarshalByRefImpl() => this.GetResolvedType().IsMarshalByRef;

    public override bool IsSubclassOf(Type c) => this.GetResolvedType().IsSubclassOf(c);

    public virtual Type UnderlyingSystemType => this.GetResolvedType().UnderlyingSystemType;

    protected virtual TypeAttributes GetAttributeFlagsImpl() => this.GetResolvedType().Attributes;

    protected override MethodInfo GetMethodImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return types == null && modifiers == null && binder == null && callConvention == CallingConventions.Any ? this.GetResolvedType().GetMethod(name, bindingAttr) : this.GetResolvedType().GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    protected override PropertyInfo GetPropertyImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      Type returnType,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      if (types != null || modifiers != null)
        return this.GetResolvedType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
      return returnType != null ? this.GetResolvedType().GetProperty(name, returnType) : this.GetResolvedType().GetProperty(name, bindingAttr);
    }

    protected override ConstructorInfo GetConstructorImpl(
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return this.GetResolvedType().GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    public virtual FieldInfo[] GetFields(BindingFlags flags) => this.GetResolvedType().GetFields(flags);

    public override PropertyInfo[] GetProperties(BindingFlags flags) => this.GetResolvedType().GetProperties(flags);

    public virtual EventInfo[] GetEvents(BindingFlags flags) => this.GetResolvedType().GetEvents(flags);

    public virtual EventInfo GetEvent(string name, BindingFlags flags) => this.GetResolvedType().GetEvent(name, flags);

    public virtual FieldInfo GetField(string name, BindingFlags flags) => this.GetResolvedType().GetField(name, flags);

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => this.GetResolvedType().GetNestedTypes(bindingAttr);

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr) => this.GetResolvedType().GetNestedType(name, bindingAttr);

    public virtual MemberInfo[] GetMember(
      string name,
      MemberTypes type,
      BindingFlags bindingAttr)
    {
      return this.GetResolvedType().GetMember(name, type, bindingAttr);
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => this.GetResolvedType().GetMembers(bindingAttr);

    public override bool IsInstanceOfType(object o) => this.GetResolvedType().IsInstanceOfType(o);

    public virtual Type[] GetInterfaces() => this.GetResolvedType().GetInterfaces();

    public virtual Type GetInterface(string name, bool ignoreCase) => name != null ? this.GetResolvedType().GetInterface(name, ignoreCase) : throw new ArgumentNullException(nameof (name));

    public override bool IsGenericParameter => this.GetResolvedType().IsGenericParameter;

    public override bool IsGenericType => this.GetResolvedType().IsGenericType;

    public virtual bool IsGenericTypeDefinition => this.GetResolvedType().IsGenericTypeDefinition;

    public virtual Guid GUID => this.GetResolvedType().GUID;

    protected virtual bool HasElementTypeImpl() => this.IsArray || this.IsByRef || this.IsPointer;

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
      return this.GetResolvedType().InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    protected override bool IsCOMObjectImpl() => this.GetResolvedType().IsCOMObject;

    public override MemberInfo[] GetDefaultMembers() => this.GetResolvedType().GetDefaultMembers();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => ((MemberInfo) this.GetResolvedType()).GetCustomAttributesData();

    public override StructLayoutAttribute StructLayoutAttribute => this.GetResolvedType().StructLayoutAttribute;

    public virtual GenericParameterAttributes GenericParameterAttributes => this.GetResolvedType().GenericParameterAttributes;

    public override MethodBase DeclaringMethod => this.GetResolvedType().DeclaringMethod;

    public virtual int GenericParameterPosition => this.GetResolvedType().GenericParameterPosition;

    public virtual Type[] GetGenericParameterConstraints() => this.GetResolvedType().GetGenericParameterConstraints();

    public virtual InterfaceMapping GetInterfaceMap(Type interfaceType) => this.GetResolvedType().GetInterfaceMap(interfaceType);

    public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria) => this.GetResolvedType().FindInterfaces(filter, filterCriteria);

    public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr) => this.GetResolvedType().GetMember(name, bindingAttr);

    public override bool IsSerializable => this.GetResolvedType().IsSerializable;

    public virtual MemberInfo[] FindMembers(
      MemberTypes memberType,
      BindingFlags bindingAttr,
      MemberFilter filter,
      object filterCriteria)
    {
      return this.GetResolvedType().FindMembers(memberType, bindingAttr, filter, filterCriteria);
    }

    public virtual MemberTypes MemberType => ((MemberInfo) this.GetResolvedType()).MemberType;

    public virtual Type DeclaringType => ((MemberInfo) this.GetResolvedType()).DeclaringType;

    public virtual Assembly Assembly => this.GetResolvedType().Assembly;

    public virtual object[] GetCustomAttributes(bool inherit) => ((MemberInfo) this.GetResolvedType()).GetCustomAttributes(inherit);

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => ((MemberInfo) this.GetResolvedType()).GetCustomAttributes(attributeType, inherit);

    public virtual bool IsDefined(Type attributeType, bool inherit) => ((MemberInfo) this.GetResolvedType()).IsDefined(attributeType, inherit);

    public override Type ReflectedType => ((MemberInfo) this.GetResolvedType()).ReflectedType;

    protected virtual TypeCode GetTypeCodeImpl() => Type.GetTypeCode(this.GetResolvedType());
  }
}
