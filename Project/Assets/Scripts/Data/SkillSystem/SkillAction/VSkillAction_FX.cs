using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能特效事件
/// </summary>
[Serializable]
public class VSkillAction_FX:VSkillActionBase
{
    public VFXData fxData;

    /// <summary>
    /// 是否循环，循环需要设置持续时间
    /// </summary>
    public bool isCycle;

    /// <summary>
    /// 持续时间
    /// </summary>
    public float persistTime;

    /// <summary>
    /// 跟踪类型
    /// </summary>
    public TrackTypeEnum trackType;

    /// <summary>
    /// 是否跟随释放者存在
    /// </summary>
    public bool isFollowParent;
}

