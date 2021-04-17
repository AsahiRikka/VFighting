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

    private VActorManager _actorManager;

    private GameObject player1;
    private GameObject player2;
    
    public GameFactory(VActorFactory actorFactory,GameStartSetting startSetting)
    {
        _actorFactory = actorFactory;
        _startSetting = startSetting;
    }

    /// <summary>
    /// 进入游戏的加载
    /// </summary>
    public async UniTask LoadGame()
    {
        _actorFactory.LoadActor(ResourcesName.FukaPrefabName, ResourcesName.FukaConfigName, ResourcesName.FukaName,PlayerEnum.player_1,
            result => player1 = result);

        _actorFactory.LoadActor(ResourcesName.FukaPrefabName, ResourcesName.FukaConfigName, ResourcesName.FukaName,PlayerEnum.player_2,
            result => player2 = result);

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        _actorManager = new VActorManager(player1.GetComponent<VActorBase>(), player2.GetComponent<VActorBase>());
    }

    public void Destroy()
    {
        _actorManager.Destory();
    }
}
