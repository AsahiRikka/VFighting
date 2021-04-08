using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 存储技能运行数据
/// </summary>
public class VActorSkillInfo
{
    /// <summary>
    /// 当前技能
    /// </summary>
    public VSkillAction currentSkill;

    /// <summary>
    /// 判断能否打断技能，会被buff，动画等判断实时修改
    /// </summary>
    public bool isInterrupt = true;
    
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