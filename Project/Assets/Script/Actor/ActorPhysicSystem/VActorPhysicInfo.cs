using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// 物理效果数据
/// </summary>
public class VActorPhysicInfo
{
    /// <summary>
    /// 角色当前垂直加速度
    /// </summary>
    public float actorVerticalAcceleration = -200;

    /// <summary>
    /// 水平摩擦加速度
    /// </summary>
    public float actorHorizontalSpeedDecay = 200;

    /// <summary>
    /// 是否在空中，在空中时会根据垂直方向速度持续尝试释放跳跃技能
    /// </summary>
    public bool inAir = false;
}
