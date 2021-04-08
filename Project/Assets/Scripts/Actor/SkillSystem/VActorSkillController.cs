using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 单个角色的技能处理
/// </summary>
public class VActorSkillController:VActorControllerBase
{
    public VActorSkillController(VSkillActions skillActions, VActorEvent actorEvent, VActorInfo actorInfo,
        VActorState actorState) : base(actorEvent)
    {
        _skillActions = skillActions;
        _actorEvent = actorEvent;
        _actorInfo = actorInfo;
        _actorState = actorState;
        
        actorEvent.SkillEvent.skillPlayTriggerEvent.AddListener(SkillPlayerTriggerEvent);
        actorEvent.SkillEvent.skillEndTriggerEvent.AddListener(SkillEndTriggerEvent);
    }

    private VActorInfo _actorInfo;
    private VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private VActorState _actorState;

    /// <summary>
    /// 技能输入被触发，这一步判断是否释放技能，判断条件：角色状态能否释放，是否有前置buff条件，当前技能是否能被打断
    /// </summary>
    /// <param name="skillAction"></param>
    private void SkillPlayerTriggerEvent(VSkillAction skillAction)
    {
        //当前技能不为空
        if (_actorInfo.skillInfo.currentSkill != null)
        {
            //当前技能无法打断
            if(!_actorInfo.skillInfo.isInterrupt)
                return;
        }
        
        //角色状态无法释放
        if(!_actorState.canSkill)
            return;
        
        //前置技能判断
        if (skillAction.preConditionData.skillContinues.Count == 0)
        {
            SkillTriggerJudge(skillAction, _actorInfo.skillInfo);
        }
        else
        {
            //遍历技能前置条件是否有一个被满足
            foreach (var skillContinue in skillAction.preConditionData.skillContinues)
            {
                foreach (var haveSkillCon in _actorInfo.skillContinueInfo.skillContinues)
                {
                    if (haveSkillCon.ID == skillContinue.ID && haveSkillCon.needLayer == skillContinue.needLayer)
                    {
                        SkillTriggerJudge(skillAction, _actorInfo.skillInfo);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="skillAction"></param>
    private void SkillEndTriggerEvent(VSkillAction skillAction)
    {                                                                                                                                                                                                                 
        
    }

    /// <summary>
    /// 技能确认释放后，对不同类型技能进行最终释放判断
    /// </summary>
    private void SkillTriggerJudge(VSkillAction skillAction,VActorSkillInfo skillInfo)
    {
        skillInfo.currentSkill = skillAction;
        DebugHelper.Log(skillAction.ToString());
    }

    /// <summary>
    /// 技能确认结束
    /// </summary>
    /// <param name="skillAction"></param>
    /// <param name="skillInfo"></param>
    private void SkillEndJudge(VSkillAction skillAction, VActorSkillInfo skillInfo)
    {
        
    }
}
