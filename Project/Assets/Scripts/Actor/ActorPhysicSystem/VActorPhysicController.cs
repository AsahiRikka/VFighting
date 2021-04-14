using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色物理效果控制器
/// </summary>
public class VActorPhysicController:VSkillEventBase
{
    public VActorPhysicController(VActorChangeProperty actorProperty, VActorState state, VActorEvent actorEvent,
        VActorSkillSignal signal, VActorReferanceGameObject referanceGameObject,VActorInfo actorInfo) : base(actorEvent)
    {
        _actorProperty = actorProperty;
        _state = state;
        _skillSignal = signal;
        _target = referanceGameObject.parent;
        _actorRig = referanceGameObject.parent.GetComponent<Rigidbody>();
        _actorInfo = actorInfo;

        referanceGameObject.actorPhysicDetect.TriggerEnterDetection += GroundEnter;
        referanceGameObject.actorPhysicDetect.TriggerExitDetection += GroundExit;
    }
    
    private VActorChangeProperty _actorProperty;
    private VActorState _state;
    private VActorSkillSignal _skillSignal;
    private GameObject _target;
    private Rigidbody _actorRig;
    private VActorInfo _actorInfo;

    protected override void SkillUpdateEvent(VSkillAction skillAction)
    {
        //物理组件
        PhysicComponent(skillAction);
    }

    private void PhysicComponent(VSkillAction skillAction)
    {
        #region 物理组件
        
        if (skillAction.physicComponent.isEnable)
        {
            //dash优先级更高，放move前面判断，只存在一个
            if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.dash] && _state.canDash &&
                skillAction.physicComponent.isDash)
            {
                _target.transform.position += new Vector3(
                    skillAction.physicComponent.dashSpeedScale *
                    _actorProperty.actorDashSpeed *
                    _state.actorFace * Time.deltaTime,
                    0, 0
                );
            }
            else if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.move] && _state.canMove &&
                     skillAction.physicComponent.isMove) 
            {
                _target.transform.position += new Vector3(
                    skillAction.physicComponent.moveSpeedScale *
                    _actorProperty.actorMoveSpeed *
                    _state.actorFace * Time.deltaTime,
                    0, 0
                );
            }
            
            if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.retreat] && _state.canMove &&
                skillAction.physicComponent.isBack) 
            {
                _target.transform.position += new Vector3(
                    skillAction.physicComponent.backSpeedScale *
                    _actorProperty.actorBackSpeed *
                    _state.actorFace * -1 * Time.deltaTime,
                    0, 0
                );
            }
            
            if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.jump] && _state.canJump &&
                skillAction.physicComponent.isJump)
            {
                
            }
        }
        #endregion
    }
    
    protected override void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        PhysicAction(currentSkill,SkillActionEnum.skillAction);
    }

    
    public void PhysicAction(VSkillAction currentSkill,SkillActionEnum e)
    {
        //PhysicReset();
        
        foreach (var physic in currentSkill.ActionPhysics)
        {
            if (physic.skillActionType == e)
            {
                _actorInfo.physicInfo.actorVerticalAcceleration *= physic.gravityScale;

                _actorRig.AddForce(physic.initVector *
                                   physic.initSpeed *
                                   Time.deltaTime, ForceMode.VelocityChange
                );
            }
        }
    }

    public void PhysicActionByAcceleration(VSkillAction currentSkill, SkillActionEnum e)
    {
        //PhysicReset();
        
        foreach (var physic in currentSkill.ActionPhysics)
        {
            if (physic.skillActionType == e)
            {
                _actorInfo.physicInfo.actorVerticalAcceleration *= physic.gravityScale;

                _actorRig.AddForce(physic.initVector *
                                   physic.initSpeed *
                                   Time.deltaTime, ForceMode.Acceleration
                );
            }
        }
    }

    private void PhysicReset()
    {
        _actorRig.velocity=Vector3.zero;
        _actorRig.angularVelocity=Vector3.zero;
    }

    #region 重力 摩擦力 模拟

    public void Update()
    {
        //重力模拟
        if (_isGravity)
        {
            _actorRig.AddForce(0,
                _actorProperty.actorWeight * _actorInfo.physicInfo.actorVerticalAcceleration * Time.deltaTime,
                0, ForceMode.Acceleration);
        }
    }

    private bool _isGravity = true;
    private void GroundEnter(TagEnum e)
    {
        if (e == TagEnum.ground)
        {
            _actorRig.velocity = new Vector3(_actorRig.velocity.x, 0, _actorRig.velocity.z);
            _target.transform.position=new Vector3(_target.transform.position.x,0,_target.transform.position.z);
            _isGravity = false;
        }
    }

    private void GroundExit(TagEnum e)
    {
        if (e == TagEnum.ground)
        {
            _isGravity = true;
        }
    }

    #endregion
}
