// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataExtensionsPolicy20
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class MetadataExtensionsPolicy20 : IMetadataExtensionsPolicy
  {
    protected ITypeUniverse m_universe;

    public MetadataExtensionsPolicy20(ITypeUniverse u) => this.m_universe = u;

    public virtual Type[] GetExtraArrayInterfaces(Type elementType)
    {
      if (elementType.IsPointer)
        return Type.EmptyTypes;
      Type[] typeArray = new Type[1]{ elementType };
      return new Type[3]
      {
        this.m_universe.GetTypeXFromName("System.Collections.Generic.IList`1").MakeGenericType(typeArray),
        this.m_universe.GetTypeXFromName("System.Collections.Generic.ICollection`1").MakeGenericType(typeArray),
        this.m_universe.GetTypeXFromName("System.Collections.Generic.IEnumerable`1").MakeGenericType(typeArray)
      };
    }

    public virtual MethodInfo[] GetExtraArrayMethods(Type arrayType) => new MethodInfo[3]
    {
      (MethodInfo) new ArrayFabricatedGetMethodInfo(arrayType),
      (MethodInfo) new ArrayFabricatedSetMethodInfo(arrayType),
      (MethodInfo) new ArrayFabricatedAddressMethodInfo(arrayType)
    };

    public virtual ConstructorInfo[] GetExtraArrayConstructors(Type arrayType)
    {
      int arrayRank = arrayType.GetArrayRank();
      return new ConstructorInfo[2]
      {
        (ConstructorInfo) new ArrayFabricatedConstructorInfo(arrayType, arrayRank),
        (ConstructorInfo) new ArrayFabricatedConstructorInfo(arrayType, arrayRank * 2)
      };
    }

    public virtual ParameterInfo GetFakeParameterInfo(
      MemberInfo member,
      Type paramType,
      int position)
    {
      return (ParameterInfo) new SimpleParameterInfo(member, paramType, position);
    }

    public virtual IEnumerable<CustomAttributeData> GetPseudoCustomAttributes(
      MetadataOnlyModule module,
      Token token)
    {
      List<CustomAttributeData> customAttributes = new List<CustomAttributeData>();
      customAttributes.AddRange(PseudoCustomAttributes.GetTypeForwardedToAttributes(module, token));
      CustomAttributeData serializableAttribute = PseudoCustomAttributes.GetSerializableAttribute(module, token);
      if (serializableAttribute != null)
        customAttributes.Add(serializableAttribute);
      return (IEnumerable<CustomAttributeData>) customAttributes;
    }

    public virtual Type TryTypeForwardResolution(
      MetadataOnlyAssembly assembly,
      string fullname,
      bool ignoreCase)
    {
      return PseudoCustomAttributes.GetRawTypeForwardedToAttribute(assembly, fullname, ignoreCase)?.ConvertToType(assembly.TypeUniverse, (Module) assembly.ManifestModuleInternal);
    }
  }
}
