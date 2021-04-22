using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能事件基类
/// </summary>
[Serializable]
public class VSkillActionBase
{
    public SkillActionEnum skillActionType;

    [ShowIf("@skillActionType == SkillActionEnum.skillHit ||" +
            "skillActionType == SkillActionEnum.beAttack")]
    public SkillActionTargetEnum target;

    [ShowIf("skillActionType",(SkillActionEnum.frame))]
    public int startFrame;

    [ShowIf("skillActionType",SkillActionEnum.keyFrame)]
    public int keyFrame;
    
    [ShowIf("skillActionType",SkillActionEnum.frame)]
    public int endFrame;
}

/// <summary>
/// 技能事件触发条件
/// </summary>
public enum SkillActionEnum
{
    /// <summary>
    /// 技能开始
    /// </summary>
    [InfoBox("技能开始")]
    skillAction,
    /// <summary>
    /// 技能运行时
    /// </summary>
    [InfoBox("技能运行时")]
    skillUpdate,
    /// <summary>
    /// 技能结束
    /// </summary>
    [InfoBox("技能结束")]
    skillEnd,
    /// <summary>
    /// 技能命中
    /// </summary>
    [InfoBox("技能命中敌方")]
    skillHit,
    /// <summary>
    /// 被攻击
    /// </summary>
    [InfoBox("被攻击")]
    beAttack,
    /// <summary>
    /// 按键触发
    /// </summary>
    [InfoBox("按键触发")]
    keyTrigger,
    /// <summary>
    /// 按帧数范围
    /// </summary>
    [InfoBox("按帧数范围")]
    frame,
    /// <summary>
    /// 指定帧
    /// </summary>
    [InfoBox("指定帧")]
    keyFrame,
    /// <summary>
    /// 攻击到防御
    /// </summary>
    [InfoBox("攻击到防御")]
    attackToDefence,
    /// <summary>
    /// 成功防御攻击
    /// </summary>
    [InfoBox("成功防御攻击")]
    successDefence,
}

/// <summary>
/// 技能事件目标
/// </summary>
public enum SkillActionTargetEnum
{
    self,
    anamy
}