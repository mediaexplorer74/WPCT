// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashingDevice
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
  public class FlashingDevice : ConnectedDevice
  {
    private unsafe IFlashingDevice* m_Device;

    internal unsafe FlashingDevice(IFlashingDevice* Device, [In] Microsoft.Windows.Flashing.Platform.FlashingPlatform Platform)
    {
      this.m_Device = Device;
      // ISSUE: explicit constructor call
      base.\u002Ector((IConnectedDevice*) 0, Platform);
      // ISSUE: fault handler
      try
      {
        this.SetDevice((IConnectedDevice*) Device);
      }
      __fault
      {
        base.Dispose(true);
      }
    }

    internal unsafe IFlashingDevice* Native => this.m_Device;

    private void \u007EFlashingDevice() => this.\u0021FlashingDevice();

    private unsafe void \u0021FlashingDevice()
    {
      IFlashingDevice* device = this.m_Device;
      if ((IntPtr) device == IntPtr.Zero)
        return;
      IFlashingDevice* iflashingDevicePtr = device;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) iflashingDevicePtr + 20))((IntPtr) iflashingDevicePtr);
      this.m_Device = (IFlashingDevice*) 0;
    }

    public unsafe string GetDeviceFriendlyName()
    {
      IFlashingDevice* device = this.m_Device;
      IFlashingDevice* iflashingDevicePtr = device;
      ushort* ptr1;
      ref ushort* local1 = ref ptr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr, ushort**)>) *(int*) (*(int*) device + 24))((ushort**) iflashingDevicePtr, (IntPtr) ref local1);
      if (error >= 0)
        return Marshal.PtrToStringUni((IntPtr) (void*) ptr1);
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EG\u0040CCFOHDEM\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAl\u003F\u0024AAy\u003F\u0024AA\u003F5\u003F\u0024AAn\u003F\u0024AAa\u0040, __arglist ());
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
      // ISSUE: fault handler
      try
      {
        IFlashingPlatform* native1 = this.m_Platform.Native;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
        {
          IFlashingPlatform* native2 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
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
        IntPtr ptr2 = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local3);
        throw new Win32Exception(error, Marshal.PtrToStringUni(ptr2));
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
      }
    }

    public unsafe Guid GetDeviceUniqueID()
    {
      IFlashingDevice* device = this.m_Device;
      IFlashingDevice* iflashingDevicePtr = device;
      _GUID guid;
      ref _GUID local1 = ref guid;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr, _GUID*)>) *(int*) (*(int*) device + 28))((_GUID*) iflashingDevicePtr, (IntPtr) ref local1);
      if (error >= 0)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        return new Guid((uint) ^(int&) ref guid, ^(ushort&) ((IntPtr) &guid + 4), ^(ushort&) ((IntPtr) &guid + 6), ^(byte&) ((IntPtr) &guid + 8), ^(byte&) ((IntPtr) &guid + 9), ^(byte&) ((IntPtr) &guid + 10), ^(byte&) ((IntPtr) &guid + 11), ^(byte&) ((IntPtr) &guid + 12), ^(byte&) ((IntPtr) &guid + 13), ^(byte&) ((IntPtr) &guid + 14), ^(byte&) ((IntPtr) &guid + 15));
      }
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DO\u0040FGPACCJK\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAu\u003F\u0024AAn\u003F\u0024AAi\u003F\u0024AAq\u003F\u0024AAu\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAI\u003F\u0024AAD\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
      // ISSUE: fault handler
      try
      {
        IFlashingPlatform* native1 = this.m_Platform.Native;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
        {
          IFlashingPlatform* native2 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
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

    public unsafe Guid GetDeviceSerialNumber()
    {
      IFlashingDevice* device = this.m_Device;
      IFlashingDevice* iflashingDevicePtr = device;
      _GUID guid;
      ref _GUID local1 = ref guid;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr, _GUID*)>) *(int*) (*(int*) device + 32))((_GUID*) iflashingDevicePtr, (IntPtr) ref local1);
      if (error >= 0)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        return new Guid((uint) ^(int&) ref guid, ^(ushort&) ((IntPtr) &guid + 4), ^(ushort&) ((IntPtr) &guid + 6), ^(byte&) ((IntPtr) &guid + 8), ^(byte&) ((IntPtr) &guid + 9), ^(byte&) ((IntPtr) &guid + 10), ^(byte&) ((IntPtr) &guid + 11), ^(byte&) ((IntPtr) &guid + 12), ^(byte&) ((IntPtr) &guid + 13), ^(byte&) ((IntPtr) &guid + 14), ^(byte&) ((IntPtr) &guid + 15));
      }
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EG\u0040FNHIMFAE\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAs\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAa\u003F\u0024AAl\u003F\u0024AA\u003F5\u003F\u0024AAn\u003F\u0024AAu\u003F\u0024AAm\u003F\u0024AAb\u0040, __arglist ());
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
      // ISSUE: fault handler
      try
      {
        IFlashingPlatform* native1 = this.m_Platform.Native;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
        {
          IFlashingPlatform* native2 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
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

    public unsafe void WriteWim([In] string WimPath, GenericProgress Progress)
    {
      ushort* numPtr1 = WimPath == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(WimPath).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      CAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E cgenericProgressShim;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
      // ISSUE: fault handler
      try
      {
        CGenericProgressShim* cgenericProgressShimPtr1 = (CGenericProgressShim*) \u003CModule\u003E.@new(8U);
        CGenericProgressShim* cgenericProgressShimPtr2;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) cgenericProgressShimPtr1 != IntPtr.Zero)
          {
            *(int*) cgenericProgressShimPtr1 = (int) &\u003CModule\u003E.\u003F\u003F_7CGenericProgressShim\u0040\u00406B\u0040;
            *(int*) ((IntPtr) cgenericProgressShimPtr1 + 4) = (int) ((IntPtr) GCHandle.Alloc((object) Progress)).ToPointer();
            cgenericProgressShimPtr2 = cgenericProgressShimPtr1;
          }
          else
            cgenericProgressShimPtr2 = (CGenericProgressShim*) 0;
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) cgenericProgressShimPtr1);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &cgenericProgressShim + 4) = (int) cgenericProgressShimPtr2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref cgenericProgressShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCGenericProgressShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingDevice* device = this.m_Device;
          IFlashingDevice* iflashingDevicePtr = device;
          ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
          ref CAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E local2 = ref cgenericProgressShim;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          CGenericProgressShim* cgenericProgressShimPtr3 = __calli((__FnPtr<CGenericProgressShim* (IntPtr)>) *(int*) (^(int&) ref cgenericProgressShim + 16))((IntPtr) ref local2);
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int error = __calli((__FnPtr<int (IntPtr, ushort*, IGenericProgress*)>) *(int*) (*(int*) device + 36))((IGenericProgress*) iflashingDevicePtr, numPtr2, (IntPtr) cgenericProgressShimPtr3);
          if (error < 0)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CI\u0040ECIEBNIH\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAw\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAW\u003F\u0024AAI\u003F\u0024AAM\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
            // ISSUE: fault handler
            try
            {
              IFlashingPlatform* native1 = this.m_Platform.Native;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
              {
                IFlashingPlatform* native2 = this.m_Platform.Native;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
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
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &cgenericProgressShim);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref cgenericProgressShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCGenericProgressShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        \u003CModule\u003E.RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002ERelease(&cgenericProgressShim);
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
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &cgenericProgressShim);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
      }
    }

    public unsafe void SkipTransfer()
    {
      IFlashingDevice* device = this.m_Device;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) device + 40))((IntPtr) device);
      if (error < 0)
      {
        CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DA\u0040FFAALIKO\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAs\u003F\u0024AAk\u003F\u0024AAi\u003F\u0024AAp\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAn\u003F\u0024AAs\u003F\u0024AAf\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingPlatform* native1 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
          {
            IFlashingPlatform* native2 = this.m_Platform.Native;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
            int num2 = num1;
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
          }
          ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local1 = ref unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local1);
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
        // ISSUE: fault handler
        try
        {
        }
        __fault
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
        }
      }
    }

    public unsafe void Reboot()
    {
      IFlashingDevice* device = this.m_Device;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) device + 44))((IntPtr) device);
      if (error < 0)
      {
        CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CC\u0040KCIGDDEL\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAb\u003F\u0024AAo\u003F\u0024AAo\u003F\u0024AAt\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingPlatform* native1 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
          {
            IFlashingPlatform* native2 = this.m_Platform.Native;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
            int num2 = num1;
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
          }
          ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local1 = ref unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local1);
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
        // ISSUE: fault handler
        try
        {
        }
        __fault
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
        }
      }
    }

    public unsafe void EnterMassStorageMode()
    {
      IFlashingDevice* device = this.m_Device;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) device + 48))((IntPtr) device);
      if (error < 0)
      {
        CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EE\u0040BNENPODH\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA\u003F5\u003F\u0024AAm\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAs\u003F\u0024AA\u003F5\u003F\u0024AAs\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAm\u003F\u0024AAo\u003F\u0024AAd\u0040, __arglist ());
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref unsignedShortConst = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IFlashingPlatform* native1 = this.m_Platform.Native;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
          {
            IFlashingPlatform* native2 = this.m_Platform.Native;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num1 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
            int num2 = num1;
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local);
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num1 + 4))((ushort*) num2, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
          }
          ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local1 = ref unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local1);
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
        // ISSUE: fault handler
        try
        {
        }
        __fault
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
        }
      }
    }

    public unsafe void SetBootMode(uint BootMode, string ProfileName)
    {
      ushort* numPtr1 = ProfileName == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(ProfileName).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
      // ISSUE: fault handler
      try
      {
        IFlashingDevice* device = this.m_Device;
        IFlashingDevice* iflashingDevicePtr = device;
        int num1 = (int) BootMode;
        ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, uint, ushort*)>) *(int*) (*(int*) device + 52))((ushort*) iflashingDevicePtr, (uint) num1, (IntPtr) numPtr2);
        if (error < 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DA\u0040MBNNDMOJ\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAs\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAb\u003F\u0024AAo\u003F\u0024AAo\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAm\u003F\u0024AAo\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
          // ISSUE: fault handler
          try
          {
            IFlashingPlatform* native1 = this.m_Platform.Native;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
            {
              IFlashingPlatform* native2 = this.m_Platform.Native;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              int num2 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
              int num3 = num2;
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local2 = ref unsignedShortConst2;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr3 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local2);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num2 + 4))((ushort*) num3, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr3);
            }
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst2;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local3);
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst2);
          }
        }
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
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
      }
    }

    public unsafe void FlashFFUFile(
      [In] string FFUFilePath,
      FlashFlags Flags,
      GenericProgress Progress,
      HandleRef CancelEvent)
    {
      ushort* numPtr1 = FFUFilePath == null ? (ushort*) 0 : (ushort*) Marshal.StringToCoTaskMemUni(FFUFilePath).ToPointer();
      CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E unsignedShortConst1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst1 + 4) = (int) numPtr1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref unsignedShortConst1 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoComFree\u0040PBG\u0040RAII\u0040\u00406B\u0040;
      CAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E cgenericProgressShim;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst2;
      // ISSUE: fault handler
      try
      {
        CGenericProgressShim* cgenericProgressShimPtr1 = (CGenericProgressShim*) \u003CModule\u003E.@new(8U);
        CGenericProgressShim* cgenericProgressShimPtr2;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) cgenericProgressShimPtr1 != IntPtr.Zero)
          {
            *(int*) cgenericProgressShimPtr1 = (int) &\u003CModule\u003E.\u003F\u003F_7CGenericProgressShim\u0040\u00406B\u0040;
            *(int*) ((IntPtr) cgenericProgressShimPtr1 + 4) = (int) ((IntPtr) GCHandle.Alloc((object) Progress)).ToPointer();
            cgenericProgressShimPtr2 = cgenericProgressShimPtr1;
          }
          else
            cgenericProgressShimPtr2 = (CGenericProgressShim*) 0;
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) cgenericProgressShimPtr1);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ((IntPtr) &cgenericProgressShim + 4) = (int) cgenericProgressShimPtr2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref cgenericProgressShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCGenericProgressShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        // ISSUE: fault handler
        try
        {
          IntPtr handle = CancelEvent.Handle;
          IFlashingDevice* device = this.m_Device;
          IFlashingDevice* iflashingDevicePtr = device;
          ref CAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E local1 = ref unsignedShortConst1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst1 + 16))((IntPtr) ref local1);
          int num1 = (int) Flags;
          ref CAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E local2 = ref cgenericProgressShim;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          CGenericProgressShim* cgenericProgressShimPtr3 = __calli((__FnPtr<CGenericProgressShim* (IntPtr)>) *(int*) (^(int&) ref cgenericProgressShim + 16))((IntPtr) ref local2);
          void* voidPtr = (void*) handle;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int error = __calli((__FnPtr<int (IntPtr, ushort*, uint, IGenericProgress*, void*)>) *(int*) (*(int*) device + 56))((void*) iflashingDevicePtr, (IGenericProgress*) numPtr2, (uint) num1, (ushort*) cgenericProgressShimPtr3, (IntPtr) voidPtr);
          if (error < 0)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &unsignedShortConst2 + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DC\u0040PGNKAIIP\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAh\u003F\u0024AA\u003F5\u003F\u0024AAF\u003F\u0024AAF\u003F\u0024AAU\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref unsignedShortConst2 = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040\u0024\u0024CBG\u0040RAII\u0040\u00406B\u0040;
            // ISSUE: fault handler
            try
            {
              IFlashingPlatform* native1 = this.m_Platform.Native;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              if ((IntPtr) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native1)((IntPtr) native1) != IntPtr.Zero)
              {
                IFlashingPlatform* native2 = this.m_Platform.Native;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num2 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
                int num3 = num2;
                ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst2;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                ushort* numPtr3 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst2 + 16))((IntPtr) ref local3);
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num2 + 4))((ushort*) num3, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr3);
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
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &cgenericProgressShim);
        }
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(int&) ref cgenericProgressShim = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDelete\u0040PAVCGenericProgressShim\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
        \u003CModule\u003E.RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002ERelease(&cgenericProgressShim);
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
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDelete\u003CCGenericProgressShim\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &cgenericProgressShim);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoComFree\u003Cunsigned\u0020short\u0020const\u0020\u002A\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst1);
      }
    }

    [HandleProcessCorruptedStateExceptions]
    protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        try
        {
          this.\u007EFlashingDevice();
        }
        finally
        {
          base.Dispose(true);
        }
      }
      else
      {
        try
        {
          this.\u0021FlashingDevice();
        }
        finally
        {
          base.Dispose(false);
        }
      }
    }
  }
}
