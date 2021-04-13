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
    
    [InfoBox("物理组件")]
    public VActorPhysicComponent physicComponent;
    
    [Space(30)]
    
    [InfoBox("技能动画配置")]
    public VMotion motion;
    
    [InfoBox("伤害事件，前提是发生敌我碰撞")]
    public List<VSkillAction_Damange> ActionDamanges;

    [InfoBox("声音事件")]
    public List<VSkillAction_Sound> ActionSounds;

    [InfoBox("跳转技能")]
    public List<VSkillAction_Animate> ActionAnimates;

    [InfoBox("buff事件")]
    public List<VSkillAction_Buff> ActionBuffs;

    [InfoBox("特效事件")]
    public List<VSkillAction_FX> ActionEffects;

    [InfoBox("物理事件")]
    public List<VSkillAction_Physic> ActionPhysics;
}
