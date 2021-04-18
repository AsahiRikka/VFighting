using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorColliderEventBind : VSkillEventBase
{
    public VActorColliderEventBind(VActorEvent actorEvent, VSkillActions skillActions, VActorController actorController,
        VActorAnimationClipEventBind bind,VActorInfo actorInfo) : base(actorEvent)
    {
        _colliderController = actorController.colliderController;
        _actorInfo = actorInfo;
        #region 初始技能
        
        BindAdding(skillActions.defaultSkillActions,bind);
        BindAdding(skillActions.beAttackSkillAction,bind);

        #endregion

        #region 其他技能

        foreach (var skill in skillActions.actorSkillActions)
        {
            BindAdding(skill,bind);
        }

        #endregion
    }

    private void BindAdding(VSkillAction skill, VActorAnimationClipEventBind bind)
    {
        VMotion makeMotion = skill.motion;
            
        foreach (var hitBox in makeMotion.hitBoxes)
        {
            if (hitBox.segmentMotion.type == VSegmentMotionType.keyFrame)
            {
                bind.AddEvent(skill,AddHitEvent,hitBox.segmentMotion.startFrame);
                bind.AddEvent(skill,RemoveHitEvent,hitBox.segmentMotion.endFrame);
            }
        }

        foreach (var passiveBox in makeMotion.passiveBoxes)
        {
            if (passiveBox.segmentMotion.type == VSegmentMotionType.keyFrame)
            {
                bind.AddEvent(skill,AddPassiveEvent,passiveBox.segmentMotion.startFrame);
                bind.AddEvent(skill,RemovePassiveEvent,passiveBox.segmentMotion.endFrame);
            }
        }

        foreach (var defenseBox in makeMotion.defenseBoxes)
        {
            if (defenseBox.segmentMotion.type == VSegmentMotionType.keyFrame)
            {
                bind.AddEvent(skill,AddDefenceEvent,defenseBox.segmentMotion.startFrame);
                bind.AddEvent(skill,RemoveDefenceEvent,defenseBox.segmentMotion.endFrame);
            }
        }
    }

    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        AddHitEvent();
        AddPassiveEvent();
        AddDefenceEvent();
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        RemoveHitEvent();
        RemoveDefenceEvent();
        RemovePassiveEvent();
    }

    private VActorInfo _actorInfo;
    private VActorColliderController _colliderController;

    private void AddHitEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.AddBoxes(skillAction,ColliderBoxTypeEnum.hit);
    }

    private void AddPassiveEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.AddBoxes(skillAction,ColliderBoxTypeEnum.passive);
    }

    private void AddDefenceEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.AddBoxes(skillAction,ColliderBoxTypeEnum.defence);
    }

    private void RemoveHitEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.RemoveBoxes(skillAction,ColliderBoxTypeEnum.hit);
    }

    private void RemovePassiveEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.RemoveBoxes(skillAction,ColliderBoxTypeEnum.passive);
    }

    private void RemoveDefenceEvent()
    {
        VSkillAction skillAction = _actorInfo.skillInfo.currentSkill;
        _colliderController.RemoveBoxes(skillAction,ColliderBoxTypeEnum.defence);
    }
}
