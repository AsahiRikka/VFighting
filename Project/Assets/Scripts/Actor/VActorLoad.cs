using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 角色资源绑定
/// </summary>
public static class VActorLoad
{
    public static void ActorDataBind(string name, GameObject actor, VActorData data)
    {
        //调整角色正朝向
        actor.transform.rotation = Quaternion.Euler(data.actorProperty.actorDefaultPositive);
        
        //角色初始化
        VActorBase actorBase = actor.AddComponent<VActorBase>();

        //成员变量初始化
        actorBase.BindInit();
        
        //基础属性数据绑定
        ActorPropertyBind(data.actorProperty,actorBase.actorProperty);
        
        //技能行为绑定
        actorBase.skillActions = data.skillActions;
        
        actorBase.ActorDataInit();
        
        //逻辑部分初始化
        actorBase.LogicInit();
    }

    private static void ActorPropertyBind(VActorProperty property,VActorChangeProperty changeProperty)
    {
        changeProperty.heathPoints = property.heathPoints;
        changeProperty.actorDamage = property.actorDamage;
        changeProperty.actorMoveSpeed = property.actorMoveSpeed;
        changeProperty.actorAttackSpeed = property.actorAttackSpeed;
        changeProperty.actorAccumulateTankSpeed = property.actorAccumulateTankSpeed;
        changeProperty.actorDirection = property.actorDefaultPositive;
    }
}
