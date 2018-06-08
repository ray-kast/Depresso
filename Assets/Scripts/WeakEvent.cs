using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

public class WeakEvent {
  class WeakDelegate {
    public Type DelegateType { get; private set; }

    public WeakReference Target { get; private set; }

    public MethodInfo Method { get; private set; }

    public WeakDelegate(Delegate handler) {
      DelegateType = handler.GetType();
      Target = new WeakReference(handler.Target);
      Method = handler.Method;
    }

    public Delegate GetDelegate() {
      if (!Target.IsAlive) return null;

      return Delegate.CreateDelegate(DelegateType, Target.Target, Method, false);
    }

    public bool Matches(Delegate dlg) {
      if (!Target.IsAlive) return false;

      return dlg.Target == Target.Target && dlg.Method == Method;
    }

    public override string ToString() {
      return string.Format("({0}) [{1}]{2}::{3}({4})", DelegateType.Name, Method.Module, Method.DeclaringType.FullName, Method.Name, string.Join(", ", (from param in Method.GetParameters() select string.Format("{0} {1}", param.ParameterType.FullName, param.Name)).ToArray()));
    }
  }

  List<WeakDelegate> handlers = new List<WeakDelegate>();

  void Cleanup() {
    for (int i = 0; i < handlers.Count;) {
      if (handlers[i].Target.IsAlive) ++i;
      else handlers.RemoveAt(i);
    }
  }

  public void Add(Delegate handler) {
    Cleanup();

    WeakDelegate dlg = new WeakDelegate(handler);

    handlers.Add(dlg);
  }

  public void Remove(Delegate handler) {
    Cleanup();

    WeakDelegate first = handlers.FirstOrDefault(h => h.Matches(handler));

    if (first != null) handlers.Remove(first);
  }

  public Delegate GetDelegate() {
    Cleanup();

    return Delegate.Combine((from handler in handlers
                             let dlg = handler.GetDelegate()
                             where dlg != null
                             select dlg).ToArray());
  }
}