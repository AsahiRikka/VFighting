using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 负责对角色朝向进行控制
/// </summary>
public class VActorDirManager
{
    public VActorDirManager(VActorBase actor1,VActorBase actor2)
    {
        _player1 = actor1.referanceGameObject.parent.transform;
        _player2 = actor2.referanceGameObject.parent.transform;
    }

    private Transform _player1;
    private Transform _player2;

    //初始顺序;true:1左2右；false：2左1右
    private bool _player = true;
    
    public void Update()
    {
        DirUpdateEvent();
    }

    /// <summary>
    /// 每当技能结束时判断方向变化
    /// </summary>
    private void DirUpdateEvent()
    {
        if (_player1.position.x < _player2.position.x && !_player)
        {
            EventManager.ActorDirTransEvent.BoradCastEvent(PlayerEnum.player_1,PlayerEnum.player_2);
            _player = true;
        }
        else if (_player1.position.x > _player2.position.x && _player) 
        {
            EventManager.ActorDirTransEvent.BoradCastEvent(PlayerEnum.player_2,PlayerEnum.player_1);
            _player = false;
        }
    }
}
