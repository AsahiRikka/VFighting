using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class VActorPhysicComponent
{
    public bool isEnable = false;

    [HorizontalGroup("G1")]
    [ShowIf("isEnable")]
    public bool isMove;

    [HorizontalGroup("G2")]
    [ShowIf("isEnable")]
    public bool isBack;

    [HorizontalGroup("G3")]
    [ShowIf("isEnable")]
    public bool isDash;

    [HorizontalGroup("G4")]
    [ShowIf("isEnable")]
    public bool isJump;

    [HorizontalGroup("G1")]
    [ShowIf("isEnable")]
    public float moveSpeedScale;

    [HorizontalGroup("G2")]
    [ShowIf("isEnable")]
    public float backSpeedScale;

    [HorizontalGroup("G3")]
    [ShowIf("isEnable")]
    public float dashSpeedScale;

    [HorizontalGroup("G4")]
    [ShowIf("isEnable")]
    public float jumpForceScale;
}
