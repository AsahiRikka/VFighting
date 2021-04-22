using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using zFrame.Event;
using Debug = UnityEngine.Debug;

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
    
    //角色硬直帧存储，每个配置只生效1次
    private List<VAnimationStraight> _straights=new List<VAnimationStraight>();
    private List<VAnimationStraight> _faultStraights=new List<VAnimationStraight>();

    private VAnimationStraight _preStraight;
    
    public void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        AnimationPlay(_animator, actor.transform, parent.transform, currentSkill, lastSkill);
        _animationFrame.SkillStartEvent();
        
        //清理
        _straights.Clear();
        _faultStraights.Clear();
        _preStraight = null;
    }

    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //刷新帧
        _animationFrame.SkillUpdateEvent(skillAction);
        
        //硬直绑定刷新
        int tempStraight = 0;
        foreach (var straight in skillAction.motion.animationStraights)
        {
            //在硬直帧范围内，高等级会覆盖低等级
            if (straight.startFrame <= _actorInfo.animationInfo.currentFrame &&
                straight.endFrame >= _actorInfo.animationInfo.currentFrame)
            {
                if (straight.straightLevel > tempStraight)
                {
                    tempStraight = straight.straightLevel;
                }
            }
        }
        _actorInfo.animationInfo.straightLevel = tempStraight;

        //是否可循环，非循环技能播放到结尾帧自动结束
        if (!skillAction.skillProperty.isLoopSkill)
        {
            if (_actorInfo.animationInfo.currentFrame >= skillAction.motion.animationEndClip)
                _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skillAction);
        }
    }

    public void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        _animationFrame.SkillEndEvent();
        //技能结束硬直归0
        _actorInfo.animationInfo.straightLevel = 0;
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
            _animator.SetTrigger(startSkill.motion.parameter + "_Trigger");
        }
        else
        {
            //播放动画
            animator.SetBool(startSkill.motion.parameter, true);
        }
    }
}
