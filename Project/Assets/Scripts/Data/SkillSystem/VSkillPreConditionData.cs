using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 释放前置条件
/// </summary>
[Serializable]
public class VSkillPreConditionData
{
    [InfoBox("需要技能连携条件，0代表最开始的技能连携，多个代表有多个技能触发路线")] 
    public List<VSkillPreConditionData_SkillContinue> skillContinues;

    [InfoBox("技能无法释放的状态集合")]
    public List<ActorStateTypeEnum> notSkillPreState;
}
