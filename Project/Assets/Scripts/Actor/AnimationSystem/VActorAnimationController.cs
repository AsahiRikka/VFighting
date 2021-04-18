using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using zFrame.Event;

/// <summary>
/// 角色动画控制器，根据信号播放动画
/// </summary>
public class VActorAnimationController:VSkillEventBase
{
    public VActorAnimationController(VActorEvent actorEvent, VActorInfo actorInfo, VSkillActions skillActions,
        VActorReferanceGameObject referance,VActorState actorState) : base(actorEvent)
    {
        _actorInfo = actorInfo;
        _skillActions = skillActions;
        _animator = _actorInfo.animationInfo.animator;
        actor = referance.actor;
        _actorEvent = actorEvent;
        _actorState = actorState;
    }

    private VActorInfo _actorInfo;
    private VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private Animator _animator;
    private GameObject actor;
    private VActorState _actorState;

    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        AnimationPlay(_animator,actor,currentSkill,lastSkill);
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        _animator.SetBool(currentSkill.motion.parameter, false);
    }

    private void AnimationPlay(Animator animator, GameObject p, VSkillAction startSkill,VSkillAction lastSkill)
    {
        //设置rootMotion
        animator.applyRootMotion = startSkill.motion.applyRoomMotion;
            
        //设置该动画初始角度
        Vector3 offset=new Vector3(0,0,0);
        if (_actorState.actorFace == -1)
            offset = new Vector3(0, 180, 0);
        p.transform.rotation = Quaternion.Euler(startSkill.motion.animationDefaultRotate + offset);
        
        //位置偏移量
        p.transform.position += startSkill.motion.animationDefaultPos;
        
        if (startSkill == lastSkill)
        {
            _animator.SetTrigger(startSkill.motion.parameter + "_Trigger");
        }
        else
        {
            //播放动画
            animator.SetBool(startSkill.motion.parameter, true);
        }
    }
}
