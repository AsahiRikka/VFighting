using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 其他控制器产生的状态影响优先级判断
/// </summary>
public class VActorStateController:VSkillEventBase
{
    public VActorStateController(VActorEvent actorEvent,VActorState actorState,VActorInfo actorInfo) : base(actorEvent)
    {
        _actorInfo = actorInfo;
        _actorState = actorState;
    }

    private VActorState _actorState;
    private VActorInfo _actorInfo;

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        //每帧对能否释放技能的判断
        _actorState.canSkill = _actorInfo.animationInfo.canSkill;
    }
}
