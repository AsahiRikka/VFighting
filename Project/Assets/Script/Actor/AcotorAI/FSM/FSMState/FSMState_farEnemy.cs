using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_farEnemy : FSMState
{
    public FSMState_farEnemy()
    {
        base.StateEnum = FSMStateEnum.farEnemy;
    }

    public override void Act(VActorAISkillClassify classify, VActorSkillInfo skillInfo, VActorAISkillEmitLogic logic)
    {
        base.Act(classify,skillInfo,logic);
    }

    protected override void SkillSetting(VActorAISkillClassify classify, VActorSkillInfo skillInfo, VActorAISkillEmitLogic Logic)
    {
        float temp = Random.Range(0f, 10f);
        if (temp < 3f)
        {
            Logic.ActorSkillUnit = Logic.SkillSetting(AISkillType.idle, classify, skillInfo);
        }
        else if (temp < 7f) 
        {
            Logic.ActorSkillUnit = Logic.SkillSetting(AISkillType.front, classify, skillInfo);
        }
        else
        {
            Logic.ActorSkillUnit = Logic.SkillSetting(AISkillType.attack, classify, skillInfo);
        }
    }

    public override void Quit(VActorAISkillClassify classify, VActorAISkillEmitLogic logic)
    {
        base.Quit(classify,logic);
    }
}
