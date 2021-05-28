using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorFXController:VSkillEventBase
{
    public VActorFXController(VActorEvent skillEvent,VActorChangeProperty property,VActorState actorState,VActorReferanceGameObject referance) : base(skillEvent)
    {
        _vfxManager = GameFramework.instance.GameManager.VFXManager;

        _property = property;
        _referance = referance;
        _state = actorState;
    }

    private readonly VActorChangeProperty _property;
    private readonly VActorState _state;
    private readonly VActorReferanceGameObject _referance;
    
    private VFXManager _vfxManager;

    public void SkillStartEvent(VSkillAction currentSkill)
    {
        foreach (var fx in currentSkill.ActionEffects)
        {
            if (fx.skillActionType == SkillActionEnum.skillAction)
            {
                _vfxManager.ActorGetFXAndPlay(fx.FXID, fx, _property,_state, _referance.parent, currentSkill);
            }
        }
    }

    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.ActorBeAttackedEvent(activeSkillAction, activeSkillAction);

        foreach (var fx in activeSkillAction.ActionEffects)
        {
            if (fx.target == SkillActionTargetEnum.self && fx.skillActionType == SkillActionEnum.beAttack) 
            {
                _vfxManager.ActorGetFXAndPlay(fx.FXID, fx, _property,_state, _referance.parent,activeSkillAction);
            }
        }
    }

    protected override void DefenceEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.DefenceEvent(activeSkillAction, passiveSkillAction);

        foreach (var fx in activeSkillAction.ActionEffects)
        {
            if (fx.skillActionType == SkillActionEnum.successDefence)
            {
                _vfxManager.ActorGetFXAndPlay(fx.FXID, fx, _property,_state, _referance.parent, activeSkillAction);
            }
        }
    }
}
