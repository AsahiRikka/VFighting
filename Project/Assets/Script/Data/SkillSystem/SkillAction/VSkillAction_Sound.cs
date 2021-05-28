using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能音效事件
/// </summary>
[Serializable]
public class VSkillAction_Sound:VSkillActionBase
{
    [InfoBox("声音类型")] 
    public SoundTypEnum soundType;
    
    [InfoBox("声音片段")]
    public AudioClip audioClip;

    [InfoBox("是否循环，循环状态需要技能系统关闭")]
    public bool isLoop;
}
