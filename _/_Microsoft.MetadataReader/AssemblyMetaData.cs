// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.AssemblyMetaData
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Globalization;

namespace Microsoft.MetadataReader
{
  internal struct AssemblyMetaData : IDisposable
  {
    public ushort majorVersion;
    public ushort minorVersion;
    public ushort buildNumber;
    public ushort revisionNumber;
    public UnmanagedStringMemoryHandle szLocale;
    public uint cbLocale;
    public UnusedIntPtr rdwProcessor;
    public uint ulProcessor;
    public UnusedIntPtr rOS;
    public uint ulOS;

    public void Init()
    {
      this.szLocale = new UnmanagedStringMemoryHandle();
      this.cbLocale = 0U;
      this.ulProcessor = 0U;
      this.ulOS = 0U;
    }

    public Version Version => new Version((int) this.majorVersion, (int) this.minorVersion, (int) this.buildNumber, (int) this.revisionNumber);

    public string LocaleString
    {
      get
      {
        if (this.szLocale == null)
          return (string) null;
        return this.szLocale.IsInvalid || this.cbLocale <= 0U ? string.Empty : this.szLocale.GetAsString((int) this.cbLocale - 1);
      }
    }

    public CultureInfo Locale => this.szLocale == null ? CultureInfo.InvariantCulture : new CultureInfo(this.LocaleString);

    public void Dispose()
    {
      if (this.szLocale == null)
        return;
      this.szLocale.Dispose();
    }
  }
}
