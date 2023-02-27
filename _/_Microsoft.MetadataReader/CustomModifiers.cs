// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.CustomModifiers
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class CustomModifiers
  {
    private readonly List<Type> _optional;
    private readonly List<Type> _required;

    public CustomModifiers(List<Type> optModifiers, List<Type> reqModifiers)
    {
      this._optional = optModifiers;
      this._required = reqModifiers;
    }

    public Type[] OptionalCustomModifiers => this._optional != null ? this._optional.ToArray() : Type.EmptyTypes;

    public Type[] RequiredCustomModifiers => this._required != null ? this._required.ToArray() : Type.EmptyTypes;
  }
}
