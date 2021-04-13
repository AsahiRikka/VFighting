using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能事件
/// </summary>
public class VSkillEventBase
{
    protected VSkillEventBase(VActorEvent actorEvent)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentSkill">当前技能</param>
    /// <param name="nextSkill">上一技能</param>
    protected virtual void SkillStartEvent(VSkillAction lastSkill,VSkillAction currentSkill)
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentSkill">当前技能</param>
    /// <param name="nextSkill">下一技能</param>
    protected virtual void SkillEndEvent(VSkillAction currentSkill,VSkillAction nextSkill)
    {
        
    }
}
