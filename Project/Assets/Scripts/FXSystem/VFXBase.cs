﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
/// <summary>
/// 特效基类，特效物体挂载并处理
/// </summary>
public class VFXBase : MonoBehaviour
{
    /// <summary>
    /// 特效固有属性
    /// </summary>
    public VFXProperty Property;

    [Space(30)]
    
    [InfoBox("实际启动后角色的传参")]
    public VFXActorProperty ActorProperty;

    [Space(30)] 
    
    private VFXControllerInfo _vfxControllerInfo;

    /// <summary>
    /// 粒子特效播放控制器
    /// </summary>
    private VFXParticleSystemControl _particleSystemControl;

    /// <summary>
    /// 碰撞控制
    /// </summary>
    private VFXColliderControl _vFXColliderControl;
    
    /// <summary>
    /// 音效控制
    /// </summary>
    private VFXSoundControl _soundControl;

    /// <summary>
    /// 特效使用时初始化，调用后再enable
    /// </summary>
    public void FXPrefabInit(VSkillAction_FX skillActionFX,Transform actor,PlayerEnum e)
    {
        _vfxControllerInfo=new VFXControllerInfo();

        _particleSystemControl = new VFXParticleSystemControl(_vfxControllerInfo, Property, ActorProperty, this);
        _vFXColliderControl = new VFXColliderControl(Property, _vfxControllerInfo, ActorProperty);
        _soundControl=new VFXSoundControl();
        
        ActorProperty.e = e;
        ActorProperty.parentTrans = actor;
        ActorProperty.trackType = skillActionFX.trackType;
        ActorProperty.offsetPos = skillActionFX.offsetPos;
        ActorProperty.offsetRotate = skillActionFX.offsetRotate;
        ActorProperty.emitVector = skillActionFX.emitVector;
        ActorProperty.emitSpeed = skillActionFX.emitSpeed;
        ActorProperty.isMove = skillActionFX.isMove;
        ActorProperty.FXContinueTypeEnum = skillActionFX.ContinueTypeEnum;
        ActorProperty.durationTime = skillActionFX.time;
        

        MyEnable();
    }
    
    private void MyEnable()
    {
        _particleSystemControl.OnEnable();
        _vFXColliderControl.OnEnable();
    }

    private void Update()
    {
        _particleSystemControl.Update();
        _vFXColliderControl.Update();
    }

    public void MyDisable()
    {
        _particleSystemControl.Disable();
        _vFXColliderControl.Disable();
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        
    }

    private void OnParticleSystemStopped()
    {
        MyDisable();
    }
}

