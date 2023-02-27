// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.OpenGenericContext
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class OpenGenericContext : GenericContext
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly Token _ownerMethod;

    public OpenGenericContext(Type[] typeArgs, Type[] methodArgs)
      : base(typeArgs, methodArgs)
    {
    }

    public OpenGenericContext(MetadataOnlyModule resolver, Type ownerType, Token ownerMethod)
      : base((Type[]) null, (Type[]) null)
    {
      this._resolver = resolver;
      this._ownerMethod = ownerMethod;
      int length = ownerType.GetGenericArguments().Length;
      Type[] typeArray = new Type[length];
      Token ownerToken;
      // ISSUE: explicit constructor call
      ((Token) ref ownerToken).\u002Ector(((MemberInfo) ownerType).MetadataToken);
      for (int position = 0; position < length; ++position)
        typeArray[position] = (Type) new MetadataOnlyTypeVariableRef(resolver, ownerToken, position);
      this.TypeArgs = typeArray;
    }

    public override GenericContext VerifyAndUpdateMethodArguments(
      int expectedNumberOfMethodArgs)
    {
      if (expectedNumberOfMethodArgs == this.MethodArgs.Length)
        return (GenericContext) this;
      Type[] methodArgs = new Type[expectedNumberOfMethodArgs];
      for (int position = 0; position < expectedNumberOfMethodArgs; ++position)
        methodArgs[position] = (Type) new MetadataOnlyTypeVariableRef(this._resolver, this._ownerMethod, position);
      return (GenericContext) new OpenGenericContext(this.TypeArgs, methodArgs);
    }
  }
}
