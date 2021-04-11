using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using zFrame.Event;

/// <summary>
/// 插入动画事件
/// </summary>
public class VActorAnimationEvent
{
    public VActorAnimationEvent(VSkillActions skillActions,VActorReferanceGameObject referance,VActorInfo actorInfo)
    {
        _skillActions = skillActions;
        _animator = referance.actorAnimator;
        _actorInfo = actorInfo;
        
        //添加事件
        foreach (var straight in _skillActions.defaultSkillActions.motion.animationStraights)
        {
            _animator.SetTarget(_skillActions.defaultSkillActions.motion.animationClip.name, straight.startFrame)
                .OnProcess(
                    (v) =>
                    {
                        SetAnimatorFalseSkill();
                    });
            
            _animator.SetTarget(_skillActions.defaultSkillActions.motion.animationClip.name, straight.endFrame)
                .OnProcess(
                    (v) =>
                    {
                        SetAnimatorCanSkill();
                    });
        }

        foreach (VSkillAction skill in _skillActions.actorSkillActions)
        {
            foreach (var straight in skill.motion.animationStraights)
            {
                _animator.SetTarget(skill.motion.animationClip.name, straight.startFrame)
                    .OnProcess(
                        (v) =>
                        {
                            SetAnimatorFalseSkill();
                        });
            
                _animator.SetTarget(skill.motion.animationClip.name, straight.endFrame)
                    .OnProcess(
                        (v) =>
                        {
                            SetAnimatorCanSkill();
                        });
            }
        }
        
        _animator.Rebind();
    }

    private VSkillActions _skillActions;
    private Animator _animator;
    private VActorInfo _actorInfo;


    private void SetAnimatorCanSkill()
    {
        _actorInfo.animationInfo.canSkill = true;
        DebugHelper.LogWarning("设置动画can");
    }

    private void SetAnimatorFalseSkill()
    {
        _actorInfo.animationInfo.canSkill = false;
        DebugHelper.LogWarning("设置动画false");
    }
}
