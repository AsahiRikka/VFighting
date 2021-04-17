using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最小动画片段
/// </summary>
[Serializable]
public class VSegmentMotion
{
    public VSegmengMotionType Type;
    
    public int startFrame;

    public int endFrame;
}

public enum VSegmengMotionType{
    keyFrame,
    allskill,
}