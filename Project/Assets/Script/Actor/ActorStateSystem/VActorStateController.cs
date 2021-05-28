using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态刷新
/// </summary>
public class VActorStateController
{
    public VActorStateController(VActorEvent actorEvent, VActorState actorState, VActorInfo actorInfo,
        VActorChangeProperty property, VActorReferanceGameObject referance,VSkillActions skillActions)
    {
        _actorInfo = actorInfo;
        _actorState = actorState;
        _property = property;
        _parent = referance.parent.transform;
        _skillEvent = actorEvent.SkillEvent;
        _skillActions = skillActions;

        EventManager.ActorDirTransEvent.Subscribe(DirTransEvent);
    }

    private VActorState _actorState;
    private VActorInfo _actorInfo;
    private VActorChangeProperty _property;
    private Transform _parent;
    private VActorSkillEvent _skillEvent;
    private VSkillActions _skillActions;

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

    /// <summary>
    /// 伤害事件，获取伤害数字进行判断
    /// </summary>
    public void DamageEvent(float damage)
    {
        if (_property.heathPoints - damage > 0)
        {
            //还未死亡，扣取生命值
            _property.heathPoints -= damage;
        }
        else
        {
            //死亡，播放死亡技能，停止所有角色信号输入
            EventManager.ActorInputEvent.BoradCastEvent(false, false);

            _skillEvent.skillPlayTriggerEvent.Invoke(_skillActions.specialSkillDic[ActorStateTypeEnum.die]);
            //禁止技能释放
            _actorState.canSkill = false;
            
            _property.heathPoints = 0;

            PlayerEnum e = _property.playerEnum;
            if (e == PlayerEnum.player_1)
            {
                EventManager.WinnerEvent.BoradCastEvent(PlayerEnum.player_2);
            }else if (e == PlayerEnum.player_2)
            {
                EventManager.WinnerEvent.BoradCastEvent(PlayerEnum.player_1);
            }

            //游戏结束
            GameFramework.instance.GameManager.GameFinish();
        }
        
        //UI控制
        EventManager.GamingHeathEvent.BoradCastEvent(_property.playerEnum,
            _property.heathPoints / _property.initHeathPoints);
    }
}
