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
}
