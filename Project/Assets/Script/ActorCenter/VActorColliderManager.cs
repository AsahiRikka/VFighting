using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 负责碰撞事件触发
/// </summary>
public class VActorColliderManager
{
    public VActorColliderManager(VActorBase actorBase1,VActorBase actorBase2)
    {
        _playerEnum1 = actorBase1.actorProperty.playerEnum;
        _playerEnum2 = actorBase2.actorProperty.playerEnum;

        _player1 = actorBase1.actorEvent;
        _player2 = actorBase2.actorEvent;
        
        Subscribe();
    }

    private PlayerEnum _playerEnum1;
    private PlayerEnum _playerEnum2;

    private VActorEvent _player1;
    private VActorEvent _player2;

    private void HitToPassiveEvent(PlayerEnum p1, VSkillAction skillAction1, PlayerEnum p2, VSkillAction skillAction2)
    {
        if (_playerEnum1 == p1)
        {
            _player1.SkillEvent.ActorAttackEvent.Invoke(skillAction1,skillAction2);
        }
        else if (_playerEnum2 == p1) 
        {
            _player2.SkillEvent.ActorAttackEvent.Invoke(skillAction1,skillAction2);
        }
    }

    private void PassiveToHitEvent(PlayerEnum p1, VSkillAction skillAction1, PlayerEnum p2, VSkillAction skillAction2)
    {
        if (_playerEnum1 == p1)
        {
            _player1.SkillEvent.ActorBeAttackedEvent.Invoke(skillAction1,skillAction2);
        }
        else if (_playerEnum2 == p1) 
        {
            _player2.SkillEvent.ActorBeAttackedEvent.Invoke(skillAction1,skillAction2);
        }
    }

    private void HitToDefenceEvent(PlayerEnum p1, VSkillAction skillAction1, PlayerEnum p2, VSkillAction skillAction2)
    {
        if (_playerEnum1 == p1)
        {
            _player1.SkillEvent.ActorHitToDefenceEvent.Invoke(skillAction1,skillAction2);
        }
        else if (_playerEnum2 == p1) 
        {
            _player2.SkillEvent.ActorHitToDefenceEvent.Invoke(skillAction1,skillAction2);
        }
    }

    private void DefenceEvent(PlayerEnum p1, VSkillAction skillAction1, PlayerEnum p2, VSkillAction skillAction2)
    {
        if (_playerEnum1 == p1)
        {
            _player1.SkillEvent.ActorDefenceEvent.Invoke(skillAction1,skillAction2);
        }
        else if (_playerEnum2 == p1) 
        {
            _player2.SkillEvent.ActorDefenceEvent.Invoke(skillAction1,skillAction2);
        }
    }

    private void Subscribe()
    {
        EventManager.HitToPassiveEvent.AddEventHandler(HitToPassiveEvent);
        EventManager.PassiveToHitEvent.AddEventHandler(PassiveToHitEvent);
        
        EventManager.HitToDefenceEvent.AddEventHandler(HitToDefenceEvent);
        EventManager.DefenceToHitEvent.AddEventHandler(DefenceEvent);
    }
    
    public void Destroy()
    {
        EventManager.HitToPassiveEvent.RemoveEventHandler(HitToPassiveEvent);
        EventManager.PassiveToHitEvent.RemoveEventHandler(PassiveToHitEvent);
        
        EventManager.HitToDefenceEvent.RemoveEventHandler(HitToDefenceEvent);
        EventManager.DefenceToHitEvent.RemoveEventHandler(DefenceEvent);
    }
}
