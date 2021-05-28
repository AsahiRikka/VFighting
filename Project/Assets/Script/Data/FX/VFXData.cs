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
}