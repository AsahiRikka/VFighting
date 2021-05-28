using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能触发数据
/// </summary>
[Serializable]
public class VSkillSignalData
{
    [InfoBox("触发方式")]
    public SkillSignalEnum SignalEnum;
    
    public List<VSkillSignalDataSegment> skillSignalDataSegments;
}

/// <summary>
/// 信号类型
/// </summary>
public enum SkillSignalEnum
{
    none,
    allPress,
    combinationPress,
    
    //hold类型释放按键会无条件解除技能
    allHold,
    combinationHold,
}
