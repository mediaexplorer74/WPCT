// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyTypeVariableRef
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
  internal class MetadataOnlyTypeVariableRef : MetadataOnlyCommonType
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly Token _ownerToken;
    private readonly int _position;

    internal MetadataOnlyTypeVariableRef(
      MetadataOnlyModule resolver,
      Token ownerToken,
      int position)
    {
      this._resolver = resolver;
      this._ownerToken = ownerToken;
      this._position = position;
    }

    private bool IsMethodVar
    {
      get
      {
        Token ownerToken1 = this._ownerToken;
        int num;
        if (!((Token) ref ownerToken1).IsType((TokenType) 167772160))
        {
          Token ownerToken2 = this._ownerToken;
          num = ((Token) ref ownerToken2).IsType((TokenType) 100663296) ? 1 : 0;
        }
        else
          num = 1;
        return num != 0;
      }
    }

    public virtual string FullName => (string) null;

    internal override MetadataOnlyModule Resolver => this._resolver;

    public virtual Type BaseType => throw new InvalidOperationException();

    public virtual bool Equals(Type other)
    {
      if (other is MetadataOnlyTypeVariableRef onlyTypeVariableRef)
      {
        int num1;
        if (((object) this.Resolver).Equals((object) onlyTypeVariableRef.Resolver))
        {
          Token ownerToken = this._ownerToken;
          int num2 = ((Token) ref ownerToken).Value;
          ownerToken = onlyTypeVariableRef._ownerToken;
          int num3 = ((Token) ref ownerToken).Value;
          if (num2 == num3)
          {
            num1 = this._position == onlyTypeVariableRef._position ? 1 : 0;
            goto label_5;
          }
        }
        num1 = 0;
label_5:
        return num1 != 0;
      }
      if (!other.IsGenericParameter)
        return false;
      bool flag = this.IsMethodVar == (other.DeclaringMethod != null);
      return this._position == other.GenericParameterPosition & flag;
    }

    public virtual bool IsAssignableFrom(Type c) => throw new InvalidOperationException();

    public virtual Type UnderlyingSystemType => throw new InvalidOperationException();

    public virtual Type GetElementType() => throw new InvalidOperationException();

    public virtual int MetadataToken => throw new InvalidOperationException();

    public override MethodInfo[] GetMethods(BindingFlags flags) => throw new InvalidOperationException();

    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr) => throw new InvalidOperationException();

    public virtual FieldInfo[] GetFields(BindingFlags flags) => throw new InvalidOperationException();

    public virtual FieldInfo GetField(string name, BindingFlags flags) => throw new InvalidOperationException();

    public override PropertyInfo[] GetProperties(BindingFlags flags) => throw new InvalidOperationException();

    public virtual EventInfo[] GetEvents(BindingFlags flags) => throw new InvalidOperationException();

    public virtual EventInfo GetEvent(string name, BindingFlags flags) => throw new InvalidOperationException();

    public override Type MakeGenericType(params Type[] argTypes) => throw new InvalidOperationException();

    public virtual Type GetNestedType(string name, BindingFlags bindingAttr) => throw new InvalidOperationException();

    public virtual Type[] GetNestedTypes(BindingFlags bindingAttr) => throw new InvalidOperationException();

    protected virtual TypeAttributes GetAttributeFlagsImpl() => throw new InvalidOperationException();

    protected override MethodInfo GetMethodImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      throw new InvalidOperationException();
    }

    protected override PropertyInfo GetPropertyImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      Type returnType,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      throw new InvalidOperationException();
    }

    public override bool IsGenericParameter => true;

    public virtual Type[] GetGenericArguments() => throw new InvalidOperationException();

    public virtual Type[] GetGenericParameterConstraints() => throw new InvalidOperationException();

    public virtual Type GetGenericTypeDefinition() => throw new InvalidOperationException();

    protected override ConstructorInfo GetConstructorImpl(
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      throw new InvalidOperationException();
    }

    public virtual Type[] GetInterfaces() => throw new InvalidOperationException();

    public virtual Type GetInterface(string name, bool ignoreCase) => throw new InvalidOperationException();

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr) => throw new InvalidOperationException();

    public virtual Guid GUID => throw new InvalidOperationException();

    protected virtual bool HasElementTypeImpl() => throw new InvalidOperationException();

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

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => throw new InvalidOperationException();

    public virtual GenericParameterAttributes GenericParameterAttributes => throw new InvalidOperationException();

    public virtual int GenericParameterPosition => this._position;

    public virtual MemberTypes MemberType => MemberTypes.TypeInfo;

    public virtual Type DeclaringType
    {
      get
      {
        if (this.IsMethodVar)
          return (Type) null;
        Token ownerToken = this._ownerToken;
        return ((Token) ref ownerToken).IsType((TokenType) 33554432) ? (Type) this._resolver.Factory.CreateSimpleType(this._resolver, this._ownerToken) : this._resolver.Factory.CreateTypeRef(this._resolver, this._ownerToken);
      }
    }

    public override MethodBase DeclaringMethod => this.IsMethodVar ? this._resolver.Factory.CreateMethodOrConstructor(this._resolver, this._ownerToken, (Type[]) null, (Type[]) null) : (MethodBase) null;

    public virtual string Name => (string) null;

    public virtual string Namespace => (string) null;

    public virtual Assembly Assembly => ((Module) this._resolver).Assembly;

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    public override Type ReflectedType => throw new NotSupportedException();

    public virtual string ToString() => this.IsMethodVar ? "MVar!!" + base.GenericParameterPosition.ToString((IFormatProvider) CultureInfo.InvariantCulture) : "Var!" + base.GenericParameterPosition.ToString((IFormatProvider) CultureInfo.InvariantCulture);

    protected virtual TypeCode GetTypeCodeImpl() => throw new InvalidOperationException();
  }
}
