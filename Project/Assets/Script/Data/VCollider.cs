using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储碰撞体数据
/// </summary>
[Serializable]
public struct VCollider
{
    public Vector3 center;

    public Vector3 size;

    public bool trigger;
}