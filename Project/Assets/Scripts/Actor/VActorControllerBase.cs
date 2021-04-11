using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class VActorControllerBase
{
    protected VActorControllerBase(VActorEvent actorEvent)
    {
        VActorSkillEvent skillEvent = actorEvent.SkillEvent;
        
        //技能事件监听添加
        
        skillEvent.skillPlayTriggerEvent.AddListener(SkillStartTriggerEvent);
        skillEvent.skillStartEvent.AddListener(SkillStartEvent);
        skillEvent.skillUpdateEvent.AddListener(SkillUpdateEvent);
        skillEvent.skillFixUpdateEvent.AddListener(SkillFixUpdateEvent);
        skillEvent.skillEndNormalEvent.AddListener(SkillEndNormalEvent);
        skillEvent.skillEndEvent.AddListener(SkillEndEvent);
    }
    
    protected virtual void SkillStartTriggerEvent(VSkillAction skillAction)
    {
        
    }

    protected virtual void SkillStartEvent(VSkillAction skillAction)
    {
        
    }

    protected virtual void SkillUpdateEvent(VSkillAction skillAction)
    {
        
    }

    protected virtual void SkillFixUpdateEvent(VSkillAction skillAction)
    {
        
    }

    protected virtual void SkillEndNormalEvent(VSkillAction skillAction)
    {
        
    }

    protected virtual void SkillEndEvent(VSkillAction skillAction)
    {
        
    }
}
