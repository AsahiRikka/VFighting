﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能连携的帧绑定
/// </summary>
public class VActorSkillContinueEvent:VSkillEventBase
{
    private VActorInfo _actorInfo;
    private VActorSkillContinueInfo _continueInfo;

    public VActorSkillContinueEvent(VSkillActions skillActions,VActorInfo actorInfo,
        VActorAnimationClipEventBind bind,VActorEvent actorEvent) : base(actorEvent)
    {
        _actorInfo = actorInfo;
        _continueInfo = actorInfo.skillContinueInfo;

        //添加技能事件响应
        foreach (var myCon in skillActions.defaultSkillActions.preConditionData.skillContinues)
        {
            bind.AddEvent(skillActions.defaultSkillActions,AddConEvent,(int)myCon.frame.x);
            bind.AddEvent(skillActions.defaultSkillActions,RemoveConEvent,(int)myCon.frame.y);
        }
        
        foreach (VSkillAction skill in skillActions.actorSkillActions)
        {
            foreach (var myCon in skill.preConditionData.skillContinues)
            {
                bind.AddEvent(skill,AddConEvent,(int)myCon.frame.x);
                bind.AddEvent(skill,RemoveConEvent,(int)myCon.frame.y);
            }
        }
    }

    private void AddConEvent()
    {
        VSkillAction currentSkill = _actorInfo.skillInfo.currentSkill;
        
        //附加连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            foreach (var con in currentSkill.preConditionData.skillContinues)
            {
                if (initCon.ID == con.ID)
                {
                    initCon.needLayer = con.needLayer;
                    initCon.targetLayer = con.targetLayer;
                }
            }
        }
    }

    private void RemoveConEvent()
    {
        VSkillAction currentSkill = _actorInfo.skillInfo.currentSkill;
        
        //消除连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            foreach (var con in currentSkill.preConditionData.skillContinues)
            {
                if (initCon.ID == con.ID)
                {
                    initCon.needLayer = 0;
                    initCon.targetLayer = 0;
                }
            }
        }
    }

    
    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        //消除连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            foreach (var con in currentSkill.preConditionData.skillContinues)
            {
                if (initCon.ID == con.ID)
                {
                    initCon.needLayer = 0;
                    initCon.targetLayer = 0;
                }
            }
        }
    }
}
