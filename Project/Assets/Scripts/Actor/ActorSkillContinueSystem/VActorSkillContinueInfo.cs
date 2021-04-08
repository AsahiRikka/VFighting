using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能连携信息
/// </summary>
public class VActorSkillContinueInfo
{
    public List<VSkillPreConditionData_SkillContinue> skillContinues;

    public VActorSkillContinueInfo(VSkillActions skillActions)
    {
        skillContinues=new List<VSkillPreConditionData_SkillContinue>();
        
        //角色初始化时获取角色所有0级连携
        foreach (var skillAction in skillActions.actorSkillActions)
        {
            foreach (var skill in skillAction.preConditionData.skillContinues)
            {
                if(!skillContinues.Contains(skill) && skill.needLayer==0)
                    skillContinues.Add(skill);
            }
        }
    }
}
