// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.IReflectionFactory
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Diagnostics.CodeAnalysis;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal interface IReflectionFactory
  {
    MetadataOnlyCommonType CreateSimpleType(
      MetadataOnlyModule scope,
      Token tokenTypeDef);

    MetadataOnlyCommonType CreateGenericType(
      MetadataOnlyModule scope,
      Token tokenTypeDef,
      Type[] typeArgs);

    MetadataOnlyCommonType CreateArrayType(
      MetadataOnlyCommonType elementType,
      int rank);

    MetadataOnlyCommonType CreateVectorType(MetadataOnlyCommonType elementType);

    MetadataOnlyCommonType CreateByRefType(MetadataOnlyCommonType type);

    MetadataOnlyCommonType CreatePointerType(MetadataOnlyCommonType type);

    MetadataOnlyTypeVariable CreateTypeVariable(
      MetadataOnlyModule resolver,
      Token typeVariableToken);

    MetadataOnlyFieldInfo CreateField(
      MetadataOnlyModule resolver,
      Token fieldDefToken,
      Type[] typeArgs,
      Type[] methodArgs);

    MetadataOnlyPropertyInfo CreatePropertyInfo(
      MetadataOnlyModule resolver,
      Token propToken,
      Type[] typeArgs,
      Type[] methodArgs);

    MetadataOnlyEventInfo CreateEventInfo(
      MetadataOnlyModule resolver,
      Token eventToken,
      Type[] typeArgs,
      Type[] methodArgs);

    MethodBase CreateMethodOrConstructor(
      MetadataOnlyModule resolver,
      Token methodToken,
      Type[] typeArgs,
      Type[] methodArgs);

    [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#")]
    bool TryCreateMethodBody(MetadataOnlyMethodInfo method, ref MethodBody body);

    Type CreateTypeRef(MetadataOnlyModule scope, Token tokenTypeRef);

    Type CreateTypeSpec(
      MetadataOnlyModule scope,
      Token tokenTypeRef,
      Type[] typeArgs,
      Type[] methodArgs);
  }
}
