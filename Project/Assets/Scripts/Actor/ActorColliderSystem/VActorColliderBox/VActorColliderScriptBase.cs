using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 碰撞器预设
/// </summary>
[Serializable]
public class VActorColliderScriptBase
{
    public BoxCollider collider;
}

public enum ColliderTypeEnum
{
    hit,
    passive,
    defence,
}