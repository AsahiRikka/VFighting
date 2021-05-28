using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorSkillContinueController
{
    public VActorSkillContinueController(VActorInfo actorInfo)
    {
        _continueInfo = actorInfo.skillContinueInfo;
        _actorAnimationInfo = actorInfo.animationInfo;
    }

    private VActorSkillContinueInfo _continueInfo;
    private VActorAnimationInfo _actorAnimationInfo;

    //存储列表，不重复处理
    private List<VSkillPreConditionData_SkillContinue> _conList = new List<VSkillPreConditionData_SkillContinue>();
    private List<VSkillPreConditionData_SkillContinue> _faultConList = new List<VSkillPreConditionData_SkillContinue>();

    public void SkillStartEvent()
    {
        _conList.Clear();
        _faultConList.Clear();
    }
    
    /// <summary>
    /// 基于动画帧的硬直
    /// </summary>
    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        foreach (var skillCon in skillAction.preConditionData.skillContinues)
        {
            if (_actorAnimationInfo.currentFrame >= skillCon.frame.x && !_conList.Contains(skillCon))
            {
                //添加连携
                AddConEvent(skillCon);
                _conList.Add(skillCon);
            }

            if (_actorAnimationInfo.currentFrame >= skillCon.frame.y && !_faultConList.Contains(skillCon))
            {
                //添加连携
                RemoveConEvent(skillCon);
                _faultConList.Add(skillCon);
            }
        }
    }

    /// <summary>
    /// 技能结束自动移除
    /// </summary>
    /// <param name="skillAction"></param>
    public void SkillEndEvent(VSkillAction skillAction)
    {
        //消除连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            foreach (var con in skillAction.preConditionData.skillContinues)
            {
                if (initCon.ID == con.ID)
                {
                    initCon.needLayer = 0;
                    initCon.targetLayer = 0;
                }
            }
        }
    }
    
    /// <summary>
    /// 连携添加
    /// </summary>
    private void AddConEvent(VSkillPreConditionData_SkillContinue skillContinue)
    {
        //附加连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            if (initCon.ID == skillContinue.ID)
            {
                initCon.needLayer = skillContinue.needLayer;
                initCon.targetLayer = skillContinue.targetLayer;
            }
        }
    }

    /// <summary>
    /// 连携移除
    /// </summary>
    private void RemoveConEvent(VSkillPreConditionData_SkillContinue skillContinue)
    {
        //消除连携
        foreach (var initCon in _continueInfo.skillContinues)
        {
            if (initCon.ID == skillContinue.ID)
            {
                initCon.needLayer = 0;
                initCon.targetLayer = 0;
            }
        }
    }
}
