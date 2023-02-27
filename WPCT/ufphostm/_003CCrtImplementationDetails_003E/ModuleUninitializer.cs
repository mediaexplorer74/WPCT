// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleUninitializer
// Assembly: ufphostm, Version=10.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 665758C6-46E8-4456-A462-54EBEBC45DB9
// Assembly location: C:\Users\Admin\Desktop\d\ufphostm.dll

using \u003CCrtImplementationDetails\u003E;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace \u003CCrtImplementationDetails\u003E
{
  internal class ModuleUninitializer : Stack
  {
    private static object @lock = new object();
    internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();

    internal void AddHandler(EventHandler handler)
    {
      bool flag = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          Monitor.Enter(ModuleUninitializer.@lock);
          flag = true;
        }
        RuntimeHelpers.PrepareDelegate((Delegate) handler);
        this.Push((object) handler);
      }
      finally
      {
        if (flag)
          Monitor.Exit(ModuleUninitializer.@lock);
      }
    }

    private ModuleUninitializer()
    {
      EventHandler eventHandler = new EventHandler(this.SingletonDomainUnload);
      AppDomain.CurrentDomain.DomainUnload += eventHandler;
      AppDomain.CurrentDomain.ProcessExit += eventHandler;
    }

    [PrePrepareMethod]
    private void SingletonDomainUnload(object source, EventArgs arguments)
    {
      bool flag = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
        }
        finally
        {
          Monitor.Enter(ModuleUninitializer.@lock);
          flag = true;
        }
        foreach (EventHandler eventHandler in (Stack) this)
          eventHandler(source, arguments);
      }
      finally
      {
        if (flag)
          Monitor.Exit(ModuleUninitializer.@lock);
      }
    }
  }
}
