// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.AssemblyNameHelper
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Configuration.Assemblies;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal static class AssemblyNameHelper
  {
    private const int ProcessorArchitectureMask = 240;
    private const int ReferenceAssembly = 112;

    public static AssemblyName GetAssemblyName(MetadataOnlyModule module)
    {
      Token assemblyToken = MetadataOnlyAssembly.GetAssemblyToken(module);
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) module.RawImport;
      AssemblyNameHelper.AssemblyNameFromDefitionBuilder fromDefitionBuilder = new AssemblyNameHelper.AssemblyNameFromDefitionBuilder(assemblyToken, module.RawMetadata, rawImport);
      AssemblyName name = fromDefitionBuilder.CalculateName();
      name.CodeBase = MetadataOnlyAssembly.GetCodeBaseFromManifestModule(module);
      if (!AssemblyNameHelper.HasV1Metadata((IMetadataImport2) module.RawImport))
      {
        PortableExecutableKinds pek;
        ImageFileMachine ifm;
        ((Module) module).GetPEKind(ref pek, ref ifm);
        ProcessorArchitecture procArchIndex = AssemblyNameHelper.CalculateProcArchIndex(pek, ifm, fromDefitionBuilder.AssemblyNameFlags);
        name.ProcessorArchitecture = procArchIndex;
      }
      else
        name.ProcessorArchitecture = ProcessorArchitecture.None;
      return name;
    }

    public static bool HasV1Metadata(IMetadataImport2 assemblyImport)
    {
      int pchName;
      assemblyImport.GetVersionString((StringBuilder) null, 0, out pchName);
      if (pchName < 2)
        return false;
      StringBuilder builder = StringBuilderPool.Get(pchName);
      assemblyImport.GetVersionString(builder, builder.Capacity, out pchName);
      bool flag = builder[1] == '1';
      StringBuilderPool.Release(ref builder);
      return flag;
    }

    public static AssemblyName GetAssemblyNameFromRef(
      Token assemblyRefToken,
      MetadataOnlyModule module,
      IMetadataAssemblyImport assemblyImport)
    {
      return new AssemblyNameHelper.AssemblyNameFromRefBuilder(assemblyRefToken, module.RawMetadata, assemblyImport).CalculateName();
    }

    private static ProcessorArchitecture CalculateProcArchIndex(
      PortableExecutableKinds pek,
      ImageFileMachine ifm,
      AssemblyNameFlags flags)
    {
      if ((flags & (AssemblyNameFlags) 240) == (AssemblyNameFlags) 112)
        return ProcessorArchitecture.None;
      if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
      {
        switch (ifm)
        {
          case ImageFileMachine.I386:
            if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
              return ProcessorArchitecture.MSIL;
            break;
          case ImageFileMachine.IA64:
            return ProcessorArchitecture.IA64;
          case ImageFileMachine.AMD64:
            return ProcessorArchitecture.Amd64;
        }
      }
      else if (ifm == ImageFileMachine.I386)
        return (pek & PortableExecutableKinds.Required32Bit) != PortableExecutableKinds.Required32Bit && (pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly ? ProcessorArchitecture.MSIL : ProcessorArchitecture.X86;
      return ProcessorArchitecture.None;
    }

    private abstract class AssemblyNameBuilder : IDisposable
    {
      private readonly MetadataFile _storage;
      protected readonly IMetadataAssemblyImport m_assemblyImport;
      protected EmbeddedBlobPointer m_publicKey;
      protected int m_cbPublicKey;
      protected int m_hashAlgId;
      protected StringBuilder m_szName;
      protected int m_chName;
      protected AssemblyNameFlags m_flags;
      protected AssemblyMetaData m_metadata;

      protected AssemblyNameBuilder(MetadataFile storage, IMetadataAssemblyImport assemblyImport)
      {
        this._storage = storage;
        this.m_assemblyImport = assemblyImport;
      }

      protected abstract void Fetch();

      public AssemblyName CalculateName()
      {
        AssemblyName name = new AssemblyName();
        this.m_metadata = new AssemblyMetaData();
        this.m_metadata.Init();
        this.m_szName = (StringBuilder) null;
        this.m_chName = 0;
        this.Fetch();
        this.m_szName = new StringBuilder();
        this.m_szName.Capacity = this.m_chName;
        this.m_metadata.szLocale = new UnmanagedStringMemoryHandle((int) this.m_metadata.cbLocale * 2);
        this.m_metadata.ulProcessor = 0U;
        this.m_metadata.ulOS = 0U;
        this.Fetch();
        name.CultureInfo = this.m_metadata.Locale;
        byte[] numArray = this._storage.ReadEmbeddedBlob(this.m_publicKey, this.m_cbPublicKey);
        name.HashAlgorithm = (AssemblyHashAlgorithm) this.m_hashAlgId;
        name.Name = this.m_szName.ToString();
        name.Version = this.m_metadata.Version;
        name.Flags = this.m_flags;
        if ((this.m_flags & AssemblyNameFlags.PublicKey) != 0)
          name.SetPublicKey(numArray);
        else
          name.SetPublicKeyToken(numArray);
        return name;
      }

      public AssemblyNameFlags AssemblyNameFlags => this.m_flags;

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      protected virtual void Dispose(bool disposing)
      {
        if (!disposing)
          return;
        this.m_metadata.szLocale.Dispose();
      }
    }

    private class AssemblyNameFromDefitionBuilder : AssemblyNameHelper.AssemblyNameBuilder
    {
      private readonly Token _assemblyToken;

      public AssemblyNameFromDefitionBuilder(
        Token assemblyToken,
        MetadataFile storage,
        IMetadataAssemblyImport assemblyImport)
        : base(storage, assemblyImport)
      {
        this._assemblyToken = assemblyToken;
      }

      protected override void Fetch() => this.m_assemblyImport.GetAssemblyProps(this._assemblyToken, out this.m_publicKey, out this.m_cbPublicKey, out this.m_hashAlgId, this.m_szName, this.m_chName, out this.m_chName, ref this.m_metadata, out this.m_flags);
    }

    private class AssemblyNameFromRefBuilder : AssemblyNameHelper.AssemblyNameBuilder
    {
      private readonly Token _assemblyRefToken;

      public AssemblyNameFromRefBuilder(
        Token assemblyRefToken,
        MetadataFile storage,
        IMetadataAssemblyImport assemblyImport)
        : base(storage, assemblyImport)
      {
        this._assemblyRefToken = ((Token) ref assemblyRefToken).TokenType == 587202560 ? assemblyRefToken : throw new ArgumentException(Resources.AssemblyRefTokenExpected);
      }

      protected override void Fetch() => this.m_assemblyImport.GetAssemblyRefProps(this._assemblyRefToken, out this.m_publicKey, out this.m_cbPublicKey, this.m_szName, this.m_chName, out this.m_chName, ref this.m_metadata, out UnusedIntPtr _, out uint _, out this.m_flags);
    }
  }
}
