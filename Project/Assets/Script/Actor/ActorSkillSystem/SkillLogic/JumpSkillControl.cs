using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 跳跃控制，进行触发判断
/// </summary>
public class JumpSkillControl
{
    public JumpSkillControl(VActorJumpRayData jumpRayData, VActorReferanceGameObject referance, VActorEvent actorEvent,
        VSkillActions skillActions,VActorInfo actorInfo)
    {
        _actorRoot = referance.actorRoot.transform;
        _actorEvent = actorEvent;
        _skillActions = skillActions;
        _skillInfo = actorInfo.skillInfo;
        _physicInfo = actorInfo.physicInfo;
        _actorRig = referance.parent.GetComponent<Rigidbody>();
        
        //设置偏移量，确认一直触发射线
        _jumpRayData.airSkillDistance = jumpRayData.airSkillDistance + 1;
        _jumpRayData.fallSkillDistance = jumpRayData.fallSkillDistance + 1;
        _jumpRayData.attackFallSkillDistance = jumpRayData.attackFallSkillDistance + 1;
    }

    private VActorJumpRayData _jumpRayData = new VActorJumpRayData();
    private readonly Transform _actorRoot;
    private readonly Rigidbody _actorRig;
    private readonly VActorEvent _actorEvent;
    private readonly VSkillActions _skillActions;
    private readonly VActorSkillInfo _skillInfo;
    private readonly VActorPhysicInfo _physicInfo;

    private RaycastHit _hit;

    private float _distance = 0;
    private bool grounded;
    
    /// <summary>
    /// 实时检测刷新距离
    /// </summary>
    public void Update()
    {
        grounded = Physics.Raycast(_actorRoot.position, Vector3.down, out _hit, 10000f,
            1 << LayerMask.NameToLayer("Ground"));
        
        #if GIZMOS
        Debug.DrawRay(_actorRoot.position, Vector3.down * 10000f, Color.red);
        #endif

        if (grounded)
        {
            float dis = _hit.distance;
            if (dis > _jumpRayData.airSkillDistance)
            {
                _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.inAir]);
            }
            if (dis < _jumpRayData.fallSkillDistance) 
            {
                _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.fall]);
            }
            if (dis < _jumpRayData.attackFallSkillDistance)  
            {
                _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.attackedFall]);
            }
        }
    }

    public void AttackedInAirJudge()
    {
        if (grounded)
        {
            float dis = _hit.distance;
            if (dis > _jumpRayData.attackFallSkillDistance)
            {
                _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.attackedInAir]);
            }else
            {
                _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.attacked]);
            }
        }
    }
}
