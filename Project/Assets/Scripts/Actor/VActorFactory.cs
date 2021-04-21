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
    /// <summary>
    /// 单个角色的加载，并进行实例化
    /// </summary>
    /// <param name="handle">加载完成时回调方法</param>
    public async UniTask LoadActor(string prefabKey, string configKey,string actorName,PlayerEnum e, Action<GameObject> handle = null)
    {
        GameObject actor;
        // 进行模型加载
        await ResourceManager.GetInstance().LoadAssetAndInstantiate(prefabKey, async result =>
        {
            actor = (GameObject)result.Result;
            // 为角色加载配置
            await ResourceManager.GetInstance().LoadAsset<VActorData>(configKey, configResult =>
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
