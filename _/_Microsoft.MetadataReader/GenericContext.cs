// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.GenericContext
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class GenericContext
  {
    public GenericContext(Type[] typeArgs, Type[] methodArgs)
    {
      this.TypeArgs = typeArgs == null ? Type.EmptyTypes : typeArgs;
      this.MethodArgs = methodArgs == null ? Type.EmptyTypes : methodArgs;
    }

    public GenericContext(MethodBase methodTypeContext)
      : this(((MemberInfo) methodTypeContext).DeclaringType.GetGenericArguments(), methodTypeContext.GetGenericArguments())
    {
    }

    public Type[] TypeArgs { get; protected set; }

    public Type[] MethodArgs { get; protected set; }

    public override bool Equals(object obj)
    {
      GenericContext genericContext = (GenericContext) obj;
      return genericContext != null && GenericContext.IsArrayEqual<Type>(this.TypeArgs, genericContext.TypeArgs) && GenericContext.IsArrayEqual<Type>(this.MethodArgs, genericContext.MethodArgs);
    }

    public override int GetHashCode() => GenericContext.GetArrayHashCode<Type>(this.TypeArgs) * 32768 + GenericContext.GetArrayHashCode<Type>(this.MethodArgs);

    public virtual GenericContext VerifyAndUpdateMethodArguments(
      int expectedNumberOfMethodArgs)
    {
      if (this.MethodArgs.Length != expectedNumberOfMethodArgs)
        throw new ArgumentException(Resources.InvalidMetadataSignature);
      return this;
    }

    private static int GetArrayHashCode<T>(T[] a)
    {
      int arrayHashCode = 0;
      for (int index = 0; index < a.Length; ++index)
        arrayHashCode += a[index].GetHashCode() * index;
      return arrayHashCode;
    }

    private static bool IsArrayEqual<T>(T[] a1, T[] a2) where T : Type
    {
      if (a1.Length != a2.Length)
        return false;
      for (int index = 0; index < a1.Length; ++index)
      {
        if (!((Type) (object) a1[index]).Equals((Type) (object) a2[index]))
          return false;
      }
      return true;
    }

    private static string ArrayToString<T>(T[] a)
    {
      if (a == null)
        return "empty";
      StringBuilder builder = StringBuilderPool.Get();
      for (int index = 0; index < a.Length; ++index)
      {
        if (index != 0)
          builder.Append(",");
        builder.Append((object) a[index]);
      }
      string str = builder.ToString();
      StringBuilderPool.Release(ref builder);
      return str;
    }

    public override string ToString() => "Type: " + GenericContext.ArrayToString<Type>(this.TypeArgs) + ", Method: " + GenericContext.ArrayToString<Type>(this.MethodArgs);

    public GenericContext DeleteMethodArgs() => this.MethodArgs.Length == 0 ? this : new GenericContext(this.TypeArgs, (Type[]) null);

    public static bool IsNullOrEmptyMethodArgs(GenericContext context) => context == null || context.MethodArgs.Length == 0;

    public static bool IsNullOrEmptyTypeArgs(GenericContext context) => context == null || context.TypeArgs.Length == 0;
  }
}
