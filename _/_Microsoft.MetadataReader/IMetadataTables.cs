// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.IMetadataTables
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;
using System.Runtime.InteropServices;

namespace Microsoft.MetadataReader
{
  [Guid("D8F579AB-402D-4b8e-82D9-5D63B1065C68")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMetadataTables
  {
    void GetStringHeapSize(out uint countBytesStrings);

    void GetBlobHeapSize(out uint countBytesBlobs);

    void GetGuidHeapSize(out uint countBytesGuids);

    void GetUserStringHeapSize(out uint countByteBlobs);

    void GetNumTables(out uint countTables);

    void GetTableIndex(uint token, out uint tableIndex);

    void GetTableInfo(
      MetadataTable tableIndex,
      out int countByteRows,
      out int countRows,
      out int countColumns,
      out int columnPrimaryKey,
      out UnusedIntPtr name);

    void GetColumnInfo_();

    void GetCodedTokenInfo_();

    void GetRow_();

    void GetColumn_();

    void GetString_();

    void GetBlob_();

    void GetGuid_();

    void GetUserString_();

    void GetNextString_();

    void GetNextBlob_();

    void GetNextGuid_();

    void GetNextUserString_();
  }
}
