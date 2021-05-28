using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLodingScreen : UIScreenBase
{
    public GameLodingCtrl GameLodingCtrl;

    private GameLoadingScreenParam _gameLoadingScreenParam;
    private void Start()
    {
        _gameLoadingScreenParam=new GameLoadingScreenParam();
        
        EventManager.UIGameLoadingEvent.AddEventHandler(ProgressChange);
    }

    private void OnDestroy()
    {
        EventManager.UIGameLoadingEvent.AddEventHandler(ProgressChange);
    }

    public override void OnDisplay()
    {
        base.OnDisplay();
        _gameLoadingScreenParam.progress = 0f;
    }

    private void ProgressChange(float progress)
    {
        if (progress > 1)
        {
            DebugHelper.LogError("错误的进度！"+progress);
            return;
        }

        _gameLoadingScreenParam.progress = progress;
        GameLodingCtrl.LoadSlider.value = _gameLoadingScreenParam.progress;

        if (_gameLoadingScreenParam.progress >= 1)
        {
            OnHide();
        }
    }
}

public class GameLoadingScreenParam : UIParamBase
{
    public float progress;
}