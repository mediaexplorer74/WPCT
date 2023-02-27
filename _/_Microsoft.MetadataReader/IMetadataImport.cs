// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.IMetadataImport
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System;
using System.Reflection;
using System.Reflection.Adds;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.MetadataReader
{
  [Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMetadataImport
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void CloseEnum(IntPtr hEnum);

    void CountEnum(HCORENUM hEnum, [ComAliasName("ULONG*")] out int pulCount);

    void ResetEnum(HCORENUM hEnum, int ulPos);

    void EnumTypeDefs(ref HCORENUM phEnum, [ComAliasName("mdTypeDef*")] out int rTypeDefs, uint cMax, [ComAliasName("ULONG*")] out uint pcTypeDefs);

    void EnumInterfaceImpls(
      ref HCORENUM phEnum,
      int td,
      out int rImpls,
      int cMax,
      ref int pcImpls);

    void EnumTypeRefs_();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindTypeDefByName([MarshalAs(UnmanagedType.LPWStr), In] string szTypeDef, [In] int tkEnclosingClass, [ComAliasName("mdTypeDef*")] out int token);

    void GetScopeProps([MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szName, [In] int cchName, [ComAliasName("ULONG*")] out int pchName, out Guid mvid);

    void GetModuleFromScope(out int mdModule);

    void GetTypeDefProps(
      [In] int td,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szTypeDef,
      [In] int cchTypeDef,
      [ComAliasName("ULONG*")] out int pchTypeDef,
      [MarshalAs(UnmanagedType.U4)] out TypeAttributes pdwTypeDefFlags,
      [ComAliasName("mdToken*")] out int ptkExtends);

    void GetInterfaceImplProps(int iiImpl, out int pClass, out int ptkIface);

    void GetTypeRefProps(
      int tr,
      [ComAliasName("mdToken*")] out int ptkResolutionScope,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szName,
      [In] int cchName,
      [ComAliasName("ULONG*")] out int pchName);

    void ResolveTypeRef_();

    void EnumMembers_();

    void EnumMembersWithName_();

    void EnumMethods(
      ref HCORENUM phEnum,
      int cl,
      [ComAliasName("mdMethodDef*")] out int mdMethodDef,
      int cMax,
      [ComAliasName("ULONG*")] out int pcTokens);

    void EnumMethodsWithName(
      ref HCORENUM phEnum,
      int cl,
      [MarshalAs(UnmanagedType.LPWStr), In] string szName,
      [ComAliasName("mdMethodDef*")] out int mdMethodDef,
      int cMax,
      [ComAliasName("ULONG*")] out int pcTokens);

    void EnumFields(ref HCORENUM phEnum, int cl, [ComAliasName("mdFieldDef*")] out int mdFieldDef, int cMax, [ComAliasName("ULONG*")] out uint pcTokens);

    void EnumFieldsWithName_();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumParams(
      ref HCORENUM phEnum,
      int mdMethodDef,
      [MarshalAs(UnmanagedType.LPArray)] int[] rParams,
      int cMax,
      [ComAliasName("ULONG*")] out uint pcTokens);

    void EnumMemberRefs_();

    void EnumMethodImpls(
      ref HCORENUM hEnum,
      Token typeDef,
      out Token methodBody,
      out Token methodDecl,
      int cMax,
      out int cTokens);

    void EnumPermissionSets_();

    void FindMember(
      [In] int typeDefToken,
      [MarshalAs(UnmanagedType.LPWStr), In] string szName,
      [In] byte[] pvSigBlob,
      [In] int cbSigBlob,
      out int memberDefToken);

    void FindMethod(
      [In] int typeDef,
      [MarshalAs(UnmanagedType.LPWStr), In] string szName,
      [In] EmbeddedBlobPointer pvSigBlob,
      [In] int cbSigBlob,
      out int methodDef);

    void FindField([In] int typeDef, [MarshalAs(UnmanagedType.LPWStr), In] string szName, [In] byte[] pvSigBlob, [In] int cbSigBlob, out int fieldDef);

    void FindMemberRef(
      [In] int typeRef,
      [MarshalAs(UnmanagedType.LPWStr), In] string szName,
      [In] byte[] pvSigBlob,
      [In] int cbSigBlob,
      out int result);

    void GetMethodProps(
      [In] uint md,
      [ComAliasName("mdTypeDef*")] out int pClass,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szMethod,
      [In] int cchMethod,
      [ComAliasName("ULONG*")] out uint pchMethod,
      [ComAliasName("DWORD*")] out MethodAttributes pdwAttr,
      [ComAliasName("PCCOR_SIGNATURE*")] out EmbeddedBlobPointer ppvSigBlob,
      [ComAliasName("ULONG*")] out uint pcbSigBlob,
      [ComAliasName("ULONG*")] out uint pulCodeRVA,
      [ComAliasName("DWORD*")] out uint pdwImplFlags);

    void GetMemberRefProps(
      [In] Token mr,
      [ComAliasName("mdMemberRef*")] out Token ptk,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szMember,
      [In] int cchMember,
      [ComAliasName("ULONG*")] out uint pchMember,
      [ComAliasName("PCCOR_SIGNATURE*")] out EmbeddedBlobPointer ppvSigBlob,
      [ComAliasName("ULONG*")] out uint pbSig);

    void EnumProperties(
      ref HCORENUM phEnum,
      int td,
      [ComAliasName("mdProperty*")] out int mdFieldDef,
      int cMax,
      [ComAliasName("ULONG*")] out uint pcTokens);

    void EnumEvents(ref HCORENUM phEnum, int td, [ComAliasName("mdEvent*")] out int mdFieldDef, int cMax, [ComAliasName("ULONG*")] out uint pcEvents);

    void GetEventProps(
      int ev,
      [ComAliasName("mdTypeDef*")] out int pClass,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szEvent,
      int cchEvent,
      [ComAliasName("ULONG*")] out int pchEvent,
      [ComAliasName("DWORD*")] out int pdwEventFlags,
      [ComAliasName("mdToken*")] out int ptkEventType,
      [ComAliasName("mdMethodDef*")] out int pmdAddOn,
      [ComAliasName("mdMethodDef*")] out int pmdRemoveOn,
      [ComAliasName("mdMethodDef*")] out int pmdFire,
      [ComAliasName("mdMethodDef*")] out int rmdOtherMethod,
      uint cMax,
      [ComAliasName("ULONG*")] out uint pcOtherMethod);

    void EnumMethodSemantics_();

    void GetMethodSemantics_();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetClassLayout(
      int typeDef,
      out uint dwPackSize,
      UnusedIntPtr zeroPtr,
      uint zeroCount,
      UnusedIntPtr zeroPtr2,
      ref uint ulClassSize);

    void GetFieldMarshal_();

    void GetRVA(int token, out uint rva, out uint flags);

    void GetPermissionSetProps_();

    void GetSigFromToken(int token, out EmbeddedBlobPointer pSig, out int cbSig);

    void GetModuleRefProps(int mur, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szName, int cchName, [ComAliasName("ULONG*")] out int pchName);

    void EnumModuleRefs(ref HCORENUM phEnum, [ComAliasName("mdModuleRef*")] out int mdModuleRef, int cMax, [ComAliasName("ULONG*")] out uint pcModuleRefs);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeSpecFromToken(Token typeSpec, out EmbeddedBlobPointer pSig, out int cbSig);

    void GetNameFromToken_();

    void EnumUnresolvedMethods_();

    void GetUserString([In] int stk, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] char[] szString, [In] int cchString, [ComAliasName("ULONG*")] out int pchString);

    void GetPinvokeMap_();

    void EnumSignatures_();

    void EnumTypeSpecs_();

    void EnumUserStrings_();

    void GetParamForMethodIndex_();

    void EnumCustomAttributes(
      ref HCORENUM phEnum,
      int tk,
      int tkType,
      [ComAliasName("mdCustomAttribute*")] out Token mdCustomAttribute,
      uint cMax,
      [ComAliasName("ULONG*")] out uint pcTokens);

    void GetCustomAttributeProps(
      [In] Token cv,
      out Token tkObj,
      out Token tkType,
      out EmbeddedBlobPointer blob,
      out int cbSize);

    void FindTypeRef([In] int tkResolutionScope, [MarshalAs(UnmanagedType.LPWStr), In] string szName, out int typeRef);

    void GetMemberProps_();

    void GetFieldProps(
      int mb,
      [ComAliasName("mdTypeDef*")] out int mdTypeDef,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szField,
      int cchField,
      [ComAliasName("ULONG*")] out int pchField,
      [ComAliasName("DWORD*")] out FieldAttributes pdwAttr,
      [ComAliasName("PCCOR_SIGNATURE*")] out EmbeddedBlobPointer ppvSigBlob,
      [ComAliasName("ULONG*")] out int pcbSigBlob,
      [ComAliasName("DWORD*")] out int pdwCPlusTypeFlab,
      [ComAliasName("UVCP_CONSTANT*")] out IntPtr ppValue,
      [ComAliasName("ULONG*")] out int pcchValue);

    void GetPropertyProps(
      Token prop,
      [ComAliasName("mdTypeDef*")] out Token pClass,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szProperty,
      int cchProperty,
      [ComAliasName("ULONG*")] out int pchProperty,
      [ComAliasName("DWORD*")] out PropertyAttributes pdwPropFlags,
      [ComAliasName("PCCOR_SIGNATURE*")] out EmbeddedBlobPointer ppvSig,
      [ComAliasName("ULONG*")] out int pbSig,
      [ComAliasName("DWORD*")] out int pdwCPlusTypeFlag,
      [ComAliasName("UVCP_CONSTANT*")] out UnusedIntPtr ppDefaultValue,
      [ComAliasName("ULONG*")] out int pcchDefaultValue,
      [ComAliasName("mdMethodDef*")] out Token pmdSetter,
      [ComAliasName("mdMethodDef*")] out Token pmdGetter,
      [ComAliasName("mdMethodDef*")] out Token rmdOtherMethod,
      uint cMax,
      [ComAliasName("ULONG*")] out uint pcOtherMethod);

    void GetParamProps(
      int tk,
      [ComAliasName("mdMethodDef*")] out int pmd,
      [ComAliasName("ULONG*")] out uint pulSequence,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder szName,
      uint cchName,
      [ComAliasName("ULONG*")] out uint pchName,
      [ComAliasName("DWORD*")] out uint pdwAttr,
      [ComAliasName("DWORD*")] out uint pdwCPlusTypeFlag,
      [ComAliasName("UVCP_CONSTANT*")] out UnusedIntPtr ppValue,
      [ComAliasName("ULONG*")] out uint pcchValue);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCustomAttributeByName(
      int tkObj,
      [MarshalAs(UnmanagedType.LPWStr)] string szName,
      out EmbeddedBlobPointer ppData,
      out uint pcbData);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    bool IsValidToken([MarshalAs(UnmanagedType.U4), In] uint tk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNestedClassProps(int tdNestedClass, [ComAliasName("mdTypeDef*")] out int tdEnclosingClass);

    void GetNativeCallConvFromSig_();

    void IsGlobal_();
  }
}
