using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static UnityEngine.AddressableAssets.Addressables;

public class ResourceManager:Singleton<ResourceManager>
{
    public IEnumerator Initialize()
    {
        yield return InitializeAsync();

        UnityEngine.ResourceManagement.ResourceManager.ExceptionHandler = ExceptionHandler;
    }

    private void ExceptionHandler(AsyncOperationHandle op, Exception ex)
    {
        string keyName = ex.Message.Split('=')[1];
        DebugHelper.LogError("资源加载错误：Addressable名称 = {0}  资源类型：{1}，DebugName：{2}", keyName, ex.GetType().Name, op.DebugName);
    }

    public AsyncOperationHandle<GameObject> InstantiateGaemObjectAsync(string key, Action<AsyncOperationHandle<GameObject>> Completed,
        Transform parent, bool instantiateInWorldSpace = false, bool trackHandle = true)
    {
        AsyncOperationHandle<GameObject> handle = InstantiateGaemObjectAsync(key, Completed, parent, instantiateInWorldSpace, trackHandle);
        return handle;
    }

    /// <summary>
    /// 加载单个资产
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="Completed">回调操作</param>
    public AsyncOperationHandle<T> LoadAsset<T>(string key, Action<AsyncOperationHandle<T>> Completed = null)
    {
        Assert.AreNotEqual<string>(key, "", "加载的资源key值为空");

        AsyncOperationHandle<T> asyncOperationHandle = LoadAssetAsync<T>(key);
        asyncOperationHandle.Completed += Completed;
        return asyncOperationHandle;
    }
    
    public AsyncOperationHandle LoadAssetAndInstantiate(string key, Action<AsyncOperationHandle> Completed = null)
    {
        Assert.AreNotEqual<string>(key, "", "加载的资源key值为空");

        AsyncOperationHandle asyncOperationHandle = InstantiateAsync(key);
        asyncOperationHandle.Completed += Completed;
        return asyncOperationHandle;
    }

    /// <summary>
    /// 根据group加载资源
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    /// <typeparam name="TObject"></typeparam>
    /// <returns></returns>
    public AsyncOperationHandle<IList<TObject>> LoadAssets<TObject>(object key, Action<TObject> callback)
    {
        return LoadAssetsAsync(key, callback);
    }

    /// <summary>
    /// 加载多个资源，keys数量不能小于2
    /// </summary>
    public AsyncOperationHandle<IList<TObject>> LoadAssets<TObject>(IList<object> keys, Action<TObject> callback, Addressables.MergeMode mode)
    {
        return LoadAssetsAsync(keys, callback, mode);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Release(AsyncOperationHandle handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }

    }


    /// <summary>
    /// 释放资源
    /// </summary>
    public void Release<TObject>(AsyncOperationHandle<TObject> handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
    }

    public AsyncOperationHandle<SceneInstance> LoadScene(string key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
    {
        return LoadSceneAsync(key, loadMode, activateOnLoad, priority);
    }
}
