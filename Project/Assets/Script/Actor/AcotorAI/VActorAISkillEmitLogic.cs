using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Random = UnityEngine.Random;

/// <summary>
/// AI技能逻辑
/// </summary>
public class VActorAISkillEmitLogic
{
    private VActorSkillUnit[] continueSkillUnit;
    private int continueSKillLength = 0;
    private int currentIndex = 0;

    public VActorSkillUnit ActorSkillUnit;

    /// <summary>
    /// 通用的AI技能释放
    /// </summary>
    /// <param name="skillType"></param>
    /// <param name="classify"></param>
    /// <param name="skillInfo"></param>
    /// <returns></returns>
    public VActorSkillUnit SkillSetting(AISkillType skillType, VActorAISkillClassify classify,VActorSkillInfo skillInfo)
    {
        //存在正在进行的连招，优先连招
        if (continueSkillUnit != null)
        {
            return ContinueSkillJudge(skillInfo);
        }
        
        if (skillType == AISkillType.escape)
        {
            int temp = Random.Range(101, 399) / 100;
            switch (temp)
            {
                case 1:
                    //移动并向后
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.moveBack);
                    break;
                case 2:
                    //向后dodge
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.dodgeBack);
                    break;
                case 3:
                    //jump向后
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.jump,2);
                    break;
                default:
                    DebugHelper.LogError("错误的AI技能判断");
                    break;
            }
        }
        
        else if (skillType == AISkillType.front)
        {
            int temp = Random.Range(100, 499) / 100;
            switch (temp)
            {
                case 1:
                    //向前移动
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.move);
                break;
                case 2:
                    //dash
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.dash);
                break;
                case 3:
                    //前dodge
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.dodgeFront);
                break;
                case 4:
                    //jump往前
                    return SkillEmitForSpecial(classify, ActorStateTypeEnum.jump, 1);
                break;
                default:
                    DebugHelper.LogError("错误的AI技能判断");
                    break;
            }
        }
        
        else if (skillType == AISkillType.attack)
        {
            return NormalSkillEmit(classify, skillInfo);
        }else if (skillType == AISkillType.idle)
        {
            return SkillEmitForSpecial(classify, ActorStateTypeEnum.idle);
        }

        return null;
    }

    //特殊技能的释放
    private VActorSkillUnit SkillEmitForSpecial(VActorAISkillClassify classify, ActorStateTypeEnum e,
        int physicSet = -1)
    {
        VActorSkillUnit result = classify.SpecialSkillDic[e].SkillPlay();
        PhysicComponent(e, result, physicSet);
        return result;
    }

    /// <summary>
    /// 物理组件设置
    /// </summary>
    /// <param name="e"></param>
    /// <param name="skillUnit"></param>
    /// <param name="manualSet">手动设置物理组件，1:move,2:retreat,3:dash,4:jump；-1：非手动</param>
    private void PhysicComponent(ActorStateTypeEnum e, VActorSkillUnit skillUnit, int manualSet = -1)
    {
        //技能释放失败
        if (skillUnit == null)
            return;

        //必须启动物理组件，其他技能选择性启动
        if (e == ActorStateTypeEnum.move || e == ActorStateTypeEnum.moveBack || e == ActorStateTypeEnum.dash) 
        {
            skillUnit.PhysicComponentSet(true,true,true,true);
        }else if (manualSet != -1)
        {
            skillUnit.PhysicComponentSet(manualSet==1,manualSet==2,manualSet==3,manualSet==4);
        }
        else
        {
            int temp = Random.Range(101, 499) / 100;
            skillUnit.PhysicComponentSet(temp == 1, temp == 2, temp == 3, temp == 4);
        }
    }

    //普通技能的释放
    private VActorSkillUnit NormalSkillEmit(VActorAISkillClassify classify,VActorSkillInfo skillInfo)
    {
        int temp = Random.Range(0, 10);
        
        //如果当前技能是其他技能前提条件
        if (classify.preConSkills[skillInfo.currentSkill.skillProperty.skillType].Count != 0 && temp <2)
        {
            int specialTemp = Random.Range(0,
                classify.preConSkills[skillInfo.currentSkill.skillProperty.skillType].Count - 1);
            VActorSkillUnit result = classify.preConSkills[skillInfo.currentSkill.skillProperty.skillType][specialTemp].SkillPlay();
            PhysicComponent(ActorStateTypeEnum.skill,result);
            return result;
        }
        
        //普通类型技能
        else if (classify.NormalSkills.Count != 0 && temp <8)
        {
            int normalTemp = Random.Range(0, classify.NormalSkills.Count - 1);
            VActorSkillUnit result = classify.NormalSkills[normalTemp].SkillPlay();
            PhysicComponent(ActorStateTypeEnum.skill,result);
            return result;
        }
        
        //连招
        else if (temp <=10)
        {
            if (classify.ContinueSkillDic.Count != 0)
            {
                int continueTemp = Random.Range(0, classify.ContinueSkillDic.Count - 1);
                int flag = 0;
                foreach (var skill in classify.ContinueSkillDic)
                {
                    if (flag == continueTemp)
                    {
                        continueSkillUnit = skill.Value;
                        continueSKillLength = 0;
                        
                        //计算技能长度
                        foreach (var con in continueSkillUnit)
                        {
                            if (con == null)
                                break;
                            continueSKillLength++;
                        }
                        break;
                    }
                    flag++;
                }
                
                //设置索引
                currentIndex = 1;
                
                //播放技能
                VActorSkillUnit result = continueSkillUnit[0].SkillPlay();
                PhysicComponent(ActorStateTypeEnum.skill, result);
                return result;
            }
        }

        return null;
    }

    private VActorSkillUnit ContinueSkillJudge(VActorSkillInfo skillInfo)
    {
        //技能已被更改
        foreach (var skillUnit in continueSkillUnit)
        {
            if (skillUnit == null)
            {
                continueSkillUnit = null;
                return null;
            }
            if (skillUnit._skillData._skillAction == skillInfo.currentSkill)
            {
                break;
            }
        }

        //如果已经是最后一个技能，置空
        if (skillInfo.currentSkill == continueSkillUnit[continueSKillLength - 1]._skillData._skillAction) 
        {
            continueSkillUnit = null;
            return null;
        }
        else
        {
            VActorSkillUnit result = continueSkillUnit[currentIndex].SkillPlay();
            PhysicComponent(ActorStateTypeEnum.skill,result);

            if (result != null)
            {
                currentIndex++;
                return result;
            }
            else
            {
                return continueSkillUnit[currentIndex - 1];
            }
        }
    }

    /// <summary>
    /// 状态转换时的重置
    /// </summary>
    public void Reset()
    {
        ActorSkillUnit = null;
    }
}

public enum AISkillType
{
    /// <summary>
    /// 逃离类，后移动，后dodge，jump往后
    /// </summary>
    escape,
    /// <summary>
    /// 前进类，前移动，dash，前dodge，jump往前
    /// </summary>
    front,
    /// <summary>
    /// 防御类，防御
    /// </summary>
    defence,
    /// <summary>
    /// 普通攻击
    /// </summary>
    attack,
    /// <summary>
    /// 停止类，下蹲，idle，防御
    /// </summary>
    idle,
}
