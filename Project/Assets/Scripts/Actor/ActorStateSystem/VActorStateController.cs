using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态刷新
/// </summary>
public class VActorStateController
{
    public VActorStateController(VActorEvent actorEvent, VActorState actorState, VActorInfo actorInfo,
        VActorChangeProperty property, VActorReferanceGameObject referance)
    {
        _actorInfo = actorInfo;
        _actorState = actorState;
        _property = property;
        _parent = referance.parent.transform;
        
        EventManager.ActorDirTransEvent.Subscribe(DirTransEvent);
    }

    private VActorState _actorState;
    private VActorInfo _actorInfo;
    private VActorChangeProperty _property;
    private Transform _parent;

    public void SkillStartEvent()
    {
        //动画硬直当前被认为等于技能硬直
        _actorInfo.skillInfo.skillStraightLevel = _actorInfo.animationInfo.straightLevel;
    }
    
    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        //动画硬直当前被认为等于技能硬直
        _actorInfo.skillInfo.skillStraightLevel = _actorInfo.animationInfo.straightLevel;
    }

    public void Destroy()
    {
        EventManager.ActorDirTransEvent.RemoveEventHandler(DirTransEvent);
    }

    /// <summary>
    /// 方向变换时参数变换
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    private void DirTransEvent(PlayerEnum left, PlayerEnum right)
    {
        if (_property.playerEnum == left)
        {
            _actorState.actorDir=Vector3.zero;
            _actorState.actorFace = 1;
        }
        else
        {
            _actorState.actorDir=new Vector3(0,180,0);
            _actorState.actorFace = -1;
        }
        _parent.rotation=Quaternion.Euler(_actorState.actorDir);
    }
}
