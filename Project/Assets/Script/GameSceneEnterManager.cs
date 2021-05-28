using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏最开始进入场景的控制，完成进入后直接销毁自身
/// </summary>
public class GameSceneEnterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.SceneEnterCompleted.AddEventHandler(SceneEnter);
    }

    private void OnDestroy()
    {
        EventManager.SceneEnterCompleted.RemoveEventHandler(SceneEnter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneEnter()
    {
        DestroyImmediate(gameObject);
    }
}
