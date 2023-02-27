// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.UnresolvedTypeName
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  [DebuggerDisplay("{TypeName},{m_AssemblyName}")]
  internal class UnresolvedTypeName
  {
    private readonly AssemblyName _assemblyName;

    public UnresolvedTypeName(string typeName, AssemblyName assemblyName)
    {
      this.TypeName = typeName;
      this._assemblyName = assemblyName;
    }

    public Type ConvertToType(ITypeUniverse universe, Module moduleContext)
    {
      string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0},{1}", new object[2]
      {
        (object) this.TypeName,
        (object) this._assemblyName.FullName
      });
      return TypeNameParser.ParseTypeName(universe, moduleContext, str);
    }

    public string TypeName { get; }
  }
}
