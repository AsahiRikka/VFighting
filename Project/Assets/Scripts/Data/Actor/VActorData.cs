using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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

    public VSkillActions skillActions;
}
