using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// 游戏循环控制
/// </summary>
public class GameFramework : MonoBehaviour
{
    public static GameFramework instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public Service _service;
    public GameSceneManager GameSceneManager;
    public GameManager GameManager;
    
    // Start is called before the first frame update
    async void Start()
    {
        DebugHelper.LogError("打开Debug窗口");
        
        _service=new Service();
        
        //设置画质分辨率
        _service.SettingManager.Init();
        
        await _service.Init();
        
        UIManager.instance.ShowUI<MainStageScreen>();
        
        //销毁开始画面
        EventManager.SceneEnterCompleted.BoradCastEvent();
    }

    private bool isEnter = false;
    // Update is called once per frame
    void Update()
    {
        _service?.Update();
    }

    private void OnDestroy()
    {
        _service?.Destroy();
    }
}
