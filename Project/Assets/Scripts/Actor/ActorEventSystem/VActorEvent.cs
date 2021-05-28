using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 角色身上的事件，以及与关键帧有关的事件触发
/// </summary>
public class VActorEvent
{
    //技能事件
    public readonly VActorSkillEvent SkillEvent;
    
    public VActorEvent()
    {
        SkillEvent=new VActorSkillEvent();
    }
}

