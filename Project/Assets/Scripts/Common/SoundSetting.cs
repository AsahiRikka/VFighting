using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 声音设置
/// </summary>
public class SoundSetting
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
}

public enum SoundTypEnum
{
    BGM,
    Effect,
    ActorVoice,
}