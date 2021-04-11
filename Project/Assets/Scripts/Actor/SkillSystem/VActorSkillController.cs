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
                    if (haveSkillCon.ID == skillContinue.ID && haveSkillCon.needLayer == skillContinue.needLayer)
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
        //过快的技能触发被忽略
        if (temp <= 0.05f)
        {
            return;
        }
        
        //打断当前技能
        if (skillInfo.currentSkill != null) 
            _actorEvent.SkillEvent.skillEndEvent.Invoke(skillInfo.currentSkill);
        
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
                actorState.actorState = ActorStateTypeEnum.move;
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

        //修改状态
        skillInfo.currentSkill = skillAction;

        //完成释放判断，释放技能
        _actorEvent.SkillEvent.skillStartEvent.Invoke(skillAction);
    }

    protected override void SkillStartEvent(VSkillAction skillAction)
    {
        _actorInfo.skillInfo.inSkillUpdate = true;
        DebugHelper.Log("开始技能：" + skillAction);
    }

    protected override void SkillEndNormalEvent(VSkillAction skillAction)
    {
        base.SkillEndNormalEvent(skillAction);

        //触发关闭技能时不是当前技能
        if (skillAction != _actorInfo.skillInfo.currentSkill)
            return;

        //进入技能结束
        _actorEvent.SkillEvent.skillEndEvent.Invoke(skillAction);

        //自然结束添加idle技能
        _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.defaultSkillActions);
    }

    protected override void SkillEndEvent(VSkillAction skillAction)
    {
        _actorInfo.skillInfo.inSkillUpdate = false;
    }

    public void Update()
    {
        if (_actorInfo.skillInfo.inSkillUpdate)
        {
            _actorEvent.SkillEvent.skillUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
        }
    }

    public void FixUpdate()
    {
        if (SkillEnterFlag < 1)
        {
            SkillEnterFlag +=  Time.fixedDeltaTime;
        }

        if (_actorInfo.skillInfo.inSkillUpdate)
        {
            _actorEvent.SkillEvent.skillFixUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
        }
    }
}
