using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class VActorControllerBase
{
    public VActorControllerBase(VActorEvent actorEvent)
    {
        VActorSkillEvent skillEvent = actorEvent.SkillEvent;
        
        //技能事件监听添加
        skillEvent.skillStartEvent.AddListener(SkillStartEvent);
        skillEvent.skillUpdateVent.AddListener(SkillUpdateEvent);
        skillEvent.skillEndEvent.AddListener(SkillEndEvent);
    }

    public virtual void SkillStartEvent(VSkillAction skillAction)
    {
        
    }

    public virtual void SkillUpdateEvent(VSkillAction skillAction)
    {
        
    }

    public virtual void SkillEndEvent(VSkillAction skillAction)
    {
        
    }
}
