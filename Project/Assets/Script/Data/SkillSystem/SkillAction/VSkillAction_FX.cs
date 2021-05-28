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
    public string FXID;

    /// <summary>
    /// 跟踪类型
    /// </summary>
    public TrackTypeEnum trackType;

    /// <summary>
    /// 相对释放位置
    /// </summary>
    public Vector3 offsetPos;

    /// <summary>
    /// 相对角度
    /// </summary>
    public Vector3 offsetRotate;

    [InfoBox("特效持续方式")]
    public FXContinueTypeEnum ContinueTypeEnum;

    [ShowIf("ContinueTypeEnum",FXContinueTypeEnum.time)]
    public float time;
    
    
    [InfoBox("是否有位移")]
    public bool isMove;
    
    /// <summary>
    /// 释放方向，移动特效
    /// </summary>
    [ShowIf("isMove")]
    public Vector3 emitVector;

    /// <summary>
    /// 释放速度
    /// </summary>
    [ShowIf("isMove")]
    public float emitSpeed;
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

/// <summary>
/// 特效持续方式
/// </summary>
public enum FXContinueTypeEnum
{
    /// <summary>
    /// 按特效预设自动关闭
    /// </summary>
    auto,
    /// <summary>
    /// 按照技能配置提供的时间
    /// </summary>
    time,
    /// <summary>
    /// 手动设置
    /// </summary>
    manual,
}