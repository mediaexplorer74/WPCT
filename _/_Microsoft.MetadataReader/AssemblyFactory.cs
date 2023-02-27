// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.AssemblyFactory
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal static class AssemblyFactory
  {
    public static Assembly CreateAssembly(
      MetadataOnlyModule manifestModule,
      string manifestFile)
    {
      return (Assembly) new MetadataOnlyAssembly(manifestModule, manifestFile);
    }

    public static Assembly CreateAssembly(
      ITypeUniverse typeUniverse,
      MetadataFile metadataImport,
      string manifestFile)
    {
      return AssemblyFactory.CreateAssembly(typeUniverse, metadataImport, (IReflectionFactory) new DefaultFactory(), manifestFile);
    }

    public static Assembly CreateAssembly(
      ITypeUniverse typeUniverse,
      MetadataFile metadataImport,
      IReflectionFactory factory,
      string manifestFile)
    {
      return AssemblyFactory.CreateAssembly(typeUniverse, metadataImport, (MetadataFile[]) null, factory, manifestFile, (string[]) null);
    }

    public static Assembly CreateAssembly(
      ITypeUniverse typeUniverse,
      MetadataFile manifestModuleImport,
      MetadataFile[] netModuleImports,
      string manifestFile,
      string[] netModuleFiles)
    {
      return AssemblyFactory.CreateAssembly(typeUniverse, manifestModuleImport, netModuleImports, (IReflectionFactory) new DefaultFactory(), manifestFile, netModuleFiles);
    }

    public static Assembly CreateAssembly(
      ITypeUniverse typeUniverse,
      MetadataFile manifestModuleImport,
      MetadataFile[] netModuleImports,
      IReflectionFactory factory,
      string manifestFile,
      string[] netModuleFiles)
    {
      int length = 1;
      if (netModuleImports != null)
        length += netModuleImports.Length;
      MetadataOnlyModule[] modules = new MetadataOnlyModule[length];
      MetadataOnlyModule metadataOnlyModule = new MetadataOnlyModule(typeUniverse, manifestModuleImport, factory, manifestFile);
      modules[0] = metadataOnlyModule;
      if (length > 1)
      {
        for (int index = 0; index < netModuleImports.Length; ++index)
          modules[index + 1] = new MetadataOnlyModule(typeUniverse, netModuleImports[index], factory, netModuleFiles[index]);
      }
      return (Assembly) new MetadataOnlyAssembly(modules, manifestFile);
    }
  }
}
