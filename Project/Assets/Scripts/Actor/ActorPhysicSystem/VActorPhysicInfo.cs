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
    /// 持续物理效果的字典
    /// </summary>
    public Dictionary<VSkillAction_Physic, bool> PhysicDic = new Dictionary<VSkillAction_Physic, bool>();
}
