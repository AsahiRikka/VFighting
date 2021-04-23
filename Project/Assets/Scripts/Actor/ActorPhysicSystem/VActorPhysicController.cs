using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// 角色物理效果控制器
/// </summary>
public class VActorPhysicController:VSkillEventBase
{
    public VActorPhysicController(VActorChangeProperty actorProperty, VActorState state, VActorEvent actorEvent,
        VActorSkillSignal signal, VActorReferanceGameObject referanceGameObject, VActorInfo actorInfo) :
        base(actorEvent)
    {
        _actorProperty = actorProperty;
        _state = state;
        _skillSignal = signal;
        _target = referanceGameObject.parent;
        _actorRig = referanceGameObject.parent.GetComponent<Rigidbody>();
        _actorInfo = actorInfo;
        _animationInfo = actorInfo.animationInfo;

        referanceGameObject.actorPhysicDetect.TriggerEnterDetection += GroundEnter;
        referanceGameObject.actorPhysicDetect.TriggerExitDetection += GroundExit;
    }
    
    private VActorChangeProperty _actorProperty;
    private VActorState _state;
    private VActorSkillSignal _skillSignal;
    private GameObject _target;
    private Rigidbody _actorRig;
    private VActorInfo _actorInfo;
    private VActorAnimationInfo _animationInfo;

    //各种类型技能效果及字典
    private Dictionary<SkillActionEnum, List<VSkillAction_Physic>> _dictionary =
        new Dictionary<SkillActionEnum, List<VSkillAction_Physic>>();
    
    //帧范围效果的列表
    private List<VSkillAction_Physic> _frameDic=new List<VSkillAction_Physic>();
    
    public void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        //重置物理
        PhysicReset();
        
        //清理持续字典
        _dictionary.Clear();
        _frameDic.Clear();
        
        //填充字典
        foreach (SkillActionEnum e in Enum.GetValues(typeof(SkillActionEnum)))
        {
            _dictionary.Add(e,new List<VSkillAction_Physic>());
        }
        foreach (var physic in currentSkill.ActionPhysics)
        {
            _dictionary[physic.skillActionType].Add(physic);
        }

