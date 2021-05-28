using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 游戏部分的工厂，负责游戏部分的加载
/// </summary>
public class GameFactory
{
    private readonly VActorFactory _actorFactory;
    private readonly GameStartSetting _startSetting;
    private readonly VFXManager _vfxManager;
    private VActorManager _actorManager;

    public GameObject Player1;
    public GameObject Player2;
    
    public GameFactory(VActorFactory actorFactory,GameStartSetting startSetting,VFXManager vfxManager,VActorManager actorManager)
    {
        _actorFactory = actorFactory;
        _startSetting = startSetting;
        _vfxManager = vfxManager;
        _actorManager = actorManager;
    }

    /// <summary>
    /// 进入游戏的加载
    /// </summary>
    public async UniTask LoadGame()
    {
        //获取双方角色
        ActorResourcesName player_1 = ResourcesName.ActorResources[_startSetting.Player_1];
        ActorResourcesName player_2 = ResourcesName.ActorResources[_startSetting.Player_2];
        
        //加载特效
        await _vfxManager.VFXInit();
        
        await _actorFactory.LoadActor(player_1.PrefabName, player_1.ConfigName, player_1.Name,PlayerEnum.player_1,
            result => Player1 = result);

        await _actorFactory.LoadActor(player_2.PrefabName, player_2.ConfigName, player_2.Name,PlayerEnum.player_2,
            result => Player2 = result);

        await UniTask.WaitUntil(() => (Player1 && Player2));
        
        _actorManager.VActorManagerInit(Player1.GetComponent<VActorBase>(), Player2.GetComponent<VActorBase>());
    }

    public void DestroyGame()
    {
        _vfxManager.VFXDestroy();
        EventManager.ActorCameraFollow.BoradCastEvent(PlayerEnum.player_1,null);
        EventManager.ActorCameraFollow.BoradCastEvent(PlayerEnum.player_2,null);
    }
}
