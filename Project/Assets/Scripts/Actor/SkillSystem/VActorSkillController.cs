using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 单个角色的技能处理
/// </summary>
public class VActorSkillController:VSkillEventBase
{
    public VActorSkillController(VSkillActions skillActions, VActorEvent actorEvent, VActorInfo actorInfo,
        VActorState actorState) : base(actorEvent)
    {
        _skillActions = skillActions;
        _actorEvent = actorEvent;
        _actorInfo = actorInfo;
        _actorState = actorState;
    }

    private VActorInfo _actorInfo;
    private readonly VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private readonly VActorState _actorState;

    private float SkillEnterFlag = 1f;

    /// <summary>
    /// 技能输入被触发，这一步判断是否释放技能，判断条件：角色状态能否释放，是否有前置buff条件，当前技能是否能被打断
    /// </summary>
    /// <param name="skillAction"></param>
    protected override void SkillStartTriggerEvent(VSkillAction skillAction)
    {
        //过快的技能触发被忽略，只有当被关闭的当前技能有动画前摇时
        if (SkillEnterFlag <= 0.5f && _actorInfo.skillInfo.currentSkill.motion.animationStraights.Count > 0) 
        {
            return;
        }
        
        //相同技能退出
        if (skillAction == _actorInfo.skillInfo.currentSkill)
            return;

        //角色状态无法释放
        if(!_actorState.canSkill)
            return;

        //前置状态是否满足，为空不需要前置状态
        if (!skillAction.preConditionData.skillPreState.Contains(_actorState.actorState) &&
            skillAction.preConditionData.skillPreState.Count > 0) 
        {
            return;
        }
        //无法触发技能的状态，为空不需要
        if (skillAction.preConditionData.notSkillPreState.Contains(_actorState.actorState) &&
            skillAction.preConditionData.notSkillPreState.Count > 0) 
        {
            return;
        }

        //连携技能判断
        if (skillAction.preConditionData.skillContinues.Count == 0)
        {
            SkillTriggerJudge(skillAction, _actorInfo.skillInfo, _actorState);
        }
        else
        {
            //遍历连携技能条件是否有一个被满足
            foreach (var skillContinue in skillAction.preConditionData.skillContinues)
            {
                foreach (var haveSkillCon in _actorInfo.skillContinueInfo.skillContinues)
                {
                    if (haveSkillCon.ID == skillContinue.ID && haveSkillCon.targetLayer == skillContinue.needLayer)
                    {
                        SkillTriggerJudge(skillAction, _actorInfo.skillInfo, _actorState);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 技能确认释放后，对不同类型技能进行最终释放判断
    /// </summary>
    private void SkillTriggerJudge(VSkillAction skillAction, VActorSkillInfo skillInfo, VActorState actorState)
    {
        //技能判断
        switch (skillAction.skillProperty.skillType)
        {
            case SkillTypeEnum.idle:
            {
                actorState.actorState = ActorStateTypeEnum.idle;
                break;
            }
            case SkillTypeEnum.moveFront:
            {
                if (!actorState.canMove || skillInfo.currentSkill.skillProperty.skillType == SkillTypeEnum.dash) 
                    return;
                actorState.actorState = ActorStateTypeEnum.move;
                break;
            }
            case SkillTypeEnum.moveBack:
            {
                if(!actorState.canMove)
                    return;
                actorState.actorState = ActorStateTypeEnum.moveBack;
                break;
            }
            case SkillTypeEnum.dash:
            {
                if(!actorState.canDash)
                    return;
                actorState.actorState = ActorStateTypeEnum.dash;
                break;
            }
            case SkillTypeEnum.jump:
            {
                if(!actorState.canJump)
                    return;
                actorState.actorState = ActorStateTypeEnum.jump;
                break;
            }
            case SkillTypeEnum.crouch:
            {
                actorState.actorState = ActorStateTypeEnum.crouch;
                break;
            }
            case SkillTypeEnum.skill:
            {
                if(!actorState.canSkill)
                    return;
                actorState.actorState = ActorStateTypeEnum.skill;
                break;
            }
            case SkillTypeEnum.attacked:
            {
                actorState.actorState = ActorStateTypeEnum.attacked;
                break;
            }
            default:
            {
                break;
            }
        }
        
        //打断当前技能
        if (skillInfo.currentSkill != null)
        {
            _actorEvent.SkillEvent.skillEndEvent.Invoke(skillInfo.currentSkill, skillAction);
        }

        VSkillAction temp = _actorInfo.skillInfo.currentSkill;
        
        //修改当前技能
        _actorInfo.skillInfo.currentSkill = skillAction;
        
        //完成释放判断，释放技能
        _actorEvent.SkillEvent.skillStartEvent.Invoke(temp, skillAction);
    }

    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        SkillEnterFlag = 0;
        DebugHelper.Log("开始技能：{0}  结束技能{1}",currentSkill,lastSkill);
    }

    protected override void SkillEndNormalEvent(VSkillAction skillAction)
    {
        //自然结束添加idle技能
        _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.defaultSkillActions);
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        
    }

    /// <summary>
    /// 角色受到攻击，敌方暂时无法影响受击者技能，受击者自动跳转到被攻击技能
    /// </summary>
    /// <param name="activeSkillAction"></param>
    /// <param name="passiveSkillAction"></param>
    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        //无视前置条件直接跳转
        SkillTriggerJudge(_skillActions.beAttackSkillAction, _actorInfo.skillInfo, _actorState);
    }

    public void Update()
    {
        _actorEvent.SkillEvent.skillUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
    }

    public void FixUpdate()
    {
        if (SkillEnterFlag < 1)
        {
            SkillEnterFlag +=  Time.fixedDeltaTime;
        }
        _actorEvent.SkillEvent.skillFixUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
    }
}
