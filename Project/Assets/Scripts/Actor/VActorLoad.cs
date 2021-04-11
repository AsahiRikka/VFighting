﻿using System;
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
        actor.transform.rotation = Quaternion.Euler(data.skillActions.defaultSkillActions.motion.animationDefaultRotate);
        
        //角色初始化
        VActorBase actorBase = actor.AddComponent<VActorBase>();

        //成员变量初始化
        actorBase.BindInit();
        
        //基础属性数据绑定
        ActorPropertyBind(data.actorProperty,actorBase.actorProperty,data);
        
        //引用绑定
        actorBase.referanceGameObject = actor.GetComponent<VActorReferanceGameObject>();
        
        //技能行为绑定
        actorBase.skillActions = data.skillActions;
        
        actorBase.ActorDataInit();
        
        //逻辑部分初始化
        actorBase.LogicInit();
    }

    private static void ActorPropertyBind(VActorProperty property,VActorChangeProperty changeProperty,VActorData data)
    {
        changeProperty.heathPoints = property.heathPoints;
        changeProperty.actorDamage = property.actorDamage;
        changeProperty.actorMoveSpeed = property.actorMoveSpeed;
        changeProperty.actorAttackSpeed = property.actorAttackSpeed;
        changeProperty.actorAccumulateTankSpeed = property.actorAccumulateTankSpeed;
        
        //使用默认动画的角度
        changeProperty.actorDirection = data.skillActions.defaultSkillActions.motion.animationDefaultRotate;
    }
}
