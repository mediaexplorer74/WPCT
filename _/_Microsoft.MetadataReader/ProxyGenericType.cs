// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ProxyGenericType
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class ProxyGenericType : TypeProxy
  {
    private readonly TypeProxy _rawType;
    private readonly Type[] _args;

    public ProxyGenericType(TypeProxy rawType, Type[] args)
      : base(rawType.Resolver)
    {
      this._rawType = rawType;
      this._args = args;
    }

    protected override Type GetResolvedTypeWorker() => this._rawType.GetResolvedType().MakeGenericType(this._args);

    public override Type[] GetGenericArguments() => (Type[]) this._args.Clone();

    public override Type GetGenericTypeDefinition() => (Type) this._rawType;

    public override string Name => ((MemberInfo) this._rawType).Name;

    public override string Namespace => ((Type) this._rawType).Namespace;

    protected override bool IsPointerImpl() => false;

    protected override bool IsArrayImpl() => false;

    protected override bool IsByRefImpl() => false;

    public override Type DeclaringType => ((MemberInfo) this._rawType).DeclaringType;

    public override bool IsGenericParameter => false;

    public override bool IsGenericType => true;

    public override bool IsEnum => ((Type) this._rawType).IsEnum;

    protected override bool IsValueTypeImpl() => this._rawType.IsValueType;

    public override Module Module => ((MemberInfo) this._rawType).Module;
  }
}
