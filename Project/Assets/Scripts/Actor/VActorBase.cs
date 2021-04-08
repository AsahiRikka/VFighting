﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
/// <summary>
/// 角色基类
/// </summary>
public class VActorBase : MonoBehaviour
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public VActorChangeProperty actorProperty;

    /// <summary>
    /// 角色状态
    /// </summary>
    public VActorState state;

    /// <summary>
    /// 技能行为集合
    /// </summary>
    public VSkillActions skillActions;

    /// <summary>
    /// 角色控制器
    /// </summary>
    public VActorController actorController;

    /// <summary>
    /// 角色控制器数据
    /// </summary>
    public VActorInfo actorInfo;

    /// <summary>
    /// 角色事件中心
    /// </summary>
    public VActorEvent actorEvent;

    /// <summary>
    /// 需要与存储数据进行绑定的初始化，最先进行
    /// </summary>
    public void BindInit()
    {
        actorProperty=new VActorChangeProperty();

        actorEvent=new VActorEvent();
        skillActions=new VSkillActions();
    }

    /// <summary>
    /// 需要绑定后数据的初始化
    /// </summary>
    public void ActorDataInit()
    {
        actorInfo=new VActorInfo(actorProperty,skillActions);
        state=new VActorState(actorProperty.playerEnum);
    }

    /// <summary>
    /// 逻辑部分初始化
    /// </summary>
    public void LogicInit()
    {
        actorController = new VActorController(actorProperty, state, skillActions, actorInfo, actorEvent);
        actorController.ControllerInit();
    }

    private void Start()
    {
        //全部初始化完成，自动进入idle技能
        actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(skillActions.defaultSkillActions);
    }

    private void Update()
    {
        actorController.ControllerUpdate();
    }

    private void OnDestroy()
    {
        actorController.COntrollerExit();
    }
}