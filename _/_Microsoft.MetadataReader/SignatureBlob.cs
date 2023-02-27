// Decompiled with JetBrains decompiler
// Type: Microsoft.MetadataReader.SignatureBlob
// Assembly: MetadataReader, Version=8.1.1702.2001, Culture=neutral, PublicKeyToken=b3f029d4c9c2ec30
// MVID: 33919EA0-1B03-4AB9-8728-74CEDD2180FC
// Assembly location: C:\Users\Admin\Desktop\d\MetadataReader.dll

using System.Reflection.Adds;

namespace Microsoft.MetadataReader
{
  internal class SignatureBlob
  {
    private readonly byte[] _signature;

    private SignatureBlob(byte[] data) => this._signature = data;

    public static SignatureBlob ReadSignature(
      MetadataFile storage,
      EmbeddedBlobPointer pointer,
      int countBytes)
    {
      return new SignatureBlob(storage.ReadEmbeddedBlob(pointer, countBytes));
    }

    public byte[] GetSignatureAsByteArray() => this._signature;
  }
}
