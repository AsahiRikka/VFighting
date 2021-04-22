using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 伤害类buff
/// </summary>
[CreateAssetMenu(order = 50,fileName = "BuffData_Damage_",menuName = "创建buff配置/创建伤害Buff配置")]
public class VBuffData_Damage:SerializedScriptableObject
{
    [ReadOnly]
    public ActorBuffTypeEnum buffType = ActorBuffTypeEnum.damange;
    
    public VBuffBase buffBaseProperty;
}