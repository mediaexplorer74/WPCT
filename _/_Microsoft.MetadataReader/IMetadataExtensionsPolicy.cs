// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.IMetadataExtensionsPolicy
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal interface IMetadataExtensionsPolicy
  {
    Type[] GetExtraArrayInterfaces(Type elementType);

    MethodInfo[] GetExtraArrayMethods(Type arrayType);

    ConstructorInfo[] GetExtraArrayConstructors(Type arrayType);

    ParameterInfo GetFakeParameterInfo(
      MemberInfo member,
      Type paramType,
      int position);

    IEnumerable<CustomAttributeData> GetPseudoCustomAttributes(
      MetadataOnlyModule module,
      Token token);

    Type TryTypeForwardResolution(
      MetadataOnlyAssembly assembly,
      string fullname,
      bool ignoreCase);
  }
}
