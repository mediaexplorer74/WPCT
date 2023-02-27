// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ConstructorInfoRef
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class ConstructorInfoRef : ConstructorInfoProxy
  {
    private readonly Token _token;
    private readonly MetadataOnlyModule _scope;

    public ConstructorInfoRef(Type declaringType, MetadataOnlyModule scope, Token token)
    {
      this.DeclaringType = declaringType;
      this._token = token;
      this._scope = scope;
    }

    protected virtual ConstructorInfo GetResolvedWorker() => (ConstructorInfo) this._scope.ResolveMethod(Token.op_Implicit(this._token));

    public virtual Type DeclaringType { get; }
  }
}
