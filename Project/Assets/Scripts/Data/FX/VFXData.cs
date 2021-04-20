using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 特效数据
/// </summary>
[CreateAssetMenu(order = 45,fileName = "FX_",menuName = "创建特效配置资源")]
public class VFXData:SerializedScriptableObject
{
    public string FXID;

    /// <summary>
    /// 特效物体
    /// </summary>
    [InfoBox("特效预设根物体为空，作为特效中心点")]
    public GameObject particleObj;

    /// <summary>
    /// 特效
    /// </summary>
    public ParticleSystem particle;

    /// <summary>
    /// 碰撞集合
    /// </summary>
    public List<VFXSegment> vFXSegments;
}