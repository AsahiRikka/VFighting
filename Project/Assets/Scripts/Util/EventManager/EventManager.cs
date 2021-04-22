using UnityEngine;
using System.Collections;

/// <summary>
/// 描述：事件系统，事件定义
/// </summary>

public class EventManager
{
    public static FEvent FeventTest = new FEvent();

    
    
    //技能事件
    
    
    
    //对于碰撞事件，只有不同阵营碰撞才触发事件。

    /// <summary>
    /// 攻击->身体
    /// </summary>
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> HitToPassiveEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    /// <summary>
    /// 身体->攻击
    /// </summary>
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> PassiveToHitEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> HitToDefenceEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> DefenceToHitEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
}
