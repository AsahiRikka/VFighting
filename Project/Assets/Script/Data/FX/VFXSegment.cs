using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特效逻辑片段
/// </summary>
[Serializable]
public class VFXSegment
{
    /// <summary>
    /// 开始时间，特效播放开始计时
    /// </summary>
    public float startTime;

    /// <summary>
    /// 结束时间，特效播放开始计时
    /// </summary>
    public float endTime;
    
    /// <summary>
    /// 攻击判定
    /// </summary>
    public VCollider fxCollider;
}
