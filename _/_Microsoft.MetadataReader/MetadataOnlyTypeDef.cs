// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyTypeDef
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using Microsoft.MetadataReader.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyTypeDef : MetadataOnlyCommonType
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly Token _tokenTypeDef;
    private readonly Type[] _typeParameters;
    private readonly Token _tokenExtends;
    private string _fullName;
    private readonly int _nameLength;
    private readonly TypeAttributes _typeAttributes;
    private Type _baseType;
    private MetadataOnlyTypeDef.TriState _fIsValueType = MetadataOnlyTypeDef.TriState.Maybe;
    private static readonly string[] s_primitiveTypeNames = new string[14]
    {
      "System.Boolean",
      "System.Char",
      "System.SByte",
      "System.Byte",
      "System.Int16",
      "System.UInt16",
      "System.Int32",
      "System.UInt32",
      "System.Int64",
      "System.UInt64",
      "System.Single",
      "System.Double",
      "System.IntPtr",
      "System.UIntPtr"
    };

    public MetadataOnlyTypeDef(MetadataOnlyModule scope, Token tokenTypeDef)
      : this(scope, tokenTypeDef, (Type[]) null)
    {
    }

    public MetadataOnlyTypeDef(MetadataOnlyModule scope, Token tokenTypeDef, Type[] typeParameters)
    {
      MetadataOnlyTypeDef.ValidateConstructorArguments(scope, tokenTypeDef);
      this._resolver = scope;
      this._tokenTypeDef = tokenTypeDef;
      this._typeParameters = (Type[]) null;
      this._resolver.GetTypeAttributes(this._tokenTypeDef, out this._tokenExtends, out this._typeAttributes, out this._nameLength);
      int length = this._resolver.CountGenericParams(this._tokenTypeDef);
      bool flag = typeParameters != null && typeParameters.Length != 0;
      if (length > 0)
      {
        if (!flag)
        {
          this._typeParameters = new Type[length];
          int num = 0;
          foreach (int genericParameterToken in this._resolver.GetGenericParameterTokens(Token.op_Implicit(this._tokenTypeDef)))
            this._typeParameters[num++] = (Type) this._resolver.Factory.CreateTypeVariable(this._resolver, new Token(genericParameterToken));
        }
        else
        {
          if (length != typeParameters.Length)
            throw new ArgumentException(Resources.WrongNumberOfGenericArguments);
          this._typeParameters = typeParameters;
        }
      }
      else
        this._typeParameters = Type.EmptyTypes;
    }

    private static void ValidateConstructorArguments(MetadataOnlyModule scope, Token tokenTypeDef)
    {
      if (scope == null)
        throw new ArgumentNullException(nameof (scope));
      if (!((Token) ref tokenTypeDef).IsType((TokenType) 33554432))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.ExpectedTokenType, new object[1]
        {
          (object) (TokenType) 33554432
        }));
    }

    private string LocalFullName
    {
      get
      {
        if (string.IsNullOrEmpty(this._fullName))
          this._resolver.GetTypeName(this._tokenTypeDef, this._nameLength, out this._fullName);
        return this._fullName;
      }
    }

    internal override MetadataOnlyModule Resolver => this._resolver;

    public virtual int MetadataToken
    {
      get
      {
        Token tokenTypeDef = this._tokenTypeDef;
        return ((Token) ref tokenTypeDef).Value;
      }
    }

    public virtual string FullName
    {
      get
      {
        if ((!base.IsGenericType || base.IsGenericTypeDefinition) && ((MemberInfo) this).DeclaringType == null)
          return this.LocalFullName;
        StringBuilder builder = StringBuilderPool.Get();
        this.GetSimpleName(builder);
        if (!base.IsGenericType || base.IsGenericTypeDefinition)
        {
          string fullName = builder.ToString();
          StringBuilderPool.Release(ref builder);
          return fullName;
        }
        builder.Append("[");
        Type[] genericArguments = base.GetGenericArguments();
        for (int index = 0; index < genericArguments.Length; ++index)
        {
          if (index > 0)
            builder.Append(",");
          builder.Append('[');
          if (genericArguments[index].FullName == null || genericArguments[index].IsGenericTypeDefinition)
            return (string) null;
          builder.Append(genericArguments[index].AssemblyQualifiedName);
          builder.Append(']');
        }
        builder.Append("]");
        string fullName1 = builder.ToString();
        StringBuilderPool.Release(ref builder);
        return fullName1;
      }
    }

    private void GetSimpleName(StringBuilder sb)
    {
      Type declaringType = ((MemberInfo) this).DeclaringType;
      if (declaringType != null)
      {
        sb.Append(declaringType.FullName);
        sb.Append('+');
      }
      sb.Append(this.LocalFullName);
    }

    public virtual string Namespace => ((MemberInfo) this).DeclaringType != null ? ((MemberInfo) this).DeclaringType.Namespace : Utility.GetNamespaceHelper(this.LocalFullName);

    public virtual string ToString()
    {
      if (!base.IsGenericType)
        return base.FullName;
      StringBuilder builder = StringBuilderPool.Get();
      this.GetSimpleName(builder);
      builder.Append("[");
      Type[] genericArguments = base.GetGenericArguments();
      for (int index = 0; index < genericArguments.Length; ++index)
      {
        if (index != 0)
          builder.Append(",");
        builder.Append((object) genericArguments[index]);
      }
      builder.Append("]");
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return str;
    }

    public virtual Type BaseType
    {
      get
      {
        if (this._baseType == null)
        {
          Token tokenExtends = this._tokenExtends;
          if (((Token) ref tokenExtends).IsNil)
            return (Type) null;
          this._baseType = this._resolver.ResolveTypeTokenInternal(this._tokenExtends, this.GenericContext);
        }
        return this._baseType;
      }
    }

    public virtual bool Equals(Type other)
    {
      if (other == null || !((object) ((MemberInfo) this).Module).Equals((object) ((MemberInfo) other).Module))
        return false;
      bool isGenericType1 = base.IsGenericType;
      bool isGenericType2 = other.IsGenericType;
      if (isGenericType1 != isGenericType2 || ((MemberInfo) this).MetadataToken != ((MemberInfo) other).MetadataToken)
        return false;
      if (!isGenericType1 && !isGenericType2)
        return true;
      Type[] genericArguments1 = base.GetGenericArguments();
      Type[] genericArguments2 = other.GetGenericArguments();
      if (genericArguments1.Length != genericArguments2.Length)
        return false;
      for (int index = 0; index < genericArguments1.Length; ++index)
      {
        if (!genericArguments1[index].Equals(genericArguments2[index]))
          return false;
      }
      return true;
    }

    public override Type MakeGenericType(params Type[] argTypes)
    {
      if (argTypes == null)
        throw new ArgumentNullException(nameof (argTypes));
      if (!base.IsGenericTypeDefinition)
        throw new InvalidOperationException();
      if (argTypes.Length == this._typeParameters.Length)
        return (Type) this.Resolver.Factory.CreateGenericType(this.Resolver, this._tokenTypeDef, argTypes);
      throw new ArgumentException(Resources.WrongNumberOfGenericArguments);
    }

    public override bool IsEnum => this._resolver.AssemblyResolver.GetTypeXFromName("System.Enum").Equals(base.BaseType);

    public virtual bool IsAssignableFrom(Type c) => MetadataOnlyTypeDef.IsAssignableFromHelper((Type) this, c);

    internal static bool IsAssignableFromHelper(Type current, Type target)
    {
      if (target == null)
        return false;
      if (current.Equals(target) || target.IsSubclassOf(current))
        return true;
      Type[] interfaces = target.GetInterfaces();
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (interfaces[index].Equals(current) || current.IsAssignableFrom(interfaces[index]))
          return true;
      }
      if (target.IsGenericParameter)
      {
        foreach (Type parameterConstraint in target.GetGenericParameterConstraints())
        {
          if (MetadataOnlyTypeDef.IsAssignableFromHelper(current, parameterConstraint))
            return true;
        }
      }
      ITypeUniverse itypeUniverse = Helpers.Universe(current);
      return itypeUniverse != null && current.Equals(itypeUniverse.GetTypeXFromName("System.Object")) && (target.IsPointer || target.IsInterface || target.IsArray);
    }

    public virtual Type UnderlyingSystemType => (Type) this;

    protected virtual bool IsValueTypeImpl()
    {
      if (this._fIsValueType == MetadataOnlyTypeDef.TriState.Maybe)
        this._fIsValueType = !this.IsValueTypeHelper() ? MetadataOnlyTypeDef.TriState.No : MetadataOnlyTypeDef.TriState.Yes;
      if (this._fIsValueType == MetadataOnlyTypeDef.TriState.Yes)
        return true;
      return this._fIsValueType == MetadataOnlyTypeDef.TriState.No && false;
    }

    private bool IsValueTypeHelper()
    {
      MetadataOnlyModule resolver = this.Resolver;
      return !base.Equals(resolver.AssemblyResolver.GetTypeXFromName("System.Enum")) && (resolver.AssemblyResolver.GetTypeXFromName("System.ValueType").Equals(base.BaseType) || base.IsEnum);
    }

    protected override bool IsPrimitiveImpl()
    {
      if (!this._resolver.IsSystemModule())
        return false;
      string fullName = base.FullName;
      foreach (string primitiveTypeName in MetadataOnlyTypeDef.s_primitiveTypeNames)
      {
        if (primitiveTypeName.Equals(fullName, StringComparison.Ordinal))
          return true;
      }
      return false;
    }

    public override bool IsGenericType => this._typeParameters.Length != 0;

    public virtual Type[] GetGenericArguments() => (Type[]) this._typeParameters.Clone();

    public virtual Type GetGenericTypeDefinition()
    {
      if (!base.IsGenericType)
        throw new InvalidOperationException();
      return base.IsGenericTypeDefinition ? (Type) this : (Type) this.Resolver.Factory.CreateSimpleType(this.Resolver, this._tokenTypeDef);
    }

    public virtual bool IsGenericTypeDefinition
    {
      get
      {
        if (!base.IsGenericType)
          return false;
        foreach (Type genericArgument in base.GetGenericArguments())
        {
          if (!genericArgument.IsGenericParameter || !((MemberInfo) genericArgument).DeclaringType.Equals((Type) this))
            return false;
        }
        return true;
      }
    }

    public virtual Type GetElementType() => (Type) null;

    public virtual FieldInfo[] GetFields(BindingFlags flags) => MetadataOnlyModule.GetFieldsOnType((MetadataOnlyCommonType) this, flags);

    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (FieldInfo field in base.GetFields(bindingAttr))
      {
        if (((MemberInfo) field).Name.Equals(name, stringComparison))
          return field;
      }
      return (FieldInfo) null;
    }

    internal static PropertyInfo GetPropertyImplHelper(
      MetadataOnlyCommonType type,
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      Type returnType,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      if (binder != null)
        throw new NotSupportedException();
      if (modifiers != null && modifiers.Length != 0)
        throw new NotSupportedException();
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (PropertyInfo property in ((Type) type).GetProperties(bindingAttr))
      {
        if (((MemberInfo) property).Name.Equals(name, stringComparison) && (returnType == null || property.PropertyType.Equals(returnType)) && MetadataOnlyTypeDef.PropertyParamTypesMatch(property, types))
          return property;
      }
      return (PropertyInfo) null;
    }

    public virtual Type[] GetInterfaces() => MetadataOnlyTypeDef.GetAllInterfacesHelper((MetadataOnlyCommonType) this);

    internal static Type[] GetAllInterfacesHelper(MetadataOnlyCommonType type)
    {
      HashSet<Type> hashSet = new HashSet<Type>();
      if (type.BaseType != null)
      {
        Type[] interfaces = type.BaseType.GetInterfaces();
        hashSet.UnionWith((IEnumerable<Type>) interfaces);
      }
      foreach (Type type1 in type.Resolver.GetInterfacesOnType((Type) type))
      {
        if (!hashSet.Contains(type1))
        {
          hashSet.Add(type1);
          Type[] interfaces = type1.GetInterfaces();
          hashSet.UnionWith((IEnumerable<Type>) interfaces);
        }
      }
      Type[] interfacesHelper = new Type[hashSet.Count];
      hashSet.CopyTo(interfacesHelper);
      return interfacesHelper;
    }

    public virtual Type GetInterface(string name, bool ignoreCase) => MetadataOnlyModule.GetInterfaceHelper(base.GetInterfaces(), name, ignoreCase);

    private static bool PropertyParamTypesMatch(PropertyInfo p, Type[] types)
    {
      if (types == null)
        return true;
      ParameterInfo[] indexParameters = p.GetIndexParameters();
      if (indexParameters.Length != types.Length)
        return false;
      int length = indexParameters.Length;
      for (int index = 0; index < length; ++index)
      {
        if (!indexParameters[index].ParameterType.Equals(types[index]))
          return false;
      }
      return true;
    }

    public virtual EventInfo[] GetEvents(BindingFlags flags) => MetadataOnlyModule.GetEventsOnType((MetadataOnlyCommonType) this, flags);

    public virtual EventInfo GetEvent(string name, BindingFlags flags)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      StringComparison stringComparison = SignatureUtil.GetStringComparison(flags);
      foreach (EventInfo eventInfo in base.GetEvents(flags))
      {
        if (((MemberInfo) eventInfo).Name.Equals(name, stringComparison))
          return eventInfo;
      }
      return (EventInfo) null;
    }

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (Type nestedType in base.GetNestedTypes(bindingAttr))
      {
        if (((MemberInfo) nestedType).Name.Equals(name, stringComparison))
          return nestedType;
      }
      return (Type) null;
    }

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => new List<Type>(this._resolver.GetNestedTypesOnType((MetadataOnlyCommonType) this, bindingAttr)).ToArray();

    public virtual MemberInfo[] GetMember(
      string name,
      MemberTypes type,
      BindingFlags bindingAttr)
    {
      MemberInfo[] members = base.GetMembers(bindingAttr);
      List<MemberInfo> memberInfoList = new List<MemberInfo>();
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (MemberInfo memberInfo in members)
      {
        if (name.Equals(memberInfo.Name, stringComparison) && (type == memberInfo.MemberType || type == MemberTypes.All))
          memberInfoList.Add(memberInfo);
      }
      return memberInfoList.ToArray();
    }

    internal static MemberInfo[] GetMembersHelper(Type type, BindingFlags bindingAttr)
    {
      List<MemberInfo> memberInfoList = new List<MemberInfo>((IEnumerable<MemberInfo>) type.GetMethods(bindingAttr));
      memberInfoList.AddRange((IEnumerable<MemberInfo>) type.GetConstructors(bindingAttr));
      memberInfoList.AddRange((IEnumerable<MemberInfo>) type.GetFields(bindingAttr));
      memberInfoList.AddRange((IEnumerable<MemberInfo>) type.GetProperties(bindingAttr));
      memberInfoList.AddRange((IEnumerable<MemberInfo>) type.GetEvents(bindingAttr));
      memberInfoList.AddRange((IEnumerable<MemberInfo>) type.GetNestedTypes(bindingAttr));
      return memberInfoList.ToArray();
    }

    public virtual Guid GUID
    {
      get
      {
        foreach (CustomAttributeData customAttributeData in (IEnumerable<CustomAttributeData>) ((MemberInfo) this).GetCustomAttributesData())
        {
          if (((MemberInfo) customAttributeData.Constructor).DeclaringType.FullName.Equals("System.Runtime.InteropServices.GuidAttribute"))
          {
            CustomAttributeTypedArgument constructorArgument = customAttributeData.ConstructorArguments[0];
            return new Guid((string) ((CustomAttributeTypedArgument) ref constructorArgument).Value);
          }
        }
        return Guid.Empty;
      }
    }

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

    protected override bool IsCOMObjectImpl() => throw new NotImplementedException();

    public override StructLayoutAttribute StructLayoutAttribute
    {
      get
      {
        if (this.IsInterface)
          return (StructLayoutAttribute) null;
        uint ulClassSize = 0;
        uint dwPackSize;
        this._resolver.RawImport.GetClassLayout(Token.op_Implicit(this._tokenTypeDef), out dwPackSize, UnusedIntPtr.Zero, 0U, UnusedIntPtr.Zero, ref ulClassSize);
        if (dwPackSize == 0U)
          dwPackSize = 8U;
        LayoutKind layoutKind;
        switch (this._typeAttributes & TypeAttributes.LayoutMask)
        {
          case TypeAttributes.NotPublic:
            layoutKind = LayoutKind.Auto;
            break;
          case TypeAttributes.SequentialLayout:
            layoutKind = LayoutKind.Sequential;
            break;
          case TypeAttributes.ExplicitLayout:
            layoutKind = LayoutKind.Explicit;
            break;
          default:
            throw new InvalidOperationException(Resources.IllegalLayoutMask);
        }
        CharSet charSet = CharSet.None;
        switch (this._typeAttributes & TypeAttributes.StringFormatMask)
        {
          case TypeAttributes.NotPublic:
            charSet = CharSet.Ansi;
            break;
          case TypeAttributes.UnicodeClass:
            charSet = CharSet.Unicode;
            break;
          case TypeAttributes.AutoClass:
            charSet = CharSet.Auto;
            break;
        }
        return new StructLayoutAttribute(layoutKind)
        {
          Size = (int) ulClassSize,
          Pack = (int) dwPackSize,
          CharSet = charSet
        };
      }
    }

    public virtual MemberTypes MemberType => this.IsNested ? MemberTypes.NestedType : MemberTypes.TypeInfo;

    public virtual Type DeclaringType => this._resolver.GetEnclosingType(new Token(((MemberInfo) this).MetadataToken)) ?? (Type) null;

    public virtual string Name => Utility.GetTypeNameFromFullNameHelper(this.LocalFullName, this.IsNested);

    public virtual Assembly Assembly => ((Module) this._resolver).Assembly;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    protected virtual TypeAttributes GetAttributeFlagsImpl() => this._typeAttributes;

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this.Resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);

    public virtual GenericParameterAttributes GenericParameterAttributes => throw new InvalidOperationException(Resources.ValidOnGenericParameterTypeOnly);

    protected virtual TypeCode GetTypeCodeImpl() => this._resolver.GetTypeCode((Type) this);

    internal override IEnumerable<PropertyInfo> GetDeclaredProperties() => this.Resolver.GetPropertiesOnDeclaredTypeOnly(this._tokenTypeDef, this.GenericContext);

    internal override IEnumerable<MethodBase> GetDeclaredMethods() => this.Resolver.GetMethodBasesOnDeclaredTypeOnly(this._tokenTypeDef, this.GenericContext, MetadataOnlyModule.EMethodKind.Methods);

    internal override IEnumerable<MethodBase> GetDeclaredConstructors() => this.Resolver.GetMethodBasesOnDeclaredTypeOnly(this._tokenTypeDef, this.GenericContext, MetadataOnlyModule.EMethodKind.Constructor);

    private enum TriState
    {
      Yes,
      No,
      Maybe,
    }
  }
}
