using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorFXController:VSkillEventBase
{
    public VActorFXController(VActorEvent skillEvent,VActorChangeProperty property,VActorReferanceGameObject referance) : base(skillEvent)
    {
        _vfxManager = GameFramework.instance._service.VFXManager;

        _property = property;
        _referance = referance;
    }

    private readonly VActorChangeProperty _property;
    private readonly VActorReferanceGameObject _referance;
    
    private VFXManager _vfxManager;

    public void SkillStartEvent(VSkillAction currentSkill)
    {
        foreach (var fx in currentSkill.ActionEffects)
        {
            if (fx.skillActionType == SkillActionEnum.skillAction)
            {
                _vfxManager.ActorGetFXAndPlay(fx.FXID, fx, _property, _referance.parent, currentSkill);
            }
        }
    }

    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {

    }

    protected override void ActorAttackEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {

    }
}
