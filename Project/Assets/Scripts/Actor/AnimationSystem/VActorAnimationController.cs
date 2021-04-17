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
        _actorInfo.animationInfo.currentFrame = 0;
        AnimationPlay(_animator,actor,currentSkill);
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        GetCurrentFrameInit(_animator);
        
        //计算当前帧数
        _actorInfo.animationInfo.currentFrame = GetCurrentFrame();

        if (_actorInfo.animationInfo.currentFrame >= (int) totalFrame * 0.95f && !skillAction.skillProperty.isLoopSkill) 
        {
            //自然结束的技能
            _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skillAction);
        }
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        _actorInfo.animationInfo.currentFrame = 0;
        _animator.SetBool(currentSkill.motion.parameter, false);
    }

    private void AnimationPlay(Animator animator, GameObject p, VSkillAction startSkill)
    {
        //设置rootMotion
        animator.applyRootMotion = startSkill.motion.applyRoomMotion;
            
        //设置该动画初始角度
        Vector3 offset=new Vector3(0,0,0);
        if(_actorState.actorFace==-1)
            offset=new Vector3(0,180,0);
        p.transform.rotation = Quaternion.Euler(startSkill.motion.animationDefaultRotate + offset);
        
        //位置偏移量
        p.transform.position += startSkill.motion.animationDefaultPos;
        
        //播放动画
        animator.SetBool(startSkill.motion.parameter, true);
    }


    private float currentTime = 0;
    private float animLength = 0;
    private float frameRate = 0;
    private float totalFrame = 0;

    private void GetCurrentFrameInit(Animator anim)
    {
        //当前动画机播放时长
        currentTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //动画片段长度
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //获取动画片段帧频
        frameRate = anim.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
        //计算动画片段总帧数
        totalFrame = animLength / (1 / frameRate);
    }
    
    private int GetCurrentFrame()
    {
        _actorInfo.animationInfo.currentFrame = (int) (Mathf.Floor(totalFrame * currentTime) % totalFrame);
        //计算当前播放的动画片段运行至哪一帧
        return _actorInfo.animationInfo.currentFrame;
    }
}
