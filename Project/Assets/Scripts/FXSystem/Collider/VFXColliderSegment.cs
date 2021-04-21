using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特效碰撞单元
/// </summary>
[Serializable]
public class VFXColliderSegment
{
    public VFXColliderScript VFXColliderScript;

    /// <summary>
    /// 开始时间,duration
    /// </summary>
    public float startTime;

    /// <summary>
    /// 关闭时间，duration
    /// </summary>
    public float endTime;
}
