using UnityEngine;
using System.Collections;

public class FEventRegisterComb : FEventRegister
{
    FEventRegister _left;
    FEventRegister _right;

    protected override void OnFirstListenerWillAdd()
    {
        base.OnFirstListenerWillAdd();
        _left.AddEventHandler(_BroadCastEvent);
        _right.AddEventHandler(_BroadCastEvent);
    }

    protected override void OnLastListenerRemoved()
    {
        _left.RemoveEventHandler(_BroadCastEvent);
        _right.RemoveEventHandler(_BroadCastEvent);
        base.OnLastListenerRemoved();
    }

    public FEventRegisterComb(FEventRegister left, FEventRegister right)
    {
        _left = left;
        _right = right;
    }

}
