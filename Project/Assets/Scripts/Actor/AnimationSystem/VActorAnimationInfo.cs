using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画数据
/// </summary>
public class VActorAnimationInfo
{
    public Animator animator;

    public int currentFrame;

    /// <summary>
    /// 动画层面上判断能否释放技能
    /// </summary>
    public bool canSkill = true;

    public VActorAnimationInfo(VActorReferanceGameObject referance)
    {
        animator = referance.actorAnimator;
    }
}
