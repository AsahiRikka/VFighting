using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态刷新
/// </summary>
public class VActorStateController
{
    public VActorStateController(VActorEvent actorEvent,VActorState actorState,VActorInfo actorInfo)
    {
        _actorInfo = actorInfo;
        _actorState = actorState;
    }

    private VActorState _actorState;
    private VActorInfo _actorInfo;

    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //每帧对能否释放技能的判断
        _actorState.canSkill = _actorInfo.animationInfo.canSkill;
    }
}
