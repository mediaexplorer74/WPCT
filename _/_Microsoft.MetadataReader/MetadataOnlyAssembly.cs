// MetadataOnlyAssembly.cs
// Type: Microsoft.MetadataReader.MetadataOnlyAssembly
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using Microsoft.Tools.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.MetadataReader
{
  public class MetadataOnlyAssembly : Assembly, IAssembly2, IDisposable
  {
    private readonly Module[] _modules;
    private readonly MetadataOnlyModule _manifestModule;
    private readonly string _manifestFile;
    private readonly AssemblyName _name;

    public MetadataOnlyAssembly(MetadataOnlyModule manifestModule, string manifestFile)
      : this(new MetadataOnlyModule[1]{ manifestModule }, manifestFile)
    {
    }

    public MetadataOnlyAssembly(MetadataOnlyModule[] modules, string manifestFile)
    {
      MetadataOnlyAssembly.VerifyModules(modules);
      this._manifestModule = modules[0];
      this._name = AssemblyNameHelper.GetAssemblyName(this._manifestModule);
      this._manifestFile = manifestFile;
      foreach (MetadataOnlyModule module in modules)
        module.SetContainingAssembly((Assembly) this);
      List<Module> moduleList = new List<Module>((IEnumerable<Module>) modules);
      foreach (string str in MetadataOnlyAssembly.GetFileNamesFromFilesTable(this._manifestModule, false))
      {
        string netModuleName = str;
        if (moduleList.Find((Predicate<Module>) (i => i.Name.Equals(netModuleName, StringComparison.OrdinalIgnoreCase))) == null)
        {
          Module module = this._manifestModule.AssemblyResolver.ResolveModule((Assembly) this, netModuleName);
          if (module == null)
            throw new InvalidOperationException(Resources.ResolverMustResolveToValidModule);
          if (module.Assembly != this)
            throw new InvalidOperationException(Resources.ResolverMustSetAssemblyProperty);
          moduleList.Add(module);
        }
      }
      this._modules = moduleList.ToArray();
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this._modules == null)
        return;
      foreach (Module module in this._modules)
      {
        if (module is IDisposable disposable)
          disposable.Dispose();
      }
    }

    private static void VerifyModules(MetadataOnlyModule[] modules)
    {
      if (modules == null || modules.Length < 1)
        throw new ArgumentException(Resources.ManifestModuleMustBeProvided);
      if (Token.op_Equality(MetadataOnlyAssembly.GetAssemblyToken(modules[0]), Token.Nil))
        throw new ArgumentException(Resources.NoAssemblyManifest);
      for (int index = 1; index < modules.Length; ++index)
      {
        if (Token.op_Inequality(MetadataOnlyAssembly.GetAssemblyToken(modules[index]), Token.Nil))
          throw new ArgumentException(Resources.ExtraAssemblyManifest);
      }
    }

    private static List<string> GetFileNamesFromFilesTable(
      MetadataOnlyModule manifestModule,
      bool getResources)
    {
      HCORENUM phEnum = new HCORENUM();
      List<string> namesFromFilesTable = new List<string>();
      StringBuilder builder = StringBuilderPool.Get();
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) manifestModule.RawImport;
      try
      {
        while (true)
        {
          int files;
          int pchName;
          UnusedIntPtr ppbHashValue;
          uint pcbHashValue;
          CorFileFlags dwFileFlags;
          do
          {
            int cTokens;
            rawImport.EnumFiles(ref phEnum, out files, 1, out cTokens);
            if (cTokens != 0)
              rawImport.GetFileProps(files, (StringBuilder) null, 0, out pchName, out ppbHashValue, out pcbHashValue, out dwFileFlags);
            else
              goto label_6;
          }
          while (!getResources && dwFileFlags == CorFileFlags.ContainsNoMetaData);
          builder.Length = 0;
          builder.EnsureCapacity(pchName);
          rawImport.GetFileProps(files, builder, builder.Capacity, out pchName, out ppbHashValue, out pcbHashValue, out dwFileFlags);
          namesFromFilesTable.Add(builder.ToString());
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
label_6:
      StringBuilderPool.Release(ref builder);
      return namesFromFilesTable;
    }

    public virtual int GetHashCode() => ((object) this._modules[0]).GetHashCode();

    public virtual bool Equals(object obj) => obj is Assembly assembly && ((object) base.ManifestModule).Equals((object) assembly.ManifestModule);

    public virtual Stream GetManifestResourceStream(Type type, string name)
    {
      StringBuilder builder = StringBuilderPool.Get();
      if (type == null)
      {
        if (name == null)
          throw new ArgumentNullException(nameof (type));
      }
      else
      {
        string str = type.Namespace;
        if (str != null)
        {
          builder.Append(str);
          if (name != null)
            builder.Append(Type.Delimiter);
        }
      }
      if (name != null)
        builder.Append(name);
      string str1 = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return base.GetManifestResourceStream(str1);
    }

    public virtual Stream GetManifestResourceStream(string name)
    {
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) 
                this._manifestModule.RawImport;
      int ptkManifestResource;
      rawImport.FindManifestResourceByName(name, out ptkManifestResource);
      Token token1 = default;
      // ISSUE: explicit constructor call
      ((Token) ref token1).\u002Ector(ptkManifestResource);
      if (((Token) ref token1).IsNil)
        return (Stream) null;
      StringBuilder builder1 = StringBuilderPool.Get(name.Length + 1);
      int ptkImplementation;
      uint pdwOffset;
      rawImport.GetManifestResourceProps(ptkManifestResource, builder1, builder1.Capacity, out int _, out ptkImplementation, out pdwOffset, out CorManifestResourceFlags _);
      StringBuilderPool.Release(ref builder1);
      Token token2;
      // ISSUE: explicit constructor call
      ((Token) ref token2).\u002Ector(ptkImplementation);
      if (((Token) ref token2).TokenType == 637534208)
      {
        if (((Token) ref token2).IsNil)
          return (Stream) new MemoryStream(this._manifestModule.RawMetadata.ReadResource((long) pdwOffset));
        int pchName;
        UnusedIntPtr ppbHashValue;
        uint pcbHashValue;
        CorFileFlags dwFileFlags;
        rawImport.GetFileProps(((Token) ref token2).Value, (StringBuilder) null, 0, out pchName, out ppbHashValue, out pcbHashValue, out dwFileFlags);
        StringBuilder builder2 = StringBuilderPool.Get(pchName);
        rawImport.GetFileProps(((Token) ref token2).Value, builder2, builder2.Capacity, out pchName, out ppbHashValue, out pcbHashValue, out dwFileFlags);
        string path = LongPathPath.Combine(LongPathPath.GetDirectoryName(base.Location), builder2.ToString());
        StringBuilderPool.Release(ref builder2);
        return (Stream) new FileStream(path, FileMode.Open);
      }
      if (((Token) ref token2).TokenType == 587202560)
        throw new NotImplementedException();
      throw new ArgumentException(Resources.InvalidMetadata);
    }

    public virtual string[] GetManifestResourceNames()
    {
      HCORENUM phEnum = new HCORENUM();
      List<string> stringList = new List<string>();
      StringBuilder builder = StringBuilderPool.Get();
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) this._manifestModule.RawImport;
      try
      {
        while (true)
        {
          int rManifestResources;
          int cTokens;
          rawImport.EnumManifestResources(ref phEnum, out rManifestResources, 1, out cTokens);
          if (cTokens != 0)
          {
            int pchName;
            int ptkImplementation;
            uint pdwOffset;
            CorManifestResourceFlags pdwResourceFlags;
            rawImport.GetManifestResourceProps(rManifestResources, (StringBuilder) null, 0, out pchName, out ptkImplementation, out pdwOffset, out pdwResourceFlags);
            builder.Length = 0;
            builder.EnsureCapacity(pchName);
            rawImport.GetManifestResourceProps(rManifestResources, builder, builder.Capacity, out pchName, out ptkImplementation, out pdwOffset, out pdwResourceFlags);
            stringList.Add(builder.ToString());
          }
          else
            break;
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
      StringBuilderPool.Release(ref builder);
      return stringList.ToArray();
    }

    public virtual AssemblyName GetName() => this._name;

    public virtual AssemblyName GetName(bool copiedName) => throw new NotImplementedException();

    public virtual string FullName => this._name.FullName;

    public virtual string Location => this._manifestFile;

    public virtual Type[] GetExportedTypes()
    {
      Type[] types = base.GetTypes();
      List<Type> typeList = new List<Type>();
      foreach (Type type in types)
      {
        if (type.IsVisible)
          typeList.Add(type);
      }
      return typeList.ToArray();
    }

    public virtual Type GetType(string name) => base.GetType(name, false, false);

    public virtual Type GetType(string name, bool throwOnError) => base.GetType(name, throwOnError, false);

    public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      for (int index = 0; index < this._modules.Length; ++index)
      {
        Type type = this._modules[index].GetType(name, false, ignoreCase);
        if (type != null)
          return type;
      }
      Type type1 = this._manifestModule.Policy.TryTypeForwardResolution(this, name, ignoreCase);
      if (type1 != null)
        return type1;
      if (throwOnError)
        throw new TypeLoadException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.CannotFindTypeInModule, new object[2]
        {
          (object) name,
          (object) this._modules[0].ScopeName
        }));
      return (Type) null;
    }

    public virtual Type[] GetTypes()
    {
      List<Type> typeList = new List<Type>();
      foreach (Module module in this._modules)
        typeList.AddRange((IEnumerable<Type>) module.GetTypes());
      return typeList.ToArray();
    }

    public virtual Module GetModule(string name)
    {
      foreach (Module module in this._modules)
      {
        if (module.ScopeName.Equals(name, StringComparison.OrdinalIgnoreCase))
          return module;
      }
      return (Module) null;
    }

    public virtual Module[] GetModules(bool getResourceModules) => this._modules;

    public virtual Module[] GetLoadedModules(bool getResourceModules) => this._modules;

    public virtual Module ManifestModule => this._modules[0];

    public MetadataOnlyModule ManifestModuleInternal => this._manifestModule;

    public virtual string CodeBase => MetadataOnlyAssembly.GetCodeBaseFromManifestModule(this._manifestModule);

    public static string GetCodeBaseFromManifestModule(MetadataOnlyModule manifestModule)
    {
      string str = ((Module) manifestModule).FullyQualifiedName;
      if (str.StartsWith("\\\\?\\"))
        str = str.Substring("\\\\?\\".Length, str.Length - "\\\\?\\".Length);
      if (!Utility.IsValidPath(str))
        return string.Empty;
      try
      {
        return new Uri(str).ToString();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public virtual MethodInfo EntryPoint
    {
      get
      {
        Token token = this._manifestModule.RawMetadata.ReadEntryPointToken();
        if (((Token) ref token).IsNil)
          return (MethodInfo) null;
        TokenType tokenType = ((Token) ref token).TokenType;
        if (tokenType == 67108864)
          throw new NotImplementedException();
        if (tokenType == 100663296)
          return (MethodInfo) base.ManifestModule.ResolveMethod(((Token) ref token).Value);
        throw new InvalidOperationException(Resources.InvalidMetadata);
      }
    }

    public static Token GetAssemblyToken(MetadataOnlyModule module)
    {
      int assemblyToken;
      return ((IMetadataAssemblyImport) module.RawImport).GetAssemblyFromScope(out assemblyToken) == 0 
                ? new Token(assemblyToken) : Token.Nil;
    }

    public virtual FileStream[] GetFiles(bool getResourceModules)
    {
      List<string> stringList = new List<string>();
      foreach (Module module in this._modules)
        stringList.Add(module.FullyQualifiedName);
      if (getResourceModules)
      {
        string directoryName = LongPathPath.GetDirectoryName(this._manifestFile);
        foreach (string path2 in MetadataOnlyAssembly.GetFileNamesFromFilesTable(this._manifestModule, true))
          stringList.Add(LongPathPath.Combine(directoryName, path2));
      }
      return MetadataOnlyAssembly.ConvertFileNamesToStreams(stringList.ToArray());
    }

    public virtual FileStream GetFile(string name)
    {
      Module module = base.GetModule(name);
      return module == null ? (FileStream) null : new FileStream(module.FullyQualifiedName, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    private static FileStream[] ConvertFileNamesToStreams(string[] filenames) => Array.ConvertAll<string, FileStream>(filenames, (Converter<string, FileStream>) (n => new FileStream(n, FileMode.Open, FileAccess.Read, FileShare.Read)));

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this._manifestModule.GetCustomAttributeData(Token.op_Implicit(MetadataOnlyAssembly.GetAssemblyToken(this._manifestModule)));

    public virtual AssemblyName[] GetReferencedAssemblies()
    {
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) this._manifestModule.RawImport;
      List<AssemblyName> assemblyNameList = new List<AssemblyName>();
      HCORENUM phEnum = new HCORENUM();
      try
      {
        while (true)
        {
          Token assemblyRefs;
          int cTokens;
          Marshal.ThrowExceptionForHR(rawImport.EnumAssemblyRefs(ref phEnum, out assemblyRefs, 1, out cTokens));
          if (cTokens != 0)
          {
            AssemblyName assemblyNameFromRef = AssemblyNameHelper.GetAssemblyNameFromRef(assemblyRefs, this._manifestModule, rawImport);
            assemblyNameList.Add(assemblyNameFromRef);
          }
          else
            break;
        }
      }
      finally
      {
        phEnum.Close(rawImport);
      }
      return assemblyNameList.ToArray();
    }

    public ITypeUniverse TypeUniverse => this._manifestModule.AssemblyResolver;
  }
}
