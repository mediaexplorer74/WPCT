// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ArrayFabricatedMethodInfo
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
  internal abstract class ArrayFabricatedMethodInfo : MethodInfo
  {
    private readonly Type _arrayType;

    protected ArrayFabricatedMethodInfo(Type arrayType) => this._arrayType = arrayType;

    protected ITypeUniverse Universe => Helpers.Universe(this._arrayType);

    protected int Rank => this._arrayType.GetArrayRank();

    protected Type GetElementType() => this._arrayType.GetElementType();

    protected ParameterInfo[] MakeParameterHelper(int extra)
    {
      int rank = this.Rank;
      Type builtInType = this.Universe.GetBuiltInType((CorElementType) 8);
      ParameterInfo[] parameterInfoArray = new ParameterInfo[rank + extra];
      for (int position = 0; position < rank; ++position)
        parameterInfoArray[position] = this.MakeParameterInfo(builtInType, position);
      return parameterInfoArray;
    }

    protected ParameterInfo MakeParameterInfo(Type t, int position) => (ParameterInfo) new SimpleParameterInfo((MemberInfo) this, t, position);

    public virtual ICustomAttributeProvider ReturnTypeCustomAttributes => throw new NotImplementedException();

    public virtual MethodInfo GetBaseDefinition() => (MethodInfo) this;

    public virtual ParameterInfo ReturnParameter => this.MakeParameterInfo(this.ReturnType, -1);

    public virtual MethodAttributes Attributes => MethodAttributes.Public;

    public virtual CallingConventions CallingConvention => CallingConventions.Standard | CallingConventions.HasThis;

    public virtual MethodInfo MakeGenericMethod(params Type[] types) => throw new InvalidOperationException();

    public virtual bool IsGenericMethodDefinition => false;

    public virtual Type[] GetGenericArguments() => Type.EmptyTypes;

    public virtual bool ContainsGenericParameters => this.GetElementType().IsGenericParameter;

    public virtual MethodBody GetMethodBody() => (MethodBody) null;

    public virtual MethodImplAttributes GetMethodImplementationFlags() => MethodImplAttributes.IL;

    public virtual object Invoke(
      object obj,
      BindingFlags invokeAttr,
      Binder binder,
      object[] parameters,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual RuntimeMethodHandle MethodHandle => throw new NotSupportedException();

    public virtual MemberTypes MemberType => MemberTypes.Method;

    public virtual Type DeclaringType => this._arrayType;

    public virtual int MetadataToken => Token.op_Implicit(new Token((TokenType) 100663296, 0));

    public virtual Module Module => ((MemberInfo) ((MemberInfo) this).DeclaringType).Module;

    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(bool inherit) => new object[0];

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => new object[0];

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotImplementedException();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => (IList<CustomAttributeData>) new CustomAttributeData[0];

    public virtual string ToString() => MetadataOnlyMethodInfo.CommonToString((MethodInfo) this);

    public virtual bool Equals(object obj) => obj is ArrayFabricatedMethodInfo fabricatedMethodInfo && ((MemberInfo) this).DeclaringType.Equals(((MemberInfo) fabricatedMethodInfo).DeclaringType) && ((MemberInfo) this).Name.Equals(((MemberInfo) fabricatedMethodInfo).Name);

    public virtual int GetHashCode() => ((object) ((MemberInfo) this).DeclaringType).GetHashCode() + ((MemberInfo) this).Name.GetHashCode();
  }
}
