// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyMethodBody
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyMethodBody : MethodBody
  {
    protected MetadataOnlyMethodBody(MetadataOnlyMethodInfo method) => this.Method = method;

    internal static MethodBody TryCreate(MetadataOnlyMethodInfo method)
    {
      MetadataOnlyModule resolver = method.Resolver;
      MethodBody body = (MethodBody) null;
      return resolver.Factory.TryCreateMethodBody(method, ref body) ? body : MetadataOnlyMethodBodyWorker.Create(method);
    }

    protected MetadataOnlyMethodInfo Method { get; }

    public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses => throw new NotImplementedException();

    public virtual bool InitLocals => throw new InvalidOperationException();

    public virtual int LocalSignatureMetadataToken => throw new InvalidOperationException();

    public virtual IList<LocalVariableInfo> LocalVariables
    {
      get
      {
        Token token;
        ((Token) ref token).\u002Ector(base.LocalSignatureMetadataToken);
        EmbeddedBlobPointer pSig = new EmbeddedBlobPointer();
        int cbSig = 0;
        if (!((Token) ref token).IsNil)
          this.Method.Resolver.RawImport.GetSigFromToken(Token.op_Implicit(token), out pSig, out cbSig);
        if (cbSig == 0)
          return (IList<LocalVariableInfo>) new MetadataOnlyLocalVariableInfo[0];
        GenericContext context = new GenericContext((MethodBase) this.Method);
        byte[] sig = this.Method.Resolver.ReadEmbeddedBlob(pSig, cbSig);
        int index1 = 0;
        SignatureUtil.ExtractCallingConvention(sig, ref index1);
        int length = SignatureUtil.ExtractInt(sig, ref index1);
        MetadataOnlyLocalVariableInfo[] localVariables = new MetadataOnlyLocalVariableInfo[length];
        for (int index2 = 0; index2 < length; ++index2)
        {
          TypeSignatureDescriptor type = SignatureUtil.ExtractType(sig, ref index1, this.Method.Resolver, context, true);
          localVariables[index2] = new MetadataOnlyLocalVariableInfo(index2, type.Type, type.IsPinned);
        }
        return (IList<LocalVariableInfo>) localVariables;
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
    public virtual int MaxStackSize => throw new InvalidOperationException();

    public virtual byte[] GetILAsByteArray() => throw new InvalidOperationException();
  }
}
