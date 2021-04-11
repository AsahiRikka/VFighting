using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using zFrame.Event;

/// <summary>
/// 角色动画控制器，根据信号播放动画
/// </summary>
public class VActorAnimationController:VActorControllerBase
{
    public VActorAnimationController(VActorEvent actorEvent, VActorInfo actorInfo, VSkillActions skillActions,
        VActorReferanceGameObject referance,VActorState actorState) : base(actorEvent)
    {
        _actorInfo = actorInfo;
        _skillActions = skillActions;
        _animator = _actorInfo.animationInfo.animator;
        parent = referance.parent;
        _actorEvent = actorEvent;
        _actorState = actorState;
    }

    private VActorInfo _actorInfo;
    private VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private Animator _animator;
    private GameObject parent;
    private VActorState _actorState;

    protected override void SkillStartEvent(VSkillAction skillAction)
    {
        _actorInfo.animationInfo.currentFrame = 0;
        AnimationPlay(_animator,parent,skillAction);
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        GetCurrentFrameInit(_animator);
        
        //计算当前帧数
        _actorInfo.animationInfo.currentFrame = GetCurrentFrame();

        //刷新角色状态
        ActorStateRefresh(skillAction);

        if (_actorInfo.animationInfo.currentFrame >= (int) totalFrame * 0.95f && !skillAction.skillProperty.isLoopSkill) 
        {
            //自然结束的技能
            _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skillAction);
        }
    }

    protected override void SkillEndEvent(VSkillAction skillAction)
    {
        _actorInfo.animationInfo.currentFrame = 0;
        _animator.SetBool(skillAction.motion.parameter, false);
    }

    private void AnimationPlay(Animator animator, GameObject p, VSkillAction startSkill)
    {
        //设置rootMotion
        animator.applyRootMotion = startSkill.motion.applyRoomMotion;
            
        //设置该动画初始角度
        p.transform.rotation = Quaternion.Euler(startSkill.motion.animationDefaultRotate);
        
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

    private void ActorStateRefresh(VSkillAction skill)
    {
        // int frame = _actorInfo.animationInfo.currentFrame;
        // foreach (var straight in skill.motion.animationStraights)
        // {
        //     if (frame >= straight.startFrame && frame <= straight.endFrame)
        //     {
        //         _actorInfo.animationInfo.canSkill = false;
        //         return;
        //     }
        // }
        //
        // _actorInfo.animationInfo.canSkill = true;
    }
}
