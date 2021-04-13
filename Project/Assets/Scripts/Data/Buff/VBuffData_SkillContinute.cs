using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能连携的buff
/// </summary>
[CreateAssetMenu(order = 50,fileName = "BuffData_SkillContinute_",menuName = "创建技能连携配置")]
public class VBuffData_SkillContinute:SerializedScriptableObject
{
    [ReadOnly] public ActorBuffTypeEnum buffType = ActorBuffTypeEnum.skillContinute;
    
    public VBuffBase buffBaseProperty;
}
 