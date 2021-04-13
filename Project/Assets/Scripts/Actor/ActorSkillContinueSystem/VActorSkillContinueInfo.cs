using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能连携信息
/// </summary>
[Serializable]
public class VActorSkillContinueInfo
{
    public List<VSkillPreConditionData_SkillContinue> skillContinues;

    public VActorSkillContinueInfo(VSkillActions skillActions)
    {
        skillContinues=new List<VSkillPreConditionData_SkillContinue>();
        
        //角色初始化时获取角色所有0级连携
        foreach (var skillAction in skillActions.actorSkillActions)
        {
            foreach (var skillCon in skillAction.preConditionData.skillContinues)
            {
                VSkillPreConditionData_SkillContinue temp = new VSkillPreConditionData_SkillContinue();
                temp.ID = skillCon.ID;
                temp.needLayer = skillCon.needLayer;
                temp.targetLayer = skillCon.targetLayer;
                foreach (var haveCon in skillContinues)
                {
                    if (haveCon.ID == skillCon.ID)
                    {
                        temp = null;
                        break;
                    }
                }
                if (temp != null && temp.needLayer == 0) 
                {
                    temp.ID = skillCon.ID;
                    temp.needLayer = 0;
                    temp.targetLayer = 0;
                    skillContinues.Add(temp);
                }
            }
        }
    }
}
