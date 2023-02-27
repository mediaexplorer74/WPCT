// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.ConnectedDeviceCollection
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=5b182dbf2043d73a
// MVID: 0AEB75AB-5740-4588-9640-3D9046B8DC96
// Assembly location: C:\Users\Admin\Desktop\d\vs\ufphostm.dll

using FlashingPlatform1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class ConnectedDeviceCollection : 
    IEnumerable<ConnectedDevice>,
    IEnumerable,
    IEnumerator<ConnectedDevice>,
    IDisposable,
    IEnumerator
  {
    private int m_Index = -1;
    private unsafe IConnectedDeviceCollection* m_Collection;
    private CPlatform m_Platform;

    internal unsafe ConnectedDeviceCollection(
      IConnectedDeviceCollection* Collection,
      [In] FlashingPlatform Platform)
    {
    }

    internal unsafe IConnectedDeviceCollection* Native => this.m_Collection;

    private void UnConnectedDeviceCollection()
    {
    }

    private unsafe void UnConnectedDeviceCollection1()
    {
      if ((IntPtr) this.m_Collection == IntPtr.Zero)
        return;
      this.m_Collection = (IConnectedDeviceCollection*) null;
    }

    public virtual unsafe int Count
    {
      get
      {
        IConnectedDeviceCollection* collection = this.m_Collection;
        return -1;
      }
    }

    public virtual object Current => (object) this.CurrentT;

    public virtual ConnectedDevice CurrentT
    {
      get
      {
        int Index = this.m_Index >= 0 ? this.m_Index : throw new InvalidOperationException();
        return Index < this.Count ? this.GetConnectedDeviceAt((uint) Index) : throw new InvalidOperationException();
      }
    }

    ConnectedDevice IEnumerator<ConnectedDevice>.Current => throw new NotImplementedException();

    public virtual IEnumerator GetEnumerator() => (IEnumerator) this.GetEnumeratorT();

    public virtual IEnumerator<ConnectedDevice> GetEnumeratorT() => (IEnumerator<ConnectedDevice>) this;

    [return: MarshalAs(UnmanagedType.U1)]
    public virtual bool MoveNext()
    {
      int index = this.m_Index;
      if (index < this.Count)
        this.m_Index = index + 1;
      return this.m_Index < this.Count || 0U > 0U;
    }

    public virtual void Reset() => this.m_Index = -1;

    public unsafe uint GetCount()
    {
      IConnectedDeviceCollection* collection = this.m_Collection;
      return 0;
    }

    public unsafe ConnectedDevice GetConnectedDeviceAt(uint Index)
    {
      ConnectedDevice connectedDeviceAt;
      try
      {
        IConnectedDeviceCollection* collection = this.m_Collection;
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
          connectedDeviceAt = (ConnectedDevice) null;
      }
      catch
      {
        throw;
      }
      return connectedDeviceAt;
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

    IEnumerator<ConnectedDevice> IEnumerable<ConnectedDevice>.GetEnumerator() => throw new NotImplementedException();
  }
}
