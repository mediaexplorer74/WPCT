// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.DefaultUniverse
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class DefaultUniverse : SimpleUniverse
  {
    public DefaultUniverse() => this.Loader = new Loader((IMutableTypeUniverse) this);

    public virtual Module ResolveModule(Assembly containingAssembly, string moduleName) => this.Loader.ResolveModule(containingAssembly, moduleName);

    public Loader Loader { get; }

    internal Assembly LoadAssemblyFromFile(
      string manifestFileName,
      string[] netModuleFileNames)
    {
      return this.Loader.LoadAssemblyFromFile(manifestFileName, netModuleFileNames);
    }

    internal Assembly LoadAssemblyFromFile(string manifestFileName) => this.Loader.LoadAssemblyFromFile(manifestFileName);

    internal MetadataOnlyModule LoadModuleFromFile(string netModulePath) => this.Loader.LoadModuleFromFile(netModulePath);

    internal Assembly LoadAssemblyFromByteArray(byte[] data) => this.Loader.LoadAssemblyFromByteArray(data);
  }
}
