using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
/// <summary>
/// 角色属性，运行中变化，影响游戏表现
/// </summary>
[Serializable]
public class VActorChangeProperty
{
    /// <summary>
    /// 当前生命值
    /// </summary>
    public float heathPoints;
    
    /// <summary>
    /// 当前攻击力，技能在此基础上计算倍率
    /// </summary>
    public float actorDamage;

    /// <summary>
    /// 角色面部朝向，正方向向右作为默认值
    /// </summary>
    public Vector3 actorDirection;

    /// <summary>
    /// 当前移动速度
    /// </summary>
    public float actorMoveSpeed;

    /// <summary>
    /// 后退速度
    /// </summary>
    public float actorBackSpeed;

    /// <summary>
    /// 冲刺速度
    /// </summary>
    public float actorDashSpeed;

    /// <summary>
    /// 跳跃力
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// 当前攻击速度，影响动画速度，从而影响技能系统
    /// </summary>
    public float actorAttackSpeed;

    /// <summary>
    /// 当前重量
    /// </summary>
    public float actorWeight;

    /// <summary>
    /// 当前蓄力速度
    /// </summary>
    public float actorAccumulateTankSpeed;

    /// <summary>
    /// 当前已蓄力长度
    /// </summary>
    public float skillAccumulateTankLength = 0;

    /// <summary>
    /// 当前蓄力槽数量
    /// </summary>
    public int skillAccumulateTankCount = 0;

    /// <summary>
    /// 角色阵营
    /// </summary>
    [Sirenix.OdinInspector.ReadOnly]
    public CampTypeEnum campTypeEnum = CampTypeEnum.red;

    /// <summary>
    /// 角色识别
    /// </summary>
    [Sirenix.OdinInspector.ReadOnly]
    public PlayerEnum playerEnum = PlayerEnum.player_1;

    /// <summary>
    /// 玩家类型
    /// </summary>
    [Sirenix.OdinInspector.ReadOnly]
    public PlayerTypeEnum playerTypeEnum = PlayerTypeEnum.immortal;

    /// <summary>
    /// 一些回复效率
    /// </summary>
    public VActorEfficiencyProperty efficiencyProperty;
}
