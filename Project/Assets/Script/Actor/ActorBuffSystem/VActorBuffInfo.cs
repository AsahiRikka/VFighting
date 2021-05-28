using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff数据
/// </summary>
public class VActorBuffInfo
{
    /// <summary>
    /// 角色控制buff集合
    /// </summary>
    public List<VBuffData_Control> buffOfControlList;

    /// <summary>
    /// 角色伤害类buff集合
    /// </summary>
    public List<VBuffData_Damage> buffOfDamageList;

    /// <summary>
    /// 角色技能连携Buff
    /// </summary>
    public List<VBuffData_SkillContinute> buffSkillContinuteList;
    
    /// <summary>
    /// 角色状态数据
    /// </summary>
    private VActorState _actorState;

    public VActorBuffInfo()
    {
        buffOfControlList=new List<VBuffData_Control>();
        buffOfDamageList=new List<VBuffData_Damage>();
    }
}

