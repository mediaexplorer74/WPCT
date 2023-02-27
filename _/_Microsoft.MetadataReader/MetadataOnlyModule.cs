// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyModule
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Microsoft.MetadataReader
{
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
  public class MetadataOnlyModule : Module, IModule2, IDisposable
  {
    private readonly string _modulePath;
    private readonly Dictionary<Thread, IMetadataImport> _cachedThreadAffinityImporter;
    private readonly object _lock = new object();
    private string _scopeName;
    private Token[] _typeCodeMapping;
    private MetadataOnlyModule.NestedTypeCache _nestedTypeInfo;
    private Assembly _assembly;
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    private const BindingFlags MembersDeclaredOnTypeOnly = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public MetadataOnlyModule(ITypeUniverse universe, MetadataFile import, string modulePath)
      : this(universe, import, (IReflectionFactory) new DefaultFactory(), modulePath)
    {
    }

    public MetadataOnlyModule(
      ITypeUniverse universe,
      MetadataFile import,
      IReflectionFactory factory,
      string modulePath)
    {
      this.AssemblyResolver = universe;
      this.RawMetadata = import;
      this.Factory = factory;
      this.Policy = (IMetadataExtensionsPolicy) new MetadataExtensionsPolicy20(universe);
      this._modulePath = modulePath;
      this._cachedThreadAffinityImporter = new Dictionary<Thread, IMetadataImport>();
    }

    public virtual string FullyQualifiedName => this._modulePath;

    internal IMetadataExtensionsPolicy Policy { get; }

    internal IReflectionFactory Factory { get; }

    public virtual string ToString() => this.RawMetadata == null ? "uninitialized" : base.ScopeName;

    public virtual bool Equals(object obj)
    {
      if (obj == this)
        return true;
      return obj is MetadataOnlyModule metadataOnlyModule && ((object) this.AssemblyResolver).Equals((object) metadataOnlyModule.AssemblyResolver) && base.ScopeName == ((Module) metadataOnlyModule).ScopeName;
    }

    public virtual int GetHashCode() => this.RawMetadata.RawPtr.GetHashCode() + ((object) this.AssemblyResolver).GetHashCode();

    public ITypeUniverse AssemblyResolver { get; }

    internal bool IsValidToken(int token) => this.RawImport.IsValidToken((uint) token);

    internal bool IsValidToken(Token token) => this.IsValidToken(((Token) ref token).Value);

    public byte[] ReadEmbeddedBlob(EmbeddedBlobPointer pointer, int countBytes) => this.RawMetadata.ReadEmbeddedBlob(pointer, countBytes);

    internal MetadataFile RawMetadata { get; }

    internal IMetadataImport RawImport => this.GetThreadSafeImporter();

    private IMetadataImport GetThreadSafeImporter()
    {
      IMetadataImport objectForIunknown;
      lock (this._lock)
      {
        if (!this._cachedThreadAffinityImporter.TryGetValue(Thread.CurrentThread, out objectForIunknown))
        {
          objectForIunknown = (IMetadataImport) Marshal.GetUniqueObjectForIUnknown(this.RawMetadata.RawPtr);
          this._cachedThreadAffinityImporter.Add(Thread.CurrentThread, objectForIunknown);
        }
      }
      return objectForIunknown;
    }

    public virtual string ScopeName
    {
      get
      {
        if (this._scopeName == null)
        {
          IMetadataImport threadSafeImporter = this.GetThreadSafeImporter();
          int pchName;
          Guid mvid;
          threadSafeImporter.GetScopeProps((StringBuilder) null, 0, out pchName, out mvid);
          StringBuilder builder = StringBuilderPool.Get(pchName);
          threadSafeImporter.GetScopeProps(builder, builder.Capacity, out pchName, out mvid);
          builder.Length = pchName - 1;
          this._scopeName = builder.ToString();
          StringBuilderPool.Release(ref builder);
        }
        return this._scopeName;
      }
    }

    public virtual Guid ModuleVersionId
    {
      get
      {
        Guid mvid;
        this.RawImport.GetScopeProps((StringBuilder) null, 0, out int _, out mvid);
        return mvid;
      }
    }

    public virtual string Name => Path.GetFileName(this._modulePath);

    internal MetadataOnlyCommonType ResolveTypeDefToken(Token token) => this.Factory.CreateSimpleType(this, token);

    private void EnsureValidToken(Token token)
    {
      if (!this.IsValidToken(token))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.InvalidMetadataToken, new object[1]
        {
          (object) token
        }));
    }

    internal Type ResolveTypeTokenInternal(Token token, GenericContext context)
    {
      this.EnsureValidToken(token);
      if (((Token) ref token).IsType((TokenType) 33554432))
        return (Type) this.ResolveTypeDefToken(token);
      if (((Token) ref token).IsType((TokenType) 16777216))
        return this.Factory.CreateTypeRef(this, token);
      if (!((Token) ref token).IsType((TokenType) 452984832))
        throw new ArgumentException(Resources.TypeTokenExpected);
      Type[] typeArgs = (Type[]) null;
      Type[] methodArgs = (Type[]) null;
      if (context != null)
      {
        typeArgs = context.TypeArgs;
        methodArgs = context.MethodArgs;
      }
      return this.Factory.CreateTypeSpec(this, token, typeArgs, methodArgs);
    }

    public Type GetGenericType(Token token, GenericContext context)
    {
      Type[] typeArgs = (Type[]) null;
      Type[] methodArgs = (Type[]) null;
      if (context != null)
      {
        typeArgs = context.TypeArgs;
        methodArgs = context.MethodArgs;
      }
      if (((Token) ref token).IsType((TokenType) 33554432))
        return typeArgs != null && typeArgs.Length != 0 ? (Type) this.Factory.CreateGenericType(this, token, typeArgs) : (Type) this.Factory.CreateSimpleType(this, token);
      if (((Token) ref token).IsType((TokenType) 16777216))
      {
        Type genericType = this.Factory.CreateTypeRef(this, token);
        if (typeArgs != null && typeArgs.Length != 0)
          genericType = genericType.MakeGenericType(typeArgs);
        return genericType;
      }
      if (((Token) ref token).IsType((TokenType) 452984832))
        return this.Factory.CreateTypeSpec(this, token, typeArgs, methodArgs);
      throw new ArgumentException(Resources.TypeTokenExpected);
    }

    private MethodBase ResolveMethodTokenInternal(
      Token methodToken,
      GenericContext context)
    {
      this.EnsureValidToken(methodToken);
      if (((Token) ref methodToken).IsType((TokenType) 100663296))
        return this.ResolveMethodDef(methodToken);
      if (((Token) ref methodToken).IsType((TokenType) 167772160))
        return this.ResolveMethodRef(methodToken, context, (Type[]) null);
      return ((Token) ref methodToken).IsType((TokenType) 721420288) ? (MethodBase) this.ResolveMethodSpec(methodToken, context) : throw new ArgumentException(Resources.MethodTokenExpected);
    }

    private MethodInfo ResolveMethodSpec(Token methodToken, GenericContext context)
    {
      Token tkParent;
      EmbeddedBlobPointer ppvSigBlob;
      int pcbSigBlob;
      ((IMetadataImport2) this.RawImport).GetMethodSpecProps(methodToken, out tkParent, out ppvSigBlob, out pcbSigBlob);
      byte[] sig = this.ReadEmbeddedBlob(ppvSigBlob, pcbSigBlob);
      int index1 = 0;
      SignatureUtil.ExtractCallingConvention(sig, ref index1);
      int length = SignatureUtil.ExtractInt(sig, ref index1);
      Type[] typeArray = new Type[length];
      for (int index2 = 0; index2 < length; ++index2)
        typeArray[index2] = SignatureUtil.ExtractType(sig, ref index1, this, context);
      Token token;
      // ISSUE: explicit constructor call
      ((Token) ref token).\u002Ector(Token.op_Implicit(tkParent));
      TokenType tokenType = ((Token) ref token).TokenType;
      MethodInfo methodInfo;
      if (tokenType != 100663296)
      {
        if (tokenType != 167772160)
          throw new InvalidOperationException();
        methodInfo = (MethodInfo) this.ResolveMethodRef(token, context, typeArray);
      }
      else
        methodInfo = this.GetGenericMethodInfo(token, new GenericContext((Type[]) null, typeArray));
      return methodInfo;
    }

    private MethodBase ResolveMethodDef(Token methodToken)
    {
      List<Type> typeParameters = this.GetTypeParameters(((Token) ref methodToken).Value);
      GenericContext context = (GenericContext) null;
      if (typeParameters.Count > 0)
        context = new GenericContext((Type[]) null, typeParameters.ToArray());
      return MetadataOnlyMethodInfo.Create(this, methodToken, context);
    }

    public MethodInfo GetGenericMethodInfo(
      Token methodToken,
      GenericContext genericContext)
    {
      return (MethodInfo) this.GetGenericMethodBase(methodToken, genericContext);
    }

    internal MethodBase GetGenericMethodBase(
      Token methodToken,
      GenericContext genericContext)
    {
      if (genericContext != null && (genericContext.TypeArgs == null || genericContext.TypeArgs.Length == 0) && (genericContext.MethodArgs == null || genericContext.MethodArgs.Length == 0))
        genericContext = (GenericContext) null;
      return MetadataOnlyMethodInfo.Create(this, methodToken, genericContext);
    }

    internal MethodBase ResolveMethodRef(
      Token memberRef,
      GenericContext context,
      Type[] genericMethodParameters)
    {
      Token declaringTypeToken;
      string nameMember;
      SignatureBlob sig;
      this.GetMemberRefData(memberRef, out declaringTypeToken, out nameMember, out sig);
      byte[] signatureAsByteArray = sig.GetSignatureAsByteArray();
      int index = 0;
      if (SignatureUtil.ExtractCallingConvention(signatureAsByteArray, ref index) == CorCallingConvention.VarArg)
        throw new NotImplementedException(Resources.VarargSignaturesNotImplemented);
      Type type = this.ResolveTypeTokenInternal(declaringTypeToken, context);
      MethodSignatureDescriptor methodSignature;
      if (type.IsArray)
      {
        methodSignature = SignatureUtil.ExtractMethodSignature(sig, this, context);
      }
      else
      {
        GenericContext context1 = (GenericContext) new OpenGenericContext(this, type, memberRef);
        methodSignature = SignatureUtil.ExtractMethodSignature(sig, this, context1);
      }
      GenericContext context2 = new GenericContext(type.GetGenericArguments(), genericMethodParameters);
      return SignatureComparer.FindMatchingMethod(nameMember, type, methodSignature, context2) ?? throw new MissingMethodException(((MemberInfo) type).Name, nameMember);
    }

    internal FieldInfo ResolveFieldRef(Token memberRef, GenericContext context)
    {
      Token declaringTypeToken;
      string nameMember;
      this.GetMemberRefData(memberRef, out declaringTypeToken, out nameMember, out SignatureBlob _);
      return this.ResolveTypeTokenInternal(declaringTypeToken, context).GetField(nameMember, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }

    internal FieldInfo ResolveFieldTokenInternal(Token fieldToken, GenericContext context)
    {
      if (((Token) ref fieldToken).IsType((TokenType) 67108864))
        return (FieldInfo) this.Factory.CreateField(this, fieldToken, (Type[]) null, (Type[]) null);
      return ((Token) ref fieldToken).IsType((TokenType) 167772160) ? this.ResolveFieldRef(fieldToken, context) : throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.InvalidMetadataToken, new object[1]
      {
        (object) fieldToken
      }));
    }

    public virtual string ResolveString(int metadataToken)
    {
      Token token;
      // ISSUE: explicit constructor call
      ((Token) ref token).\u002Ector(metadataToken);
      IMetadataImport rawImport = this.RawImport;
      int pchString;
      rawImport.GetUserString(Token.op_Implicit(token), (char[]) null, 0, out pchString);
      char[] szString = new char[pchString];
      rawImport.GetUserString(Token.op_Implicit(token), szString, szString.Length, out pchString);
      return new string(szString);
    }

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this.GetCustomAttributeData(base.MetadataToken);

    internal Type ResolveTypeRef(ITypeReference typeReference)
    {
      Token resolutionScope = typeReference.ResolutionScope;
      string rawName = typeReference.RawName;
      TokenType tokenType = ((Token) ref resolutionScope).TokenType;
      if (tokenType <= 16777216)
      {
        if (tokenType == null)
          return this.GetType(typeReference.FullName);
        if (tokenType == 16777216)
          return this.Factory.CreateTypeRef(this, resolutionScope).GetNestedType(rawName, BindingFlags.Public | BindingFlags.NonPublic);
      }
      else
      {
        if (tokenType == 436207616)
          return this.ResolveModuleRef(resolutionScope).GetType(typeReference.FullName);
        if (tokenType == 587202560)
        {
          Assembly assembly = this.AssemblyResolver.ResolveAssembly((Module) this, resolutionScope);
          if (assembly == null)
            throw new InvalidOperationException(Resources.ResolverMustResolveToValidAssembly);
          if (((IAssembly2) assembly).TypeUniverse != this.AssemblyResolver)
            throw new InvalidOperationException(Resources.ResolvedAssemblyMustBeWithinSameUniverse);
          return assembly.GetType(rawName, true);
        }
      }
      throw new InvalidOperationException(Resources.InvalidMetadata);
    }

    internal Module ResolveModuleRef(Token moduleRefToken)
    {
      if (base.Assembly == null)
        throw new InvalidOperationException(Resources.CannotResolveModuleRefOnNetModule);
      StringBuilder builder = StringBuilderPool.Get();
      IMetadataImport rawImport = this.RawImport;
      int pchName;
      rawImport.GetModuleRefProps(((Token) ref moduleRefToken).Value, (StringBuilder) null, 0, out pchName);
      builder.EnsureCapacity(pchName);
      rawImport.GetModuleRefProps(((Token) ref moduleRefToken).Value, builder, builder.Capacity, out pchName);
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return base.Assembly.GetModule(str);
    }

    internal Token LookupTypeToken(string className) => this.FindTypeDefByName((Type) null, className, true);

    internal Token FindTypeDefByName(Type outerType, string className, bool fThrow)
    {
      Token outerTypeDefToken;
      // ISSUE: explicit constructor call
      ((Token) ref outerTypeDefToken).\u002Ector(0);
      if (outerType != null)
      {
        if (((MemberInfo) outerType).Module != this)
          throw new InvalidOperationException(Resources.DifferentTokenResolverForOuterType);
        // ISSUE: explicit constructor call
        ((Token) ref outerTypeDefToken).\u002Ector(((MemberInfo) outerType).MetadataToken);
      }
      return this.FindTypeDefByName(outerTypeDefToken, className, fThrow);
    }

    internal Token FindTypeDefByName(Token outerTypeDefToken, string className, bool fThrow)
    {
      if (((Token) ref outerTypeDefToken).IsNil)
        ;
      int token;
      int typeDefByName1 = this.RawImport.FindTypeDefByName(className, Token.op_Implicit(outerTypeDefToken), out token);
      if (!fThrow && typeDefByName1 == -2146234064)
        return Token.Nil;
      if (typeDefByName1 != 0)
        throw Marshal.GetExceptionForHR(typeDefByName1);
      Token typeDefByName2;
      // ISSUE: explicit constructor call
      ((Token) ref typeDefByName2).\u002Ector(token);
      return typeDefByName2;
    }

    internal void GetMemberRefData(
      Token token,
      out Token declaringTypeToken,
      out string nameMember,
      out SignatureBlob sig)
    {
      if (!this.IsValidToken(token))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
            Resources.InvalidMetadataToken, new object[1]
        {
          (object) token
        }));
      IMetadataImport rawImport = this.RawImport;
      uint pchMember;
      EmbeddedBlobPointer ppvSigBlob;
      uint pbSig;
      rawImport.GetMemberRefProps(token, out declaringTypeToken, (StringBuilder) null, 
          0, out pchMember, out ppvSigBlob, out pbSig);
      StringBuilder builder = StringBuilderPool.Get((int) pchMember);
      rawImport.GetMemberRefProps(token, out declaringTypeToken, builder, builder.Capacity, 
          out pchMember, out ppvSigBlob, out pbSig);
      nameMember = builder.ToString();
      StringBuilderPool.Release(ref builder);
      sig = SignatureBlob.ReadSignature(this.RawMetadata, ppvSigBlob, (int) pbSig);
    }

    internal uint GetMethodRva(int methodDef)
    {
      uint pulCodeRVA;
      this.RawImport.GetMethodProps((uint) methodDef, out int _, (StringBuilder) null, 
          0, out uint _, out MethodAttributes _, out EmbeddedBlobPointer _, out uint _, out pulCodeRVA, out uint _);
      return pulCodeRVA;
    }

    internal MethodImplAttributes GetMethodImplFlags(int methodToken)
    {
      uint flags;
      this.RawImport.GetRVA(methodToken, out uint _, out flags);
      return (MethodImplAttributes) flags;
    }

    internal void GetMethodAttrs(
      Token methodDef,
      out Token declaringTypeDef,
      out MethodAttributes attrs,
      out uint nameLength)
    {
      if (!this.IsValidToken(methodDef))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.InvalidMetadataToken, new object[1]
        {
          (object) methodDef
        }));
      int pClass;
      this.RawImport.GetMethodProps((uint) ((Token) ref methodDef).Value, out pClass, (StringBuilder) null, 0, out nameLength, out attrs, out EmbeddedBlobPointer _, out uint _, out uint _, out uint _);
      declaringTypeDef = new Token(pClass);
    }

    internal void GetMethodSig(Token methodDef, out SignatureBlob signature)
    {
      if (!this.IsValidToken(methodDef))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.InvalidMetadataToken, new object[1]
        {
          (object) methodDef
        }));
      EmbeddedBlobPointer ppvSigBlob;
      uint pcbSigBlob;
      this.RawImport.GetMethodProps((uint) ((Token) ref methodDef).Value, out int _, (StringBuilder) null, 0, out uint _, out MethodAttributes _, out ppvSigBlob, out pcbSigBlob, out uint _, out uint _);
      signature = SignatureBlob.ReadSignature(this.RawMetadata, ppvSigBlob, (int) pcbSigBlob);
    }

    internal void GetMethodName(Token methodDef, uint nameLength, out string name)
    {
      if (!this.IsValidToken(methodDef))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.InvalidMetadataToken, new object[1]
        {
          (object) methodDef
        }));
      uint md = (uint) ((Token) ref methodDef).Value;
      IMetadataImport rawImport = this.RawImport;
      StringBuilder builder = StringBuilderPool.Get((int) nameLength);
      rawImport.GetMethodProps(md, out int _, builder, builder.Capacity, out nameLength, out MethodAttributes _, out EmbeddedBlobPointer _, out uint _, out uint _, out uint _);
      name = builder.ToString();
      StringBuilderPool.Release(ref builder);
    }

    internal CorElementType GetEnumUnderlyingType(Token tokenTypeDef)
    {
      IMetadataImport rawImport = this.RawImport;
      HCORENUM phEnum = new HCORENUM();
      try
      {
        int mdFieldDef;
        uint pcTokens;
        for (rawImport.EnumFields(ref phEnum, ((Token) ref tokenTypeDef).Value, out mdFieldDef, 1, out pcTokens); pcTokens > 0U; rawImport.EnumFields(ref phEnum, ((Token) ref tokenTypeDef).Value, out mdFieldDef, 1, out pcTokens))
        {
          FieldAttributes pdwAttr;
          EmbeddedBlobPointer ppvSigBlob;
          int pcbSigBlob;
          rawImport.GetFieldProps(mdFieldDef, out int _, (StringBuilder) null, 0, out int _, out pdwAttr, out ppvSigBlob, out pcbSigBlob, out int _, out IntPtr _, out int _);
          if ((pdwAttr & FieldAttributes.Static) == FieldAttributes.PrivateScope)
          {
            byte[] sig = this.ReadEmbeddedBlob(ppvSigBlob, pcbSigBlob);
            int index = 0;
            SignatureUtil.ExtractCallingConvention(sig, ref index);
            return SignatureUtil.ExtractElementType(sig, ref index);
          }
        }
        throw new ArgumentException(Resources.OperationValidOnEnumOnly);
      }
      finally
      {
        phEnum.Close(rawImport);
      }
    }

    internal void GetTypeAttributes(
      Token tokenTypeDef,
      out Token tokenExtends,
      out TypeAttributes attr,
      out int nameLength)
    {
      int ptkExtends;
      this.RawImport.GetTypeDefProps(((Token) ref tokenTypeDef).Value, (StringBuilder) null, 0, out nameLength, out attr, out ptkExtends);
      tokenExtends = new Token(ptkExtends);
    }

    internal void GetTypeName(Token tokenTypeDef, int nameLength, out string name)
    {
      IMetadataImport rawImport = this.RawImport;
      StringBuilder builder = StringBuilderPool.Get(nameLength);
      rawImport.GetTypeDefProps(((Token) ref tokenTypeDef).Value, builder, builder.Capacity, out nameLength, out TypeAttributes _, out int _);
      name = TypeNameQuoter.GetQuotedTypeName(builder.ToString());
      StringBuilderPool.Release(ref builder);
    }

    internal static ConstructorInfo[] GetConstructorsOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(flags, nameof (GetConstructorsOnType));
      List<ConstructorInfo> constructorInfoList = new List<ConstructorInfo>();
      foreach (ConstructorInfo declaredConstructor in type.GetDeclaredConstructors())
      {
        if (Utility.IsBindingFlagsMatching((MethodBase) declaredConstructor, false, flags))
          constructorInfoList.Add(declaredConstructor);
      }
      return constructorInfoList.ToArray();
    }

    internal static ConstructorInfo GetConstructorOnType(
      MetadataOnlyCommonType type,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      MetadataOnlyModule.CheckBinderAndModifiersforLMR(binder, modifiers);
      foreach (ConstructorInfo method in MetadataOnlyModule.GetConstructorsOnType(type, bindingAttr))
      {
        if (SignatureUtil.IsCallingConventionMatch((MethodBase) method, callConvention) && SignatureUtil.IsParametersTypeMatch((MethodBase) method, types))
          return method;
      }
      return (ConstructorInfo) null;
    }

    private static void CheckBinderAndModifiersforLMR(Binder binder, ParameterModifier[] modifiers)
    {
      if (binder != null)
        throw new NotSupportedException();
      if (modifiers != null && modifiers.Length != 0)
        throw new NotSupportedException();
    }

    internal static MethodInfo GetMethodImplHelper(
      Type type,
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConv,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      if (modifiers != null && modifiers.Length != 0)
        throw new NotSupportedException();
      MethodInfo[] methods = type.GetMethods(bindingAttr);
      if (binder == null)
        return MetadataOnlyModule.FilterMethod(methods, name, bindingAttr, callConv, types);
      List<MethodBase> methodBaseList = new List<MethodBase>();
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (MethodInfo method in methods)
      {
        if (((MemberInfo) method).Name.Equals(name, stringComparison) && SignatureUtil.IsCallingConventionMatch((MethodBase) method, callConv))
          methodBaseList.Add((MethodBase) method);
      }
      return binder.SelectMethod(bindingAttr, methodBaseList.ToArray(), types, modifiers) as MethodInfo;
    }

    private static MethodInfo FilterMethod(
      MethodInfo[] methods,
      string name,
      BindingFlags bindingAttr,
      CallingConventions callConv,
      Type[] types)
    {
      bool flag = false;
      MethodInfo methodInfo = (MethodInfo) null;
      StringComparison stringComparison = SignatureUtil.GetStringComparison(bindingAttr);
      foreach (MethodInfo method in methods)
      {
        if (!flag || ((MemberInfo) methodInfo).DeclaringType == null || ((MemberInfo) methodInfo).DeclaringType.Equals(((MemberInfo) method).DeclaringType))
        {
          if (((MemberInfo) method).Name.Equals(name, stringComparison) && SignatureUtil.IsCallingConventionMatch((MethodBase) method, callConv) && SignatureUtil.IsParametersTypeMatch((MethodBase) method, types))
          {
            if (flag)
              throw new AmbiguousMatchException();
            methodInfo = method;
            flag = true;
          }
        }
        else
          break;
      }
      return methodInfo;
    }

    internal static MethodInfo[] GetMethodsOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(flags, nameof (GetMethodsOnType));
      List<MethodInfo> methods1 = new List<MethodInfo>();
      foreach (MethodInfo declaredMethod in type.GetDeclaredMethods())
      {
        if (Utility.IsBindingFlagsMatching((MethodBase) declaredMethod, false, flags))
          methods1.Add(declaredMethod);
      }
      if (MetadataOnlyModule.WalkInheritanceChain(flags) && type.BaseType != null)
      {
        MethodInfo[] methods2 = type.BaseType.GetMethods(flags);
        List<MethodInfo> collection = new List<MethodInfo>();
        foreach (MethodInfo inheritedMethod in methods2)
        {
          if (MetadataOnlyModule.IncludeInheritedMethod(inheritedMethod, (IEnumerable<MethodInfo>) methods1, flags))
            collection.Add(inheritedMethod);
        }
        methods1.AddRange((IEnumerable<MethodInfo>) collection);
      }
      return methods1.ToArray();
    }

    private static bool WalkInheritanceChain(BindingFlags flags) => (flags & BindingFlags.DeclaredOnly) == 0;

    private static IList<PropertyInfo> FilterInheritedProperties(
      IList<PropertyInfo> inheritedProperties,
      IList<PropertyInfo> properties,
      BindingFlags flags)
    {
      if (properties == null || ((ICollection<PropertyInfo>) properties).Count == 0)
        return inheritedProperties;
      List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();
      List<MethodInfo> methods1 = new List<MethodInfo>();
      List<MethodInfo> methods2 = new List<MethodInfo>();
      foreach (PropertyInfo property in (IEnumerable<PropertyInfo>) properties)
      {
        MethodInfo getMethod = property.GetGetMethod();
        if (getMethod != null)
          methods1.Add(getMethod);
        MethodInfo setMethod = property.GetSetMethod();
        if (setMethod != null)
          methods2.Add(setMethod);
      }
      foreach (PropertyInfo inheritedProperty in (IEnumerable<PropertyInfo>) inheritedProperties)
      {
        MethodInfo getMethod = inheritedProperty.GetGetMethod();
        if (getMethod == null || MetadataOnlyModule.IncludeInheritedAccessor(getMethod, (IEnumerable<MethodInfo>) methods1, flags))
        {
          MethodInfo setMethod = inheritedProperty.GetSetMethod();
          if (setMethod == null || MetadataOnlyModule.IncludeInheritedAccessor(setMethod, (IEnumerable<MethodInfo>) methods2, flags))
            propertyInfoList.Add(inheritedProperty);
        }
      }
      return (IList<PropertyInfo>) propertyInfoList;
    }

    private static IList<EventInfo> FilterInheritedEvents(
      IList<EventInfo> inheritedEvents,
      IList<EventInfo> events)
    {
      if (events == null || ((ICollection<EventInfo>) events).Count == 0)
        return inheritedEvents;
      List<EventInfo> eventInfoList = new List<EventInfo>();
      foreach (EventInfo inheritedEvent in (IEnumerable<EventInfo>) inheritedEvents)
      {
        bool flag = false;
        foreach (EventInfo eventInfo in (IEnumerable<EventInfo>) events)
        {
          if (((MemberInfo) inheritedEvent).Name.Equals(((MemberInfo) eventInfo).Name, StringComparison.Ordinal))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          eventInfoList.Add(inheritedEvent);
      }
      return (IList<EventInfo>) eventInfoList;
    }

    private static bool IncludeInheritedMethod(
      MethodInfo inheritedMethod,
      IEnumerable<MethodInfo> methods,
      BindingFlags flags)
    {
      return !((MethodBase) inheritedMethod).IsStatic ? !((MethodBase) inheritedMethod).IsVirtual || !MetadataOnlyModule.IsOverride(methods, inheritedMethod) : (flags & BindingFlags.FlattenHierarchy) != 0;
    }

    private static bool IncludeInheritedAccessor(
      MethodInfo inheritedMethod,
      IEnumerable<MethodInfo> methods,
      BindingFlags flags)
    {
      return (!((MethodBase) inheritedMethod).IsStatic || (flags & BindingFlags.FlattenHierarchy) != 0) && !MetadataOnlyModule.IsOverride(methods, inheritedMethod);
    }

    private static bool IncludeInheritedField(FieldInfo inheritedField, BindingFlags flags) => !inheritedField.IsPrivate && (!inheritedField.IsStatic || (flags & BindingFlags.FlattenHierarchy) != 0);

    internal IEnumerable<MethodBase> GetMethodBasesOnDeclaredTypeOnly(
      Token tokenTypeDef,
      GenericContext context,
      MetadataOnlyModule.EMethodKind kind)
    {
      IMetadataImport import = this.RawImport;
      HCORENUM hEnum = new HCORENUM();
      try
      {
        while (true)
        {
          MetadataOnlyModule.EMethodKind emethodKind;
          List<Type> genericParams;
          GenericContext newContext;
          MethodBase methodBase;
          do
          {
            Token token;
            int methodToken;
            int size;
            import.EnumMethods(ref hEnum, ((Token) ref token).Value, out methodToken, 1, out size);
            if (size != 0)
            {
              genericParams = this.GetTypeParameters(methodToken);
              GenericContext genericContext;
              newContext = new GenericContext(genericContext.TypeArgs, genericParams.ToArray());
              methodBase = this.GetGenericMethodBase(new Token(methodToken), newContext);
            }
            else
              goto label_6;
          }
          while (methodBase is ConstructorInfo != (emethodKind == MetadataOnlyModule.EMethodKind.Constructor));
          yield return methodBase;
          genericParams = (List<Type>) null;
          newContext = (GenericContext) null;
          methodBase = (MethodBase) null;
        }
label_6:;
      }
      finally
      {
        hEnum.Close(import);
      }
    }

    private List<Type> GetTypeParameters(int token)
    {
      List<Type> typeParameters = new List<Type>();
      foreach (int genericParameterToken in this.GetGenericParameterTokens(token))
      {
        Token typeVariableToken;
        // ISSUE: explicit constructor call
        ((Token) ref typeVariableToken).\u002Ector(genericParameterToken);
        if (((Token) ref typeVariableToken).IsType((TokenType) 704643072))
          typeParameters.Add((Type) this.Factory.CreateTypeVariable(this, typeVariableToken));
      }
      return typeParameters;
    }

    private static bool MatchSignatures(MethodBase m1, MethodBase methodCandidate)
    {
      if (((MemberInfo) m1).Name != ((MemberInfo) methodCandidate).Name && (((MemberInfo) m1).Name.Length <= ((MemberInfo) methodCandidate).Name.Length || ((MemberInfo) m1).Name[((MemberInfo) m1).Name.Length - ((MemberInfo) methodCandidate).Name.Length - 1] != '.' || !((MemberInfo) m1).Name.EndsWith(((MemberInfo) methodCandidate).Name, StringComparison.Ordinal)) || m1.IsStatic != methodCandidate.IsStatic)
        return false;
      ParameterInfo[] parameters1 = m1.GetParameters();
      ParameterInfo[] parameters2 = methodCandidate.GetParameters();
      if (parameters1.Length != parameters2.Length)
        return false;
      if (m1.IsGenericMethodDefinition)
      {
        Type[] genericArguments = methodCandidate.GetGenericArguments();
        m1 = (MethodBase) (m1 as MethodInfo).MakeGenericMethod(genericArguments);
        parameters1 = m1.GetParameters();
      }
      for (int index = 0; index < parameters1.Length; ++index)
      {
        if (!parameters1[index].ParameterType.Equals(parameters2[index].ParameterType))
          return false;
      }
      MethodInfo methodInfo1 = m1 as MethodInfo;
      MethodInfo methodInfo2 = methodCandidate as MethodInfo;
      return (methodInfo1 == null || methodInfo2 != null) && (methodInfo1 != null || methodInfo2 == null) && (methodInfo1 == null || methodInfo1.ReturnType.Equals(methodInfo2.ReturnType));
    }

    private static bool IsOverride(IEnumerable<MethodInfo> methods, MethodInfo m)
    {
      foreach (MethodInfo method in methods)
      {
        if (MetadataOnlyModule.IsOverride(method, m))
          return true;
      }
      return false;
    }

    private static bool IsOverride(MethodInfo m1, MethodInfo m2) => MetadataOnlyModule.MatchSignatures((MethodBase) m1, (MethodBase) m2);

    internal static FieldInfo[] GetFieldsOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(flags, nameof (GetFieldsOnType));
      List<FieldInfo> fieldInfoList = new List<FieldInfo>();
      foreach (FieldInfo fieldInfo in type.Resolver.GetFieldsOnDeclaredTypeOnly(new Token(((MemberInfo) type).MetadataToken), type.GenericContext))
      {
        if (Utility.IsBindingFlagsMatching(fieldInfo, false, flags))
          fieldInfoList.Add(fieldInfo);
      }
      if (MetadataOnlyModule.WalkInheritanceChain(flags) && type.BaseType != null)
      {
        FieldInfo[] fields = type.BaseType.GetFields(flags);
        List<FieldInfo> collection = new List<FieldInfo>();
        foreach (FieldInfo inheritedField in fields)
        {
          if (MetadataOnlyModule.IncludeInheritedField(inheritedField, flags))
            collection.Add(inheritedField);
        }
        fieldInfoList.AddRange((IEnumerable<FieldInfo>) collection);
      }
      return fieldInfoList.ToArray();
    }

    private IEnumerable<FieldInfo> GetFieldsOnDeclaredTypeOnly(
      Token typeDefToken,
      GenericContext context)
    {
      HCORENUM hEnum = new HCORENUM();
      IMetadataImport import = this.RawImport;
      Type[] typeArgs = Type.EmptyTypes;
      Type[] methodArgs = Type.EmptyTypes;
      GenericContext genericContext;
      if (genericContext != null)
      {
        typeArgs = genericContext.TypeArgs;
        methodArgs = genericContext.MethodArgs;
      }
      try
      {
        while (true)
        {
          Token token;
          int fieldToken;
          uint size;
          import.EnumFields(ref hEnum, Token.op_Implicit(token), out fieldToken, 1, out size);
          if (size != 0U)
          {
            FieldInfo fieldInfo = (FieldInfo) this.Factory.CreateField(this, new Token(fieldToken), typeArgs, methodArgs);
            yield return fieldInfo;
            fieldInfo = (FieldInfo) null;
          }
          else
            break;
        }
      }
      finally
      {
        hEnum.Close(import);
      }
    }

    internal static PropertyInfo[] GetPropertiesOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(flags, nameof (GetPropertiesOnType));
      List<PropertyInfo> properties = new List<PropertyInfo>();
      bool isInherited = false;
      foreach (PropertyInfo declaredProperty in type.GetDeclaredProperties())
      {
        bool isStatic = false;
        bool isPublic = false;
        MetadataOnlyModule.CheckIsStaticAndIsPublicOnProperty(declaredProperty, ref isStatic, ref isPublic);
        if (Utility.IsBindingFlagsMatching((MemberInfo) declaredProperty, isStatic, isPublic, isInherited, flags))
          properties.Add(declaredProperty);
      }
      if (MetadataOnlyModule.WalkInheritanceChain(flags) && type.BaseType != null)
      {
        IList<PropertyInfo> collection = MetadataOnlyModule.FilterInheritedProperties((IList<PropertyInfo>) type.BaseType.GetProperties(flags), (IList<PropertyInfo>) properties, flags);
        properties.AddRange((IEnumerable<PropertyInfo>) collection);
      }
      return properties.ToArray();
    }

    internal IEnumerable<PropertyInfo> GetPropertiesOnDeclaredTypeOnly(
      Token tokenTypeDef,
      GenericContext context)
    {
      HCORENUM hEnum = new HCORENUM();
      IMetadataImport import = this.RawImport;
      try
      {
        while (true)
        {
          Token token;
          int propertyToken;
          uint size;
          import.EnumProperties(ref hEnum, ((Token) ref token).Value, out propertyToken, 1, out size);
          if (size != 0U)
          {
            GenericContext genericContext;
            PropertyInfo property = (PropertyInfo) this.Factory.CreatePropertyInfo(this, new Token(propertyToken), genericContext.TypeArgs, genericContext.MethodArgs);
            yield return property;
            property = (PropertyInfo) null;
          }
          else
            break;
        }
      }
      finally
      {
        hEnum.Close(import);
      }
    }

    internal static EventInfo[] GetEventsOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(flags, nameof (GetEventsOnType));
      List<EventInfo> events = new List<EventInfo>();
      foreach (EventInfo eventInfo in type.Resolver.GetEventsOnDeclaredTypeOnly(new Token(((MemberInfo) type).MetadataToken), type.GenericContext))
      {
        bool isStatic = false;
        bool isPublic = false;
        MetadataOnlyModule.CheckIsStaticAndIsPublicOnEvent(eventInfo, ref isStatic, ref isPublic);
        if (Utility.IsBindingFlagsMatching((MemberInfo) eventInfo, isStatic, isPublic, false, flags))
          events.Add(eventInfo);
      }
      if (MetadataOnlyModule.WalkInheritanceChain(flags) && type.BaseType != null)
      {
        IList<EventInfo> collection = MetadataOnlyModule.FilterInheritedEvents((IList<EventInfo>) type.BaseType.GetEvents(flags), (IList<EventInfo>) events);
        events.AddRange((IEnumerable<EventInfo>) collection);
      }
      return events.ToArray();
    }

    private IEnumerable<EventInfo> GetEventsOnDeclaredTypeOnly(
      Token tokenTypeDef,
      GenericContext context)
    {
      HCORENUM hEnum = new HCORENUM();
      IMetadataImport import = this.RawImport;
      try
      {
        while (true)
        {
          Token token;
          int eventToken;
          uint size;
          import.EnumEvents(ref hEnum, ((Token) ref token).Value, out eventToken, 1, out size);
          if (size != 0U)
          {
            GenericContext genericContext;
            EventInfo eventInfo = (EventInfo) this.Factory.CreateEventInfo(this, new Token(eventToken), genericContext.TypeArgs, genericContext.MethodArgs);
            yield return eventInfo;
            eventInfo = (EventInfo) null;
          }
          else
            break;
        }
      }
      finally
      {
        hEnum.Close(import);
      }
    }

    internal IEnumerable<Type> GetNestedTypesOnType(
      MetadataOnlyCommonType type,
      BindingFlags flags)
    {
      return this.GetNestedTypesOnType(new Token(((MemberInfo) type).MetadataToken), flags);
    }

    private void EnsureNestedTypeCacheExists()
    {
      if (this._nestedTypeInfo != null)
        return;
      this._nestedTypeInfo = new MetadataOnlyModule.NestedTypeCache(this);
    }

    internal IEnumerable<Type> GetNestedTypesOnType(
      Token tokenTypeDef,
      BindingFlags flags)
    {
      BindingFlags bindingFlags;
      MetadataOnlyModule.CheckBindingFlagsInMethod(bindingFlags, nameof (GetNestedTypesOnType));
      this.EnsureNestedTypeCacheExists();
      Token tokenTypeDef1;
      IEnumerable<int> e = this._nestedTypeInfo.GetNestedTokens(tokenTypeDef1);
      if (e != null)
      {
        foreach (int typeToken in e)
        {
          Type type = this.ResolveType(typeToken);
          bool isPublic = type.IsPublic || type.IsNestedPublic;
          if (Utility.IsBindingFlagsMatching((MemberInfo) type, false, isPublic, false, bindingFlags))
            yield return type;
          type = (Type) null;
        }
      }
    }

    public IList<CustomAttributeData> GetCustomAttributeData(
      int memberTokenValue)
    {
      List<CustomAttributeData> customAttributeData1 = new List<CustomAttributeData>();
      HCORENUM phEnum = new HCORENUM();
      IMetadataImport rawImport = this.RawImport;
      try
      {
        while (true)
        {
          Token mdCustomAttribute;
          uint pcTokens;
          rawImport.EnumCustomAttributes(ref phEnum, memberTokenValue, 0, out mdCustomAttribute, 1U, out pcTokens);
          if (pcTokens != 0U)
          {
            Token tkType;
            rawImport.GetCustomAttributeProps(mdCustomAttribute, out Token _, out tkType, out EmbeddedBlobPointer _, out int _);
            ConstructorInfo ctor = this.ResolveCustomAttributeConstructor(tkType);
            CustomAttributeData customAttributeData2 = (CustomAttributeData) new MetadataOnlyCustomAttributeData(this, mdCustomAttribute, ctor);
            customAttributeData1.Add(customAttributeData2);
          }
          else
            break;
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
      IEnumerable<CustomAttributeData> customAttributes = this.Policy.GetPseudoCustomAttributes(this, new Token(memberTokenValue));
      customAttributeData1.AddRange(customAttributes);
      return (IList<CustomAttributeData>) customAttributeData1;
    }

    private ConstructorInfo ResolveCustomAttributeConstructor(
      Token customAttributeConstructorTokenValue)
    {
      Token token = customAttributeConstructorTokenValue;
      this.EnsureValidToken(token);
      if (((Token) ref token).IsType((TokenType) 100663296))
        return (ConstructorInfo) this.ResolveMethodDef(token);
      if (!((Token) ref token).IsType((TokenType) 167772160))
        throw new ArgumentException(Resources.MethodTokenExpected);
      Token declaringTypeToken;
      this.GetMemberRefData(token, out declaringTypeToken, out string _, out SignatureBlob _);
      return (ConstructorInfo) new ConstructorInfoRef(this.ResolveTypeTokenInternal(declaringTypeToken, (GenericContext) null), this, token);
    }

    internal void LazyAttributeParse(
      Token token,
      ConstructorInfo constructorInfo,
      out IList<CustomAttributeTypedArgument> constructorArguments,
      out IList<CustomAttributeNamedArgument> namedArguments)
    {
      EmbeddedBlobPointer blob;
      int cbSize;
      this.RawImport.GetCustomAttributeProps(token, out Token _, out Token _, out blob, out cbSize);
      byte[] customAttributeBlob = this.RawMetadata.ReadEmbeddedBlob(blob, cbSize);
      int startIndex = 0;
      if (BitConverter.ToInt16(customAttributeBlob, startIndex) != (short) 1)
        throw new ArgumentException(Resources.InvalidCustomAttributeFormat);
      int index = startIndex + 2;
      constructorArguments = this.GetConstructorArguments(constructorInfo, customAttributeBlob, ref index);
      namedArguments = this.GetNamedArguments(constructorInfo, customAttributeBlob, ref index);
    }

    private IList<CustomAttributeTypedArgument> GetConstructorArguments(
      ConstructorInfo constructorInfo,
      byte[] customAttributeBlob,
      ref int index)
    {
      ParameterInfo[] parameters = ((MethodBase) constructorInfo).GetParameters();
      IList<CustomAttributeTypedArgument> constructorArguments = (IList<CustomAttributeTypedArgument>) new List<CustomAttributeTypedArgument>(parameters.Length);
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        Type parameterType = parameters[index1].ParameterType;
        CorElementType typeId = SignatureUtil.GetTypeId(parameterType);
        Type argumentType = (Type) null;
        object attributeArgumentValue;
        if (typeId != 28)
        {
          attributeArgumentValue = this.GetCustomAttributeArgumentValue(typeId, parameterType, customAttributeBlob, ref index);
          argumentType = parameterType;
        }
        else
        {
          CorElementType argumentTypeId;
          SignatureUtil.ExtractCustomAttributeArgumentType(this.AssemblyResolver, (Module) this, customAttributeBlob, ref index, out argumentTypeId, out argumentType);
          attributeArgumentValue = this.GetCustomAttributeArgumentValue(argumentTypeId, argumentType, customAttributeBlob, ref index);
        }
        CustomAttributeTypedArgument attributeTypedArgument;
        // ISSUE: explicit constructor call
        ((CustomAttributeTypedArgument) ref attributeTypedArgument).\u002Ector(argumentType, attributeArgumentValue);
        ((ICollection<CustomAttributeTypedArgument>) constructorArguments).Add(attributeTypedArgument);
      }
      return constructorArguments;
    }

    private IList<CustomAttributeNamedArgument> GetNamedArguments(
      ConstructorInfo constructorInfo,
      byte[] customAttributeBlob,
      ref int index)
    {
      ushort uint16 = BitConverter.ToUInt16(customAttributeBlob, index);
      index += 2;
      IList<CustomAttributeNamedArgument> namedArguments = (IList<CustomAttributeNamedArgument>) new List<CustomAttributeNamedArgument>((int) uint16);
      if (uint16 == (ushort) 0 && index != customAttributeBlob.Length)
        throw new ArgumentException(Resources.InvalidCustomAttributeFormat);
      for (int index1 = 0; index1 < (int) uint16; ++index1)
      {
        NamedArgumentType namedArgumentType = SignatureUtil.ExtractNamedArgumentType(customAttributeBlob, ref index);
        CorElementType argumentTypeId;
        Type argumentType;
        SignatureUtil.ExtractCustomAttributeArgumentType(this.AssemblyResolver, (Module) this, customAttributeBlob, ref index, out argumentTypeId, out argumentType);
        string stringValue = SignatureUtil.ExtractStringValue(customAttributeBlob, ref index);
        if (argumentType == null)
          SignatureUtil.ExtractCustomAttributeArgumentType(this.AssemblyResolver, (Module) this, customAttributeBlob, ref index, out argumentTypeId, out argumentType);
        object attributeArgumentValue = this.GetCustomAttributeArgumentValue(argumentTypeId, argumentType, customAttributeBlob, ref index);
        MemberInfo memberInfo = namedArgumentType != NamedArgumentType.Field ? (MemberInfo) ((MemberInfo) constructorInfo).DeclaringType.GetProperty(stringValue) : (MemberInfo) ((MemberInfo) constructorInfo).DeclaringType.GetField(stringValue, BindingFlags.Instance | BindingFlags.Public);
        CustomAttributeTypedArgument attributeTypedArgument;
        // ISSUE: explicit constructor call
        ((CustomAttributeTypedArgument) ref attributeTypedArgument).\u002Ector(argumentType, attributeArgumentValue);
        CustomAttributeNamedArgument attributeNamedArgument;
        // ISSUE: explicit constructor call
        ((CustomAttributeNamedArgument) ref attributeNamedArgument).\u002Ector(memberInfo, attributeTypedArgument);
        ((ICollection<CustomAttributeNamedArgument>) namedArguments).Add(attributeNamedArgument);
      }
      if (index != customAttributeBlob.Length)
        throw new ArgumentException(Resources.InvalidCustomAttributeFormat);
      return namedArguments;
    }

    private object GetCustomAttributeArgumentValue(
      CorElementType typeId,
      Type type,
      byte[] customAttributeBlob,
      ref int index)
    {
      object attributeArgumentValue = (object) null;
      CorElementType corElementType = typeId;
      if (corElementType != 29)
      {
        attributeArgumentValue = corElementType == 80 ? (object) SignatureUtil.ExtractTypeValue(this.AssemblyResolver, (Module) this, customAttributeBlob, ref index) : (corElementType == 85 ? SignatureUtil.ExtractValue(SignatureUtil.GetTypeId(MetadataOnlyModule.GetUnderlyingType(type)), customAttributeBlob, ref index) : SignatureUtil.ExtractValue(typeId, customAttributeBlob, ref index));
      }
      else
      {
        uint uintValue = SignatureUtil.ExtractUIntValue(customAttributeBlob, ref index);
        if (uintValue != uint.MaxValue)
          attributeArgumentValue = (object) SignatureUtil.ExtractListOfValues(type.GetElementType(), this.AssemblyResolver, (Module) this, uintValue, customAttributeBlob, ref index);
      }
      return attributeArgumentValue;
    }

    internal static Type GetUnderlyingType(Type enumType) => enumType.GetFields(BindingFlags.Instance | BindingFlags.Public)[0].FieldType;

    internal Type GetEnclosingType(Token tokenTypeDef)
    {
      Token token;
      // ISSUE: explicit constructor call
      ((Token) ref token).\u002Ector(Token.op_Implicit(this.GetNestedClassProps(tokenTypeDef)));
      return ((Token) ref token).IsNil ? (Type) null : this.ResolveTypeTokenInternal(token, (GenericContext) null);
    }

    public AssemblyName GetAssemblyNameFromAssemblyRef(Token assemblyRefToken)
    {
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) this.RawImport;
      return AssemblyNameHelper.GetAssemblyNameFromRef(assemblyRefToken, this, rawImport);
    }

    internal Token GetNestedClassProps(Token tokenTypeDef)
    {
      int tdEnclosingClass;
      int nestedClassProps = this.RawImport.GetNestedClassProps(Token.op_Implicit(tokenTypeDef), out tdEnclosingClass);
      switch (nestedClassProps)
      {
        case -2146234064:
          return new Token(0);
        case 0:
          return new Token(tdEnclosingClass);
        default:
          throw Marshal.GetExceptionForHR(nestedClassProps);
      }
    }

    internal int CountGenericParams(Token token)
    {
      if (!(this.RawImport is IMetadataImport2 rawImport))
        return 0;
      HCORENUM hEnum = new HCORENUM();
      rawImport.EnumGenericParams(ref hEnum, ((Token) ref token).Value, out int _, 1U, out uint _);
      int pulCount;
      try
      {
        rawImport.CountEnum(hEnum, out pulCount);
      }
      finally
      {
        hEnum.Close(rawImport);
      }
      return pulCount;
    }

    internal IEnumerable<int> GetGenericParameterTokens(int typeOrMethodToken)
    {
      Token token = new Token(typeOrMethodToken);
      if (this.RawImport is IMetadataImport2 importer2)
      {
        HCORENUM hEnum = new HCORENUM();
        try
        {
          while (true)
          {
            int mdGenericParam;
            uint count;
            importer2.EnumGenericParams(ref hEnum, typeOrMethodToken, out mdGenericParam, 1U, out count);
            if (count == 1U)
              yield return mdGenericParam;
            else
              break;
          }
        }
        finally
        {
          hEnum.Close(importer2);
        }
      }
    }

    internal IEnumerable<Type> GetConstraintTypes(int gpToken)
    {
      int tk;
      Token token = new Token(tk);
      if (this.RawImport is IMetadataImport2 importer2)
      {
        HCORENUM hEnum = new HCORENUM();
        try
        {
          while (true)
          {
            int mdGenericConstraint;
            uint count;
            importer2.EnumGenericParamConstraints(ref hEnum, tk, out mdGenericConstraint, 1U, out count);
            if (count == 1U)
            {
              int constraintTypeToken;
              importer2.GetGenericParamConstraintProps(mdGenericConstraint, out int _, out constraintTypeToken);
              yield return this.ResolveTypeTokenInternal(new Token(constraintTypeToken), (GenericContext) null);
            }
            else
              break;
          }
        }
        finally
        {
          hEnum.Close(importer2);
        }
      }
    }

    internal void GetGenericParameterProps(
      int mdGenericParam,
      out int ownerTypeToken,
      out int ownerMethodToken,
      out string name,
      out GenericParameterAttributes attributes,
      out uint genIndex)
    {
      IMetadataImport2 rawImport = this.RawImport as IMetadataImport2;
      HCORENUM hcorenum = new HCORENUM();
      try
      {
        int pdwParamFlags;
        int ptOwner;
        int ptkKind;
        uint pchName;
        rawImport.GetGenericParamProps(mdGenericParam, out genIndex, out pdwParamFlags, out ptOwner, out ptkKind, (StringBuilder) null, 0U, out pchName);
        attributes = (GenericParameterAttributes) pdwParamFlags;
        StringBuilder builder = StringBuilderPool.Get((int) pchName);
        rawImport.GetGenericParamProps(mdGenericParam, out genIndex, out pdwParamFlags, out ptOwner, out ptkKind, builder, (uint) builder.Capacity, out pchName);
        name = builder.ToString();
        StringBuilderPool.Release(ref builder);
        Token token;
        // ISSUE: explicit constructor call
        ((Token) ref token).\u002Ector(ptOwner);
        if (((Token) ref token).IsType((TokenType) 100663296))
        {
          ownerMethodToken = ptOwner;
          ownerTypeToken = 0;
        }
        else
        {
          ownerTypeToken = ptOwner;
          ownerMethodToken = 0;
        }
      }
      finally
      {
        hcorenum.Close(rawImport);
      }
    }

    internal IEnumerable<Type> GetInterfacesOnType(Type type)
    {
      IMetadataImport import = this.RawImport;
      Type type1;
      if (type1.IsGenericParameter)
      {
        foreach (Type c in this.GetConstraintTypes(((MemberInfo) type1).MetadataToken))
        {
          if (c.IsInterface)
            yield return c;
        }
      }
      else
      {
        HCORENUM hEnum = new HCORENUM();
        int cImpls = 1;
        while (true)
        {
          int rImpls;
          import.EnumInterfaceImpls(ref hEnum, ((MemberInfo) type1).MetadataToken, out rImpls, 1, ref cImpls);
          if (cImpls == 1)
          {
            Token tImpl = new Token(rImpls);
            int cls;
            int iface;
            import.GetInterfaceImplProps(((Token) ref tImpl).Value, out cls, out iface);
            Token tkClass = new Token(cls);
            Token tkInterface = new Token(iface);
            Type result = this.ResolveTypeTokenInternal(tkInterface, new GenericContext(type1.GetGenericArguments(), (Type[]) null));
            yield return result;
            tImpl = new Token();
            tkClass = new Token();
            tkInterface = new Token();
            result = (Type) null;
          }
          else
            break;
        }
        hEnum.Close(import);
      }
    }

    public static Type GetInterfaceHelper(Type[] interfaces, string name, bool ignoreCase)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      Type interfaceHelper = (Type) null;
      foreach (Type type in interfaces)
      {
        if (Utility.Compare(name, ((MemberInfo) type).Name, ignoreCase))
          interfaceHelper = interfaceHelper == null ? type : throw new AmbiguousMatchException();
      }
      return interfaceHelper;
    }

    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
    public IEnumerable<Type> GetTypeList()
    {
      foreach (int typeToken in this.GetTypeTokenList())
      {
        Type result = this.ResolveTypeTokenInternal(new Token(typeToken), (GenericContext) null);
        yield return result;
        result = (Type) null;
      }
    }

    private IEnumerable<int> GetTypeTokenList()
    {
      IMetadataImport import = this.RawImport;
      HCORENUM hEnum = new HCORENUM();
      try
      {
        uint count = 1;
        while (true)
        {
          int rTypeDefs;
          import.EnumTypeDefs(ref hEnum, out rTypeDefs, 1U, out count);
          if (count == 1U)
            yield return rTypeDefs;
          else
            break;
        }
      }
      finally
      {
        hEnum.Close(import);
      }
    }

    private static void CheckBindingFlagsInMethod(BindingFlags flags, string methodName)
    {
      if ((flags | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.ExactBinding) != (BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.ExactBinding))
        throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.MethodIsUsingUnsupportedBindingFlags, new object[2]
        {
          (object) methodName,
          (object) flags
        }));
    }

    private static void CheckIsStaticAndIsPublicOnProperty(
      PropertyInfo propertyInfo,
      ref bool isStatic,
      ref bool isPublic)
    {
      bool flag = true;
      MetadataOnlyModule.CheckIsStaticAndIsPublic(propertyInfo.GetGetMethod(flag), ref isStatic, ref isPublic);
      MetadataOnlyModule.CheckIsStaticAndIsPublic(propertyInfo.GetSetMethod(flag), ref isStatic, ref isPublic);
    }

    private static void CheckIsStaticAndIsPublicOnEvent(
      EventInfo eventInfo,
      ref bool isStatic,
      ref bool isPublic)
    {
      bool flag = true;
      MetadataOnlyModule.CheckIsStaticAndIsPublic(eventInfo.GetAddMethod(flag), ref isStatic, ref isPublic);
      MetadataOnlyModule.CheckIsStaticAndIsPublic(eventInfo.GetRemoveMethod(flag), ref isStatic, ref isPublic);
      MetadataOnlyModule.CheckIsStaticAndIsPublic(eventInfo.GetRaiseMethod(flag), ref isStatic, ref isPublic);
    }

    private static void CheckIsStaticAndIsPublic(
      MethodInfo methodInfo,
      ref bool isStatic,
      ref bool isPublic)
    {
      if (methodInfo == null)
        return;
      if (((MethodBase) methodInfo).IsStatic)
        isStatic = true;
      if (!((MethodBase) methodInfo).IsPublic)
        return;
      isPublic = true;
    }

    internal void SetContainingAssembly(Assembly assembly) => this._assembly = assembly;

    public virtual Assembly Assembly => this._assembly;

    public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      if (ignoreCase)
        throw new NotImplementedException(Resources.CaseInsensitiveTypeLookupNotImplemented);
      if (className == null)
        throw new ArgumentNullException(nameof (className));
      if (TypeNameParser.IsCompoundType(className))
        return TypeNameParser.ParseTypeName(this.AssemblyResolver, (Module) this, className, throwOnError);
      Token typeDefByName = this.FindTypeDefByName((Type) null, className, false);
      if (!((Token) ref typeDefByName).IsNil)
        return this.ResolveType(((Token) ref typeDefByName).Value);
      if (throwOnError)
        throw new TypeLoadException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CannotFindTypeInModule, new object[2]
        {
          (object) className,
          (object) ((object) this).ToString()
        }));
      return (Type) null;
    }

    public virtual Type[] GetTypes() => new List<Type>(this.GetTypeList()).ToArray();

    public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
    {
      List<Type> typeList = new List<Type>();
      foreach (Type type in this.GetTypeList())
      {
        if (filter.Invoke(type, filterCriteria))
          typeList.Add(type);
      }
      return typeList.ToArray();
    }

    public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      foreach (FieldInfo field in base.GetFields(bindingAttr))
      {
        if (((MemberInfo) field).Name.Equals(name))
          return field;
      }
      return (FieldInfo) null;
    }

    public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(bindingFlags, nameof (GetFields));
      IMetadataImport rawImport = this.RawImport;
      HCORENUM phEnum = new HCORENUM();
      List<FieldInfo> fieldInfoList = new List<FieldInfo>();
      try
      {
        uint pcTokens = 1;
        while (true)
        {
          int mdFieldDef;
          rawImport.EnumFields(ref phEnum, base.MetadataToken, out mdFieldDef, 1, out pcTokens);
          if (pcTokens == 1U)
          {
            FieldInfo fieldInfo = this.ResolveField(mdFieldDef);
            if (Utility.IsBindingFlagsMatching(fieldInfo, false, bindingFlags))
              fieldInfoList.Add(fieldInfo);
          }
          else
            break;
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
      return fieldInfoList.ToArray();
    }

    protected virtual MethodInfo GetMethodImpl(
      string name,
      BindingFlags bindingAttr,
      Binder binder,
      CallingConventions callConvention,
      Type[] types,
      ParameterModifier[] modifiers)
    {
      MetadataOnlyModule.CheckBinderAndModifiersforLMR(binder, modifiers);
      return MetadataOnlyModule.FilterMethod(base.GetMethods(bindingAttr), name, bindingAttr, callConvention, types);
    }

    public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      MetadataOnlyModule.CheckBindingFlagsInMethod(bindingFlags, nameof (GetMethods));
      IMetadataImport rawImport = this.RawImport;
      HCORENUM phEnum = new HCORENUM();
      List<MethodInfo> methodInfoList = new List<MethodInfo>();
      try
      {
        int pcTokens = 1;
        while (true)
        {
          int mdMethodDef;
          rawImport.EnumMethods(ref phEnum, base.MetadataToken, out mdMethodDef, 1, out pcTokens);
          if (pcTokens == 1)
          {
            MethodBase method = this.ResolveMethodTokenInternal(new Token(mdMethodDef), (GenericContext) null);
            if (Utility.IsBindingFlagsMatching(method, false, bindingFlags) && method is MethodInfo methodInfo)
              methodInfoList.Add(methodInfo);
          }
          else
            break;
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
      return methodInfoList.ToArray();
    }

    public virtual int MetadataToken
    {
      get
      {
        int mdModule;
        this.RawImport.GetModuleFromScope(out mdModule);
        return mdModule;
      }
    }

    public virtual bool IsResource() => false;

    public virtual Type ResolveType(
      int metadataToken,
      Type[] genericTypeArguments,
      Type[] genericMethodArguments)
    {
      Type type = this.ResolveTypeTokenInternal(new Token(metadataToken), new GenericContext(genericTypeArguments, genericMethodArguments));
      Helpers.EnsureResolve(type);
      return type;
    }

    public virtual FieldInfo ResolveField(
      int metadataToken,
      Type[] genericTypeArguments,
      Type[] genericMethodArguments)
    {
      return this.ResolveFieldTokenInternal(new Token(metadataToken), new GenericContext(genericTypeArguments, genericMethodArguments));
    }

    public virtual MethodBase ResolveMethod(
      int metadataToken,
      Type[] genericTypeArguments,
      Type[] genericMethodArguments)
    {
      return this.ResolveMethodTokenInternal(new Token(metadataToken), new GenericContext(genericTypeArguments, genericMethodArguments));
    }

    public virtual MemberInfo ResolveMember(
      int metadataToken,
      Type[] genericTypeArguments,
      Type[] genericMethodArguments)
    {
      throw new NotImplementedException();
    }

    public virtual byte[] ResolveSignature(int metadataToken) => throw new NotImplementedException();

    internal bool IsSystemModule() => ((object) this.AssemblyResolver.GetSystemAssembly()).Equals((object) base.Assembly);

    internal TypeCode GetTypeCode(Type type)
    {
      if (type.IsEnum)
      {
        type = MetadataOnlyModule.GetUnderlyingType(type);
        return Type.GetTypeCode(type);
      }
      if (!this.IsSystemModule())
        return TypeCode.Object;
      Token token;
      // ISSUE: explicit constructor call
      ((Token) ref token).\u002Ector(((MemberInfo) type).MetadataToken);
      if (this._typeCodeMapping == null)
        this._typeCodeMapping = this.CreateTypeCodeMapping();
      for (int typeCode = 0; typeCode < this._typeCodeMapping.Length; ++typeCode)
      {
        if (Token.op_Equality(token, this._typeCodeMapping[typeCode]))
          return (TypeCode) typeCode;
      }
      return TypeCode.Object;
    }

    private Token[] CreateTypeCodeMapping() => new Token[19]
    {
      null,
      this.LookupTypeToken("System.Object"),
      this.LookupTypeToken("System.DBNull"),
      this.LookupTypeToken("System.Boolean"),
      this.LookupTypeToken("System.Char"),
      this.LookupTypeToken("System.SByte"),
      this.LookupTypeToken("System.Byte"),
      this.LookupTypeToken("System.Int16"),
      this.LookupTypeToken("System.UInt16"),
      this.LookupTypeToken("System.Int32"),
      this.LookupTypeToken("System.UInt32"),
      this.LookupTypeToken("System.Int64"),
      this.LookupTypeToken("System.UInt64"),
      this.LookupTypeToken("System.Single"),
      this.LookupTypeToken("System.Double"),
      this.LookupTypeToken("System.Decimal"),
      this.LookupTypeToken("System.DateTime"),
      null,
      this.LookupTypeToken("System.String")
    };

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      foreach (object o in this._cachedThreadAffinityImporter.Values)
        Marshal.ReleaseComObject(o);
      this._cachedThreadAffinityImporter.Clear();
      if (this.RawMetadata != null)
        this.RawMetadata.Dispose();
    }

    public int RowCount(MetadataTable metadataTableIndex)
    {
      int num;
      int countRows;
      ((IMetadataTables) this.RawImport).GetTableInfo(metadataTableIndex, out num, out countRows, out num, out num, out UnusedIntPtr _);
      return countRows;
    }

    public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine) => ((IMetadataImport2) this.RawImport).GetPEKind(out peKind, out machine);

    public virtual int MDStreamVersion => throw new NotImplementedException();

    internal enum EMethodKind
    {
      Constructor,
      Methods,
    }

    private class NestedTypeCache
    {
      private readonly Dictionary<int, List<int>> _cache;

      public NestedTypeCache(MetadataOnlyModule outer)
      {
        this._cache = new Dictionary<int, List<int>>();
        foreach (int typeToken in outer.GetTypeTokenList())
        {
          int key = Token.op_Implicit(outer.GetNestedClassProps(new Token(typeToken)));
          if (key != 0)
          {
            if (this._cache.ContainsKey(key))
              this._cache[key].Add(typeToken);
            else
              this._cache.Add(key, new List<int>()
              {
                typeToken
              });
          }
        }
      }

      public IEnumerable<int> GetNestedTokens(Token tokenTypeDef)
      {
        List<int> intList;
        return this._cache.TryGetValue(Token.op_Implicit(tokenTypeDef), out intList) ? (IEnumerable<int>) intList : (IEnumerable<int>) null;
      }
    }
  }
}
