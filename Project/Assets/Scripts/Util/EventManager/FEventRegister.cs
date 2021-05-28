using UnityEngine;
using System.Collections;
using System;

public interface IRelease {
    void Release();
}

public abstract class FEventRegisterBase
{
    protected Delegate _delegate;

    public abstract FEventRegister CastDown();

    protected virtual void OnLastListenerRemoved() { }

    protected virtual void OnFirstListenerWillAdd() { }

    protected void _AddEventHandler(Delegate d)
    {
        if (_delegate == null)
        {
            OnFirstListenerWillAdd();
        }
        _delegate = Delegate.Combine(_delegate, d);
    }

    protected void _RemoveEventHandler(Delegate d)
    {
        _delegate = Delegate.RemoveAll(_delegate, d);
        if (_delegate == null)
        {
            OnLastListenerRemoved();
        }
    }
    protected IRelease _Subscribe(Delegate cb)
    {
        _AddEventHandler(cb);
        return new HandlerRemover(this, cb);
    }

    class HandlerRemover : IRelease
    {
        FEventRegisterBase _source;
        Delegate _value;

        public HandlerRemover(FEventRegisterBase source, Delegate value)
        {
            _source = source;
            _value = value;
        }

        void IRelease.Release()
        {
            _source._RemoveEventHandler(_value);
        }

    }

    static public FEventRegister operator +(FEventRegisterBase left, FEventRegisterBase right)
    {
        return left.CastDown() + right.CastDown();
    }

}
public class FEventRegister : FEventRegisterBase
{
    public void AddEventHandler(Action cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action cb)
    {
        _RemoveEventHandler(cb);
    }

    public IRelease Subscribe(Action cb)
    {
        return _Subscribe(cb);
    }
    public override FEventRegister CastDown()
    {
        return this;
    }

    protected void _BroadCastEvent()
    {
        if (_delegate != null)
        {
            (_delegate as Action)();
        }
    }
    static public FEventRegister operator +(FEventRegister left, FEventRegister right)
    {
        return left.CastDown() + right.CastDown();
    }

    public FEventRegister<T0> CastTo<T0>(Func<T0> castCall)
    {
        return new FEventRegisterCastTo<T0>(this, castCall);
    }
}
public class FEventRegister<T0> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0> cb)
    {
        _RemoveEventHandler(cb);
    }

    public IRelease Subscribe(Action<T0> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0>)(arg0);
        }
    }
    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0>(this);
    }

    public FEventRegister<U0> CastTo<U0>(Func<T0, U0> castCall)
    {
        return new FEventRegisterCastFromTo<T0, U0>(this, castCall);
    }
}

public class FEventRegister<T0, T1> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0, T1> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1> cb)
    {
        _RemoveEventHandler(cb);
    }

    public IRelease Subscribe(Action<T0, T1> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1>)(arg0, arg1);
        }
    }
    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0, T1>(this);
    }

}
public class FEventRegister<T0, T1, T2> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0, T1, T2> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1, T2> cb)
    {
        _RemoveEventHandler(cb);
    }

    public IRelease Subscribe(Action<T0, T1, T2> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1, T2 arg2)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1, T2>)(arg0, arg1, arg2);
        }
    }
    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0, T1, T2>(this);
    }

}

public class FEventRegister<T0, T1, T2, T3> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0, T1, T2, T3> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1, T2, T3> cb)
    {
        _RemoveEventHandler(cb);
    }

    public IRelease Subscribe(Action<T0, T1, T2, T3> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1, T2, T3>)(arg0, arg1, arg2, arg3);
        }
    }
    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0, T1, T2, T3>(this);
    }

}