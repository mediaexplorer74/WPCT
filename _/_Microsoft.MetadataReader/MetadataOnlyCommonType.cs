// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyCommonType
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("\\{Name = {Name} FullName = {FullName}\\}")]
  internal abstract class MetadataOnlyCommonType : Type
  {
    internal abstract MetadataOnlyModule Resolver { get; }

    internal virtual GenericContext GenericContext => new GenericContext(this.GetGenericArguments(), (Type[]) null);

    internal virtual IEnumerable<MethodBase> GetDeclaredMethods() => (IEnumerable<MethodBase>) new MethodInfo[0];

    internal virtual IEnumerable<MethodBase> GetDeclaredConstructors() => (IEnumerable<MethodBase>) new MethodInfo[0];

    internal virtual IEnumerable<PropertyInfo> GetDeclaredProperties() => (IEnumerable<PropertyInfo>) new PropertyInfo[0];

    public virtual PropertyInfo[] GetProperties(BindingFlags flags) => MetadataOnlyModule.GetPropertiesOnType(this, flags);

    protected virtual PropertyInfo GetPropertyImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      Type returnType,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return MetadataOnlyTypeDef.GetPropertyImplHelper(this, name, bindingAttr, binder, returnType, types, modifiers);
    }

    public virtual MethodInfo[] GetMethods(BindingFlags flags) => MetadataOnlyModule.GetMethodsOnType(this, flags);

    protected virtual MethodInfo GetMethodImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return MetadataOnlyModule.GetMethodImplHelper((Type) this, name, bindingAttr, binder, callConvention, types, modifiers);
    }

    public virtual MemberInfo[] GetMembers(BindingFlags bindingAttr) => MetadataOnlyTypeDef.GetMembersHelper((Type) this, bindingAttr);

    protected virtual ConstructorInfo GetConstructorImpl(
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      return MetadataOnlyModule.GetConstructorOnType(this, bindingAttr, binder, callConvention, types, modifiers);
    }

    public virtual ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => MetadataOnlyModule.GetConstructorsOnType(this, bindingAttr);

    public virtual Module Module => (Module) this.Resolver;

    public virtual bool Equals(object objOther) => objOther is Type type && this.Equals(type);

    public virtual int GetHashCode() => ((MemberInfo) this).MetadataToken;

    public virtual bool ContainsGenericParameters
    {
      get
      {
        if (this.HasElementType)
          return this.GetRootElementType().ContainsGenericParameters;
        if (base.IsGenericParameter)
          return true;
        if (!base.IsGenericType)
          return false;
        foreach (Type genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.ContainsGenericParameters)
            return true;
        }
        return false;
      }
    }

    private Type GetRootElementType()
    {
      Type rootElementType = (Type) this;
      while (rootElementType.HasElementType)
        rootElementType = rootElementType.GetElementType();
      return rootElementType;
    }

    public virtual bool IsSubclassOf(Type c)
    {
      Type type = (Type) this;
      if (type.Equals(c))
        return false;
      for (; type != null; type = type.BaseType)
      {
        if (type.Equals(c))
          return true;
      }
      return false;
    }

    protected virtual bool IsContextfulImpl()
    {
      Type typeXfromName = this.Resolver.AssemblyResolver.GetTypeXFromName("System.ContextBoundObject");
      return typeXfromName != null && typeXfromName.IsAssignableFrom((Type) this);
    }

    protected virtual bool IsMarshalByRefImpl()
    {
      Type typeXfromName = this.Resolver.AssemblyResolver.GetTypeXFromName("System.MarshalByRefObject");
      return typeXfromName != null && typeXfromName.IsAssignableFrom((Type) this);
    }

    public virtual MemberInfo[] GetDefaultMembers()
    {
      Type typeXfromName = this.Resolver.AssemblyResolver.GetTypeXFromName("System.Reflection.DefaultMemberAttribute");
      if (typeXfromName == null)
        return new MemberInfo[0];
      CustomAttributeData customAttributeData = (CustomAttributeData) null;
      for (Type type = (Type) this; type != null; type = type.BaseType)
      {
        IList<CustomAttributeData> customAttributesData = ((MemberInfo) type).GetCustomAttributesData();
        for (int index = 0; index < ((ICollection<CustomAttributeData>) customAttributesData).Count; ++index)
        {
          if (((MemberInfo) customAttributesData[index].Constructor).DeclaringType.Equals(typeXfromName))
          {
            customAttributeData = customAttributesData[index];
            break;
          }
        }
        if (customAttributeData != null)
          break;
      }
      if (customAttributeData == null)
        return new MemberInfo[0];
      CustomAttributeTypedArgument constructorArgument = customAttributeData.ConstructorArguments[0];
      return this.GetMember(((CustomAttributeTypedArgument) ref constructorArgument).Value as string) ?? new MemberInfo[0];
    }

    public virtual bool IsInstanceOfType(object o) => false;

    public virtual string AssemblyQualifiedName
    {
      get
      {
        string fullName = this.FullName;
        return fullName == null ? (string) null : Assembly.CreateQualifiedName(this.Assembly.GetName().ToString(), fullName);
      }
    }

    public virtual bool IsSerializable => (this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic || this.QuickSerializationCastCheck();

    private bool QuickSerializationCastCheck()
    {
      ITypeUniverse itypeUniverse = Helpers.Universe((Type) this);
      Type typeXfromName1 = itypeUniverse.GetTypeXFromName("System.Enum");
      Type typeXfromName2 = itypeUniverse.GetTypeXFromName("System.Delegate");
      for (Type type = this.UnderlyingSystemType; type != null; type = type.BaseType)
      {
        if (type.Equals(typeXfromName1) || type.Equals(typeXfromName2))
          return true;
      }
      return false;
    }

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual bool IsEnum => false;

    protected virtual bool IsArrayImpl() => false;

    protected virtual bool IsByRefImpl() => false;

    protected virtual bool IsPointerImpl() => false;

    protected virtual bool IsPrimitiveImpl() => false;

    public virtual bool IsGenericType => false;

    public virtual bool IsGenericParameter => false;

    public virtual MethodBase DeclaringMethod => throw new InvalidOperationException(Resources.ValidOnGenericParameterTypeOnly);

    protected virtual bool IsCOMObjectImpl() => throw new NotImplementedException();

    public virtual StructLayoutAttribute StructLayoutAttribute => (StructLayoutAttribute) null;

    public virtual int GetArrayRank() => throw new ArgumentException(Resources.OperationValidOnArrayTypeOnly);

    public virtual Type MakeGenericType(params Type[] argTypes) => throw new InvalidOperationException();

    public virtual Type MakeByRefType() => (Type) this.Resolver.Factory.CreateByRefType(this);

    public virtual Type MakePointerType() => (Type) this.Resolver.Factory.CreatePointerType(this);

    public virtual Type MakeArrayType() => (Type) this.Resolver.Factory.CreateVectorType(this);

    public virtual Type MakeArrayType(int rank) => this.MakeArrayTypeHelper(rank);

    [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
    private Type MakeArrayTypeHelper(int rank)
    {
      if (rank <= 0)
        throw new IndexOutOfRangeException();
      return (Type) this.Resolver.Factory.CreateArrayType(this, rank);
    }

    internal static string TypeSigToString(Type pThis)
    {
      StringBuilder builder = StringBuilderPool.Get();
      MetadataOnlyCommonType.TypeSigToString(pThis, builder);
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return str;
    }

    internal static void TypeSigToString(Type pThis, StringBuilder sb)
    {
      Type type = pThis;
      while (type.HasElementType)
        type = type.GetElementType();
      if (type.IsNested)
      {
        sb.Append(((MemberInfo) pThis).Name);
      }
      else
      {
        string str = ((object) pThis).ToString();
        if (type.IsPrimitive || type.FullName == "System.Void")
          str = str.Substring("System.".Length);
        sb.Append(str);
      }
    }
  }
}
