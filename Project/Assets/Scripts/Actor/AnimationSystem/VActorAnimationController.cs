using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using zFrame.Event;

/// <summary>
/// 角色动画控制器，根据信号播放动画
/// </summary>
public class VActorAnimationController
{
    private VActorAnimationFrame _animationFrame;

    public VActorAnimationController(VActorEvent actorEvent, VActorInfo actorInfo, VActorReferanceGameObject referance,
        VActorState actorState)
    {
        _animationFrame = new VActorAnimationFrame(referance.actorAnimator, actorInfo.animationInfo);
        
        _actorInfo = actorInfo;
        _animator = _actorInfo.animationInfo.animator;
        actor = referance.actor;
        parent = referance.parent;
        _actorEvent = actorEvent;
        _actorState = actorState;
    }

    private VActorInfo _actorInfo;
    private VActorEvent _actorEvent;
    private Animator _animator;
    private GameObject actor;
    private GameObject parent;
    private VActorState _actorState;

    public void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        AnimationPlay(_animator, actor.transform, parent.transform, currentSkill, lastSkill);
        _animationFrame.SkillStartEvent();
    }

    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        _animationFrame.SkillUpdateEvent(skillAction);
        
        if (!skillAction.skillProperty.isLoopSkill)
        {
            if (_actorInfo.animationInfo.currentFrame >= skillAction.motion.animationEndClip)
                _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skillAction);
        }
    }

    public void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        _animationFrame.SkillEndEvent();
        _actorInfo.animationInfo.canSkill = true;
        _animator.SetBool(currentSkill.motion.parameter, false);
    }

    private void AnimationPlay(Animator animator, Transform actor,Transform parent, VSkillAction startSkill,VSkillAction lastSkill)
    {
        //设置rootMotion
        animator.applyRootMotion = startSkill.motion.applyRoomMotion;
            
        //设置该动画初始角度
        Vector3 offset=new Vector3(0,0,0);
        if (_actorState.actorFace == -1)
            offset = new Vector3(0, 180, 0);
        parent.transform.rotation = Quaternion.Euler(offset);

        Transform transform;
        (transform = actor.transform).localRotation = Quaternion.Euler(startSkill.motion.animationDefaultRotate);
        
        //位置偏移量
        transform.localPosition += startSkill.motion.animationDefaultPos;
        
        if (startSkill == lastSkill)
        {
            DebugHelper.LogWarning("播放动画");
            _animator.SetTrigger(startSkill.motion.parameter + "_Trigger");
        }
        else
        {
            //播放动画
            animator.SetBool(startSkill.motion.parameter, true);
        }
    }
}
