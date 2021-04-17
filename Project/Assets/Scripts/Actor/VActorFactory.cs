using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 角色工厂
/// </summary>
public class VActorFactory
{

    // public async Task LoadActor(string prefabKey, string configKey,string actorName,PlayerEnum ePlayer, Action<GameObject> handle = null)
    // {
    //     try
    //     {
    //         MyLoadActor(prefabKey, configKey, actorName, ePlayer);
    //     }
    //     catch (Exception e)
    //     {
    //         DebugHelper.LogError(e.Message);
    //         throw;
    //     }
    // }
    
    /// <summary>
    /// 单个角色的加载，并进行实例化
    /// </summary>
    /// <param name="handle">加载完成时回调方法</param>
    public void LoadActor(string prefabKey, string configKey,string actorName,PlayerEnum e, Action<GameObject> handle = null)
    {
        GameObject actor;
        // 进行模型加载
        ResourceManager.GetInstance().LoadAssetAndInstantiate(prefabKey, async result =>
        {
            actor = (GameObject)result.Result;
            // 为角色加载配置
            ResourceManager.GetInstance().LoadAsset<VActorData>(configKey, configResult =>
            {
                /*
                 * 翠花到此一游；
                 */

                VActorLoad.ActorDataBind(actorName, actor, configResult.Result,e);
               
                handle?.Invoke(actor);
            });
        });
    }
}
