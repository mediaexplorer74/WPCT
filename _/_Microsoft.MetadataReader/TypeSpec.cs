// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.TypeSpec
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Diagnostics;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("{TypeSpecToken}")]
  internal class TypeSpec : TypeProxy, ITypeSpec, ITypeSignatureBlob, ITypeProxy
  {
    private readonly GenericContext _context;

    public TypeSpec(
      MetadataOnlyModule module,
      Token typeSpecToken,
      Type[] typeArgs,
      Type[] methodArgs)
      : base(module)
    {
      this.TypeSpecToken = typeSpecToken;
      this._context = new GenericContext(typeArgs, methodArgs);
    }

    public Token TypeSpecToken { get; }

    public byte[] Blob
    {
      get
      {
        EmbeddedBlobPointer pSig;
        int cbSig;
        this.m_resolver.RawImport.GetTypeSpecFromToken(this.TypeSpecToken, out pSig, out cbSig);
        return this.m_resolver.ReadEmbeddedBlob(pSig, cbSig);
      }
    }

    public Module DeclaringScope => (Module) this.Resolver;

    protected override Type GetResolvedTypeWorker()
    {
      byte[] blob = this.Blob;
      int index = 0;
      return SignatureUtil.ExtractType(blob, ref index, this.Resolver, this._context);
    }
  }
}
