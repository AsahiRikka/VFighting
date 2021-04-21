using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableResources : MonoBehaviour
{
    public static AddressableResources instance;
    
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    /// <summary>
    /// 特效资源
    /// </summary>
    [InfoBox("特效资源")] 
    public List<AssetReferenceGameObject> FXReferance;
}
