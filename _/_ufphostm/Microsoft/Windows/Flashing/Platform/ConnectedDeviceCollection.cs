// Decompiled with JetBrains decompiler
// Type: Microsoft.Windows.Flashing.Platform.ConnectedDeviceCollection
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using FlashingPlatform;
using RAII;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Flashing.Platform
{
  public class ConnectedDeviceCollection : IEnumerable<ConnectedDevice>, IEnumerator<ConnectedDevice>
  {
    private unsafe IConnectedDeviceCollection* m_Collection;
    private int m_Index;
    internal Microsoft.Windows.Flashing.Platform.FlashingPlatform m_Platform;

    internal unsafe ConnectedDeviceCollection(
      IConnectedDeviceCollection* Collection,
      [In] Microsoft.Windows.Flashing.Platform.FlashingPlatform Platform)
    {
      this.m_Collection = Collection;
      this.m_Index = -1;
      this.m_Platform = Platform;
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    internal unsafe IConnectedDeviceCollection* Native => this.m_Collection;

    private void \u007EConnectedDeviceCollection() => this.\u0021ConnectedDeviceCollection();

    private unsafe void \u0021ConnectedDeviceCollection()
    {
      IConnectedDeviceCollection* collection = this.m_Collection;
      if ((IntPtr) collection == IntPtr.Zero)
        return;
      IConnectedDeviceCollection* deviceCollectionPtr = collection;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num = __calli((__FnPtr<int (IntPtr)>) *(int*) (*(int*) deviceCollectionPtr + 8))((IntPtr) deviceCollectionPtr);
      this.m_Collection = (IConnectedDeviceCollection*) 0;
    }

    public virtual unsafe int Count
    {
      get
      {
        IConnectedDeviceCollection* collection = this.m_Collection;
        return (int) __calli((__FnPtr<uint (IntPtr)>) *(int*) *(int*) collection)((IntPtr) collection);
      }
    }

    public virtual object Current => (object) this.CurrentT;

    public virtual unsafe ConnectedDevice CurrentT
    {
      get
      {
        int Index = this.m_Index >= 0 ? this.m_Index : throw new InvalidOperationException(Marshal.PtrToStringUni((IntPtr) (void*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DC\u0040CMEHKPEC\u0040\u003F\u0024AAY\u003F\u0024AAo\u003F\u0024AAu\u003F\u0024AA\u003F5\u003F\u0024AAm\u003F\u0024AAu\u003F\u0024AAs\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAa\u003F\u0024AAl\u003F\u0024AAl\u003F\u0024AA\u003F5\u003F\u0024AAM\u003F\u0024AAo\u003F\u0024AAv\u003F\u0024AAe\u003F\u0024AAN\u003F\u0024AAe\u003F\u0024AAx\u003F\u0024AAt\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F\u0024AA\u0040));
        return Index < this.Count ? this.GetConnectedDeviceAt((uint) Index) : throw new InvalidOperationException(Marshal.PtrToStringUni((IntPtr) (void*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040MEAAIPAK\u0040\u003F\u0024AAE\u003F\u0024AAn\u003F\u0024AAu\u003F\u0024AAm\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAi\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AA\u003F5\u003F\u0024AAh\u003F\u0024AAa\u003F\u0024AAs\u003F\u0024AA\u003F5\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F\u0024AA\u0040));
      }
    }

    public virtual IEnumerator GetEnumerator() => (IEnumerator) this.GetEnumeratorT();

    public virtual IEnumerator<ConnectedDevice> GetEnumeratorT() => (IEnumerator<ConnectedDevice>) this;

    [return: MarshalAs(UnmanagedType.U1)]
    public virtual bool MoveNext()
    {
      int index = this.m_Index;
      if (index < this.Count)
        this.m_Index = index + 1;
      return this.m_Index < this.Count;
    }

    public virtual void Reset() => this.m_Index = -1;

    public unsafe uint GetCount()
    {
      IConnectedDeviceCollection* collection = this.m_Collection;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      return __calli((__FnPtr<uint (IntPtr)>) *(int*) *(int*) collection)((IntPtr) collection);
    }

    public unsafe ConnectedDevice GetConnectedDeviceAt(uint Index)
    {
      CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E iconnectedDevice;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &iconnectedDevice + 4) = 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref iconnectedDevice = (int) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024CAutoRelease\u0040PAUIConnectedDevice\u0040FlashingPlatform\u0040\u0040\u0040RAII\u0040\u00406B\u0040;
      ConnectedDevice connectedDeviceAt;
      // ISSUE: fault handler
      try
      {
        IConnectedDeviceCollection* collection = this.m_Collection;
        IConnectedDeviceCollection* deviceCollectionPtr = collection;
        int num1 = (int) Index;
        ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E local1 = ref iconnectedDevice;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        IConnectedDevice** iconnectedDevicePtr = __calli((__FnPtr<IConnectedDevice** (IntPtr)>) *(int*) (^(int&) ref iconnectedDevice + 4))((IntPtr) ref local1);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int error = __calli((__FnPtr<int (IntPtr, uint, IConnectedDevice**)>) *(int*) (*(int*) collection + 4))((IConnectedDevice**) deviceCollectionPtr, (uint) num1, (IntPtr) iconnectedDevicePtr);
        if (error < 0)
        {
          CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E unsignedShortConst;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &unsignedShortConst + 4) = (int) \u003CModule\u003E.UfphNativeStrFormat((ushort*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1FG\u0040NPJPCCED\u0040\u003F\u0024AAF\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAt\u003F\u0024AAo\u003F\u0024AA\u003F5\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAc\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AAn\u003F\u0024AAe\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAv\u003F\u0024AAi\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F5\u003F\u0024AAa\u0040, __arglist ((int) Index));
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
              ref CAutoDeleteArray\u003Cunsigned\u0020short\u0020const\u0020\u003E local2 = ref unsignedShortConst;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              ushort* numPtr = __calli((__FnPtr<ushort* (IntPtr)>) *(int*) (^(int&) ref unsignedShortConst + 16))((IntPtr) ref local2);
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, FlashingPlatform.LogLevel, ushort*)>) *(int*) (*(int*) num2 + 4))((ushort*) num3, (FlashingPlatform.LogLevel) 2, (IntPtr) numPtr);
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
          ref CAutoRelease\u003CFlashingPlatform\u003A\u003AIConnectedDevice\u0020\u002A\u003E local4 = ref iconnectedDevice;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          connectedDeviceAt = new ConnectedDevice(__calli((__FnPtr<IConnectedDevice* (IntPtr)>) *(int*) (^(int&) ref iconnectedDevice + 40))((IntPtr) ref local4), this.m_Platform);
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
      return connectedDeviceAt;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
    {
      if (A_0)
      {
        this.\u007EConnectedDeviceCollection();
      }
      else
      {
        try
        {
          this.\u0021ConnectedDeviceCollection();
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

    ~ConnectedDeviceCollection() => this.Dispose(false);
  }
}
