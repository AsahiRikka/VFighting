using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色物理效果控制器
/// </summary>
public class VActorPhysicController:VSkillEventBase
{
    public VActorPhysicController(VActorChangeProperty actorProperty, VActorState state, VActorEvent actorEvent,
        VActorSkillSignal signal, VActorReferanceGameObject referanceGameObject) : base(actorEvent)
    {
        _actorProperty = actorProperty;
        _state = state;
        _skillSignal = signal;
        _target = referanceGameObject.parent;
    }
    
    private VActorChangeProperty _actorProperty;
    private VActorState _state;
    private VActorSkillSignal _skillSignal;
    private GameObject _target;

    protected override void SkillUpdateEvent(VSkillAction skillAction)
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

            #endregion
        }
    }
}
