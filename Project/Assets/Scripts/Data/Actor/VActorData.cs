using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

/// <summary>
/// 角色数据
/// </summary>
[CreateAssetMenu(order = 41,fileName = "Actor_",menuName = "创建角色资源")]
[TypeInfoBox("角色数据")]
public class VActorData : SerializedScriptableObject
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public string actorID;

    public string actorName;
    
    public VActorProperty actorProperty;

    [Space(30)]
    
    [InfoBox("跳跃射线参数")]
    public VActorJumpRayData jumpRay;
    
    [Space(30)]
    
    [OdinSerialize]
    public VSkillActions skillActions;
}
