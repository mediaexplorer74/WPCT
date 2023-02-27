// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyMethodInfo
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
  internal class MetadataOnlyMethodInfo : MethodInfo
  {
    private readonly Token _methodDef;
    private string _name;
    private readonly uint _nameLength;
    private Type _tOwner;
    private MethodSignatureDescriptor _descriptor;
    private ParameterInfo _returnParameter;
    private MethodBody _methodBody;
    private readonly MethodAttributes _attrs;
    private readonly Type[] _typeArgs;
    private readonly Type[] _methodArgs;
    private GenericContext _context;
    private Token _declaringTypeDef;
    private SignatureBlob _sigBlob;
    private bool _fullyInitialized;

    internal static MethodBase Create(
      MetadataOnlyModule resolver,
      Token methodDef,
      GenericContext context)
    {
      Type[] typeArgs = Type.EmptyTypes;
      Type[] methodArgs = Type.EmptyTypes;
      if (context != null)
      {
        typeArgs = context.TypeArgs;
        methodArgs = context.MethodArgs;
      }
      return resolver.Factory.CreateMethodOrConstructor(resolver, methodDef, typeArgs, methodArgs);
    }

    public MetadataOnlyMethodInfo(MetadataOnlyMethodInfo method)
    {
      this.Resolver = method.Resolver;
      this._methodDef = method._methodDef;
      this._tOwner = method._tOwner;
      this._descriptor = method._descriptor;
      this._name = method._name;
      this._nameLength = method._nameLength;
      this._attrs = method._attrs;
      this._returnParameter = method._returnParameter;
      this._methodBody = method._methodBody;
      this._declaringTypeDef = method._declaringTypeDef;
      this._sigBlob = method._sigBlob;
      this._typeArgs = method._typeArgs;
      this._methodArgs = method._methodArgs;
      this._context = method._context;
      this._fullyInitialized = method._fullyInitialized;
    }

    public MetadataOnlyMethodInfo(
      MetadataOnlyModule resolver,
      Token methodDef,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      this.Resolver = resolver;
      this._methodDef = methodDef;
      this._typeArgs = typeArgs;
      this._methodArgs = methodArgs;
      resolver.GetMethodAttrs(methodDef, out this._declaringTypeDef, out this._attrs, out this._nameLength);
    }

    private void InitializeName()
    {
      if (!string.IsNullOrEmpty(this._name))
        return;
      this.Resolver.GetMethodName(this._methodDef, this._nameLength, out this._name);
    }

    private void Initialize()
    {
      Type ownerType = (Type) null;
      Type[] typeArgs = (Type[]) null;
      if (!((Token) ref this._declaringTypeDef).IsNil)
        this.GetOwnerTypeAndTypeArgs(out ownerType, out typeArgs);
      else
        typeArgs = this._typeArgs;
      Type[] genericMethodArgs = this.GetGenericMethodArgs();
      GenericContext context = new GenericContext(typeArgs, genericMethodArgs);
      this.Resolver.GetMethodSig(this._methodDef, out this._sigBlob);
      MethodSignatureDescriptor methodSignature = SignatureUtil.ExtractMethodSignature(this._sigBlob, this.Resolver, context);
      this._tOwner = ownerType;
      this._context = context;
      this._descriptor = methodSignature;
      this._fullyInitialized = true;
    }

    private void GetOwnerTypeAndTypeArgs(out Type ownerType, out Type[] typeArgs)
    {
      Type type = (Type) this.Resolver.ResolveTypeDefToken(this._declaringTypeDef);
      GenericContext context = new GenericContext(this._typeArgs, this._methodArgs);
      if (type.IsGenericType && GenericContext.IsNullOrEmptyTypeArgs(context))
        context = new GenericContext(type.GetGenericArguments(), this._methodArgs);
      Type genericType = this.Resolver.GetGenericType(new Token(((MemberInfo) type).MetadataToken), context);
      ownerType = genericType;
      typeArgs = context.TypeArgs;
    }

    private Type[] GetGenericMethodArgs()
    {
      Type[] typeArray = (Type[]) null;
      int length = this.Resolver.CountGenericParams(this._methodDef);
      bool flag = this._methodArgs != null && this._methodArgs.Length != 0;
      if (length > 0)
      {
        if (!flag)
        {
          typeArray = new Type[length];
          int num = 0;
          foreach (int genericParameterToken in this.Resolver.GetGenericParameterTokens(Token.op_Implicit(this._methodDef)))
            typeArray[num++] = (Type) this.Resolver.Factory.CreateTypeVariable(this.Resolver, new Token(genericParameterToken));
        }
        else
        {
          if (length != this._methodArgs.Length)
            throw new ArgumentException(Resources.WrongNumberOfGenericArguments);
          typeArray = this._methodArgs;
        }
      }
      return typeArray == null ? Type.EmptyTypes : typeArray;
    }

    public virtual int MetadataToken
    {
      get
      {
        Token methodDef = this._methodDef;
        return ((Token) ref methodDef).Value;
      }
    }

    internal MetadataOnlyModule Resolver { get; }

    public virtual Module Module => (Module) this.Resolver;

    public virtual Type ReturnType
    {
      get
      {
        if (!this._fullyInitialized)
          this.Initialize();
        return this._descriptor.ReturnParameter.Type;
      }
    }

    public virtual bool Equals(object obj)
    {
      if (!(obj is MetadataOnlyMethodInfo metadataOnlyMethodInfo) || !((MemberInfo) this).DeclaringType.Equals(((MemberInfo) metadataOnlyMethodInfo).DeclaringType))
        return false;
      if (!((MethodBase) this).IsGenericMethod)
        return ((object) metadataOnlyMethodInfo).GetHashCode() == ((object) this).GetHashCode();
      if (!((MethodBase) metadataOnlyMethodInfo).IsGenericMethod)
        return false;
      Type[] genericArguments1 = ((MethodBase) this).GetGenericArguments();
      Type[] genericArguments2 = ((MethodBase) metadataOnlyMethodInfo).GetGenericArguments();
      if (genericArguments1.Length != genericArguments2.Length)
        return false;
      for (int index = 0; index < genericArguments1.Length; ++index)
      {
        if (!genericArguments1[index].Equals(genericArguments2[index]))
          return false;
      }
      return true;
    }

    public virtual int GetHashCode() => ((object) this.Resolver).GetHashCode() * (int) short.MaxValue + this._methodDef.GetHashCode();

    public virtual string ToString() => MetadataOnlyMethodInfo.CommonToString((MethodInfo) this);

    internal static string CommonToString(MethodInfo m)
    {
      StringBuilder builder = StringBuilderPool.Get();
      MetadataOnlyCommonType.TypeSigToString(m.ReturnType, builder);
      builder.Append(" ");
      MetadataOnlyMethodInfo.ConstructMethodString(m, builder);
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return str;
    }

    private static void ConstructMethodString(MethodInfo m, StringBuilder sb)
    {
      sb.Append(((MemberInfo) m).Name);
      string str = "";
      if (((MethodBase) m).IsGenericMethod)
      {
        sb.Append("[");
        foreach (Type genericArgument in ((MethodBase) m).GetGenericArguments())
        {
          sb.Append(str);
          MetadataOnlyCommonType.TypeSigToString(genericArgument, sb);
          str = ",";
        }
        sb.Append("]");
      }
      sb.Append("(");
      MetadataOnlyMethodInfo.ConstructParameters(sb, ((MethodBase) m).GetParameters(), ((MethodBase) m).CallingConvention);
      sb.Append(")");
    }

    private static void ConstructParameters(
      StringBuilder sb,
      ParameterInfo[] parameters,
      CallingConventions callingConvention)
    {
      Type[] parameters1 = new Type[parameters.Length];
      for (int index = 0; index < parameters.Length; ++index)
        parameters1[index] = parameters[index].ParameterType;
      MetadataOnlyMethodInfo.ConstructParameters(sb, parameters1, callingConvention);
    }

    private static void ConstructParameters(
      StringBuilder sb,
      Type[] parameters,
      CallingConventions callingConvention)
    {
      string str = "";
      for (int index = 0; index < parameters.Length; ++index)
      {
        Type parameter = parameters[index];
        sb.Append(str);
        MetadataOnlyCommonType.TypeSigToString(parameter, sb);
        if (parameter.IsByRef)
        {
          --sb.Length;
          sb.Append(" ByRef");
        }
        str = ", ";
      }
      if ((callingConvention & CallingConventions.VarArgs) != CallingConventions.VarArgs)
        return;
      sb.Append(str);
      sb.Append("...");
    }

    public virtual Type DeclaringType
    {
      get
      {
        if (!this._fullyInitialized)
          this.Initialize();
        return this._tOwner;
      }
    }

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

    public virtual ParameterInfo[] GetParameters()
    {
      if (!this._fullyInitialized)
        this.Initialize();
      int length1 = this._descriptor.Parameters.Length;
      ParameterInfo[] parameters = new ParameterInfo[length1];
      Type[] typeArray = new Type[length1];
      for (int index = 0; index < length1; ++index)
        typeArray[index] = this._descriptor.Parameters[index].Type;
      int[] numArray = new int[length1 + 1];
      IMetadataImport rawImport = this.Resolver.RawImport;
      HCORENUM hcorenum = new HCORENUM();
      IMetadataImport metadataImport = rawImport;
      ref HCORENUM local1 = ref hcorenum;
      Token methodDef = this._methodDef;
      int mdMethodDef = ((Token) ref methodDef).Value;
      int[] rParams = numArray;
      int length2 = numArray.Length;
      uint num;
      ref uint local2 = ref num;
      if (metadataImport.EnumParams(ref local1, mdMethodDef, rParams, length2, out local2) == 1)
      {
        for (int position = 0; position < length1; ++position)
          parameters[position] = this.Resolver.Policy.GetFakeParameterInfo((MemberInfo) this, typeArray[position], position);
        return parameters;
      }
      hcorenum.Close(rawImport);
      if (num == 0U)
        return parameters;
      ParameterInfo parameterInfo = (ParameterInfo) null;
      for (int index1 = 0; (long) index1 < (long) num; ++index1)
      {
        int tk = numArray[index1];
        uint pulSequence;
        rawImport.GetParamProps(tk, out int _, out pulSequence, (StringBuilder) null, 0U, out uint _, out uint _, out uint _, out UnusedIntPtr _, out uint _);
        if (pulSequence == 0U)
        {
          parameterInfo = (ParameterInfo) new MetadataOnlyParameterInfo(this.Resolver, new Token(tk), base.ReturnType, this._descriptor.ReturnParameter.CustomModifiers);
        }
        else
        {
          uint index2 = pulSequence - 1U;
          parameters[(int) index2] = (ParameterInfo) new MetadataOnlyParameterInfo(this.Resolver, new Token(tk), typeArray[(int) index2], this._descriptor.Parameters[(int) index2].CustomModifiers);
        }
      }
      if (parameterInfo == null)
        parameterInfo = this.Resolver.Policy.GetFakeParameterInfo((MemberInfo) this, base.ReturnType, -1);
      this._returnParameter = parameterInfo;
      for (int position = 0; position < length1; ++position)
      {
        if (parameters[position] == null)
          parameters[position] = this.Resolver.Policy.GetFakeParameterInfo((MemberInfo) this, typeArray[position], position);
      }
      return parameters;
    }

    public virtual ParameterInfo ReturnParameter
    {
      get
      {
        if (this._returnParameter == null)
          ((MethodBase) this).GetParameters();
        return this._returnParameter == null ? this.Resolver.Policy.GetFakeParameterInfo((MemberInfo) this, base.ReturnType, -1) : this._returnParameter;
      }
    }

    public virtual MethodAttributes Attributes => this._attrs;

    public virtual CallingConventions CallingConvention
    {
      get
      {
        if (!this._fullyInitialized)
          this.Initialize();
        CorCallingConvention callingConvention1 = this._descriptor.CallingConvention;
        CallingConventions callingConvention2 = (callingConvention1 & CorCallingConvention.Mask) != CorCallingConvention.VarArg ? CallingConventions.Standard : CallingConventions.VarArgs;
        if ((callingConvention1 & CorCallingConvention.HasThis) != 0)
          callingConvention2 |= CallingConventions.HasThis;
        if ((callingConvention1 & CorCallingConvention.ExplicitThis) != 0)
          callingConvention2 |= CallingConventions.ExplicitThis;
        return callingConvention2;
      }
    }

    public virtual MemberTypes MemberType => MemberTypes.Method;

    public virtual bool IsGenericMethodDefinition
    {
      get
      {
        if (!this._fullyInitialized)
          this.Initialize();
        if ((this._descriptor.CallingConvention & CorCallingConvention.Generic) == 0)
          return false;
        if (GenericContext.IsNullOrEmptyMethodArgs(this._context))
          return true;
        MethodInfo methodOrConstructor = this.Resolver.Factory.CreateMethodOrConstructor(this.Resolver, this._methodDef, (Type[]) null, (Type[]) null) as MethodInfo;
        foreach (Type methodArg in this._context.MethodArgs)
        {
          if (!methodArg.IsGenericParameter || !((object) methodOrConstructor).Equals((object) methodArg.DeclaringMethod))
            return false;
        }
        return true;
      }
    }

    public virtual bool IsGenericMethod
    {
      get
      {
        if (!this._fullyInitialized)
          this.Initialize();
        return !GenericContext.IsNullOrEmptyMethodArgs(this._context);
      }
    }

    public virtual MethodInfo MakeGenericMethod(params Type[] types)
    {
      if (!((MethodBase) this).IsGenericMethodDefinition)
        throw new InvalidOperationException();
      return (MethodInfo) MetadataOnlyMethodInfo.Create(this.Resolver, this._methodDef, new GenericContext(this._context.TypeArgs, types));
    }

    public virtual Type[] GetGenericArguments()
    {
      if (!this._fullyInitialized)
        this.Initialize();
      return (Type[]) this._context.MethodArgs.Clone();
    }

    public virtual MethodInfo GetGenericMethodDefinition()
    {
      if (!((MethodBase) this).IsGenericMethod)
        throw new InvalidOperationException();
      return ((MethodBase) this).IsGenericMethodDefinition ? (MethodInfo) this : this.Resolver.Factory.CreateMethodOrConstructor(this.Resolver, this._methodDef, this._context.TypeArgs, (Type[]) null) as MethodInfo;
    }

    public virtual bool ContainsGenericParameters
    {
      get
      {
        if (((MemberInfo) this).DeclaringType.ContainsGenericParameters)
          return true;
        foreach (Type genericArgument in ((MethodBase) this).GetGenericArguments())
        {
          if (genericArgument.ContainsGenericParameters)
            return true;
        }
        return false;
      }
    }

    public virtual MethodBody GetMethodBody()
    {
      if (this._methodBody == null)
        this._methodBody = MetadataOnlyMethodBody.TryCreate(this);
      return this._methodBody;
    }

    public virtual MethodImplAttributes GetMethodImplementationFlags() => this.Resolver.GetMethodImplFlags(Token.op_Implicit(this._methodDef));

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

    public virtual MethodInfo GetBaseDefinition()
    {
      if (!((MethodBase) this).IsVirtual || ((MethodBase) this).IsStatic || ((MemberInfo) this).DeclaringType == null || ((MemberInfo) this).DeclaringType.IsInterface)
        return (MethodInfo) this;
      Type baseType = ((MemberInfo) this).DeclaringType.BaseType;
      if (baseType == null)
        return (MethodInfo) this;
      List<Type> typeList = new List<Type>();
      foreach (ParameterInfo parameter in ((MethodBase) this).GetParameters())
        typeList.Add(parameter.ParameterType);
      MethodInfo method = baseType.GetMethod(((MemberInfo) this).Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, (Binder) null, ((MethodBase) this).CallingConvention, typeList.ToArray(), (ParameterModifier[]) null);
      return method == null ? (MethodInfo) this : method.GetBaseDefinition();
    }

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual ICustomAttributeProvider ReturnTypeCustomAttributes => throw new NotSupportedException();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this.Resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);
  }
}
