// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.MetadataOnlyMethodBodyWorker
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Adds;
using System.Reflection.Mock;
using System.Runtime.InteropServices;

namespace Microsoft.MetadataReader
{
  internal class MetadataOnlyMethodBodyWorker : MetadataOnlyMethodBody
  {
    private static readonly byte[] s_EmptyByteArray = new byte[0];
    private readonly MetadataOnlyMethodBodyWorker.IMethodHeader _header;

    public MetadataOnlyMethodBodyWorker(
      MetadataOnlyMethodInfo method,
      MetadataOnlyMethodBodyWorker.IMethodHeader header)
      : base(method)
    {
      this._header = header;
    }

    internal static MethodBody Create(MetadataOnlyMethodInfo method)
    {
      MetadataOnlyModule resolver = method.Resolver;
      uint methodRva = resolver.GetMethodRva(((MemberInfo) method).MetadataToken);
      if (methodRva == 0U)
        return (MethodBody) null;
      MetadataOnlyMethodBodyWorker.IMethodHeader methodHeader = MetadataOnlyMethodBodyWorker.GetMethodHeader(methodRva, resolver);
      return (MethodBody) new MetadataOnlyMethodBodyWorker(method, methodHeader);
    }

    public static MetadataOnlyMethodBodyWorker.IMethodHeader GetMethodHeader(
      uint rva,
      MetadataOnlyModule scope)
    {
      byte[] numArray = scope.RawMetadata.ReadRva((long) rva, 1);
      switch ((MetadataOnlyMethodBodyWorker.MethodHeaderFlags) ((int) numArray[0] & 3))
      {
        case MetadataOnlyMethodBodyWorker.MethodHeaderFlags.TinyFormat:
          return (MetadataOnlyMethodBodyWorker.IMethodHeader) new MetadataOnlyMethodBodyWorker.TinyHeader(numArray[0]);
        case MetadataOnlyMethodBodyWorker.MethodHeaderFlags.FatFormat:
          return (MetadataOnlyMethodBodyWorker.IMethodHeader) scope.RawMetadata.ReadRvaStruct<MetadataOnlyMethodBodyWorker.FatHeader>((long) rva);
        default:
          throw new InvalidOperationException(Resources.InvalidMetadata);
      }
    }

