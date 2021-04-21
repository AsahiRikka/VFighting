using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 粒子系统播放
/// </summary>
public class VFXParticleSystemControl
{
    public VFXParticleSystemControl(VFXControllerInfo controllerInfo, VFXProperty property,
        VFXActorProperty actorProperty, VFXBase vfxBase)
    {
        _controllerInfo = controllerInfo;
        _property = property;
        _vfxBase = vfxBase;
        _actorProperty = actorProperty;

        _fxTrans = _property.FXParent.transform;
    }

    private VFXControllerInfo _controllerInfo;
    private VFXProperty _property;
    private VFXBase _vfxBase;
    private VFXActorProperty _actorProperty;

    private Transform _fxTrans;

    /// <summary>
    /// 开始时间
    /// </summary>
    private float initTime;

    /// <summary>
    /// 当前配置结束时间
    /// </summary>
    private float currentTime;

    /// <summary>
    /// 开始播放特效
    /// </summary>
    public void OnEnable()
    {
        if (_actorProperty.FXContinueTypeEnum == FXContinueTypeEnum.time)
        {
            currentTime = _actorProperty.durationTime;
        }else if (_actorProperty.FXContinueTypeEnum == FXContinueTypeEnum.auto)
        {
            currentTime = _property.durationTime;
        }
        
        //先设置父物体为角色
        _fxTrans.SetParent(_actorProperty.parentTrans);
        
        //修改释放位置
        _fxTrans.localPosition = _actorProperty.offsetPos;
        _fxTrans.localRotation = Quaternion.Euler(_actorProperty.offsetRotate);

        //如果相对世界的特效取消父物体
        if (_actorProperty.trackType == TrackTypeEnum.world)
        {
            _fxTrans.SetParent(null);
        }

        //播放特效
        _property.ParticleSystem.Play();
        initTime = Time.time;
    }

    public void Update()
    {
        //自动结束
        if (_actorProperty.FXContinueTypeEnum != FXContinueTypeEnum.manual)
        {
            //计算播放时间
            _controllerInfo.playTime = Time.time - initTime;
        
            if (_controllerInfo.playTime >= currentTime)
            {
                _vfxBase.MyDisable();
            }
        }

        //如果有特效位移进行位移
        if (_actorProperty.isMove)
        {
            _fxTrans.position += _actorProperty.emitVector * (_actorProperty.emitSpeed * Time.deltaTime);
        }
    }

    public void Disable()
    {
        //计时清零
        _controllerInfo.playTime = 0;
        
        //停止播放
        _property.ParticleSystem.Stop();
    }
}
