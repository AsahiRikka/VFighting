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

    public ActorStateTypeEnum skillType = ActorStateTypeEnum.skill;
    
    [InfoBox("技能优先级，大于技能硬直等级才可释放")]
    public int priority;

    [InfoBox("技能形式")] 
    public SkillAnimPlayType PlayType;
}

public enum SkillAnimPlayType
{
    [InfoBox("循环手动结束")]
    loop,
    [InfoBox("结束帧自动退出")]
    autoQuit,
    [InfoBox("保持最后一帧，手动结束")]
    holdEnd,
}