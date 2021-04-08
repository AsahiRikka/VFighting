using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏部分的工厂，负责游戏部分的加载
/// </summary>
public class GameFactory
{
    private readonly VActorFactory _actorFactory;
    private readonly GameStartSetting _startSetting;

    private GameObject actor;
    public GameFactory(VActorFactory actorFactory,GameStartSetting startSetting)
    {
        _actorFactory = actorFactory;
        _startSetting = startSetting;
    }

    /// <summary>
    /// 进入游戏的加载
    /// </summary>
    public void LoadGame()
    {
        _actorFactory.LoadActor(ResourcesName.FukaPrefabName, ResourcesName.FukaConfigName, ResourcesName.FukaName,
            result => actor = result);
        
        
    }
}
