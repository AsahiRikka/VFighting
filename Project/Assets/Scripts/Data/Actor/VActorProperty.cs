using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 角色基础属性
/// </summary>
[Serializable]
public class VActorProperty
{
    /// <summary>
    /// 基础生命值
    /// </summary>
    public float heathPoints = 1000;

    /// <summary>
    /// 基础攻击力，技能在此基础上计算倍率
    /// </summary>
    public float actorDamage = 100;

    /// <summary>
    /// 基础移动速度
    /// </summary>
    public float actorMoveSpeed = 100;

    /// <summary>
    /// 基础攻击速度，影响动画速度，从而影响技能系统
    /// </summary>
    public float actorAttackSpeed = 100;

    /// <summary>
    /// 基础蓄力速度
    /// </summary>
    public float actorAccumulateTankSpeed = 100;

    /// <summary>
    /// 角色基础重量
    /// </summary>
    public float actorWeight = 100;

    /// <summary>
    /// 角色正方向
    /// </summary>
    // [InfoBox("角色正方向，向右为准")] 
    // public Vector3 actorDefaultRotate = Vector3.zero;
}