    public override IList<ExceptionHandlingClause> ExceptionHandlingClauses
    {
      get
      {
        if ((this._header.Flags & MetadataOnlyMethodBodyWorker.MethodHeaderFlags.MoreSects) == (MetadataOnlyMethodBodyWorker.MethodHeaderFlags) 0)
          return (IList<ExceptionHandlingClause>) new ExceptionHandlingClause[0];
        MetadataOnlyModule resolver = this.Method.Resolver;
        long num1 = ((long) resolver.GetMethodRva(((MemberInfo) this.Method).MetadataToken) + (long) this._header.HeaderSizeBytes + (long) this._header.CodeSize - 1L & -4L) + 4L;
        MetadataOnlyMethodBodyWorker.CorILMethod_Sect corIlMethodSect = (MetadataOnlyMethodBodyWorker.CorILMethod_Sect) resolver.RawMetadata.ReadRvaStruct<byte>(num1);
        if ((corIlMethodSect & ~(MetadataOnlyMethodBodyWorker.CorILMethod_Sect.EHTable | MetadataOnlyMethodBodyWorker.CorILMethod_Sect.FatFormat)) != 0)
          throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.UnsupportedExceptionFlags, new object[1]
          {
            (object) corIlMethodSect
          }));
        bool flag = (corIlMethodSect & MetadataOnlyMethodBodyWorker.CorILMethod_Sect.FatFormat) != 0;
        int num2;
        int num3;
        if (flag)
        {
          byte[] numArray = resolver.RawMetadata.ReadRva(num1 + 1L, 3);
          num2 = (int) numArray[0] + (int) numArray[1] * 256 + (int) numArray[2] * 256 * 256;
          num3 = 24;
        }
        else
        {
          num2 = (int) resolver.RawMetadata.ReadRvaStruct<byte>(num1 + 1L);
          num3 = 12;
        }
        int length = (num2 - 4) / num3;
        ExceptionHandlingClause[] array = (ExceptionHandlingClause[]) new MetadataOnlyMethodBodyWorker.ExceptionHandlingClauseWorker[length];
        long num4 = num1 + 4L;
        for (int index = 0; index < length; ++index)
        {
          MetadataOnlyMethodBodyWorker.IEHClause data = flag ? (MetadataOnlyMethodBodyWorker.IEHClause) resolver.RawMetadata.ReadRvaStruct<MetadataOnlyMethodBodyWorker.EHFat>(num4) : (MetadataOnlyMethodBodyWorker.IEHClause) resolver.RawMetadata.ReadRvaStruct<MetadataOnlyMethodBodyWorker.EHSmall>(num4);
          num4 += (long) num3;
          array[index] = (ExceptionHandlingClause) new MetadataOnlyMethodBodyWorker.ExceptionHandlingClauseWorker((MethodInfo) this.Method, data);
        }
        return (IList<ExceptionHandlingClause>) Array.AsReadOnly<ExceptionHandlingClause>(array);
      }
    }

    public override int MaxStackSize => this._header.MaxStack;

    public override byte[] GetILAsByteArray()
    {
      if (this._header.CodeSize == 0)
        return MetadataOnlyMethodBodyWorker.s_EmptyByteArray;
      MetadataOnlyModule resolver = this.Method.Resolver;
      uint methodRva = resolver.GetMethodRva(((MemberInfo) this.Method).MetadataToken);
      return resolver.RawMetadata.ReadRva((long) methodRva + (long) this._header.HeaderSizeBytes, this._header.CodeSize);
    }

    public override bool InitLocals => (this._header.Flags & MetadataOnlyMethodBodyWorker.MethodHeaderFlags.InitLocals) != 0;

    public override int LocalSignatureMetadataToken
    {
      get
      {
        Token localVarSigTok = this._header.LocalVarSigTok;
        return ((Token) ref localVarSigTok).Value;
      }
    }

    private class ExceptionHandlingClauseWorker : ExceptionHandlingClause
    {
      private readonly MethodInfo _method;
      private readonly MetadataOnlyMethodBodyWorker.IEHClause _data;

      public ExceptionHandlingClauseWorker(
        MethodInfo method,
        MetadataOnlyMethodBodyWorker.IEHClause data)
      {
        this._method = method;
        this._data = data;
      }

      public virtual Type CatchType => ((MemberInfo) this._method).Module.ResolveType(Token.op_Implicit(this._data.ClassToken), ((MemberInfo) this._method).DeclaringType.GetGenericArguments(), ((MethodBase) this._method).GetGenericArguments());

      public virtual int FilterOffset => this._data.FilterOffset;

      public virtual ExceptionHandlingClauseOptions Flags => this._data.Flags;

      public virtual int HandlerLength => this._data.HandlerLength;

      public virtual int HandlerOffset => this._data.HandlerOffset;

      public virtual int TryLength => this._data.TryLength;

      public virtual int TryOffset => this._data.TryOffset;
    }

    internal interface IMethodHeader
    {
      int MaxStack { get; }

      int CodeSize { get; }

      Token LocalVarSigTok { get; }

      MetadataOnlyMethodBodyWorker.MethodHeaderFlags Flags { get; }

      int HeaderSizeBytes { get; }
    }

    [Flags]
    internal enum MethodHeaderFlags
    {
      FatFormat = 3,
      TinyFormat = 2,
      MoreSects = 8,
      InitLocals = 16, // 0x00000010
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class TinyHeader : MetadataOnlyMethodBodyWorker.IMethodHeader
    {
      private readonly byte _flagsAndSize;

      public TinyHeader()
      {
      }

      public TinyHeader(byte data) => this._flagsAndSize = data;

      public MetadataOnlyMethodBodyWorker.MethodHeaderFlags Flags => (MetadataOnlyMethodBodyWorker.MethodHeaderFlags) ((int) this._flagsAndSize & 3);

      public int CodeSize => (int) this._flagsAndSize >> 2 & 63;

      public int MaxStack => 8;

      public Token LocalVarSigTok => Token.Nil;

      public int HeaderSizeBytes => 1;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class FatHeader : MetadataOnlyMethodBodyWorker.IMethodHeader
    {
      private readonly short _flagsAndSize;
      private readonly short _maxStack;
      private readonly uint _codeSize;
      private readonly uint _localVarSigTok;

      public MetadataOnlyMethodBodyWorker.MethodHeaderFlags Flags => (MetadataOnlyMethodBodyWorker.MethodHeaderFlags) ((int) this._flagsAndSize & 4095);

      public int MaxStack => (int) this._maxStack;

      public int CodeSize => (int) this._codeSize;

      public Token LocalVarSigTok => new Token(this._localVarSigTok);

      public int HeaderSizeBytes => ((int) this._flagsAndSize >> 12 & 15) * 4;
    }

    [Flags]
    private enum CorILMethod_Sect
    {
      EHTable = 1,
      OptILTable = 2,
      FatFormat = 64, // 0x00000040
      MoreSects = 128, // 0x00000080
    }

    private interface IEHClause
    {
      ExceptionHandlingClauseOptions Flags { get; }

      int TryOffset { get; }

      int TryLength { get; }

      int HandlerOffset { get; }

      int HandlerLength { get; }

      Token ClassToken { get; }

      int FilterOffset { get; }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class EHSmall : MetadataOnlyMethodBodyWorker.IEHClause
    {
      private readonly ushort _flags;
      private readonly ushort _tryOffset;
      private readonly byte _tryLength;
      private readonly byte _handlerOffset1;
      private readonly byte _handlerOffset2;
      private readonly byte _handlerLength;
      private readonly uint _classToken;

      ExceptionHandlingClauseOptions MetadataOnlyMethodBodyWorker.IEHClause.Flags => (ExceptionHandlingClauseOptions) this._flags;

      int MetadataOnlyMethodBodyWorker.IEHClause.TryOffset => (int) this._tryOffset;

      int MetadataOnlyMethodBodyWorker.IEHClause.TryLength => (int) this._tryLength;

      int MetadataOnlyMethodBodyWorker.IEHClause.HandlerOffset => (int) this._handlerOffset2 * 256 + (int) this._handlerOffset1;

      int MetadataOnlyMethodBodyWorker.IEHClause.HandlerLength => (int) this._handlerLength;

      Token MetadataOnlyMethodBodyWorker.IEHClause.ClassToken => new Token(this._classToken);

      int MetadataOnlyMethodBodyWorker.IEHClause.FilterOffset { get; }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class EHFat : MetadataOnlyMethodBodyWorker.IEHClause
    {
      private readonly uint _flags;
      private readonly uint _classToken;

      ExceptionHandlingClauseOptions MetadataOnlyMethodBodyWorker.IEHClause.Flags => (ExceptionHandlingClauseOptions) this._flags;

      int MetadataOnlyMethodBodyWorker.IEHClause.TryOffset { get; }

      int MetadataOnlyMethodBodyWorker.IEHClause.TryLength { get; }

      int MetadataOnlyMethodBodyWorker.IEHClause.HandlerOffset { get; }

      int MetadataOnlyMethodBodyWorker.IEHClause.HandlerLength { get; }

      Token MetadataOnlyMethodBodyWorker.IEHClause.ClassToken => new Token(this._classToken);

      int MetadataOnlyMethodBodyWorker.IEHClause.FilterOffset { get; }
    }
  }
}
