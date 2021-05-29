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
        _animationFrame.SkillStartEvent();
        AnimationPlay(_animator, actor.transform, parent.transform, currentSkill, lastSkill);

        //清理
        _straights.Clear();
        _faultStraights.Clear();
        _preStraight = null;
        
        //如果有硬直先判断为硬直
        if (currentSkill.motion.animationStraights.Count > 0)
        {
            _actorInfo.animationInfo.straightLevel = 5;
        }
    }
    
    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //刷新帧
        _animationFrame.SkillUpdateEvent(skillAction);
        
        if(_actorInfo.animationInfo.currentFrame==0)
            return;

        //硬直绑定刷新
        int tempStraight = 0;
        foreach (var straight in skillAction.motion.animationStraights)
        {
            //在硬直帧范围内，高等级会覆盖低等级
            if (straight.type == VSegmentMotionType.keyFrame)
            {
                if (straight.startFrame <= _actorInfo.animationInfo.currentFrame &&
                    straight.endFrame >= _actorInfo.animationInfo.currentFrame)
                {
                    if (straight.straightLevel > tempStraight)
                    {
                        tempStraight = straight.straightLevel;
                    }
                }
            }
            else if (straight.type == VSegmentMotionType.allskill) 
            {
                if (straight.straightLevel > tempStraight)
                {
                    tempStraight = straight.straightLevel;
                }
            }
        }
        _actorInfo.animationInfo.straightLevel = tempStraight;
        if (_actorInfo.animationInfo.currentFrame >= skillAction.motion.animationEndClip)
        {
            //技能播放到结尾帧自动结束
            if (skillAction.skillProperty.PlayType == SkillAnimPlayType.autoQuit)
                _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skillAction);
            //如果技能需要保持最后一帧，不自动结束
            else if (skillAction.skillProperty.PlayType == SkillAnimPlayType.holdEnd)
                _animator.speed = 0;
        }
    }

    public void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        //非自动结束类技能，要手动关闭动画
        if (currentSkill.skillProperty.PlayType != SkillAnimPlayType.autoQuit) 
        {
            _animator.SetBool(currentSkill.motion.parameter, false);
        }
        _animationFrame.SkillEndEvent();
        //技能结束硬直归0
        _actorInfo.animationInfo.straightLevel = 0;
    }

    private string lastTrigger = "";
    private void AnimationPlay(Animator animator, Transform actor,Transform parent, VSkillAction startSkill,VSkillAction lastSkill)
    {
        //设置rootMotion
        animator.applyRootMotion = startSkill.motion.applyRoomMotion;

        //恢复动画机播放
        animator.speed = 1;
        
        //非循环动画使用trigger
        if (startSkill.skillProperty.PlayType==SkillAnimPlayType.autoQuit)
        {
            _animator.ResetTrigger(lastTrigger);
            _animator.SetTrigger(startSkill.motion.parameter);

            lastTrigger = startSkill.motion.parameter;
        }
        else
        {
            //播放动画
            animator.SetBool(startSkill.motion.parameter, true);
        }
    }
}
