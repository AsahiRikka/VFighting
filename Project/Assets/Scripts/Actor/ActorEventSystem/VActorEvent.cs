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

    public VActorEvent()
    {
        SkillEvent=new VActorSkillEvent();
    }
}

