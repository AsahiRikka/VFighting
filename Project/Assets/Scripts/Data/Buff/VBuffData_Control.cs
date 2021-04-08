using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 控制类技能
/// </summary>
[CreateAssetMenu(order = 50,fileName = "BuffData_Control_",menuName = "创建控制类buff配置")]
public class VBuffData_Control:SerializedScriptableObject
{
    [ReadOnly]
    public ActorBuffTypeEnum buffType = ActorBuffTypeEnum.control;
    
    public VBuffBase buffBaseProperty;

    [Space(20)] 
    [InfoBox("技能释放开启/禁止")] 
    public bool skillSet = true;

    [InfoBox("移动开启/禁止")]
    public bool moveSet = true;

    [InfoBox("跳跃开启/禁止")] 
    public bool jumpSet = true;

    [InfoBox("移动速度增减")] 
    public List<VBuffEffectType> moveSpeed;

    [InfoBox("攻击速度增减")] 
    public List<VBuffEffectType> attackSpeed;
}
