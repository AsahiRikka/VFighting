using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorSkillContinueController:VActorControllerBase
{
    public VActorSkillContinueController(VActorEvent actorEvent) : base(actorEvent)
    {
        
    }

    protected override void SkillEndEvent(VSkillAction skillAction)
    {
        //技能结束时存在技能连携需要消除
    }
}
