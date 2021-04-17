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
    public static void ActorDataBind(string name, GameObject actor, VActorData data,PlayerEnum e)
    {
        //设置摄像机跟踪
        string act = "Player1";
        if (e == PlayerEnum.player_2)
            act = "Player2";
        GameObject camera = GameObject.Find(act);
        CameraFollowObj follow;
        camera.TryGetComponent<CameraFollowObj>(out follow);
        if (follow == null)
        {
            follow = camera.AddComponent<CameraFollowObj>();
            follow.actor = actor;
        }
        else
        {
            follow.actor = actor;
        }
        
        
        //角色初始化
        VActorBase actorBase = actor.AddComponent<VActorBase>();

        //成员变量初始化
        actorBase.BindInit();
        
        //基础属性数据绑定
        ActorPropertyBind(data.actorProperty,actorBase.actorProperty,data,e);
        
        //调整角色正朝向
        actor.transform.rotation = Quaternion.Euler(data.skillActions.defaultSkillActions.motion.animationDefaultRotate);
        if (e == PlayerEnum.player_2)
        {
            actor.transform.rotation =
                Quaternion.Euler(data.skillActions.defaultSkillActions.motion.animationDefaultRotate +new Vector3(0,180,0));
            actor.transform.position=new Vector3(5,0,0);
        }
        
        //引用绑定
        actorBase.referanceGameObject = actor.GetComponent<VActorReferanceGameObject>();
        
        //技能行为绑定
        actorBase.skillActions = data.skillActions;
        
        actorBase.ActorDataInit();
        
        //逻辑部分初始化
        actorBase.LogicInit();
    }

    private static void ActorPropertyBind(VActorProperty property,VActorChangeProperty changeProperty,VActorData data,PlayerEnum e)
    {
        changeProperty.heathPoints = property.heathPoints;
        changeProperty.actorDamage = property.actorDamage;
        changeProperty.actorMoveSpeed = property.actorMoveSpeed;
        changeProperty.actorBackSpeed = property.actorBackSpeed;
        changeProperty.actorDashSpeed = property.actorDashSpeed;
        changeProperty.jumpForce = property.jumpForce;
        changeProperty.actorAttackSpeed = property.actorAttackSpeed;
        changeProperty.actorAccumulateTankSpeed = property.actorAccumulateTankSpeed;
        changeProperty.actorWeight = property.actorWeight;

        changeProperty.playerEnum = e;
        
    }
}
