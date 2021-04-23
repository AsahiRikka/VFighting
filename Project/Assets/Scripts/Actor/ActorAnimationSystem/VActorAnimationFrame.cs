using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 计算动画帧
/// </summary>
public class VActorAnimationFrame
{
    public VActorAnimationFrame(Animator animator,VActorAnimationInfo actorAnimationInfo)
    {
        anim = animator;
        _animationInfo = actorAnimationInfo;
    }

    private float currentTime;
    private float length;
    private float frameRate;
    private float totalFrame;
    private int currentFrame;

    private AnimationClip _clip;

    private Animator anim;
    private VActorAnimationInfo _animationInfo;

    public void SkillStartEvent()
    {
        _animationInfo.currentFrame = 0;
    }

    public void SkillUpdateEvent(VSkillAction currentSkill)
    {
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip!=currentSkill.motion.animationClip)
            return;

        _clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;

        if (_clip == currentSkill.motion.animationClip)
        {
            //当前动画机播放时长
            currentTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //动画片段长度
            length = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            //获取动画片段帧频
            frameRate = anim.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate; 
            //计算动画片段总帧数
            totalFrame = length / (1 / frameRate);
            //计算当前播放的动画片段运行至哪一帧
            _animationInfo.currentFrame = (int)(Mathf.Floor(totalFrame * currentTime) % totalFrame);
        }
    }

    public void SkillEndEvent()
    {
        _animationInfo.currentFrame = 0;
    }
}
