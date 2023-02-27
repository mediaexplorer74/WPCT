// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.PseudoCustomAttributes
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal static class PseudoCustomAttributes
  {
    public const string TypeForwardedToAttributeName = "System.Runtime.CompilerServices.TypeForwardedToAttribute";
    public const string SerializableAttributeName = "System.SerializableAttribute";

    public static IEnumerable<CustomAttributeData> GetTypeForwardedToAttributes(
      MetadataOnlyAssembly assembly)
    {
      return PseudoCustomAttributes.GetTypeForwardedToAttributes(assembly.ManifestModuleInternal);
    }

    public static IEnumerable<CustomAttributeData> GetTypeForwardedToAttributes(
      MetadataOnlyModule manifestModule,
      Token token)
    {
      return ((Token) ref token).TokenType != 536870912 ? (IEnumerable<CustomAttributeData>) new CustomAttributeData[0] : PseudoCustomAttributes.GetTypeForwardedToAttributes(manifestModule);
    }

    public static IEnumerable<CustomAttributeData> GetTypeForwardedToAttributes(
      MetadataOnlyModule manifestModule)
    {
      MetadataOnlyModule metadataOnlyModule;
      ITypeUniverse itu = metadataOnlyModule.AssemblyResolver;
      Type argumentType = itu.GetBuiltInType((CorElementType) 80);
      Assembly systemAssembly = itu.GetSystemAssembly();
      Type attributeType = systemAssembly.GetType("System.Runtime.CompilerServices.TypeForwardedToAttribute", false, false);
      if (attributeType != null)
      {
        IEnumerable<UnresolvedTypeName> raw = PseudoCustomAttributes.GetRawTypeForwardedToAttributes(metadataOnlyModule);
        foreach (UnresolvedTypeName utn in raw)
        {
          ConstructorInfo[] constructors = attributeType.GetConstructors();
          Type argumentValue = utn.ConvertToType(itu, (Module) metadataOnlyModule);
          CustomAttributeTypedArgument forwardedTypeInfo = new CustomAttributeTypedArgument(argumentType, (object) argumentValue);
          List<CustomAttributeTypedArgument> typedArguments = new List<CustomAttributeTypedArgument>(1);
          typedArguments.Add(forwardedTypeInfo);
          List<CustomAttributeNamedArgument> namedArguments = new List<CustomAttributeNamedArgument>(0);
          MetadataOnlyCustomAttributeData attribute = new MetadataOnlyCustomAttributeData(constructors[0], (IList<CustomAttributeTypedArgument>) typedArguments, (IList<CustomAttributeNamedArgument>) namedArguments);
          yield return (CustomAttributeData) attribute;
          constructors = (ConstructorInfo[]) null;
          argumentValue = (Type) null;
          forwardedTypeInfo = new CustomAttributeTypedArgument();
          typedArguments = (List<CustomAttributeTypedArgument>) null;
          namedArguments = (List<CustomAttributeNamedArgument>) null;
          attribute = (MetadataOnlyCustomAttributeData) null;
        }
      }
    }

    internal static IEnumerable<UnresolvedTypeName> GetRawTypeForwardedToAttributes(
      MetadataOnlyAssembly assembly)
    {
      return PseudoCustomAttributes.GetRawTypeForwardedToAttributes(assembly.ManifestModuleInternal);
    }

    internal static bool GetNextExportedType(
      ref HCORENUM hEnum,
      IMetadataAssemblyImport assemblyImport,
      StringBuilder typeName,
      out Token implementationToken)
    {
      implementationToken = Token.Nil;
      int rExportedTypes;
      uint cTokens;
      assemblyImport.EnumExportedTypes(ref hEnum, out rExportedTypes, 1, out cTokens);
      if (cTokens == 0U)
        return false;
      int pchName;
      int ptkImplementation;
      int ptkTypeDef;
      CorTypeAttr pdwExportedTypeFlags;
      assemblyImport.GetExportedTypeProps(rExportedTypes, (StringBuilder) null, 0, out pchName, out ptkImplementation, out ptkTypeDef, out pdwExportedTypeFlags);
      implementationToken = new Token(ptkImplementation);
      if (((Token) ref implementationToken).TokenType == 587202560)
      {
        typeName.Length = 0;
        typeName.EnsureCapacity(pchName);
        assemblyImport.GetExportedTypeProps(rExportedTypes, typeName, typeName.Capacity, out pchName, out ptkImplementation, out ptkTypeDef, out pdwExportedTypeFlags);
      }
      return true;
    }

    internal static IEnumerable<UnresolvedTypeName> GetRawTypeForwardedToAttributes(
      MetadataOnlyModule manifestModule)
    {
      HCORENUM hEnum = new HCORENUM();
      IMetadataAssemblyImport assemblyImport = (IMetadataAssemblyImport) manifestModule.RawImport;
      try
      {
        StringBuilder typeName = StringBuilderPool.Get();
        Token implementationToken;
        while (PseudoCustomAttributes.GetNextExportedType(ref hEnum, assemblyImport, typeName, out implementationToken))
        {
          if (((Token) ref implementationToken).TokenType == 587202560)
          {
            AssemblyName assemblyName = AssemblyNameHelper.GetAssemblyNameFromRef(implementationToken, manifestModule, assemblyImport);
            yield return new UnresolvedTypeName(typeName.ToString(), assemblyName);
            assemblyName = (AssemblyName) null;
          }
        }
        StringBuilderPool.Release(ref typeName);
        typeName = (StringBuilder) null;
        implementationToken = new Token();
      }
      finally
      {
        hEnum.Close(assemblyImport);
      }
    }

    internal static UnresolvedTypeName GetRawTypeForwardedToAttribute(
      MetadataOnlyAssembly assembly,
      string fullname,
      bool ignoreCase)
    {
      return PseudoCustomAttributes.GetRawTypeForwardedToAttribute(assembly.ManifestModuleInternal, fullname, ignoreCase);
    }

    internal static UnresolvedTypeName GetRawTypeForwardedToAttribute(
      MetadataOnlyModule manifestModule,
      string fullname,
      bool ignoreCase)
    {
      HCORENUM hEnum = new HCORENUM();
      IMetadataAssemblyImport rawImport = (IMetadataAssemblyImport) manifestModule.RawImport;
      if (string.IsNullOrEmpty(fullname))
        return (UnresolvedTypeName) null;
      UnresolvedTypeName forwardedToAttribute = (UnresolvedTypeName) null;
      try
      {
        StringBuilder builder = StringBuilderPool.Get();
        Token implementationToken;
        while (PseudoCustomAttributes.GetNextExportedType(ref hEnum, rawImport, builder, out implementationToken))
        {
          if (((Token) ref implementationToken).TokenType == 587202560 && fullname.Length == builder.Length)
          {
            string str = builder.ToString();
            if (Utility.Compare(str, fullname, ignoreCase))
            {
              AssemblyName assemblyNameFromRef = AssemblyNameHelper.GetAssemblyNameFromRef(implementationToken, manifestModule, rawImport);
              forwardedToAttribute = new UnresolvedTypeName(str, assemblyNameFromRef);
              break;
            }
          }
        }
        StringBuilderPool.Release(ref builder);
      }
      finally
      {
        hEnum.Close(rawImport);
      }
      return forwardedToAttribute;
    }

    public static Type GetTypeFromTypeForwardToAttribute(CustomAttributeData data)
    {
      CustomAttributeTypedArgument constructorArgument = data.ConstructorArguments[0];
      return (Type) ((CustomAttributeTypedArgument) ref constructorArgument).Value;
    }

    public static CustomAttributeData GetSerializableAttribute(
      MetadataOnlyModule module,
      Token token)
    {
      if (((Token) ref token).TokenType != 33554432)
        return (CustomAttributeData) null;
      TypeAttributes pdwTypeDefFlags;
      module.RawImport.GetTypeDefProps(((Token) ref token).Value, (StringBuilder) null, 0, out int _, out pdwTypeDefFlags, out int _);
      return (pdwTypeDefFlags & TypeAttributes.Serializable) == TypeAttributes.NotPublic ? (CustomAttributeData) null : PseudoCustomAttributes.GetSerializableAttribute(module, true);
    }

    internal static CustomAttributeData GetSerializableAttribute(
      MetadataOnlyModule module,
      bool isRequired)
    {
      Type type = module.AssemblyResolver.GetSystemAssembly().GetType("System.SerializableAttribute", isRequired, false);
      return type == null ? (CustomAttributeData) null : (CustomAttributeData) new MetadataOnlyCustomAttributeData(type.GetConstructors()[0], (IList<CustomAttributeTypedArgument>) new List<CustomAttributeTypedArgument>(0), (IList<CustomAttributeNamedArgument>) new List<CustomAttributeNamedArgument>(0));
    }
  }
}
