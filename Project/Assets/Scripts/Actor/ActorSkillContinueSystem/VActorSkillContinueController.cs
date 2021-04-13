using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorSkillContinueController:VSkillEventBase
{
    public VActorSkillContinueController(VActorEvent actorEvent,VActorInfo actorInfo) : base(actorEvent)
    {
        _continueInfo = actorInfo.skillContinueInfo;
    }

    private VActorSkillContinueInfo _continueInfo;

    protected override void SkillStartEvent(VSkillAction lastSkill,VSkillAction currentSkill)
    {
        //技能开始附加连携，
        foreach (var initCon in _continueInfo.skillContinues)
        {
            foreach (var con in currentSkill.preConditionData.skillContinues)
            {
                if (initCon.ID == con.ID)
                {
                    if (initCon.targetLayer == con.needLayer)
                    {
                        initCon.needLayer = con.needLayer;
                        initCon.targetLayer = con.targetLayer;
                    }
                }
            }
        }
    }

    protected override void SkillEndEvent(VSkillAction currentSkill,VSkillAction nextSkill)
    {
        //结束时消除所有连携，附加下一技能的连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            initCon.needLayer = 0;
            initCon.targetLayer = 0;

            if (nextSkill != null)
            {
                foreach (var saveCon in nextSkill.preConditionData.skillContinues)
                {
                    if (initCon.ID == saveCon.ID)
                    {
                        initCon.needLayer = saveCon.needLayer;
                        initCon.targetLayer = saveCon.targetLayer;
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
