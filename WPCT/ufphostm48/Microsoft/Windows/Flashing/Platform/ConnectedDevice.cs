// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.ConnectedDevice
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
  public class ConnectedDevice : IDisposable
  {
    private unsafe IConnectedDevice* m_Device;
    public FlashingPlatform m_Platform;
    private bool m_OwnDevice;
    private static unsafe IConnectedDevice* Device;

    internal unsafe ConnectedDevice(IConnectedDevice* Device, [In] FlashingPlatform Platform)
    {
      this.m_OwnDevice = (IntPtr) Device != IntPtr.Zero;
      this.m_Platform = Platform;
    }

    internal unsafe IConnectedDevice* Native => this.m_Device;

    internal unsafe void SetDevice(IConnectedDevice* Device) => this.m_Device = Device;

    private void UnConnectedDevice()
    {
    }

    private unsafe void UnConnectedDevice1()
    {
      if ((IntPtr) this.m_Device == IntPtr.Zero)
        return;
      if (this.m_OwnDevice)
        ;
      this.m_Device = (IConnectedDevice*) null;
    }

    public virtual unsafe string GetDevicePath()
    {
      IConnectedDevice* device = this.m_Device;
      int error = -1;
      if (error >= 0)
        return (string) null;
      try
      {
        IFlashingPlatform* native = this.m_Platform.Native;
        IntPtr ptr = new IntPtr();
        throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
      }
      catch
      {
        throw;
      }
    }

    public virtual unsafe void SendRawData([In] byte[] Message, uint MessageLength, uint Timeout)
    {
      try
      {
        IntPtr destination = new IntPtr();
        Marshal.Copy(Message, 0, destination, (int) MessageLength);
        IConnectedDevice* device = this.m_Device;
        int error = -1;
        if (error < 0)
        {
          try
          {
            IFlashingPlatform* native = this.m_Platform.Native;
            IntPtr ptr = new IntPtr();
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          catch
          {
            throw;
          }
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

    public virtual unsafe void ReceiveRawData(
      out byte[] Message,
      ref uint MessageLength,
      uint Timeout)
    {
      uint length = MessageLength;
      try
      {
        IConnectedDevice* device = this.m_Device;
        int error = -1;
        MessageLength = length;
        if (error < 0)
        {
          try
          {
            IFlashingPlatform* native = this.m_Platform.Native;
            IntPtr ptr = new IntPtr();
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          catch
          {
            throw;
          }
        }
        else
          Message = new byte[(int) length];
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

    [return: MarshalAs(UnmanagedType.U1)]
    public virtual unsafe bool CanFlash()
    {
      IConnectedDevice* device = this.m_Device;
      return false;
    }

    public virtual unsafe FlashingDevice CreateFlashingDevice()
    {
      FlashingDevice flashingDevice;
      try
      {
        IConnectedDevice* device = this.m_Device;
        int error = -1;
        if (error < 0)
        {
          try
          {
            IFlashingPlatform* native = this.m_Platform.Native;
            IntPtr ptr = new IntPtr();
            throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
          }
          catch
          {
            throw;
          }
        }
        else
          flashingDevice = (FlashingDevice) null;
      }
      catch
      {
        throw;
      }
      return flashingDevice;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        this.UnConnectedDevice();
      }
      else
      {
        try
        {
          this.UnConnectedDevice();
        }
        finally
        {
        }
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }//class end

  public enum DeviceLogType
  {
  }
    public class UNLOCK_TOKEN_FILES
    {
        public int TokenIdBitmask;
    }

    public class UNLOCK_ID
    {
        public string UnlockId;
        public string OemId;
        public string PlatformId;
    }
}
