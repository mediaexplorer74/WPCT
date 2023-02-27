// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.Loader
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using Microsoft.Tools.IO;
using System;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class Loader
  {
    private readonly IMutableTypeUniverse _universe;
    private readonly MetadataDispenser _dispenser = new MetadataDispenser();
    private IReflectionFactory _factory;

    public Loader(IMutableTypeUniverse universe) => this._universe = universe;

    public IReflectionFactory Factory
    {
      get
      {
        if (this._factory == null)
          this._factory = (IReflectionFactory) new DefaultFactory();
        return this._factory;
      }
      set => this._factory = value;
    }

    private MetadataFile OpenMetadataFile(string filename) => this._dispenser.OpenFileAsFileMapping(filename);

    public Assembly LoadAssemblyFromFile(string file)
    {
      MetadataFile metadataImport = this.OpenMetadataFile(file);
      Assembly assembly = AssemblyFactory.CreateAssembly((ITypeUniverse) this._universe, metadataImport, this.Factory, metadataImport.FilePath);
      this._universe.AddAssembly(assembly);
      return assembly;
    }

    public Assembly LoadAssemblyFromFile(string manifestFile, string[] netModuleFiles)
    {
      MetadataFile manifestModuleImport = this.OpenMetadataFile(manifestFile);
      MetadataFile[] netModuleImports = (MetadataFile[]) null;
      if (netModuleFiles != null && netModuleFiles.Length != 0)
      {
        netModuleImports = new MetadataFile[netModuleFiles.Length];
        for (int index = 0; index < netModuleFiles.Length; ++index)
          netModuleImports[index] = this.OpenMetadataFile(netModuleFiles[index]);
      }
      Assembly assembly = AssemblyFactory.CreateAssembly((ITypeUniverse) this._universe, manifestModuleImport, netModuleImports, this.Factory, manifestModuleImport.FilePath, netModuleFiles);
      this._universe.AddAssembly(assembly);
      return assembly;
    }

    public Assembly LoadAssemblyFromByteArray(byte[] data)
    {
      Assembly assembly = AssemblyFactory.CreateAssembly((ITypeUniverse) this._universe, this._dispenser.OpenFromByteArray(data), this.Factory, string.Empty);
      this._universe.AddAssembly(assembly);
      return assembly;
    }

    public MetadataOnlyModule LoadModuleFromFile(string moduleFileName) => new MetadataOnlyModule((ITypeUniverse) this._universe, this._dispenser.OpenFileAsFileMapping(moduleFileName), this.Factory, moduleFileName);

    public Module ResolveModule(Assembly containingAssembly, string moduleName)
    {
      if (containingAssembly == null || string.IsNullOrEmpty(containingAssembly.Location))
        throw new ArgumentException("manifestModule needs to be associated with an assembly with valid location");
      string modulePath = LongPathPath.Combine(LongPathPath.GetDirectoryName(containingAssembly.Location), moduleName);
      MetadataOnlyModule metadataOnlyModule = new MetadataOnlyModule((ITypeUniverse) this._universe, this._dispenser.OpenFileAsFileMapping(modulePath), this.Factory, modulePath);
      metadataOnlyModule.SetContainingAssembly(containingAssembly);
      return (Module) metadataOnlyModule;
    }
  }
}
