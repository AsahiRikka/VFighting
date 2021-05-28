using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 其他设置
/// </summary>
[Serializable]
public class OtherSetting:ICloneable
{
    public ResolutionTypeEnum resolutionType;
    
    public QualitySettingTypeEnum gameQuality;

    public object Clone()
    {
        OtherSetting newOther=new OtherSetting();
        newOther.resolutionType = this.resolutionType;
        newOther.gameQuality = this.gameQuality;
        return newOther;
    }
}

/// <summary>
/// 分辨率设置
/// </summary>
public enum ResolutionTypeEnum
{
    FullScreen,
    w1920H1080,
    W1080H720,
}

/// <summary>
/// 画面等级设置
/// </summary>
public enum QualitySettingTypeEnum
{
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh,
    Ultra
}