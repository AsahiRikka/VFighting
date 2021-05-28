using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// buff影响方式
/// </summary>
[Serializable]
public class VBuffEffectType
{
    [InfoBox("buff触发方式")]
    public BuffEffectTypeEnum buffEffectType;

    [InfoBox("buff数值意义")]
    public BuffEffectValueTypeEnum buffEffectValueType;

    [InfoBox("开始结束时间只对持续类型buff有影响")]
    
    public float startTime;
    public float endTime;
}

public enum BuffEffectTypeEnum
{
    /// <summary>
    /// buff开始时触发
    /// </summary>
    buffStartTrigger,
    /// <summary>
    /// buff结束时触发
    /// </summary>
    buffEndTrigger,
    /// <summary>
    /// 按时间点触发，根据startTime
    /// </summary>
    timeTrigger,
    /// <summary>
    /// 在一段时间内完成全部效果
    /// </summary>
    persistOfTime,
    /// <summary>
    /// 在整个buff时间内完成全部效果
    /// </summary>
    allOfBuff,
}

public enum BuffEffectValueTypeEnum
{
    scale,
    number,
}