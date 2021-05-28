using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特效自身属性
/// </summary>
[Serializable]
public class VFXProperty
{
    /// <summary>
    /// 特效ID
    /// </summary>
    public string ID;

    /// <summary>
    /// 特效物体引用
    /// </summary>
    public GameObject FXParent;
    
    /// <summary>
    /// 是否循环
    /// </summary>
    public bool isLooping;

    /// <summary>
    /// 特效持续时间，循环起效
    /// </summary>
    public float durationTime;
    
    /// <summary>
    /// 特效引用
    /// </summary>
    public ParticleSystem ParticleSystem;
    
    /// <summary>
    /// 特效的碰撞
    /// </summary>
    public VFXCollider FXCollider;
}
