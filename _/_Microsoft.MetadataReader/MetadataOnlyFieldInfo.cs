// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyFieldInfo
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyFieldInfo : FieldInfo, IFieldInfo2
  {
    private readonly MetadataOnlyModule _resolver;
    private readonly int _fieldDefToken;
    private readonly int _declaringClassToken;
    private Type _fieldType;
    private GenericContext _context;
    private string _name;
    private readonly int _nameLength;
    private CustomModifiers _customModifiers;
    private bool _initialized;

    public MetadataOnlyFieldInfo(
      MetadataOnlyModule resolver,
      Token fieldDefToken,
      Type[] typeArgs,
      Type[] methodArgs)
    {
      this._resolver = resolver;
      this._fieldDefToken = Token.op_Implicit(fieldDefToken);
      if (typeArgs != null || methodArgs != null)
        this._context = new GenericContext(typeArgs, methodArgs);
      FieldAttributes pdwAttr;
      this._resolver.RawImport.GetFieldProps(this._fieldDefToken, out this._declaringClassToken, (StringBuilder) null, 0, out this._nameLength, out pdwAttr, out EmbeddedBlobPointer _, out int _, out int _, out IntPtr _, out int _);
      this.Attributes = pdwAttr;
    }

    private void InitializeName()
    {
      if (!string.IsNullOrEmpty(this._name))
        return;
      IMetadataImport rawImport = this._resolver.RawImport;
      StringBuilder builder = StringBuilderPool.Get(this._nameLength);
      rawImport.GetFieldProps(this._fieldDefToken, out int _, builder, builder.Capacity, out int _, out FieldAttributes _, out EmbeddedBlobPointer _, out int _, out int _, out IntPtr _, out int _);
      this._name = builder.ToString();
      StringBuilderPool.Release(ref builder);
    }

    private void Initialize()
    {
      if (this._initialized)
        return;
      EmbeddedBlobPointer ppvSigBlob;
      int pcbSigBlob;
      this._resolver.RawImport.GetFieldProps(this._fieldDefToken, out int _, (StringBuilder) null, 0, out int _, out FieldAttributes _, out ppvSigBlob, out pcbSigBlob, out int _, out IntPtr _, out int _);
      byte[] sig = this._resolver.ReadEmbeddedBlob(ppvSigBlob, pcbSigBlob);
      int index = 0;
      SignatureUtil.ExtractCallingConvention(sig, ref index);
      this._customModifiers = SignatureUtil.ExtractCustomModifiers(sig, ref index, this._resolver, this._context);
      if (this._resolver.RawImport.IsValidToken((uint) this._declaringClassToken))
      {
        Type type = this._resolver.ResolveType(this._declaringClassToken);
        if (type.IsGenericType && (this._context == null || this._context.TypeArgs == null || this._context.TypeArgs.Length == 0))
          this._context = this._context != null ? new GenericContext(type.GetGenericArguments(), this._context.MethodArgs) : new GenericContext(type.GetGenericArguments(), (Type[]) null);
      }
      this._fieldType = SignatureUtil.ExtractType(sig, ref index, this._resolver, this._context);
      this._initialized = true;
    }

    public virtual string ToString() => MetadataOnlyCommonType.TypeSigToString(base.FieldType) + " " + ((MemberInfo) this).Name;

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    private object ParseDefaultValue()
    {
      this.Initialize();
      EmbeddedBlobPointer ppvSigBlob;
      int pcbSigBlob;
      int pdwCPlusTypeFlab;
      IntPtr ppValue;
      int pcchValue;
      this._resolver.RawImport.GetFieldProps(this._fieldDefToken, out int _, (StringBuilder) null, 0, out int _, out FieldAttributes _, out ppvSigBlob, out pcbSigBlob, out pdwCPlusTypeFlab, out ppValue, out pcchValue);
      byte[] sig = this._resolver.ReadEmbeddedBlob(ppvSigBlob, pcbSigBlob);
      int index = 0;
      SignatureUtil.ExtractCallingConvention(sig, ref index);
      CorElementType corElementType = SignatureUtil.ExtractElementType(sig, ref index);
      if (corElementType == 17)
      {
        SignatureUtil.ExtractToken(sig, ref index);
        corElementType = (CorElementType) pdwCPlusTypeFlab;
      }
      else if (corElementType == 21)
      {
        SignatureUtil.ExtractType(sig, ref index, this._resolver, this._context);
        corElementType = (CorElementType) pdwCPlusTypeFlab;
      }
      switch (corElementType - 2)
      {
        case 0:
          return Marshal.ReadByte(ppValue) == (byte) 0 ? (object) false : (object) true;
        case 1:
          return (object) (char) Marshal.ReadInt16(ppValue);
        case 2:
          return (object) (sbyte) Marshal.ReadByte(ppValue);
        case 3:
          return (object) Marshal.ReadByte(ppValue);
        case 4:
          return (object) Marshal.ReadInt16(ppValue);
        case 5:
          return (object) (ushort) Marshal.ReadInt16(ppValue);
        case 6:
          return (object) Marshal.ReadInt32(ppValue);
        case 7:
          return (object) (uint) Marshal.ReadInt32(ppValue);
        case 8:
          return (object) Marshal.ReadInt64(ppValue);
        case 9:
          return (object) (ulong) Marshal.ReadInt64(ppValue);
        case 10:
          float[] destination1 = new float[1];
          Marshal.Copy(ppValue, destination1, 0, 1);
          return (object) destination1[0];
        case 11:
          double[] destination2 = new double[1];
          Marshal.Copy(ppValue, destination2, 0, 1);
          return (object) destination2[0];
        case 12:
          return (object) Marshal.PtrToStringAuto(ppValue, pcchValue);
        case 16:
          return (object) null;
        case 22:
          return (object) Marshal.ReadIntPtr(ppValue);
        default:
          throw new InvalidOperationException(Resources.IncorrectElementTypeValue);
      }
    }

    public virtual FieldAttributes Attributes { get; }

    public virtual MemberTypes MemberType => MemberTypes.Field;

    public virtual string Name
    {
      get
      {
        this.InitializeName();
        return this._name;
      }
    }

    public virtual object[] GetCustomAttributes(bool inherit) => throw new NotSupportedException();

    public virtual object[] GetCustomAttributes(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual bool IsDefined(Type attributeType, bool inherit) => throw new NotSupportedException();

    public virtual Type ReflectedType => throw new NotSupportedException();

    public virtual Type[] GetOptionalCustomModifiers()
    {
      this.Initialize();
      return this._customModifiers == null ? Type.EmptyTypes : this._customModifiers.OptionalCustomModifiers;
    }

    public virtual Type[] GetRequiredCustomModifiers()
    {
      this.Initialize();
      return this._customModifiers == null ? Type.EmptyTypes : this._customModifiers.RequiredCustomModifiers;
    }

    public virtual Type FieldType
    {
      get
      {
        this.Initialize();
        return this._fieldType;
      }
    }

    public virtual Type DeclaringType
    {
      get
      {
        this.Initialize();
        return this._resolver.GetGenericType(new Token(this._declaringClassToken), this._context);
      }
    }

    public virtual object GetValue(object obj) => throw new NotSupportedException();

    public virtual byte[] GetRvaField()
    {
      if ((base.Attributes & FieldAttributes.HasFieldRVA) == FieldAttributes.PrivateScope)
        throw new InvalidOperationException(Resources.OperationValidOnRVAFieldsOnly);
      StructLayoutAttribute structLayoutAttribute = base.FieldType.StructLayoutAttribute;
      if (structLayoutAttribute.Value == LayoutKind.Auto)
        throw new InvalidOperationException(Resources.OperationInvalidOnAutoLayoutFields);
      uint rva;
      this._resolver.RawImport.GetRVA(((MemberInfo) this).MetadataToken, out rva, out uint _);
      int num = structLayoutAttribute.Size;
      if (num == 0)
      {
        switch (Type.GetTypeCode(base.FieldType))
        {
          case TypeCode.Int32:
            num = 4;
            break;
          case TypeCode.Int64:
            num = 8;
            break;
        }
      }
      return this._resolver.RawMetadata.ReadRva((long) rva, num);
    }

    public virtual object GetRawConstantValue()
    {
      if (!this.IsLiteral)
        throw new InvalidOperationException(Resources.OperationValidOnLiteralFieldsOnly);
      return this.ParseDefaultValue();
    }

    public virtual RuntimeFieldHandle FieldHandle => throw new NotSupportedException();

    public virtual void SetValue(
      object obj,
      object value,
      BindingFlags invokeAttr,
      Binder binder,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }

    public virtual int MetadataToken => this._fieldDefToken;

    public virtual Module Module => (Module) this._resolver;

    public virtual bool Equals(object obj) => obj is MetadataOnlyFieldInfo metadataOnlyFieldInfo && ((object) metadataOnlyFieldInfo._resolver).Equals((object) this._resolver) && metadataOnlyFieldInfo._fieldDefToken.Equals(this._fieldDefToken) && ((MemberInfo) this).DeclaringType.Equals(((MemberInfo) metadataOnlyFieldInfo).DeclaringType);

    public virtual int GetHashCode() => ((object) this._resolver).GetHashCode() * (int) short.MaxValue + this._fieldDefToken.GetHashCode();

    public virtual IList<CustomAttributeData> GetCustomAttributesData() => this._resolver.GetCustomAttributeData(((MemberInfo) this).MetadataToken);
  }
}
