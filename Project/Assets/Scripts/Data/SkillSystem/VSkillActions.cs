using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 一个角色的技能事件集合
/// </summary>
[Serializable]
public class VSkillActions
{
    [InfoBox("默认技能，一般为idle")]
    public VSkillAction defaultSkillActions;

    [InfoBox("受击技能")] 
    public VSkillAction beAttackSkillAction;
    
    [InfoBox("技能集合")]
    public List<VSkillAction> actorSkillActions;
}
