using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorDamageController:VSkillEventBase
{
    public VActorDamageController(VActorEvent actorEvent, VActorStateController stateController) : base(actorEvent)
    {
        _stateController = stateController;
    }

    private VActorStateController _stateController;

    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        //敌人对自身的伤害
        foreach (var damage in passiveSkillAction.ActionDamanges)
        {
            if (damage.skillActionType == SkillActionEnum.skillHit && damage.target == SkillActionTargetEnum.anamy) 
            {
                _stateController.DamageEvent(damage.damage);
            }
        }
        
        //技能对释放者的伤害
        foreach (var damage in activeSkillAction.ActionDamanges)
        {
            if (damage.skillActionType == SkillActionEnum.skillHit && damage.target == SkillActionTargetEnum.self) 
            {
                _stateController.DamageEvent(damage.damage);
            }
        }
    }
}
