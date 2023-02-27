// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashingPlatform
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

using FlashingPlatform1;
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
        public static Guid GuidDevinterfaceUfp;

        public unsafe IFlashingPlatform* Native => this.m_Platform;

    public static int ILogger { get; private set; }

    public unsafe FlashingPlatform(string LogFile)
    {
      uint num1;
      uint num2;
      int num3 = NativeFlashingPlatform.GetFlashingPlatformVersion(&num1, &num2);
      if (num3 >= 0)
        ;
      if ((int) num1 != (int) FlashingPlatform.MajorVerion || (int) num2 != (int) FlashingPlatform.MinorVerion)
        num3 = -2147019873;
      if (num3 >= 0)
        ;
      ushort* numPtr;
      if (LogFile != null)
        numPtr = (ushort*) Marshal.StringToCoTaskMemUni(LogFile).ToPointer();
      else
        numPtr = (ushort*) null;
    }

    private unsafe void UnFlashingPlatform()
    {
      if ((IntPtr) (CDeviceNotificationCallbackShim*) null != IntPtr.Zero)
        ;
      if ((IntPtr) this.m_Platform != IntPtr.Zero)
        this.m_Platform = (IFlashingPlatform*) null;
      this.m_DeviceNotificationCallback = (DeviceNotificationCallback) null;
    }

    public void GetVersion(out uint Major, out uint Minor)
    {
      Major = FlashingPlatform.MajorVerion;
      Minor = FlashingPlatform.MinorVerion;
    }

    public Logger GetLogger() => new Logger(this);

    public unsafe ConnectedDevice CreateConnectedDevice([In] string DevicePath)
    {
      ushort* numPtr = DevicePath == null ? (ushort*) null : (ushort*) Marshal.StringToCoTaskMemUni(DevicePath).ToPointer();
      ConnectedDevice connectedDevice;
      try
      {
        try
        {
          IFlashingPlatform* platform = this.m_Platform;
          int error = -1;
          if (error < 0)
          {
            try
            {
              IntPtr ptr = new IntPtr();
              throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
            }
            catch
            {
              throw;
            }
          }
          else
            connectedDevice = (ConnectedDevice) null;
        }
        catch
        {
          throw;
        }
      }
      catch
      {
        throw;
      }
      return connectedDevice;
    }

    private unsafe bool calli(object value1, IFlashingPlatform* ptr2, object value2) => throw new NotImplementedException();

    public unsafe FlashingDevice CreateFlashingDevice([In] string DevicePath)
    {
      ushort* numPtr = DevicePath == null ? (ushort*) null : (ushort*) Marshal.StringToCoTaskMemUni(DevicePath).ToPointer();
      FlashingDevice flashingDevice;
      try
      {
        try
        {
          if (-1 >= 0)
            ;
          flashingDevice = (FlashingDevice) null;
        }
        catch
        {
          throw;
        }
      }
      catch
      {
        throw;
      }
      return flashingDevice;
    }

    public unsafe ConnectedDeviceCollection GetConnectedDeviceCollection()
    {
      ConnectedDeviceCollection deviceCollection;
      try
      {
        IFlashingPlatform* platform1 = this.m_Platform;
        int error = -1;
        if (error < 0)
        {
          try
          {
            IFlashingPlatform* platform2 = this.m_Platform;
            IntPtr ptr = new IntPtr();
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          catch
          {
            throw;
          }
        }
        else
          deviceCollection = (ConnectedDeviceCollection) null;
      }
      catch
      {
        throw;
      }
      return deviceCollection;
    }

    public unsafe void RegisterDeviceNotificationCallback(
      Guid[] guids,
      string v,
      DeviceNotificationCallback Callback,
      ref DeviceNotificationCallback OldCallback)
    {
      CDeviceNotificationCallbackShim* notificationCallbackShimPtr1;
      if (Callback != null)
      {
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr2 = (CDeviceNotificationCallbackShim*) null;
        CDeviceNotificationCallbackShim* notificationCallbackShimPtr3;
        try
        {
          notificationCallbackShimPtr3 = (IntPtr) notificationCallbackShimPtr2 == IntPtr.Zero ? (CDeviceNotificationCallbackShim*) null : (CDeviceNotificationCallbackShim*) null;
        }
        catch
        {
          throw;
        }
        notificationCallbackShimPtr1 = notificationCallbackShimPtr3;
      }
      else
        notificationCallbackShimPtr1 = (CDeviceNotificationCallbackShim*) null;
      try
      {
        IFlashingPlatform* platform1 = this.m_Platform;
        int error = -1;
        if (error < 0)
        {
          try
          {
            IFlashingPlatform* platform2 = this.m_Platform;
            IntPtr ptr = new IntPtr();
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          catch
          {
            throw;
          }
        }
        else
        {
          if (OldCallback != null)
            OldCallback = this.m_DeviceNotificationCallback;
          this.m_DeviceNotificationCallback = Callback;
          this.m_DeviceNotificationCallbackShim = (CDeviceNotificationCallbackShim*) null;
        }
      }
      catch
      {
        throw;
      }
      try
      {
        try
        {
        }
        catch
        {
          throw;
        }
      }
      catch
      {
        throw;
      }
    }

    public unsafe string GetErrorMessage(int HResult)
    {
      IFlashingPlatform* platform = this.m_Platform;
      ushort* ptr = (ushort*) null;
      return (IntPtr) ptr == IntPtr.Zero ? (string) null : Marshal.PtrToStringUni((IntPtr) (void*) ptr);
    }

    public unsafe int Thor2ResultFromHResult(int HResult)
    {
      IFlashingPlatform* platform = this.m_Platform;
      return -1;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
        return;
      try
      {
        this.UnFlashingPlatform();
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
