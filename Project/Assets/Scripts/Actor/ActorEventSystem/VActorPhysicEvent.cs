using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 物理事件帧绑定
/// </summary>
public class VActorPhysicEvent:VSkillEventBase
{
    private Dictionary<VSkillAction_Physic, bool> _dictionary = new Dictionary<VSkillAction_Physic, bool>();
    
    public VActorPhysicEvent(VActorAnimationClipEventBind bind, VSkillActions skillActions,
        VActorController actorController, VActorInfo actorInfo,VActorEvent actorEvent):base(actorEvent)
    {
        _actorController = actorController;
        _actorInfo = actorInfo;
        
        foreach (var physic in skillActions.defaultSkillActions.ActionPhysics)
        {
            if (physic.skillActionType == SkillActionEnum.keyFrame)
            {
                bind.AddEvent(skillActions.defaultSkillActions,AddKeyFrameEvent,physic.keyFrame);
            }
            else if (physic.skillActionType == SkillActionEnum.frame)
            {
                _dictionary.Add(physic,false);
                
                bind.AddEvent(skillActions.defaultSkillActions,AddFrameEvent,physic.startFrame);
                bind.AddEvent(skillActions.defaultSkillActions,RemoveFrameEvent,physic.endFrame);
            }
        }

        foreach (var skill in skillActions.actorSkillActions)
        {
            foreach (var physic in skill.ActionPhysics)
            {
                if (physic.skillActionType == SkillActionEnum.keyFrame)
                {
                    bind.AddEvent(skill,AddKeyFrameEvent,physic.keyFrame);
                }
                else if (physic.skillActionType == SkillActionEnum.frame)
                {
                    _dictionary.Add(physic,false);
                    
                    bind.AddEvent(skill,AddFrameEvent,physic.startFrame);
                    bind.AddEvent(skill,RemoveFrameEvent,physic.endFrame);
                }
            }
        }
    }

    private VActorController _actorController;
    private VActorInfo _actorInfo;

    private void AddKeyFrameEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _actorController.physicController.PhysicAction(skillAction,SkillActionEnum.keyFrame);
    }

    private void AddFrameEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        foreach (var physic in skillAction.ActionPhysics)
        {
            if (_dictionary.ContainsKey(physic))
            {
                _dictionary[physic] = true;
            }
        }
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        base.SkillUpdateEvent(skillAction);
        foreach (var physic in skillAction.ActionPhysics)
        {
            if (_dictionary.ContainsKey(physic))
            {
                _actorController.physicController.PhysicActionByAcceleration(skillAction,SkillActionEnum.frame);
            }
        }
    }

    private void RemoveFrameEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        foreach (var physic in skillAction.ActionPhysics)
        {
            if (_dictionary.ContainsKey(physic))
            {
                _dictionary[physic] = false;
            }
        }
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        foreach (var physic in currentSkill.ActionPhysics)
        {
            if (_dictionary.ContainsKey(physic))
            {
                _dictionary[physic] = true;
            }
        }
    }
}
