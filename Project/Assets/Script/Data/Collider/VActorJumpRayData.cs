using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 跳跃技能射线投射距离，用于判断是否进入某个跳跃阶段
/// </summary>
[Serializable]
public class VActorJumpRayData
{
    /// <summary>
    /// 空中停滞距离，超过此距离尝试触发air技能
    /// </summary>
    public float airSkillDistance;

    /// <summary>
    /// 下落技能距离，小于此距离且未接触地面尝试播放落地技能
    /// </summary>
    public float fallSkillDistance;

    /// <summary>
    /// 空中被攻击下落技能距离，在空中攻击动作下，小于此距离尝试触发
    /// </summary>
    public float attackFallSkillDistance;
}