        //技能开始的物理效果
        foreach (var physic in _dictionary[SkillActionEnum.skillAction])
        {
            PhysicEffect(physic);
        }
    }
    
    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //根据关键帧的物理效果
        foreach (var physic in _dictionary[SkillActionEnum.keyFrame])
        {
            if (_animationInfo.currentFrame >= physic.keyFrame)
            {
                PhysicEffect(physic);
            }
        }
        
        //根据帧范围的物理效果添加和删除
        foreach (var physic in _dictionary[SkillActionEnum.frame])
        {
            if (_animationInfo.currentFrame >= physic.startFrame && !_frameDic.Contains(physic))
            {
                _frameDic.Add(physic);
            }
            if (_animationInfo.currentFrame >= physic.endFrame && _frameDic.Contains(physic))
            {
                _frameDic.Remove(physic);
            }
        }
    }
    public void SkillFixUpdateEvent(VSkillAction skillAction)
    {
        //物理组件
        PhysicComponent(skillAction);
        
        //技能全程物理效果
        foreach (var physic in _dictionary[SkillActionEnum.skillUpdate])
        {
            PhysicActionByAcceleration(physic);
        }
        
        //根据帧范围的物理效果
        foreach (var physic in _frameDic)
        {
            PhysicActionByAcceleration(physic);
        }
    }

    public void SkillEndEvent()
    {
        //清理持续字典
        _dictionary.Clear();
        _frameDic.Clear();
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
                _actorRig.MovePosition(_target.transform.position + new Vector3(
                    skillAction.physicComponent.dashSpeedScale *
                    _actorProperty.actorDashSpeed *
                    _state.actorFace * Time.fixedDeltaTime,
                    0, 0
                ));
            }
            else if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.move] && _state.canMove &&
                     skillAction.physicComponent.isMove)
            {
                _actorRig.MovePosition(_target.transform.position + new Vector3(
                    skillAction.physicComponent.moveSpeedScale *
                    _actorProperty.actorMoveSpeed *
                    _state.actorFace * Time.fixedDeltaTime,
                    0, 0
                ));
            }
            
            if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.retreat] && _state.canMove &&
                skillAction.physicComponent.isBack) 
            {
                _actorRig.MovePosition(_target.transform.position + new Vector3(
                    skillAction.physicComponent.backSpeedScale *
                    _actorProperty.actorBackSpeed *
                    _state.actorFace * -1 * Time.fixedDeltaTime,
                    0, 0
                ));
            }
            
            if (_skillSignal.physicComponentDic[VActorPhysicComponentEnum.jump] && _state.canJump &&
                skillAction.physicComponent.isJump)
            {
                _actorRig.AddForce(new Vector3(
                    0,
                    skillAction.physicComponent.jumpForceScale *
                    _actorProperty.jumpForce * Time.fixedDeltaTime,
                    0
                ), ForceMode.VelocityChange);
            }
        }
        #endregion
    }

    /// <summary>
    /// 角色攻击效果
    /// </summary>
    /// <param name="activeSkillAction"></param>
    /// <param name="passiveSkillAction"></param>
    protected override void ActorAttackEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        //技能命中对自己的效果
        foreach (var physic in activeSkillAction.ActionPhysics)
        {
            if (physic.target == SkillActionTargetEnum.self && physic.skillActionType == SkillActionEnum.skillHit) 
            {
                PhysicEffect(physic);
            }
        }

        //技能命中敌人对自己的效果
        foreach (var physic in passiveSkillAction.ActionPhysics)
        {
            if (physic.target == SkillActionTargetEnum.anamy && physic.skillActionType == SkillActionEnum.beAttack)  
            {
                PhysicEffect(physic);
            }
        }
    }

    /// <summary>
    /// 被攻击时
    /// </summary>
    /// <param name="activeSkillAction"></param>
    /// <param name="passiveSkillAction"></param>
    public void BeAttackedEvent(VSkillAction activeSkillAction, VSkillAction passiveSkillAction)
    {
        base.ActorBeAttackedEvent(activeSkillAction,passiveSkillAction);
        
        //被命中时自身效果
        foreach (var physic in activeSkillAction.ActionPhysics)
        {
            if (physic.target == SkillActionTargetEnum.self && physic.skillActionType == SkillActionEnum.beAttack) 
            {
                PhysicEffect(physic);
            }
        }

        //被命中时敌人对命中者的效果
        foreach (var physic in passiveSkillAction.ActionPhysics)
        {
            if (physic.target == SkillActionTargetEnum.anamy && physic.skillActionType == SkillActionEnum.skillHit) 
            {
                PhysicEffect(physic);
            }
        }
    }
    

    /// <summary>
    /// 物理效果实现
    /// </summary>
    /// <param name="physic"></param>
    private void PhysicEffect(VSkillAction_Physic physic)
    {
        _actorInfo.physicInfo.actorVerticalAcceleration *= physic.gravityScale;
        _actorRig.AddForce(physic.initVector *
                           physic.initSpeed *
                           _state.actorFace *
                           Time.deltaTime, ForceMode.VelocityChange
        );
    }

    private void PhysicActionByAcceleration(VSkillAction_Physic physic)
    {
        _actorInfo.physicInfo.actorVerticalAcceleration *= physic.gravityScale;
        _actorRig.AddForce(physic.initVector *
                           physic.initSpeed *
                           _state.actorFace *
                           Time.fixedDeltaTime, ForceMode.Acceleration
        );
    }

    private void PhysicReset()
    {
        _actorRig.velocity=Vector3.zero;
        _actorRig.angularVelocity=Vector3.zero;
    }

    #region 重力 摩擦力 模拟

    public void Update()
    {

    }

    public void FixUpdate()
    {
        // 重力模拟
        // if (_isGravity)
        // {
        //     _actorRig.AddForce(0,
        //         _actorProperty.actorWeight * _actorInfo.physicInfo.actorVerticalAcceleration * Time.deltaTime,
        //         0, ForceMode.Acceleration);
        // }
        // 摩擦力模拟
        // if (Mathf.Abs(_actorRig.velocity.x) > 1f)
        // {
        //     var velocity = _actorRig.velocity;
        //     _actorRig.AddForce(new Vector3(
        //         _actorInfo.physicInfo.actorHorizontalSpeedDecay *
        //         Time.deltaTime,
        //         0, 0
        //     ),ForceMode.Acceleration);
        // }
        // else
        // {
        //     PhysicReset();
        // }
    }

    private bool _isGravity = true;
    private void GroundEnter(TagEnum e)
    {
        if (e == TagEnum.ground)
        {
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
