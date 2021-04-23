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

    public void SkillStartEvent()
    {
        //动画硬直当前被认为等于技能硬直
        _actorInfo.skillInfo.skillStraightLevel = _actorInfo.animationInfo.straightLevel;
    }
    
    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //动画硬直当前被认为等于技能硬直
        _actorInfo.skillInfo.skillStraightLevel = _actorInfo.animationInfo.straightLevel;
    }
}
