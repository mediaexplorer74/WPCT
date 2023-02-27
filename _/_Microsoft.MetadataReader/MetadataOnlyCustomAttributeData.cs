// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyCustomAttributeData
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyCustomAttributeData : CustomAttributeData
  {
    private readonly ConstructorInfo _ctor;
    private readonly MetadataOnlyModule _module;
    private readonly Token _token;
    private IList<CustomAttributeTypedArgument> _typedArguments;
    private IList<CustomAttributeNamedArgument> _namedArguments;

    public MetadataOnlyCustomAttributeData(
      MetadataOnlyModule module,
      Token token,
      ConstructorInfo ctor)
    {
      this._ctor = ctor;
      this._token = token;
      this._module = module;
    }

    public MetadataOnlyCustomAttributeData(
      ConstructorInfo ctor,
      IList<CustomAttributeTypedArgument> typedArguments,
      IList<CustomAttributeNamedArgument> namedArguments)
    {
      this._ctor = ctor;
      this._typedArguments = typedArguments;
      this._namedArguments = namedArguments;
    }

    public virtual ConstructorInfo Constructor => this._ctor;

    private void InitArgumentData()
    {
      IList<CustomAttributeTypedArgument> constructorArguments;
      IList<CustomAttributeNamedArgument> namedArguments;
      this._module.LazyAttributeParse(this._token, this._ctor, out constructorArguments, out namedArguments);
      this._typedArguments = constructorArguments;
      this._namedArguments = namedArguments;
    }

    public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
    {
      get
      {
        if (this._typedArguments == null)
          this.InitArgumentData();
        return this._typedArguments;
      }
    }

    public virtual IList<CustomAttributeNamedArgument> NamedArguments
    {
      get
      {
        if (this._namedArguments == null)
          this.InitArgumentData();
        return this._namedArguments;
      }
    }
  }
}
