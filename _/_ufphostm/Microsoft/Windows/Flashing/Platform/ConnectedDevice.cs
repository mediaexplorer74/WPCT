// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.ConnectedDevice
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
  public class ConnectedDevice : IDisposable
  {
    private unsafe IConnectedDevice* m_Device;
    private bool m_OwnDevice;
    internal Microsoft.Windows.Flashing.Platform.FlashingPlatform m_Platform;

    internal unsafe ConnectedDevice(IConnectedDevice* Device, [In] Microsoft.Windows.Flashing.Platform.FlashingPlatform Platform)
    {
      this.m_Device = Device;
      this.m_OwnDevice = (IntPtr) Device != IntPtr.Zero;
      this.m_Platform = Platform;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    internal unsafe IConnectedDevice* Native => this.m_Device;

    internal unsafe void SetDevice(IConnectedDevice* Device) => this.m_Device = Device;

    private void \u007EConnectedDevice() => this.\u0021ConnectedDevice();

    private unsafe void \u0021ConnectedDevice()
    {
      IConnectedDevice* device = this.m_Device;
      if ((IntPtr) device == IntPtr.Zero)
        return;
      if (this.m_OwnDevice)
      {
        IConnectedDevice* iconnectedDevicePtr = device;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) iconnectedDevicePtr + 20))((IntPtr) iconnectedDevicePtr);
      }
      this.m_Device = (IConnectedDevice*) 0;
    }

    public virtual unsafe string GetDevicePath()
    {
      IConnectedDevice* device = this.m_Device;
      IConnectedDevice* iconnectedDevicePtr = device;
      ushort* ptr1;
      ref ushort* local1 = ref ptr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int error = __calli((__FnPtr<int (IntPtr, ushort**)>) *(int*) *(int*) device)((ushort**) iconnectedDevicePtr, (IntPtr) ref local1);
      if (error >= 0)
        return Marshal.PtrToStringUni((IntPtr) (void*) ptr1);
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DE\u0040GCEKIMOP\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAp\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAh\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
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

    public virtual unsafe void SendRawData([In] byte[] Message, uint MessageLength, uint Timeout)
    {
      CAutoDeleteArray\u003Cunsigned\u0020char\u003E arrayUnsignedChar;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &arrayUnsignedChar + 4) = (int) \u003CModule\u003E.new\u005B\u005D(MessageLength);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref arrayUnsignedChar = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040E\u0040RAII\u0040\u00406B\u0040;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: fault handler
      try
      {
        ref CAutoDeleteArray\u003Cunsigned\u0020char\u003E local1 = ref arrayUnsignedChar;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        IntPtr destination = (IntPtr) (void*) __calli((__FnPtr<byte* (IntPtr)>) *(int*) (^(int&) ref arrayUnsignedChar + 16))((IntPtr) ref local1);
        Marshal.Copy(Message, 0, destination, (int) MessageLength);
        IConnectedDevice* device = this.m_Device;
        IConnectedDevice* iconnectedDevicePtr = device;
        ref CAutoDeleteArray\u003Cunsigned\u0020char\u003E local2 = ref arrayUnsignedChar;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        byte* numPtr1 = __calli((__FnPtr<byte* (IntPtr)>) *(int*) (^(int&) ref arrayUnsignedChar + 16))((IntPtr) ref local2);
        int num1 = (int) MessageLength;
        int num2 = (int) Timeout;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, byte*, uint, uint)>) *(int*) (*(int*) device + 4))((uint) iconnectedDevicePtr, (uint) numPtr1, (byte*) num1, (IntPtr) num2);
        if (error < 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DA\u0040DIJJDJIP\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAs\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAw\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAa\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
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
              int num3 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
              int num4 = num3;
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local3);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num3 + 4))((ushort*) num4, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr2);
            }
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local4 = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local4);
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &unsignedShortConst);
          }
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D), (void*) &arrayUnsignedChar);
      }
      \u003CModule\u003E.RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D(&arrayUnsignedChar);
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
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D), (void*) &arrayUnsignedChar);
      }
    }

    public virtual unsafe void ReceiveRawData(
      out byte[] Message,
      ref uint MessageLength,
      uint Timeout)
    {
      uint length = MessageLength;
      CAutoDeleteArray\u003Cunsigned\u0020char\u003E arrayUnsignedChar;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &arrayUnsignedChar + 4) = (int) \u003CModule\u003E.new\u005B\u005D(MessageLength);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref arrayUnsignedChar = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoDeleteArray\u0040E\u0040RAII\u0040\u00406B\u0040;
      CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
      // ISSUE: fault handler
      try
      {
        IConnectedDevice* device = this.m_Device;
        IConnectedDevice* iconnectedDevicePtr = device;
        ref CAutoDeleteArray\u003Cunsigned\u0020char\u003E local1 = ref arrayUnsignedChar;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        byte* numPtr1 = __calli((__FnPtr<byte* (IntPtr)>) *(int*) (^(int&) ref arrayUnsignedChar + 16))((IntPtr) ref local1);
        ref uint local2 = ref length;
        int num1 = (int) Timeout;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, byte*, uint*, uint)>) *(int*) (*(int*) device + 8))((uint) iconnectedDevicePtr, (uint*) numPtr1, (byte*) ref local2, (IntPtr) num1);
        MessageLength = length;
        if (error < 0)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DG\u0040KNJJMPJN\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AAi\u003F\u0024AAv\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAw\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAa\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ());
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
              int num2 = (int) __calli((__FnPtr<ILogger* (IntPtr)>) *(int*) *(int*) native2)((IntPtr) native2);
              int num3 = num2;
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local3 = ref unsignedShortConst;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr2 = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local3);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num2 + 4))((ushort*) num3, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr2);
            }
            ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local4 = ref unsignedShortConst;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            IntPtr ptr = (IntPtr) (void*) __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local4);
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
          Message = new byte[(int) length];
          ref CAutoDeleteArray\u003Cunsigned\u0020char\u003E local5 = ref arrayUnsignedChar;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          Marshal.Copy((IntPtr) (void*) __calli((__FnPtr<byte* (IntPtr)>) *(int*) (^(int&) ref arrayUnsignedChar + 16))((IntPtr) ref local5), Message, 0, (int) MessageLength);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D), (void*) &arrayUnsignedChar);
      }
      \u003CModule\u003E.RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D(&arrayUnsignedChar);
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
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(RAII\u002ECAutoDeleteArray\u003Cunsigned\u0020char\u003E\u002E\u007Bdtor\u007D), (void*) &arrayUnsignedChar);
      }
    }

    [return: MarshalAs(UnmanagedType.U1)]
    public virtual unsafe bool CanFlash()
    {
      IConnectedDevice* device = this.m_Device;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      return __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) device + 12))((IntPtr) device) != 0;
    }

    public virtual unsafe FlashingDevice CreateFlashingDevice()
    {
      CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E platformIflashingDevice;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &platformIflashingDevice + 4) = 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref platformIflashingDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIFlashingDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      FlashingDevice flashingDevice;
      // ISSUE: fault handler
      try
      {
        IConnectedDevice* device = this.m_Device;
        IConnectedDevice* iconnectedDevicePtr = device;
        ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E local1 = ref platformIflashingDevice;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        IFlashingDevice** iflashingDevicePtr = __calli((__FnPtr<IFlashingDevice** (IntPtr)>) *(int*) (^(int&) ref platformIflashingDevice + 4))((IntPtr) ref local1);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, IFlashingDevice**)>) *(int*) (*(int*) device + 16))((IFlashingDevice**) iconnectedDevicePtr, (IntPtr) iflashingDevicePtr);
        if (error < 0)
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1EC\u0040LEPNEJDA\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAf\u003F\u0024AAl\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AAh\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u0040, __arglist ());
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
        else
        {
          ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIFlashingDevice\u0020\u002A\u003E local4 = ref platformIflashingDevice;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          flashingDevice = new FlashingDevice(__calli((__FnPtr<IFlashingDevice* (IntPtr)>) *(int*) (^(int&) ref platformIflashingDevice + 40))((IntPtr) ref local4), this.m_Platform);
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
      return flashingDevice;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        this.\u007EConnectedDevice();
      }
      else
      {
        try
        {
          this.\u0021ConnectedDevice();
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

    ~ConnectedDevice() => this.Dispose(false);
  }
}
