// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.Logger
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

using FlashingPlatform1;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class Logger : IDisposable
  {
    private unsafe ILogger* m_Logger;
    internal FlashingPlatform m_Platform;

    public unsafe Logger([In] FlashingPlatform Platform)
    {
      IFlashingPlatform* native = Platform.Native;
    }

    internal unsafe ILogger* Native => this.m_Logger;

    public unsafe void SetLogLevel(LogLevel Level)
    {
      ILogger* logger = this.m_Logger;
    }

    public unsafe void Log(LogLevel Level, [In] string Message)
    {
      ushort* numPtr;
      if (Message != null)
        numPtr = (ushort*) Marshal.StringToCoTaskMemUni(Message).ToPointer();
      else
        numPtr = (ushort*) null;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
        return;
      try
      {
      }
      finally
      {
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
