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

    private bool canTranfer = true;
    private float myTime = 0;
    
    public void Update()
    {
        DirUpdateEvent();
        if (canTranfer == false && Time.time - myTime > 1f) 
        {
            canTranfer = true;
        }
        
        //检测敌人距离的事件
        EventManager.ActorFarEvent.BoradCastEvent(_player2.position.x - _player1.position.x);
    }

    /// <summary>
    /// 判断方向变化，每次判断需间隔1s
    /// </summary>
    private void DirUpdateEvent()
    {
        if (_player1.position.x < _player2.position.x && !_player && canTranfer)
        {
            EventManager.ActorDirTransEvent.BoradCastEvent(PlayerEnum.player_1,PlayerEnum.player_2);
            _player = true;
            myTime = Time.time;
            canTranfer = false;
        }
        else if (_player1.position.x > _player2.position.x && _player && canTranfer) 
        {
            EventManager.ActorDirTransEvent.BoradCastEvent(PlayerEnum.player_2,PlayerEnum.player_1);
            _player = false;
            myTime = Time.time;
            canTranfer = false;
        }
    }
}
