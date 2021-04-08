using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能事件
/// </summary>
[CreateAssetMenu(order = 42,fileName = "SkillAction_",menuName = "创建技能事件")]
public class VSkillAction:SerializedScriptableObject
{
    public VSkillProperty skillProperty;

    [Space(30)]
    
    public VSkillSignalData signalData;

    public VSkillPreConditionData preConditionData;
    
    [Space(30)]
    
    [InfoBox("技能动画配置")]
    public VMotion motion;
    
    public List<VSkillAction_Damange> ActionDamanges;

    public List<VSkillAction_Sound> ActionSounds;

    public List<VSkillAction_Animate> ActionAnimates;

    public List<VSkillAction_Buff> ActionBuffs;

    public List<VSkillAction_FX> ActionEffects;

    public List<VSkillAction_Physic> ActionPhysics;
}
