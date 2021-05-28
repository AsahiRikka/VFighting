using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class AddressableResources : MonoBehaviour
{
    public static AddressableResources instance;
    
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    /// <summary>
    /// 特效资源
    /// </summary>
    [InfoBox("特效资源")] 
    public List<AssetReferenceGameObject> FXReferance;

    [InfoBox("声音预设")] 
    public AssetReferenceGameObject soundPrefab;

    [InfoBox("混音器")] public AudioMixer AudioMixer;
}
