using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
[Serializable]
public class VSkillProperty
{
    public string skillActionID;

    public string skillActionName;

    public SkillTypeEnum skillType = SkillTypeEnum.other;
    
    public int priority;

    [InfoBox("能否打断")] 
    public bool canInterrupt = true;
}

public enum SkillTypeEnum
{
    idle,
    moveFront,
    moveBack,
    dash,
    jump,
    crouch,
    other,
}