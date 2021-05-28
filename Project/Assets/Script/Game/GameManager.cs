using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 游戏部分加载并执行
/// </summary>
public class GameManager : MonoBehaviour
{
    private GameFactory _gameFactory;

    private VActorManager _actorManager;
    public VFXManager VFXManager;
    private GameSceneManager _sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        VFXManager = new VFXManager();
        _actorManager=new VActorManager();

        _gameFactory=new GameFactory(
            new VActorFactory(),
            GameFramework.instance._service.SettingManager.GameStartSetting,
            VFXManager,
            _actorManager);
    }

    public async void GameStart()
    {
        //加载界面UI
        UIManager.instance.OnlyShowUI<GameLodingScreen>();
        EventManager.UIGameLoadingEvent.BoradCastEvent(0.2f);
        
        //加载角色，角色间管理器
        await _gameFactory.LoadGame();

        UIManager.instance.ShowUI<GameLodingScreen>();
        EventManager.UIGameLoadingEvent.BoradCastEvent(0.4f);
        
        //加载场景
        if (_sceneManager == null)
        {
            _sceneManager = GameFramework.instance.GameSceneManager;
        }
        _sceneManager.gameObject.SetActive(true);
        
        UIManager.instance.ShowUI<GameLodingScreen>();
        EventManager.UIGameLoadingEvent.BoradCastEvent(0.9f);
        
        //UI控制
        UIManager.instance.OnlyShowUI<GamingScreen>();

        UIManager.instance.ShowUI<GameLodingScreen>();
        EventManager.UIGameLoadingEvent.BoradCastEvent(1f);
        
        //加载完成，开启倒计时UI
        UIManager.instance.ShowUI<GameCountDownScreen>();

        gameObject.SetActive(true);
        
        //开启协程准备输入
        StartCoroutine(StarGame());
    }

    IEnumerator StarGame()
    {
        yield return new WaitForSeconds(3);

        EventManager.ActorInputEvent.BoradCastEvent(true, true);
    }

    // Update is called once per frame
    void Update()
    {
        _actorManager?.Update();
    }

    public void GameFinish()
    {
        //关闭角色输入
        EventManager.ActorInputEvent.BoradCastEvent(false, false);
        
        //显示结束UI
        UIManager.instance.ShowUI<GameFinishScreen>();
    }

    public void GameQuit()
    {
        //关闭角色输入
        EventManager.ActorInputEvent.BoradCastEvent(false, false);
        
        //角色间管理器关闭
        _actorManager.VActorManagerDisable();
        
        //销毁角色
        _gameFactory.DestroyGame();
        Destroy(_gameFactory.Player1);
        Destroy(_gameFactory.Player2);
        
        //UI 控制
        UIManager.instance.OnlyShowUI<MainStageScreen>();
        
        //停止场景
        if (_sceneManager != null)
        {
            _sceneManager.gameObject.SetActive(false);
        }
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _actorManager?.Destroy();
    }
}
