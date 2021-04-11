using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 动画片段资源
/// </summary>
[CreateAssetMenu(order = 43,fileName = "Motion_",menuName = "创建动画资源")]
public class VMotion:SerializedScriptableObject
{
    public string motionID;

    public string motionName;

    public string parameter;

    [InfoBox("动画默认角度")] 
    public Vector3 animationDefaultRotate = Vector3.zero;

    [InfoBox("动画使用rootMotion")]
    public bool applyRoomMotion = true;
    
    public AnimationClip animationClip;

    public List<VActorPassiveBox> passiveBoxes;

    public List<VActorHitBox> hitBoxes;

    public List<VActorDefenseBox> defenseBoxes;

    public List<VAnimationStraight> animationStraights;
}
