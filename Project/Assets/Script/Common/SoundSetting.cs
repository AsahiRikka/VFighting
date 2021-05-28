using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 声音设置
/// </summary>
[Serializable]
public class SoundSetting:ICloneable
{
    /// <summary>
    /// 音效大小
    /// </summary>
    [Range(0,1)]
    public double effectSound = 1;

    /// <summary>
    /// 音乐大小
    /// </summary>
    [Range(0,1)]
    public double musicSound = 1;

    /// <summary>
    /// 人声大小
    /// </summary>
    [Range(0,1)]
    public double characterSound = 1;

    public object Clone()
    {
        SoundSetting newSound=new SoundSetting();
        newSound.effectSound = this.effectSound;
        newSound.musicSound = this.musicSound;
        newSound.characterSound = this.characterSound;
        return newSound;
    }
}

public enum SoundTypEnum
{
    BGM,
    Effect,
    ActorVoice,
}