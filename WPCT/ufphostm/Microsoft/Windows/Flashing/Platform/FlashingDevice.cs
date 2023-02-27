// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.FlashingDevice
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
  public class FlashingDevice : ConnectedDevice
  {
    public unsafe IFlashingDevice* m_Device;

    public unsafe FlashingDevice(IFlashingDevice* Device, [In] FlashingPlatform Platform)
      : base((IConnectedDevice*) null, Platform)
    {
      try
      {
        this.SetDevice((IConnectedDevice*) Device);
      }
      catch
      {
        base.Dispose(true);
        throw;
      }
    }

    public unsafe IFlashingDevice* Native => this.m_Device;

    public static unsafe IFlashingDevice* Device { get; private set; }

    public unsafe void UnFlashingDevice()
    {
      if ((IntPtr) this.m_Device == IntPtr.Zero)
        return;
      this.m_Device = (IFlashingDevice*) null;
    }

    public unsafe object calli(
      int int32,
      object value1,
      object value2,
      IFlashingDevice* ptr,
      object value3)
    {
      throw new NotImplementedException();
    }

    public unsafe string GetDeviceFriendlyName()
    {
      IFlashingDevice* device = this.m_Device;
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

    public unsafe Guid GetDeviceUniqueID()
    {
      IFlashingDevice* device = this.m_Device;
      int error = -1;
      if (error >= 0)
        return new Guid();
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

    public unsafe Guid GetDeviceSerialNumber()
    {
      IFlashingDevice* device = this.m_Device;
      int error = -1;
      if (error >= 0)
        return new Guid();
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

    public unsafe void WriteWim([In] string WimPath, GenericProgress Progress)
    {
      ushort* numPtr = WimPath == null ? (ushort*) null : (ushort*) Marshal.StringToCoTaskMemUni(WimPath).ToPointer();
      try
      {
        CGenericProgressShim* cgenericProgressShimPtr1 = (CGenericProgressShim*) null;
        try
        {
          CGenericProgressShim* cgenericProgressShimPtr2;
          if ((IntPtr) cgenericProgressShimPtr1 != IntPtr.Zero)
          {
            *(int*) (cgenericProgressShimPtr1 + 4 / sizeof (CGenericProgressShim)) = 0;
            cgenericProgressShimPtr2 = cgenericProgressShimPtr1;
          }
          else
            cgenericProgressShimPtr2 = (CGenericProgressShim*) null;
        }
        catch
        {
          throw;
        }
        try
        {
          IFlashingDevice* device = this.m_Device;
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
      }
      catch
      {
        throw;
      }
      try
      {
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
      catch
      {
        throw;
      }
    }

    public unsafe void SkipTransfer()
    {
      IFlashingDevice* device = this.m_Device;
      int error = -1;
      if (error >= 0)
        return;
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

    public unsafe void Reboot()
    {
      IFlashingDevice* device = this.m_Device;
      int error = -1;
      if (error >= 0)
        return;
      try
      {
        IFlashingPlatform* native = this.m_Platform.Native;
        IntPtr ptr = new IntPtr();
        throw new Win32Exception(error, Marshal.PtrToStringUni(ptr));
      }
      catch
      {
      }
    }

    public unsafe void EnterMassStorageMode()
    {
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
      {
        try
        {
        }
        catch
        {
          throw;
        }
      }
    }

    public unsafe void SetBootMode(uint BootMode, string ProfileName)
    {
      ushort* numPtr = ProfileName == null ? (ushort*) null : (ushort*) Marshal.StringToCoTaskMemUni(ProfileName).ToPointer();
      try
      {
        IFlashingDevice* device = this.m_Device;
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

    public unsafe void FlashFFUFile(
      [In] string FFUFilePath,
      FlashFlags Flags,
      GenericProgress Progress,
      HandleRef CancelEvent)
    {
      IntPtr num;
      ushort* numPtr;
      if (FFUFilePath != null)
      {
        num = Marshal.StringToCoTaskMemUni(FFUFilePath);
        numPtr = (ushort*) num.ToPointer();
      }
      else
        numPtr = (ushort*) null;
      try
      {
        CGenericProgressShim* cgenericProgressShimPtr1 = (CGenericProgressShim*) null;
        try
        {
          CGenericProgressShim* cgenericProgressShimPtr2;
          if ((IntPtr) cgenericProgressShimPtr1 != IntPtr.Zero)
          {
            *(int*) cgenericProgressShimPtr1 = 0;
            CGenericProgressShim* cgenericProgressShimPtr3 = cgenericProgressShimPtr1 + 4 / sizeof (CGenericProgressShim);
            num = (IntPtr) GCHandle.Alloc((object) Progress);
            int pointer = (int) num.ToPointer();
            *(int*) cgenericProgressShimPtr3 = pointer;
            cgenericProgressShimPtr2 = cgenericProgressShimPtr1;
          }
          else
            cgenericProgressShimPtr2 = (CGenericProgressShim*) null;
        }
        catch
        {
          throw;
        }
        try
        {
          IntPtr handle = CancelEvent.Handle;
          IFlashingDevice* device = this.m_Device;
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
      try
      {
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
      catch
      {
        throw;
      }
    }

    [HandleProcessCorruptedStateExceptions]
    protected new void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        try
        {
          this.UnFlashingDevice();
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
          this.UnFlashingDevice();
        }
        finally
        {
          base.Dispose(false);
        }
      }
    }

        public string GetLogs(DeviceLogType deviceLogType, string logFolderPath)
        {
            throw new NotImplementedException();
        }

        public UNLOCK_ID GetDeviceUnlockID()
        {
            throw new NotImplementedException();
        }

        public void RelockDevice()
        {
            throw new NotImplementedException();
        }

        public UNLOCK_TOKEN_FILES QueryUnlockTokenFiles()
        {
            throw new NotImplementedException();
        }

        public int GetBitlockerState()
        {
            throw new NotImplementedException();
        }

        public void UnlockDevice(uint tokenId, string tokenFilePath, string pin)
        {
            throw new NotImplementedException();
        }

        public byte[] GetDeviceProperties()
        {
            throw new NotImplementedException();
        }
    }
}
