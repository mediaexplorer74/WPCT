// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyTypeReference
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("\\{Name = {Name} FullName = {FullName} {TypeRefToken}\\}")]
  internal class MetadataOnlyTypeReference : TypeProxy, ITypeReference, ITypeProxy
  {
    public MetadataOnlyTypeReference(MetadataOnlyModule resolver, Token typeRef)
      : base(resolver)
    {
      this.TypeRefToken = typeRef;
    }

    protected override Type GetResolvedTypeWorker() => this.m_resolver.ResolveTypeRef((ITypeReference) this);

    public Module DeclaringScope => (Module) this.m_resolver;

    public Token TypeRefToken { get; }

    public Token ResolutionScope
    {
      get
      {
        IMetadataImport rawImport = this.m_resolver.RawImport;
        Token typeRefToken = this.TypeRefToken;
        int tr = ((Token) ref typeRefToken).Value;
        int num1;
        ref int local1 = ref num1;
        int num2;
        ref int local2 = ref num2;
        rawImport.GetTypeRefProps(tr, out local1, (StringBuilder) null, 0, out local2);
        Token resolutionScope;
        ((Token) ref resolutionScope).\u002Ector(num1);
        return resolutionScope;
      }
    }

    public virtual string RawName
    {
      get
      {
        Token typeRefToken = this.TypeRefToken;
        int tr = ((Token) ref typeRefToken).Value;
        int ptkResolutionScope;
        int pchName;
        this.m_resolver.RawImport.GetTypeRefProps(tr, out ptkResolutionScope, (StringBuilder) null, 0, out pchName);
        StringBuilder builder = StringBuilderPool.Get(pchName);
        this.m_resolver.RawImport.GetTypeRefProps(tr, out ptkResolutionScope, builder, builder.Capacity, out pchName);
        string rawName = builder.ToString();
        StringBuilderPool.Release(ref builder);
        return rawName;
      }
    }

    public override string Namespace => ((MemberInfo) this).DeclaringType != null ? ((MemberInfo) this).DeclaringType.Namespace : Utility.GetNamespaceHelper(base.FullName);

    public override string Name => Utility.GetTypeNameFromFullNameHelper(base.FullName, this.IsNested);

    public override string FullName
    {
      get
      {
        Token typeRefToken = this.TypeRefToken;
        int tr = ((Token) ref typeRefToken).Value;
        string empty = string.Empty;
        string str = string.Empty;
        StringBuilder builder;
        while (true)
        {
          int ptkResolutionScope;
          int pchName;
          this.m_resolver.RawImport.GetTypeRefProps(tr, out ptkResolutionScope, (StringBuilder) null, 0, out pchName);
          builder = StringBuilderPool.Get(pchName);
          Token token;
          ((Token) ref token).\u002Ector(ptkResolutionScope);
          this.m_resolver.RawImport.GetTypeRefProps(tr, out ptkResolutionScope, builder, builder.Capacity, out pchName);
          if (((Token) ref token).IsType((TokenType) 16777216))
          {
            str = "+" + (object) builder + str;
            tr = ((Token) ref token).Value;
          }
          else
            break;
        }
        builder.Append(str);
        string fullName = builder.ToString();
        StringBuilderPool.Release(ref builder);
        return fullName;
      }
    }

    private AssemblyName RequestedAssemblyName
    {
      get
      {
        Token resolutionScope = this.ResolutionScope;
        TokenType tokenType = ((Token) ref resolutionScope).TokenType;
        if (tokenType <= 16777216)
        {
          if (tokenType != null)
          {
            if (tokenType == 16777216)
              return new MetadataOnlyTypeReference(this.m_resolver, resolutionScope).RequestedAssemblyName;
            goto label_8;
          }
        }
        else if (tokenType != 436207616)
        {
          if (tokenType == 587202560)
            return this.m_resolver.GetAssemblyNameFromAssemblyRef(resolutionScope);
          goto label_8;
        }
        return ((Module) this.m_resolver).Assembly.GetName();
label_8:
        throw new InvalidOperationException(Resources.InvalidMetadata);
      }
    }

    public override Assembly Assembly => (Assembly) new AssemblyRef(this.RequestedAssemblyName, this.TypeUniverse);

    public override string AssemblyQualifiedName => Assembly.CreateQualifiedName(this.RequestedAssemblyName.ToString(), base.FullName);

    protected override bool IsByRefImpl() => false;

    protected override bool IsArrayImpl() => false;

    public override bool IsGenericParameter => false;

    protected override bool IsPointerImpl() => false;

    public override Type DeclaringType
    {
      get
      {
        Token typeRefToken = this.TypeRefToken;
        int ptkResolutionScope;
        this.m_resolver.RawImport.GetTypeRefProps(((Token) ref typeRefToken).Value, out ptkResolutionScope, (StringBuilder) null, 0, out int _);
        Token tokenTypeRef;
        ((Token) ref tokenTypeRef).\u002Ector(ptkResolutionScope);
        return ((Token) ref tokenTypeRef).IsType((TokenType) 16777216) ? this.m_resolver.Factory.CreateTypeRef(this.m_resolver, tokenTypeRef) : (Type) null;
      }
    }
  }
}
