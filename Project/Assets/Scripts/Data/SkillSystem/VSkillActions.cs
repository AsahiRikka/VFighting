using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
/// <summary>
/// 一个角色的技能事件集合
/// </summary>
public class VSkillActions
{
    [InfoBox("普通技能集合，指skill类型")]
    public List<VSkillAction> actorSkillActions;

    [InfoBox("特殊技能，需要特殊处理")] 
    [OdinSerialize]
    public Dictionary<ActorStateTypeEnum, VSkillAction> specialSkillDic;
}
