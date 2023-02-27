// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.CommonIdeHelper
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal static class CommonIdeHelper
  {
    public static AssemblyName GetNameFromPath(string path) => AssemblyFactory.CreateAssembly((ITypeUniverse) new CommonIdeHelper.EmptyUniverse(), new MetadataDispenser().OpenFile(path), path).GetName();

    private class EmptyUniverse : ITypeUniverse
    {
      public Type GetBuiltInType(CorElementType elementType) => throw new NotImplementedException();

      public Type GetTypeXFromName(string fullName) => throw new NotImplementedException();

      public Assembly GetSystemAssembly() => throw new NotImplementedException();

      public Assembly ResolveAssembly(AssemblyName name) => throw new NotImplementedException();

      public Assembly ResolveAssembly(Module scope, Token tokenAssemblyRef) => throw new NotImplementedException();

      public Module ResolveModule(Assembly containingAssembly, string moduleName) => throw new NotImplementedException();
    }
  }
}
