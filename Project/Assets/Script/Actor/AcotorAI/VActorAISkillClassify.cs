using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 为AI进行技能分类
/// </summary>
public class VActorAISkillClassify
{
    //初始时存储连携类技能
    public Dictionary<string, VActorSkillUnit[]> ContinueSkillDic = new Dictionary<string, VActorSkillUnit[]>();

    //初始时存储有前提条件的技能，如果一个技能有多个条件，可存在在不同词条中
    public Dictionary<ActorStateTypeEnum, List<VActorSkillUnit>> preConSkills =
        new Dictionary<ActorStateTypeEnum, List<VActorSkillUnit>>();

    //没有特殊要求的普通技能
    public List<VActorSkillUnit> NormalSkills = new List<VActorSkillUnit>();
    
    //特殊标识的技能
    public Dictionary<ActorStateTypeEnum, VActorSkillUnit> SpecialSkillDic =
        new Dictionary<ActorStateTypeEnum, VActorSkillUnit>();
}
