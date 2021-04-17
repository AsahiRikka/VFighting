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
        VMotion makeMotion = skillActions.defaultSkillActions.motion;
        #region 初始技能

        foreach (var hitBox in makeMotion.hitBoxes)
        {
            if (hitBox.segmentMotion.Type == VSegmengMotionType.keyFrame)
            {
                bind.AddEvent(skillActions.defaultSkillActions,AddHitEvent,hitBox.segmentMotion.startFrame);
                bind.AddEvent(skillActions.defaultSkillActions,RemoveHitEvent,hitBox.segmentMotion.endFrame);
            }
        }
        
        // if (makeMotion.hitBoxes.Count > 0 && makeMotion.hitBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
        // {
        //     bind.AddEvent(skillActions.defaultSkillActions,AddHitEvent,makeMotion.hitBoxes[0].segmentMotion.startFrame);
        //     bind.AddEvent(skillActions.defaultSkillActions,RemoveHitEvent,makeMotion.hitBoxes[0].segmentMotion.endFrame);
        // }
        if (makeMotion.passiveBoxes.Count > 0&& makeMotion.passiveBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
        {
            bind.AddEvent(skillActions.defaultSkillActions,AddPassiveEvent,makeMotion.passiveBoxes[0].segmentMotion.startFrame);
            bind.AddEvent(skillActions.defaultSkillActions,RemovePassiveEvent,makeMotion.passiveBoxes[0].segmentMotion.endFrame);
        }
        if (makeMotion.defenseBoxes.Count > 0&& makeMotion.defenseBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
        {
            bind.AddEvent(skillActions.defaultSkillActions,AddDefenceEvent,makeMotion.defenseBoxes[0].segmentMotion.startFrame);
            bind.AddEvent(skillActions.defaultSkillActions,RemoveDefenceEvent,makeMotion.defenseBoxes[0].segmentMotion.endFrame);
        }

        #endregion

        #region 其他技能

        foreach (var skill in skillActions.actorSkillActions)
        {
            makeMotion = skill.motion;
            
            foreach (var hitBox in makeMotion.hitBoxes)
            {
                if (hitBox.segmentMotion.Type == VSegmengMotionType.keyFrame)
                {
                    bind.AddEvent(skill,AddHitEvent,hitBox.segmentMotion.startFrame);
                    bind.AddEvent(skill,RemoveHitEvent,hitBox.segmentMotion.endFrame);
                }
            }
            
            // if (makeMotion.hitBoxes.Count > 0&& makeMotion.hitBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
            // {
            //     bind.AddEvent(skill,AddHitEvent,makeMotion.hitBoxes[0].segmentMotion.startFrame);
            //     bind.AddEvent(skill,RemoveHitEvent,makeMotion.hitBoxes[0].segmentMotion.endFrame);
            // }
            if (makeMotion.passiveBoxes.Count > 0&&makeMotion.passiveBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
            {
                bind.AddEvent(skill,AddPassiveEvent,makeMotion.passiveBoxes[0].segmentMotion.startFrame);
                bind.AddEvent(skill,RemovePassiveEvent,makeMotion.passiveBoxes[0].segmentMotion.endFrame);
            }
            if (makeMotion.defenseBoxes.Count > 0&& makeMotion.defenseBoxes[0].segmentMotion.Type==VSegmengMotionType.keyFrame)
            {
                bind.AddEvent(skill,AddDefenceEvent,makeMotion.defenseBoxes[0].segmentMotion.startFrame);
                bind.AddEvent(skill,RemoveDefenceEvent,makeMotion.defenseBoxes[0].segmentMotion.endFrame);
            }
        }

        #endregion
    }

    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        VMotion makeMotion = currentSkill.motion;
        if (makeMotion.hitBoxes.Count > 0 && makeMotion.hitBoxes[0].segmentMotion.Type==VSegmengMotionType.allskill)
        {
            AddHitEvent();
        }
        if (makeMotion.passiveBoxes.Count > 0&& makeMotion.passiveBoxes[0].segmentMotion.Type==VSegmengMotionType.allskill)
        {
            AddPassiveEvent();
        }
        if (makeMotion.defenseBoxes.Count > 0&& makeMotion.defenseBoxes[0].segmentMotion.Type==VSegmengMotionType.allskill)
        {
            AddDefenceEvent();
        }
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
