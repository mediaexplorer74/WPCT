// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyCommonArrayType
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
  internal class MetadataOnlyCommonArrayType : MetadataOnlyCommonType
  {
    private readonly MetadataOnlyCommonType _elementType;
    private readonly Type _baseType;

    public MetadataOnlyCommonArrayType(MetadataOnlyCommonType elementType)
    {
      this._baseType = Helpers.Universe((Type) elementType).GetTypeXFromName("System.Array");
      this._elementType = elementType;
    }

    public virtual string Namespace => this._elementType.Namespace;

    internal override MetadataOnlyModule Resolver => this._elementType.Resolver;

    public virtual Type BaseType => this._baseType;

    protected override bool IsArrayImpl() => true;

    public virtual Type UnderlyingSystemType => (Type) this;

    public virtual Type GetElementType() => (Type) this._elementType;

    public override int GetHashCode() => ((object) this._elementType).GetHashCode();

    public virtual int MetadataToken => 33554432;

    public virtual Type[] GetGenericArguments() => this._elementType.GetGenericArguments();

    public virtual Type GetGenericTypeDefinition() => throw new InvalidOperationException();

    internal override IEnumerable<MethodBase> GetDeclaredMethods() => (IEnumerable<MethodBase>) this.Resolver.Policy.GetExtraArrayMethods((Type) this);

    internal override IEnumerable<MethodBase> GetDeclaredConstructors() => (IEnumerable<MethodBase>) this.Resolver.Policy.GetExtraArrayConstructors((Type) this);

    public virtual FieldInfo[] GetFields(BindingFlags flags) => new FieldInfo[0];

    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr) => (FieldInfo) null;

    public virtual EventInfo[] GetEvents(BindingFlags flags) => new EventInfo[0];

    public virtual EventInfo GetEvent(string name, BindingFlags flags) => (EventInfo) null;

    protected virtual TypeAttributes GetAttributeFlagsImpl() => (TypeAttributes) (257 | 8192);

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr) => (Type) null;

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => new Type[0];

    public virtual Type[] GetInterfaces()
    {
      List<Type> typeList = new List<Type>((IEnumerable<Type>) this._baseType.GetInterfaces());
      typeList.AddRange((IEnumerable<Type>) this.Resolver.Policy.GetExtraArrayInterfaces((Type) this._elementType));
      return typeList.ToArray();
    }

    public virtual Type GetInterface(string name, bool ignoreCase) => MetadataOnlyModule.GetInterfaceHelper(base.GetInterfaces(), name, ignoreCase);

    public override Type MakeGenericType(params Type[] argTypes) => throw new InvalidOperationException();

    public virtual MemberTypes MemberType => MemberTypes.TypeInfo;

    public virtual Type DeclaringType => (Type) null;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual Assembly Assembly => this._elementType.Assembly;

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

    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      CustomAttributeData serializableAttribute = PseudoCustomAttributes.GetSerializableAttribute(this.Resolver, false);
      if (serializableAttribute == null)
        return (IList<CustomAttributeData>) new CustomAttributeData[0];
      return (IList<CustomAttributeData>) new CustomAttributeData[1]
      {
        serializableAttribute
      };
    }

    public virtual GenericParameterAttributes GenericParameterAttributes => throw new InvalidOperationException(Resources.ValidOnGenericParameterTypeOnly);

    protected virtual TypeCode GetTypeCodeImpl() => TypeCode.Object;

    public virtual string FullName => throw new InvalidOperationException();

    public virtual string Name => throw new InvalidOperationException();

    public virtual bool IsAssignableFrom(Type c) => throw new InvalidOperationException();

    public virtual bool Equals(Type o) => throw new InvalidOperationException();
  }
}
