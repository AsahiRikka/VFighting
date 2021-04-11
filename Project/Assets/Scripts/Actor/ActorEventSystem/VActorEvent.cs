using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 角色身上的一些事件
/// </summary>
public class VActorEvent
{
    public VActorSkillEvent SkillEvent;
    public VActorAnimationEvent AnimationEvent;
    public VActorEvent(VSkillActions skillActions, VActorReferanceGameObject referance, VActorInfo actorInfo)
    {
        SkillEvent=new VActorSkillEvent();
        AnimationEvent=new VActorAnimationEvent(skillActions,referance,actorInfo);
    }
}

