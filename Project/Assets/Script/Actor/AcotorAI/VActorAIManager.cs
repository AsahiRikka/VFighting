using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 角色AI控制器
/// </summary>
public class VActorAIManager
{
    private FSMSystem _fsmSystem;

    public VActorAIManager(VActorEvent actorEvent, VActorSkillInfo skillInfo,
        Dictionary<VActorPhysicComponentEnum, bool> physicComponentDic,VSkillActions skillActions,VActorState state)
    {
        _skillEvent = actorEvent.SkillEvent;
        _skillInfo = skillInfo;
        _physicComponentDic = physicComponentDic;
        _skillActions = skillActions;
        _actorState = state;

        _fsmSystem = new FSMSystem(skillInfo, skillActions, SkillClassify);
        
        SkillDataInit();
    }

    private VActorSkillEvent _skillEvent;
    private VActorSkillInfo _skillInfo;
    private Dictionary<VActorPhysicComponentEnum, bool> _physicComponentDic;
    private VSkillActions _skillActions;
    private VActorState _actorState;

    #region 技能分类管理

    public VActorAISkillClassify SkillClassify = new VActorAISkillClassify();

    private void SkillDataInit()
    {
        //特殊技能
        foreach (var skill in _skillActions.specialSkillDic)
        {
            SkillClassify.SpecialSkillDic.Add(skill.Key,new VActorSkillUnit(_skillEvent,skill.Value,_physicComponentDic,_skillInfo));
        }

        //初始化前提条件字典
        foreach (ActorStateTypeEnum state in Enum.GetValues(typeof(ActorStateTypeEnum)))
        {
            SkillClassify.preConSkills.Add(state,new List<VActorSkillUnit>());
        }

        //填充字典
        foreach (var skill in _skillActions.actorSkillActions)
        {
            //要排除非主动触发技能
            if (skill.signalData.SignalEnum == SkillSignalEnum.none) 
                return;

            //前置状态的技能
            foreach (var state in skill.preConditionData.skillPreState)
            {
                SkillClassify.preConSkills[state].Add(new VActorSkillUnit(_skillEvent,skill,_physicComponentDic,_skillInfo));
            }

            foreach (var con in skill.preConditionData.skillContinues)
            {
                //默认连招不会超出10个技能
                if(!SkillClassify.ContinueSkillDic.ContainsKey(con.ID))
                    SkillClassify.ContinueSkillDic.Add(con.ID,new VActorSkillUnit[10]);

                //会按照顺序连携
                SkillClassify.ContinueSkillDic[con.ID][con.needLayer] =
                    new VActorSkillUnit(_skillEvent, skill, _physicComponentDic,_skillInfo);
            }

            //普通技能
            if (skill.preConditionData.skillContinues.Count == 0 && skill.preConditionData.skillPreState.Count == 0) 
            {
                SkillClassify.NormalSkills.Add(new VActorSkillUnit(_skillEvent,skill,_physicComponentDic,_skillInfo));
            }
        }
    }

    #endregion
    
    public void Update()
    {
        _fsmSystem.MyUpdate();
    }

    public void Destroy()
    {
        _fsmSystem.MyDestroy();
    }
}
