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

    private float SkillEnterFlag = 100;

    /// <summary>
    /// 技能输入被触发，这一步判断是否释放技能，判断条件：角色状态能否释放，是否有前置buff条件，当前技能是否能被打断
    /// </summary>
    /// <param name="skillAction"></param>
    protected override void SkillStartTriggerEvent(VSkillAction skillAction)
    {
        //相同技能退出
        if (skillAction == _actorInfo.skillInfo.currentSkill)
            return;

        //角色状态无法释放
        if(!_actorState.canSkill)
            return;

        //前置技能判断
        if (skillAction.preConditionData.skillContinues.Count == 0)
        {
            SkillTriggerJudge(skillAction, _actorInfo.skillInfo, _actorState);
        }
        else
        {
            //遍历技能前置条件是否有一个被满足
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
        float temp = SkillEnterFlag;
        SkillEnterFlag = 0;
        //过快的技能触发被忽略，只有当被关闭的当前技能有动画前摇时
        if (temp <= 0.02f && skillInfo.currentSkill.motion.animationStraights.Count > 0) 
        {
            return;
        }

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
                if(!actorState.canMove)
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
            _actorEvent.SkillEvent.skillEndEvent.Invoke(skillInfo.currentSkill, skillAction);
        
        //完成释放判断，释放技能
        _actorEvent.SkillEvent.skillStartEvent.Invoke(skillInfo.currentSkill, skillAction);
    }

    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        //修改状态
        _actorInfo.skillInfo.currentSkill = currentSkill;

        DebugHelper.Log("开始技能：" + currentSkill);
    }

    protected override void SkillEndNormalEvent(VSkillAction skillAction)
    {
        base.SkillEndNormalEvent(skillAction);

        //触发关闭技能时不是当前技能
        if (skillAction != _actorInfo.skillInfo.currentSkill)
            return;

        //进入技能结束
        _actorEvent.SkillEvent.skillEndEvent.Invoke(skillAction, null);

        //自然结束添加idle技能
        _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.defaultSkillActions);
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
