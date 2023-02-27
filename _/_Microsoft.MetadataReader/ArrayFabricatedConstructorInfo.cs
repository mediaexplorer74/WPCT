// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.ArrayFabricatedConstructorInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;

namespace Microsoft.MetadataReader
{
  internal class ArrayFabricatedConstructorInfo : MetadataOnlyConstructorInfo
  {
    private readonly int _numParams;

    public ArrayFabricatedConstructorInfo(Type arrayType, int numParams)
      : base((MethodBase) ArrayFabricatedConstructorInfo.MakeMethodInfo(arrayType, numParams))
    {
      this._numParams = numParams;
    }

    private static MethodInfo MakeMethodInfo(Type arrayType, int numParams) => (MethodInfo) new ArrayFabricatedConstructorInfo.Adapter(arrayType, numParams);

    public override bool Equals(object obj) => obj is ArrayFabricatedConstructorInfo fabricatedConstructorInfo && ((MemberInfo) this).DeclaringType.Equals(((MemberInfo) fabricatedConstructorInfo).DeclaringType) && ((object) this).ToString().Equals(((object) fabricatedConstructorInfo).ToString());

    public override int GetHashCode() => ((object) ((MemberInfo) this).DeclaringType).GetHashCode() + this._numParams;

    public override object[] GetCustomAttributes(bool inherit) => new object[0];

    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => new object[0];

    private class Adapter : ArrayFabricatedMethodInfo
    {
      private readonly int _numParams;

      public Adapter(Type arrayType, int numParams)
        : base(arrayType)
      {
        this._numParams = numParams;
      }

      public virtual string Name => ".ctor";

      public virtual ParameterInfo[] GetParameters()
      {
        Type builtInType = this.Universe.GetBuiltInType((CorElementType) 8);
        ParameterInfo[] parameters = new ParameterInfo[this._numParams];
        for (int position = 0; position < this._numParams; ++position)
          parameters[position] = this.MakeParameterInfo(builtInType, position);
        return parameters;
      }

      public override MethodAttributes Attributes => MethodAttributes.Public | MethodAttributes.RTSpecialName;

      public virtual Type ReturnType => this.Universe.GetBuiltInType((CorElementType) 1);
    }
  }
}
