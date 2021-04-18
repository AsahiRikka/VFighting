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
        
        skillEvent.ActorAttackEvent.AddListener(ActorAttackEvent);
        skillEvent.ActorBeAttackedEvent.AddListener(ActorBeAttackedEvent);
    }  
    
    protected virtual void SkillStartTriggerEvent(VSkillAction skillAction)
    {
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lastSkill"></param>
    /// <param name="currentSkill"></param>
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
    /// 攻击事件
    /// </summary>
    /// <param name="activeSkill">主动方技能事件，attack以及self</param>
    /// <param name="passiveSkill">被动方被攻击对主动方的效果，attack以及anamy</param>
    protected virtual void ActorAttackEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        
    }
    
    /// <summary>
    /// 受攻击
    /// </summary>
    /// <param name="activeSkill">受击者的技能事件，beAttack以及self</param>
    /// <param name="passiveSkill">受击者对主动方的效果，beAttack以及anamy</param>
    protected virtual void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
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
