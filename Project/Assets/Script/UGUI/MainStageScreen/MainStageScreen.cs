using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStageScreen : UIScreenBase
{
    private MainStageCtrl _stageCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        _stageCtrl = GetComponent<MainStageCtrl>();

        _stageCtrl.PlayetToPlayer.onClick.AddListener(PlayerToPlayerPush);
        _stageCtrl.PlayerToComputer.onClick.AddListener(PlayerToComputerPush);
        _stageCtrl.Setting.onClick.AddListener(SettingPush);
        _stageCtrl.QuitGame.onClick.AddListener(QuitGamePush);
    }

    private void PlayerToPlayerPush()
    {
        _mainStateParam.mainStageType = 1;
        EventManager.UIMainStageEvent.BoradCastEvent(_mainStateParam);
        
        //显示角色选择UI，退出主界面
        UIManager.instance.OnlyShowUI<ActorSelectScreen>();
    }

    private void PlayerToComputerPush()
    {
        _mainStateParam.mainStageType = 2;
        EventManager.UIMainStageEvent.BoradCastEvent(_mainStateParam);
        
        //显示角色选择UI，退出主界面
        UIManager.instance.OnlyShowUI<ActorSelectScreen>();
    }

    private void SettingPush()
    {
        _mainStateParam.mainStageType = 3;
        EventManager.UIMainStageEvent.BoradCastEvent(_mainStateParam);
        
        //显示设置弹窗
        UIManager.instance.ShowUI<SettingScreen>();
    }

    private void QuitGamePush()
    {
        _mainStateParam.mainStageType = 4;
        EventManager.UIMainStageEvent.BoradCastEvent(_mainStateParam);
        //退出游戏
        Application.Quit();
    }
    
    private MainStateParam _mainStateParam = new MainStateParam();

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {

    }
}

public class MainStateParam:UIParamBase
{
    /// <summary>
    /// 1:PVP,2:PVC,3:Setting,4:Quit
    /// </summary>
    public int mainStageType = 0;
}
