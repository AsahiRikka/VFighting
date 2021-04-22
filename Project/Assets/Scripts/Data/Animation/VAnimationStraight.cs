using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 动画硬直，一些无法被打断的片段
/// </summary>
[Serializable]
public class VAnimationStraight
{
    [InfoBox("硬直等级")]
    public int straightLevel;
    
    public int startFrame;

    public int endFrame;
}
