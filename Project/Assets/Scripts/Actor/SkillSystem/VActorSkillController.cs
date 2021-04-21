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
        VActorState actorState,VActorChangeProperty property,VActorController controller) : base(actorEvent)
    {
        _skillActions = skillActions;
        _actorEvent = actorEvent;
        _actorInfo = actorInfo;
        _actorState = actorState;
        _property = property;

        _animationController = controller.animationController;
        _colliderController = controller.colliderController;
        _physicController = controller.physicController;
        _stateController = controller.stateController;
        _continueController = controller.skillContinueController;
        _fxController = controller.FXController;
    }

    private VActorInfo _actorInfo;
    private readonly VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private readonly VActorState _actorState;
    private readonly VActorChangeProperty _property;

    private float SkillEnterFlag = 1f;

    private readonly VActorAnimationController _animationController;
    private readonly VActorColliderController _colliderController;
    private readonly VActorPhysicController _physicController;
    private readonly VActorStateController _stateController;
    private readonly VActorSkillContinueController _continueController;
    private readonly VActorFXController _fxController;

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
        base.SkillStartEvent(lastSkill,currentSkill);

        _continueController.SkillStartEvent();
        
        _animationController.SkillStartEvent(lastSkill,currentSkill);
        
        _colliderController.SkillStartEvent(lastSkill,currentSkill);
        _physicController.SkillStartEvent(lastSkill,currentSkill);
        _fxController.SkillStartEvent(currentSkill);
        
        SkillEnterFlag = 0;
        inSkill = true;
        DebugHelper.Log("角色{0}，开始技能：{1}  结束技能{2}", _property.playerEnum.ToString(), currentSkill, lastSkill);
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        base.SkillUpdateEvent(skillAction);
        
        _animationController.SkillUpdateEvent(skillAction);
        
        _continueController.SkillUpdateEvent(skillAction);
        _stateController.SkillUpdateEvent(skillAction);
        _colliderController.SkillUpdateEvent(skillAction);
        _physicController.SkillUpdateEvent(skillAction);
    }

    protected override void SkillFixUpdateEvent(VSkillAction skillAction)
    {
        base.SkillFixUpdateEvent(skillAction);
        _physicController.SkillFixUpdateEvent(skillAction);
    }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        base.SkillEndEvent(currentSkill,nextSkill);

        inSkill = false;
        
        _animationController.SkillEndEvent(currentSkill,nextSkill);
        
        _continueController.SkillEndEvent(currentSkill);
        _colliderController.SkillEndEvent(currentSkill,nextSkill);
        _physicController.SkillEndEvent();
    }

    protected override void SkillEndNormalEvent(VSkillAction skillAction)
    {
        base.SkillEndNormalEvent(skillAction);
        
        //自然结束添加idle技能
        SkillTriggerJudge(_skillActions.defaultSkillActions, _actorInfo.skillInfo, _actorState);
    }
    
    /// <summary>
    /// 角色受到攻击，敌方暂时无法影响受击者技能，受击者自动跳转到被攻击技能
    /// </summary>
    /// <param name="activeSkillAction"></param>
    /// <param name="passiveSkillAction"></param>
    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.ActorBeAttackedEvent(activeSkillAction,passiveSkillAction);
        SkillTriggerJudge(_skillActions.beAttackSkillAction, _actorInfo.skillInfo, _actorState);
    }

    private bool inSkill = false;

    public void Update()
    {
        if(inSkill)
            _actorEvent.SkillEvent.skillUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
    }

    public void FixUpdate()
    {
        if (SkillEnterFlag < 1)
        {
            SkillEnterFlag +=  Time.fixedDeltaTime;
        }
        if(inSkill)
            _actorEvent.SkillEvent.skillFixUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
    }
}
