using UnityEngine;
using System.Collections;
using System;

public class FEventRegisterCastDown<T0> : FEventRegister
{
    FEventRegister<T0> _upper;
    private void BindAction(T0 arg0)
    {
        _BroadCastEvent();
    }
    protected override void OnFirstListenerWillAdd()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnFirstListenerWillAdd();
    }
    protected override void OnLastListenerRemoved()
    {
        base.OnLastListenerRemoved();
        _upper.AddEventHandler(BindAction);
    }

    public FEventRegisterCastDown(FEventRegister<T0> upper)
    {
        _upper = upper;
    }
}

public class FEventRegisterCastTo<T0> : FEventRegister<T0>
{
    FEventRegister _inner;
    Func<T0> _castvridge;
    private void BindAction()
    {
        if (_castvridge != null)
        {
            _BroadCastEvent(_castvridge());
        }
    }
    protected override void OnLastListenerRemoved()
    {
        _inner.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListenerWillAdd()
    {
        base.OnFirstListenerWillAdd();
        _inner.AddEventHandler(BindAction);
    }
    public FEventRegisterCastTo(FEventRegister inner, Func<T0> castFun)
    {
        _inner = inner;
        _castvridge = castFun;
    }
}
public class FEventRegisterCastFromTo<T0, U0> : FEventRegister<U0>
{
    FEventRegister<T0> _inner;
    Func<T0, U0> _castvridge;
    private void BindAction(T0 arg0)
    {
        if (_castvridge != null)
        {
            _BroadCastEvent(_castvridge(arg0));
        }
    }
    protected override void OnLastListenerRemoved()
    {
        _inner.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListenerWillAdd()
    {
        base.OnFirstListenerWillAdd();
        _inner.AddEventHandler(BindAction);
    }
    public FEventRegisterCastFromTo(FEventRegister<T0> inner, Func<T0, U0> castFun)
    {
        _inner = inner;
        _castvridge = castFun;
    }
}

public class FEventRegisterCastDown<T0, T1> : FEventRegister
{
    FEventRegister<T0, T1> _upper;
    private void BindAction(T0 arg0, T1 arg1)
    {
        _BroadCastEvent();
    }
    protected override void OnFirstListenerWillAdd()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnFirstListenerWillAdd();
    }
    protected override void OnLastListenerRemoved()
    {
        base.OnLastListenerRemoved();
        _upper.AddEventHandler(BindAction);
    }

    public FEventRegisterCastDown(FEventRegister<T0, T1> upper)
    {
        _upper = upper;
    }
}
public class FEventRegisterCastDown<T0, T1, T2> : FEventRegister
{
    FEventRegister<T0, T1, T2> _upper;
    private void BindAction(T0 arg0, T1 arg1, T2 arg2)
    {
        _BroadCastEvent();
    }
    protected override void OnFirstListenerWillAdd()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnFirstListenerWillAdd();
    }
    protected override void OnLastListenerRemoved()
    {
        base.OnLastListenerRemoved();
        _upper.AddEventHandler(BindAction);
    }

    public FEventRegisterCastDown(FEventRegister<T0, T1, T2> upper)
    {
        _upper = upper;
    }
}
public class FEventRegisterCastDown<T0, T1, T2, T3> : FEventRegister
{
    FEventRegister<T0, T1, T2, T3> _upper;
    private void BindAction(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        _BroadCastEvent();
    }
    protected override void OnFirstListenerWillAdd()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnFirstListenerWillAdd();
    }
    protected override void OnLastListenerRemoved()
    {
        base.OnLastListenerRemoved();
        _upper.AddEventHandler(BindAction);
    }

    public FEventRegisterCastDown(FEventRegister<T0, T1, T2, T3> upper)
    {
        _upper = upper;
    }
}