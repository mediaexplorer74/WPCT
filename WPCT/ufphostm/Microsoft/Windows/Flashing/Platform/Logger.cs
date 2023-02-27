// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.Logger
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using FlashingPlatform;
using RAII;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class Logger : IDisposable
  {
    private unsafe ILogger* m_Logger;
    internal Microsoft.Windows.Flashing.Platform.FlashingPlatform m_Platform;

    internal unsafe Logger([In] Microsoft.Windows.Flashing.Platform.FlashingPlatform Platform)
    {
      IFlashingPlatform* native = Platform.Native;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      this.m_Logger = __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native)((IntPtr) native);
      this.m_Platform = Platform;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    internal unsafe ILogger* Native => this.m_Logger;

    private unsafe void \u007ELogger() => this.m_Logger = (ILogger*) 0;

    private unsafe void \u0021Logger() => this.m_Logger = (ILogger*) 0;

    public unsafe void SetLogLevel(LogLevel Level)
    {
      ILogger* logger = this.m_Logger;
      ILogger* iloggerPtr = logger;
      int num1 = (int) Level;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num2 = __calli((__FnPtr<int (IntPtr, FlashingPlatform.LogLevel)>) *(int*) *(int*) logger)((FlashingPlatform.LogLevel) iloggerPtr, (IntPtr) num1);
    }

    public unsafe void Log(LogLevel Level, [In] string Message)
    {
      ushort* numPtr1 = Message == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(Message).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      // ISSUE: fault handler
      try
      {
        ILogger* logger = this.m_Logger;
        ILogger* iloggerPtr = logger;
        int num = (int) Level;
        ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local = ref unsignedShortConst;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) logger + 4))((ushort*) iloggerPtr, (FlashingPlatform.LogLevel) num, (IntPtr) numPtr2);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
      }
      \u003CModule\u003E.RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D(&unsignedShortConst);
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual unsafe void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        this.m_Logger = (ILogger*) 0;
      }
      else
      {
        try
        {
          this.m_Logger = (ILogger*) 0;
        }
        finally
        {
          // ISSUE: explicit finalizer call
          base.Finalize();
        }
      }
    }

    public virtual void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~Logger() => this.Dispose(false);
  }
}
