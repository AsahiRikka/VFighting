using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能特效事件
/// </summary>
[Serializable]
public class VSkillAction_FX:VSkillActionBase
{
    public VFXData fxData;

    /// <summary>
    /// 是否循环
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


/// <summary>
/// 追踪类型
/// </summary>
public enum TrackTypeEnum
{
    /// <summary>
    /// 相对世界坐标
    /// </summary>
    [InfoBox("相对世界坐标")]
    world,
    /// <summary>
    /// 相对父物体坐标
    /// </summary>
    [InfoBox("相对释放者")]
    relative,
}