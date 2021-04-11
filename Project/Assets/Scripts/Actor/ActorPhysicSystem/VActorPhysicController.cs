using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色物理效果控制器
/// </summary>
public class VActorPhysicController:VActorControllerBase
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public VActorChangeProperty _actorProperty;

    /// <summary>
    /// 角色状态
    /// </summary>
    public VActorState _state;

    public VActorPhysicController(VActorChangeProperty actorProperty, VActorState state,VActorEvent actorEvent):base(actorEvent)
    {
        _actorProperty = actorProperty;
        _state = state;
    }
}
