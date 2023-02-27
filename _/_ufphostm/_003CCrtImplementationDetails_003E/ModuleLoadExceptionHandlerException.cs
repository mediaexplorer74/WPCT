﻿// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadExceptionHandlerException
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using \u003CCrtImplementationDetails\u003E;
using System;
using System.Runtime.Serialization;

namespace \u003CCrtImplementationDetails\u003E
{
  [Serializable]
  internal class ModuleLoadExceptionHandlerException : ModuleLoadException
  {
    private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";
    private Exception \u003Cbacking_store\u003ENestedException;

    protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.NestedException = (Exception) info.GetValue(nameof (NestedException), typeof (Exception));
    }

    public ModuleLoadExceptionHandlerException(
      string message,
      Exception innerException,
      Exception nestedException)
      : base(message, innerException)
    {
      this.NestedException = nestedException;
    }

    public Exception NestedException
    {
      get => this.\u003Cbacking_store\u003ENestedException;
      set => this.\u003Cbacking_store\u003ENestedException = value;
    }

    public override string ToString()
    {
      string str1 = this.InnerException == null ? string.Empty : this.InnerException.ToString();
      string str2 = this.NestedException == null ? string.Empty : this.NestedException.ToString();
      object[] objArray = new object[4]
      {
        (object) this.GetType(),
        null,
        null,
        null
      };
      string str3 = this.Message == null ? string.Empty : this.Message;
      objArray[1] = (object) str3;
      string str4 = str1 == null ? string.Empty : str1;
      objArray[2] = (object) str4;
      string str5 = str2 == null ? string.Empty : str2;
      objArray[3] = (object) str5;
      return string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", objArray);
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("NestedException", (object) this.NestedException, typeof (Exception));
    }
  }
}
