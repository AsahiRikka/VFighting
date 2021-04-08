using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff基本数据
/// </summary>
[Serializable]
public class VBuffBase
{
    public string BuffID;
    
    public VBuffSetting buffSetting;

    public VBuffFXSetting buffFXSetting;
}


/// <summary>
/// Buff类型
/// </summary>
public enum ActorBuffTypeEnum
{
    /// <summary>
    /// 伤害相关
    /// </summary>
    damange,
    
    /// <summary>
    /// 控制类
    /// </summary>
    control,
}