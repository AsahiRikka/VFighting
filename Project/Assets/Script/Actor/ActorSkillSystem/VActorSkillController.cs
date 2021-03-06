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
        VActorState actorState, VActorChangeProperty property, VActorReferanceGameObject referance,
        VActorController controller) : base(actorEvent)
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
        _soundController = controller.soundController;

        _jumpSkillControl = new JumpSkillControl(property.jumpRayData, referance, actorEvent, skillActions, actorInfo);
    }

    private VActorInfo _actorInfo;
    private readonly VActorEvent _actorEvent;
    private VSkillActions _skillActions;
    private readonly VActorState _actorState;
    private readonly VActorChangeProperty _property;

    private readonly VActorAnimationController _animationController;
    private readonly VActorColliderController _colliderController;
    private readonly VActorPhysicController _physicController;
    private readonly VActorStateController _stateController;
    private readonly VActorSkillContinueController _continueController;
    private readonly VActorFXController _fxController;
    private readonly VActorSoundController _soundController;

    private JumpSkillControl _jumpSkillControl;

    /// <summary>
    /// 技能输入被触发，这一步判断是否释放技能，判断条件：角色状态能否释放，是否有前置buff条件，当前技能是否能被打断
    /// </summary>
    /// <param name="skillAction"></param>
    bool flag = false;
    protected override void SkillStartTriggerEvent(VSkillAction skillAction)
    {
        //角色状态无法释放
        if (!_actorState.canSkill)
        {
            return;
        }
        _actorState.canSkill = false;

        //硬直检查
        if (skillAction.skillProperty.priority <= _actorInfo.skillInfo.skillStraightLevel)
        {
            _actorState.canSkill = true;
            return;
        }

        //前置状态是否满足，为空不需要前置状态
        if (!skillAction.preConditionData.skillPreState.Contains(_actorState.actorState) &&
            skillAction.preConditionData.skillPreState.Count > 0)
        {
            _actorState.canSkill = true;
            return;
        }
        //无法触发技能的状态，为空不需要
        if (skillAction.preConditionData.notSkillPreState.Contains(_actorState.actorState) &&
            skillAction.preConditionData.notSkillPreState.Count > 0)
        {
            _actorState.canSkill = true;
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

        _actorState.canSkill = true;
    }

    /// <summary>
    /// 技能确认释放后，对不同类型技能进行最终释放判断
    /// </summary>
    private void SkillTriggerJudge(VSkillAction skillAction, VActorSkillInfo skillInfo, VActorState actorState)
    {
        //确认释放后停止技能输入
        actorState.canSkill = false;
        
        //技能判断
        actorState.actorState = skillAction.skillProperty.skillType;

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
        _soundController.SkillStartEvent(currentSkill);
        _stateController.SkillStartEvent();

        //初始化完成恢复技能输入
        _actorState.canSkill = true;
        
        inSkill = true;
        DebugHelper.Log("角色{0}，开始技能：{1}  结束技能{2}", _property.playerEnum.ToString(), currentSkill, lastSkill);
    }

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        base.SkillUpdateEvent(skillAction);
        
        _animationController.SkillUpdateEvent(skillAction);
        
        _continueController.SkillUpdateEvent(skillAction);
        _physicController.SkillUpdateEvent(skillAction);
        _colliderController.SkillUpdateEvent(skillAction);
        _stateController.SkillUpdateEvent(skillAction);
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
        
        //确认是当前技能
        if(skillAction!=_actorInfo.skillInfo.currentSkill)
            return;

        //自然结束添加idle技能
        SkillTriggerJudge(_skillActions.specialSkillDic[ActorStateTypeEnum.idle], _actorInfo.skillInfo, _actorState);
    }
    
    /// <summary>
    /// 角色受到攻击，敌方暂时无法影响受击者技能，受击者自动跳转到被攻击技能
    /// </summary>
    /// <param name="activeSkillAction"></param>
    /// <param name="passiveSkillAction"></param>
    protected override void ActorBeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.ActorBeAttackedEvent(activeSkillAction,passiveSkillAction);

        if(_actorInfo.physicInfo.inAir)
            _jumpSkillControl.AttackedInAirJudge();
        else
            _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.attacked]);
        
        _physicController.BeAttackedEvent(activeSkillAction,passiveSkillAction);
    }

    private bool inSkill = false;

    public void Update()
    {
        if(inSkill)
            _actorEvent.SkillEvent.skillUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
        
        _jumpSkillControl.Update();
    }

    public void FixUpdate()
    {
        if(inSkill)
            _actorEvent.SkillEvent.skillFixUpdateEvent.Invoke(_actorInfo.skillInfo.currentSkill);
    }

    public void Destroy()
    {
        _stateController.Destroy();
    }
}
