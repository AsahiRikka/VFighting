using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
/// <summary>
/// 特效与角色有关参数
/// </summary>
[Serializable]
public class VFXActorProperty
{
    /// <summary>
    /// 特效阵营
    /// </summary>
    [ReadOnly]
    public PlayerEnum e;

    /// <summary>
    /// 特效父物体
    /// </summary>
    [ReadOnly]
    public Transform parentTrans;
    
    /// <summary>
    /// 跟踪类型
    /// </summary>
    [ReadOnly]
    public TrackTypeEnum trackType;

    /// <summary>
    /// 相对释放位置
    /// </summary>
    [ReadOnly]
    public Vector3 offsetPos;

    /// <summary>
    /// 相对角度
    /// </summary>
    [ReadOnly]
    public Vector3 offsetRotate;

    /// <summary>
    /// 特效是否移动
    /// </summary>
    public bool isMove;
    
    /// <summary>
    /// 释放方向，移动特效
    /// </summary>
    [ReadOnly]
    public Vector3 emitVector;

    /// <summary>
    /// 移动速度
    /// </summary>
    [ReadOnly]
    public float emitSpeed;

    /// <summary>
    /// 特效持续类型
    /// </summary>
    public FXContinueTypeEnum FXContinueTypeEnum;

    /// <summary>
    /// 默认特效存在时间
    /// </summary>
    public float durationTime;
}
