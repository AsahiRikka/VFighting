using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using zFrame.Event;

/// <summary>
/// 负责调用动画帧绑定功能，将与动画有关的帧事件进行绑定
/// </summary>
public class VActorAnimationEvent:VSkillEventBase
{
    public VActorAnimationEvent(VSkillActions skillActions, VActorInfo actorInfo, VActorAnimationClipEventBind bind,
        VActorEvent actorEvent) : base(actorEvent)
    {
        _skillActions = skillActions;
        _actorInfo = actorInfo;

        //动画硬直
        foreach (var straight in _skillActions.defaultSkillActions.motion.animationStraights)
        {
            bind.AddEvent(_skillActions.defaultSkillActions,SetAnimatorFalseSkill,straight.startFrame);
            bind.AddEvent(_skillActions.defaultSkillActions,SetAnimatorCanSkill,straight.endFrame);
        }

        foreach (VSkillAction skill in _skillActions.actorSkillActions)
        {
            foreach (var straight in skill.motion.animationStraights)
            {
                bind.AddEvent(skill,SetAnimatorFalseSkill,straight.startFrame);
                bind.AddEvent(skill,SetAnimatorCanSkill,straight.endFrame);
            }
        }
    }

    private VSkillActions _skillActions;
    private VActorInfo _actorInfo;

    private void SetAnimatorCanSkill()
    {
        _actorInfo.animationInfo.canSkill = true;
    }

    private void SetAnimatorFalseSkill()
    {
        _actorInfo.animationInfo.canSkill = false;
    }

    /// <summary>
    /// 技能被打断，无法执行帧事件时
    /// </summary>
    /// <param name="currentSkill"></param>
    /// <param name="nextSkill"></param>
    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        base.SkillEndEvent(currentSkill, nextSkill);
        SetAnimatorCanSkill();
    }
}
