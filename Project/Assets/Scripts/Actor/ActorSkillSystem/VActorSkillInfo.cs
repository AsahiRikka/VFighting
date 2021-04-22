using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 存储技能运行数据
/// </summary>
[Serializable]
public class VActorSkillInfo
{
    /// <summary>
    /// 当前技能
    /// </summary>
    public VSkillAction currentSkill;

    /// <summary>
    /// 技能释放方向，1为右，-1为左
    /// </summary>
    public int skillDir;

    public VActorSkillInfo(VActorChangeProperty property)
    {
        if (property.playerEnum == PlayerEnum.player_1)
            skillDir = 1;
        else if (property.playerEnum == PlayerEnum.player_2)
            skillDir = -1;
    }
}