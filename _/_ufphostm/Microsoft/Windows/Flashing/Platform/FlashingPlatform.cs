// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashingPlatform
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using FlashingPlatform;
using RAII;
using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class FlashingPlatform : IDisposable
  {
    private unsafe IFlashingPlatform* m_Platform;
    private DeviceNotificationCallback m_DeviceNotificationCallback;
    private unsafe CDeviceNotificationCallbackShim* m_DeviceNotificationCallbackShim;
    public static uint MajorVerion = 0;
    public static uint MinorVerion = 2;

    internal unsafe IFlashingPlatform* Native => this.m_Platform;

    public unsafe FlashingPlatform(string LogFile)
    {
      uint num1;
      uint num2;
      int error = NativeFlashingPlatform.GetFlashingPlatformVersion(&num1, &num2);
      if (error < 0)
      {
        CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1FA\u0040DFPBFODB\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAh\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AA\u003F5\u003F\u0024AAp\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAf\u003F\u0024AAo\u003F\u0024AAr\u003F\u0024AAm\u003F\u0024AA\u003F5\u0040, __arglist ());
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local = ref unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
          throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
        }
      }
      else
      {
        if ((int) num1 != (int) Microsoft.Windows.Flashing.Platform.FlashingPlatform.MajorVerion || (int) num2 != (int) Microsoft.Windows.Flashing.Platform.FlashingPlatform.MinorVerion)
          error = -2147019873;
        if (error < 0)
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1KE\u0040NICNNOJH\u0040\u003F\u0024AAM\u003F\u0024AAi\u003F\u0024AAs\u003F\u0024AAm\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAc\u003F\u0024AAh\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAv\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAs\u003F\u0024AAi\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AA\u003F5\u003F\u0024AAo\u003F\u0024AAf\u003F\u0024AA\u003F5\u003F\u0024AAn\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAi\u003F\u0024AAv\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u0040, __arglist ((int) Microsoft.Windows.Flashing.Platform.FlashingPlatform.MajorVerion, (int) Microsoft.Windows.Flashing.Platform.FlashingPlatform.MinorVerion, (int) num1, (int) num2));
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
          // ISSUE: fault handler
          try
          {
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
          }
        }
        else
        {
          ushort* numPtr = LogFile == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(LogFile).ToPointer();
          CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
          CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E iflashingPlatform;
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
          // ISSUE: fault handler
          try
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &iflashingPlatform + 4) = 0;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref iflashingPlatform = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIFlashingPlatform\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
            // ISSUE: fault handler
            try
            {
              ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* A_0 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
              ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E local2 = ref iflashingPlatform;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              IFlashingPlatform** A_1 = __calli((__FnPtr<IFlashingPlatform** (IntPtr)>) *(int*) (^(int&) ref iflashingPlatform + 4))((IntPtr) ref local2);
              int flashingPlatform = NativeFlashingPlatform.CreateFlashingPlatform(A_0, A_1);
              if (flashingPlatform < 0)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EG\u0040HDIJANGN\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAh\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AA\u003F5\u003F\u0024AAp\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAf\u003F\u0024AAo\u0040, __arglist ());
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
                // ISSUE: fault handler
                try
                {
                  ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst2;
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  // ISSUE: cast to a function pointer type
                  // ISSUE: function pointer call
                  IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local3);
                  throw new Win32Exception(flashingPlatform, Marshal.PtrToStringUni(ptr));
                }
                __fault
                {
                  // ISSUE: method pointer
                  // ISSUE: cast to a function pointer type
                  \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst2);
                }
              }
              else
              {
                ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E local4 = ref iflashingPlatform;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                this.m_Platform = __calli((__FnPtr<IFlashingPlatform* (IntPtr)>) *(int*) (^(int&) ref iflashingPlatform + 40))((IntPtr) ref local4);
                this.m_DeviceNotificationCallbackShim = (CDeviceNotificationCallbackShim*) 0;
                this.m_DeviceNotificationCallback = (DeviceNotificationCallback) null;
              }
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &iflashingPlatform);
            }
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref iflashingPlatform = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIFlashingPlatform\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
            \u003CModule\u003E.RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E\u002ERelease(&iflashingPlatform);
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
          }
          \u003CModule\u003E.RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D(&unsignedShortConst1);
          // ISSUE: fault handler
          try
          {
            // ISSUE: fault handler
            try
            {
              // ISSUE: fault handler
              try
              {
              }
              __fault
              {
                // ISSUE: method pointer
                // ISSUE: cast to a function pointer type
                \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst2);
              }
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingPlatform\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &iflashingPlatform);
            }
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
          }
        }
      }
    }

    private void \u007EFlashingPlatform() => this.\u0021FlashingPlatform();

    private unsafe void \u0021FlashingPlatform()
    {
      CDeviceNotificationCallbackShim* notificationCallbackShim = this.m_DeviceNotificationCallbackShim;
      if ((IntPtr) notificationCallbackShim != IntPtr.Zero)
      {
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr = notificationCallbackShim;
        \u003CModule\u003E.gcroot\u003CMicrosoft\u003A\u003AWindows\u003A\u003AFlashing\u003A\u003APlatform\u003A\u003ADeviceNotificationCallback\u0020\u005E\u003E\u002E\u007Bdtor\u007D((gcroot\u003CMicrosoft\u003A\u003AWindows\u003A\u003AFlashing\u003A\u003APlatform\u003A\u003ADeviceNotificationCallback\u0020\u005E\u003E*) ((IntPtr) notificationCallbackShimPtr + 4));
        \u003CModule\u003E.delete((void*) notificationCallbackShimPtr);
        this.m_DeviceNotificationCallbackShim = (CDeviceNotificationCallbackShim*) 0;
      }
      IFlashingPlatform* platform = this.m_Platform;
      if ((IntPtr) platform != IntPtr.Zero)
      {
        IFlashingPlatform* iflashingPlatformPtr = platform;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) iflashingPlatformPtr + 28))((IntPtr) iflashingPlatformPtr);
        this.m_Platform = (IFlashingPlatform*) 0;
      }
      this.m_DeviceNotificationCallback = (DeviceNotificationCallback) null;
    }

    public void GetVersion(out uint Major, out uint Minor)
    {
      Major = Microsoft.Windows.Flashing.Platform.FlashingPlatform.MajorVerion;
      Minor = Microsoft.Windows.Flashing.Platform.FlashingPlatform.MinorVerion;
    }

    public Logger GetLogger() => new Logger(this);

    public unsafe ConnectedDevice CreateConnectedDevice([In] string DevicePath)
    {
      ushort* numPtr1 = DevicePath == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(DevicePath).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      ConnectedDevice connectedDevice;
      // ISSUE: fault handler
      try
      {
        CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E iconnectedDevice;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &iconnectedDevice + 4) = 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref iconnectedDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIConnectedDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingPlatform* platform1 = this.m_Platform;
          IFlashingPlatform* iflashingPlatformPtr = platform1;
          ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
          ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E local2 = ref iconnectedDevice;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IConnectedDevice** iconnectedDevicePtr = __calli((__FnPtr<IConnectedDevice** (IntPtr)>) *(int*) (^(int&) ref iconnectedDevice + 4))((IntPtr) ref local2);
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int error = __calli((__FnPtr<int (IntPtr, ushort*, IConnectedDevice**)>) *(int*) (*(int*) platform1 + 4))((IConnectedDevice**) iflashingPlatformPtr, numPtr2, (IntPtr) iconnectedDevicePtr);
          if (error < 0)
          {
            CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EE\u0040GEIPDOOK\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AAn\u003F\u0024AAe\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u0040, __arglist ());
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
            // ISSUE: fault handler
            try
            {
              IFlashingPlatform* platform2 = this.m_Platform;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform2)((IntPtr) platform2) != IntPtr.Zero)
              {
                IFlashingPlatform* platform3 = this.m_Platform;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform3)((IntPtr) platform3);
                int num2 = num1;
                ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst2;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                ushort* numPtr3 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local3);
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr3);
              }
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local4 = ref unsignedShortConst2;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local4);
              throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst2);
            }
          }
          else
          {
            ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E local5 = ref iconnectedDevice;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            connectedDevice = new ConnectedDevice(__calli((__FnPtr<IConnectedDevice* (IntPtr)>) *(int*) (^(int&) ref iconnectedDevice + 40))((IntPtr) ref local5), this);
          }
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &iconnectedDevice);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref iconnectedDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIConnectedDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        \u003CModule\u003E.RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E\u002ERelease(&iconnectedDevice);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
      }
      \u003CModule\u003E.RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D(&unsignedShortConst1);
      return connectedDevice;
    }

    public unsafe FlashingDevice CreateFlashingDevice([In] string DevicePath)
    {
      ushort* numPtr1 = DevicePath == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(DevicePath).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      FlashingDevice flashingDevice;
      // ISSUE: fault handler
      try
      {
        CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E platformIflashingDevice;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &platformIflashingDevice + 4) = 0;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref platformIflashingDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIFlashingDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingPlatform* platform1 = this.m_Platform;
          IFlashingPlatform* iflashingPlatformPtr = platform1;
          ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
          ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E local2 = ref platformIflashingDevice;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IFlashingDevice** iflashingDevicePtr = __calli((__FnPtr<IFlashingDevice** (IntPtr)>) *(int*) (^(int&) ref platformIflashingDevice + 4))((IntPtr) ref local2);
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int error = __calli((__FnPtr<int (IntPtr, ushort*, IFlashingDevice**)>) *(int*) (*(int*) platform1 + 8))((IFlashingDevice**) iflashingPlatformPtr, numPtr2, (IntPtr) iflashingDevicePtr);
          if (error < 0)
          {
            CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EC\u0040LEPNEJDA\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAh\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u0040, __arglist ());
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
            // ISSUE: fault handler
            try
            {
              IFlashingPlatform* platform2 = this.m_Platform;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform2)((IntPtr) platform2) != IntPtr.Zero)
              {
                IFlashingPlatform* platform3 = this.m_Platform;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform3)((IntPtr) platform3);
                int num2 = num1;
                ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst2;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                ushort* numPtr3 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local3);
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr3);
              }
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local4 = ref unsignedShortConst2;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local4);
              throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst2);
            }
          }
          else
          {
            ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E local5 = ref platformIflashingDevice;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            flashingDevice = new FlashingDevice(__calli((__FnPtr<IFlashingDevice* (IntPtr)>) *(int*) (^(int&) ref platformIflashingDevice + 40))((IntPtr) ref local5), this);
          }
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &platformIflashingDevice);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref platformIflashingDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIFlashingDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        \u003CModule\u003E.RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E\u002ERelease(&platformIflashingDevice);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
      }
      \u003CModule\u003E.RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D(&unsignedShortConst1);
      return flashingDevice;
    }

    public unsafe ConnectedDeviceCollection GetConnectedDeviceCollection()
    {
      CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDeviceCollection\u0020\u002A\u003E deviceCollection1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &deviceCollection1 + 4) = 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref deviceCollection1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIConnectedDeviceCollection\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      ConnectedDeviceCollection deviceCollection2;
      // ISSUE: fault handler
      try
      {
        IFlashingPlatform* platform1 = this.m_Platform;
        IFlashingPlatform* iflashingPlatformPtr = platform1;
        ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDeviceCollection\u0020\u002A\u003E local1 = ref deviceCollection1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        IConnectedDeviceCollection** deviceCollectionPtr = __calli((__FnPtr<IConnectedDeviceCollection** (IntPtr)>) *(int*) (^(int&) ref deviceCollection1 + 4))((IntPtr) ref local1);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, IConnectedDeviceCollection**)>) *(int*) (*(int*) platform1 + 12))((IConnectedDeviceCollection**) iflashingPlatformPtr, (IntPtr) deviceCollectionPtr);
        if (error < 0)
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1FE\u0040BCHEDBFP\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AAn\u003F\u0024AAe\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAc\u0040, __arglist ());
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
          // ISSUE: fault handler
          try
          {
            IFlashingPlatform* platform2 = this.m_Platform;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform2)((IntPtr) platform2) != IntPtr.Zero)
            {
              IFlashingPlatform* platform3 = this.m_Platform;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform3)((IntPtr) platform3);
              int num2 = num1;
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local2 = ref unsignedShortConst;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local2);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
            }
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local3);
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
          }
        }
        else
        {
          ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDeviceCollection\u0020\u002A\u003E local4 = ref deviceCollection1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          deviceCollection2 = new ConnectedDeviceCollection(__calli((__FnPtr<IConnectedDeviceCollection* (IntPtr)>) *(int*) (^(int&) ref deviceCollection1 + 40))((IntPtr) ref local4), this);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDeviceCollection\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &deviceCollection1);
      }
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref deviceCollection1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIConnectedDeviceCollection\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      \u003CModule\u003E.RAII\u002ECAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDeviceCollection\u0020\u002A\u003E\u002ERelease(&deviceCollection1);
      return deviceCollection2;
    }

    public unsafe void RegisterDeviceNotificationCallback(
      DeviceNotificationCallback Callback,
      ref DeviceNotificationCallback OldCallback)
    {
      CDeviceNotificationCallbackShim* notificationCallbackShimPtr1;
      if (Callback != null)
      {
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr2 = (CDeviceNotificationCallbackShim*) \u003CModule\u003E.@new(8U);
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr3;
        // ISSUE: fault handler
        try
        {
          notificationCallbackShimPtr3 = (IntPtr) notificationCallbackShimPtr2 == IntPtr.Zero ? (CDeviceNotificationCallbackShim*) 0 : \u003CModule\u003E.CDeviceNotificationCallbackShim\u002E\u007Bctor\u007D(notificationCallbackShimPtr2, Callback);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) notificationCallbackShimPtr2);
        }
        notificationCallbackShimPtr1 = notificationCallbackShimPtr3;
      }
      else
        notificationCallbackShimPtr1 = (CDeviceNotificationCallbackShim*) 0;
      CAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E notificationCallbackShim;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &notificationCallbackShim + 4) = (int) notificationCallbackShimPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref notificationCallbackShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCDeviceNotificationCallbackShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: fault handler
      try
      {
        IFlashingPlatform* platform1 = this.m_Platform;
        IFlashingPlatform* iflashingPlatformPtr = platform1;
        ref CAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E local1 = ref notificationCallbackShim;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr4 = __calli((__FnPtr<CDeviceNotificationCallbackShim* (IntPtr)>) *(int*) (^(int&) ref notificationCallbackShim + 16))((IntPtr) ref local1);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, IDeviceNotificationCallback*, IDeviceNotificationCallback**)>) *(int*) (*(int*) platform1 + 16))((IDeviceNotificationCallback**) iflashingPlatformPtr, (IDeviceNotificationCallback*) notificationCallbackShimPtr4, IntPtr.Zero);
        if (error < 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1GA\u0040LFCCFCBB\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAg\u003F\u0024AAi\u003F\u0024AAs\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAn\u003F\u0024AAo\u003F\u0024AAt\u003F\u0024AAi\u003F\u0024AAf\u003F\u0024AAi\u0040, __arglist ());
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
          // ISSUE: fault handler
          try
          {
            IFlashingPlatform* platform2 = this.m_Platform;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform2)((IntPtr) platform2) != IntPtr.Zero)
            {
              IFlashingPlatform* platform3 = this.m_Platform;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) platform3)((IntPtr) platform3);
              int num2 = num1;
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local2 = ref unsignedShortConst;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local2);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
            }
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local3);
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
          }
        }
        else
        {
          if (OldCallback != null)
            OldCallback = this.m_DeviceNotificationCallback;
          this.m_DeviceNotificationCallback = Callback;
          ref CAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E local4 = ref notificationCallbackShim;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          this.m_DeviceNotificationCallbackShim = __calli((__FnPtr<CDeviceNotificationCallbackShim* (IntPtr)>) *(int*) (^(int&) ref notificationCallbackShim + 40))((IntPtr) ref local4);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &notificationCallbackShim);
      }
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref notificationCallbackShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCDeviceNotificationCallbackShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      \u003CModule\u003E.RAII\u002ECAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E\u002ERelease(&notificationCallbackShim);
      // ISSUE: fault handler
      try
      {
        // ISSUE: fault handler
        try
        {
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCDeviceNotificationCallbackShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &notificationCallbackShim);
      }
    }

    public unsafe string GetErrorMessage(int HResult)
    {
      IFlashingPlatform* platform = this.m_Platform;
      IFlashingPlatform* iflashingPlatformPtr = platform;
      int num = HResult;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      ushort* ptr = __calli((__FnPtr<ushort* (IntPtr, int)>) *(int*) (*(int*) platform + 20))((int) iflashingPlatformPtr, (IntPtr) num);
      return (IntPtr) ptr != IntPtr.Zero ? Marshal.PtrToStringUni((IntPtr) (void*) ptr) : (string) null;
    }

    public unsafe int Thor2ResultFromHResult(int HResult)
    {
      IFlashingPlatform* platform = this.m_Platform;
      IFlashingPlatform* iflashingPlatformPtr = platform;
      int num = HResult;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      return __calli((__FnPtr<int (IntPtr, int)>) *(int*) (*(int*) platform + 24))((int) iflashingPlatformPtr, (IntPtr) num);
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        this.\u007EFlashingPlatform();
      }
      else
      {
        try
        {
          this.\u0021FlashingPlatform();
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

    ~FlashingPlatform() => this.Dispose(false);
  }
}
