using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能连携条件
/// </summary>
[Serializable]
public class VSkillPreConditionData_SkillContinue
{
    [InfoBox("识别ID，相同ID被当成一个连招")]
    public string ID;
    
    [InfoBox("需要连携级数")]
    public int needLayer;

    [InfoBox("相应帧率范围")] 
    public Vector2 frame;

    [InfoBox("技能发动后的连携级数")]
    public int targetLayer;
}
