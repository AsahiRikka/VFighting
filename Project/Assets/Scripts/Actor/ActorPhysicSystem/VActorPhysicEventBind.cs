using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 物理事件帧绑定
/// </summary>
public class VActorPhysicEventBind
{
    private Dictionary<VSkillAction_Physic, bool> _dictionary;
    
    public VActorPhysicEventBind(VActorAnimationClipEventBind bind, VSkillActions skillActions,
        VActorController actorController, VActorInfo actorInfo)
    {
        _dictionary = actorInfo.physicInfo.PhysicDic;
        
        _actorController = actorController;
        _actorInfo = actorInfo;
        
        BindAdding(skillActions.defaultSkillActions,bind);
        BindAdding(skillActions.beAttackSkillAction,bind);

        foreach (var skill in skillActions.actorSkillActions)
        {
            BindAdding(skill,bind);
        }
    }

    private VActorController _actorController;
    private VActorInfo _actorInfo;

    private void BindAdding(VSkillAction skill, VActorAnimationClipEventBind bind)
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

}
