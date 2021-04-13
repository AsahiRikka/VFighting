using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能物理效果事件
/// </summary>
[Serializable]
public class VSkillAction_Physic:VSkillActionBase
{
    
    public float initSpeed = 1;

    public Vector3 initAngle = Vector3.zero;

    public float speedDecay = 1;

    public int bounceTimes = 1;

    public float gravityScale = 1;
}

public enum VActorPhysicComponentEnum
{
    move,
    dash,
    jump,
    retreat,
}