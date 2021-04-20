﻿using System;
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
    /// 动画层面上判断能否释放技能
    /// </summary>
    public bool canSkill = true;

    /// <summary>
    /// 当前帧数
    /// </summary>
    public int currentFrame;

    public VActorAnimationInfo(VActorReferanceGameObject referance)
    {
        animator = referance.actorAnimator;
    }
}
