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

    public SkillTypeEnum skillType = SkillTypeEnum.skill;
    
    [InfoBox("技能优先级，大于技能硬直等级才可释放")]
    public int priority;

    [InfoBox("能否打断")] 
    public bool canInterrupt = true;
    
    [InfoBox("是否是循环技能，如果不是动画播放完成自动结束")]
    public bool isLoopSkill;
}

public enum SkillTypeEnum
{
    idle,
    moveFront,
    moveBack,
    dash,
    jump,
    crouch,
    attacked,
    skill,
}