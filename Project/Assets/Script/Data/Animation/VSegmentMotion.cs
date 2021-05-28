using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 最小动画片段
/// </summary>
[Serializable]
public class VSegmentMotion
{
    public VSegmentMotionType type;
    
    [ShowIf("type",VSegmentMotionType.keyFrame)]
    public int startFrame;

    [ShowIf("type",VSegmentMotionType.keyFrame)]
    public int endFrame;
}

public enum VSegmentMotionType{
    keyFrame,
    allskill,
}