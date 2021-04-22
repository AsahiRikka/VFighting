using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画数据
/// </summary>
[Serializable]
public class VActorAnimationInfo
{
    public Animator animator;

    /// <summary>
    /// 动画硬直等级，技能优先级高于此才能释放
    /// </summary>
    public int straightLevel;

    /// <summary>
    /// 当前帧数
    /// </summary>
    public int currentFrame;

    public VActorAnimationInfo(VActorReferanceGameObject referance)
    {
        animator = referance.actorAnimator;
    }
}
