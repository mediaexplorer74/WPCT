// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.AssemblyRef
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("AssemblyRef: {m_name}")]
  internal class AssemblyRef : AssemblyProxy
  {
    private readonly AssemblyName _name;

    public AssemblyRef(AssemblyName name, ITypeUniverse universe)
      : base(universe)
    {
      this._name = name;
    }

    protected virtual Assembly GetResolvedAssemblyWorker() => this.TypeUniverse.ResolveAssembly(this._name);

    protected virtual AssemblyName GetNameWithNoResolution() => this._name;

    public virtual AssemblyName GetName() => this._name;
  }
}
