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
    public static void ActorDataBind(string name,GameObject parent, VActorData data,PlayerEnum e)
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
            follow.actor = parent;
        }
        else
        {
            follow.actor = parent;
        }
        
        //角色初始化
        VActorBase actorBase = parent.AddComponent<VActorBase>();

        //成员变量初始化
        actorBase.BindInit();
        
        //调整角色正朝向
        parent.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        if (e == PlayerEnum.player_2)
        {
            parent.transform.rotation =
                Quaternion.Euler(new Vector3(0,180,0));
            parent.transform.position=new Vector3(5,0,0);
        }
        
        //基础属性数据绑定
        ActorPropertyBind(data.actorProperty,actorBase.actorProperty,data,e);
        
        //设置layer
        Transform trans = parent.transform;
        foreach (var t in trans.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = LayerMask.NameToLayer(actorBase.actorProperty.Layer);
        }

        //引用绑定
        actorBase.referanceGameObject = parent.GetComponent<VActorReferanceGameObject>();
        
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
        if (e == PlayerEnum.player_1)
        {
            changeProperty.Layer = "Player_1";
        }
        else
        {
            changeProperty.Layer = "Player_2";
        }
    }
}
