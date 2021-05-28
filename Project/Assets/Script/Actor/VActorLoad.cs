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
        EventManager.ActorCameraFollow.BoradCastEvent(e, parent.transform);

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
        
        //可变参数设置
        ActorGameSettingBind(actorBase.actorProperty, GameFramework.instance._service.SettingManager.GameModeSetting,
            GameFramework.instance._service.SettingManager.GameStartSetting);
        
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

    private static void ActorPropertyBind(VActorProperty property, VActorChangeProperty changeProperty, VActorData data,
        PlayerEnum e)
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
        changeProperty.jumpRayData = data.jumpRay;

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

    private static void ActorGameSettingBind(VActorChangeProperty changeProperty, GameModeSetting gameModeSetting,
        GameStartSetting startSetting)
    {
        //人机设置
        if (gameModeSetting.GameMode == GameModeTypeEnum.playerToComputer &&
            changeProperty.playerEnum == PlayerEnum.player_2)
        {
            changeProperty.playerTypeEnum = PlayerTypeEnum.computer;
        }

        //生命值设置
        if (startSetting.healthScale == HeathScaleTypeEnum.two)
        {
            changeProperty.heathPoints *= 2;
        }else if (startSetting.healthScale == HeathScaleTypeEnum.three)
        {
            changeProperty.heathPoints *= 3;
        }
        
        //起始生命值设置
        changeProperty.initHeathPoints = changeProperty.heathPoints;
    }
}
