using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能物理效果事件
/// </summary>
[Serializable]
public class VSkillAction_Physic:VSkillActionBase
{
    /// <summary>
    /// 初始速度
    /// </summary>
    public float initSpeed = 1;
    
    /// <summary>
    /// 初始角度
    /// </summary>
    public Vector3 initVector = Vector3.right;
    
    /// <summary>
    /// 水平速度衰减
    /// </summary>
    public float speedDecay = 1;
    
    /// <summary>
    /// 落地反弹次数
    /// </summary>
    public int bounceTimes = 1;
    
    /// <summary>
    /// 重力倍率
    /// </summary>
    public float gravityScale = 1;

    // public DotweenTypeEnum e;
    //
    // public Vector3 moveVector;
    //
    // public float time;
    //
    // public Ease ease;
    
}

public enum VActorPhysicComponentEnum
{
    move,
    dash,
    jump,
    retreat,
}

public enum DotweenTypeEnum
{
    move,
    jump,
    
}