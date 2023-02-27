// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.DefaultFactory
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class DefaultFactory : IReflectionFactory
  {
    public virtual MetadataOnlyCommonType CreateSimpleType(
      MetadataOnlyModule scope,
      Token tokenTypeDef)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyTypeDef(scope, tokenTypeDef);
    }

    public virtual MetadataOnlyCommonType CreateGenericType(
      MetadataOnlyModule scope,
      Token tokenTypeDef,
      Type[] typeArgs)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyTypeDef(scope, tokenTypeDef, typeArgs);
    }

    public virtual MetadataOnlyCommonType CreateArrayType(
      MetadataOnlyCommonType elementType,
      int rank)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyArrayType(elementType, rank);
    }

    public virtual MetadataOnlyCommonType CreateVectorType(
      MetadataOnlyCommonType elementType)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyVectorType(elementType);
    }

    public virtual MetadataOnlyCommonType CreateByRefType(
      MetadataOnlyCommonType type)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyModifiedType(type, "&");
    }

    public virtual MetadataOnlyCommonType CreatePointerType(
      MetadataOnlyCommonType type)
    {
      return (MetadataOnlyCommonType) new MetadataOnlyModifiedType(type, "*");
    }

    public virtual MetadataOnlyTypeVariable CreateTypeVariable(
      MetadataOnlyModule resolver,
      Token typeVariableToken)
    {
      return new MetadataOnlyTypeVariable(resolver, typeVariableToken);
    }

    public virtual MetadataOnlyFieldInfo CreateField(
      MetadataOnlyModule resolver,
      Token fieldDefToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      return new MetadataOnlyFieldInfo(resolver, fieldDefToken, typeArgs, methodArgs);
    }

    public virtual MetadataOnlyPropertyInfo CreatePropertyInfo(
      MetadataOnlyModule resolver,
      Token propToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      return new MetadataOnlyPropertyInfo(resolver, propToken, typeArgs, methodArgs);
    }

    public virtual MetadataOnlyEventInfo CreateEventInfo(
      MetadataOnlyModule resolver,
      Token eventToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      return new MetadataOnlyEventInfo(resolver, eventToken, typeArgs, methodArgs);
    }

    public virtual MetadataOnlyConstructorInfo CreateConstructorInfo(
      MethodBase method)
    {
      return new MetadataOnlyConstructorInfo(method);
    }

    public virtual MetadataOnlyMethodInfo CreateMethodInfo(
      MetadataOnlyMethodInfo method)
    {
      return new MetadataOnlyMethodInfo(method);
    }

    public virtual MethodBase CreateMethodOrConstructor(
      MetadataOnlyModule resolver,
      Token methodDef,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      MetadataOnlyMethodInfo metadataOnlyMethodInfo = new MetadataOnlyMethodInfo(resolver, methodDef, typeArgs, methodArgs);
      return DefaultFactory.IsRawConstructor((MethodInfo) metadataOnlyMethodInfo) ? (MethodBase) this.CreateConstructorInfo((MethodBase) metadataOnlyMethodInfo) : (MethodBase) this.CreateMethodInfo(metadataOnlyMethodInfo);
    }

    private static bool IsRawConstructor(MethodInfo m)
    {
      if ((((MethodBase) m).Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.PrivateScope)
        return false;
      string name = ((MemberInfo) m).Name;
      return name.Equals(ConstructorInfo.ConstructorName, StringComparison.Ordinal) || name.Equals(ConstructorInfo.TypeConstructorName, StringComparison.Ordinal);
    }

    public virtual bool TryCreateMethodBody(MetadataOnlyMethodInfo method, ref MethodBody body) => false;

    public virtual Type CreateTypeRef(MetadataOnlyModule scope, Token tokenTypeRef) => (Type) new MetadataOnlyTypeReference(scope, tokenTypeRef);

    public virtual Type CreateTypeSpec(
      MetadataOnlyModule scope,
      Token tokenTypeSpec,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      return (Type) new TypeSpec(scope, tokenTypeSpec, typeArgs, methodArgs);
    }
  }
}
