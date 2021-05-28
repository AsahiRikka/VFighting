using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设置的管理器
/// </summary>
public class GameSettingManager
{
    public GameModeSetting GameModeSetting=new GameModeSetting();
    public GameStartSetting GameStartSetting=new GameStartSetting();
    public GameCommonSetting GameCommonSetting=new GameCommonSetting();
    public InputSetting InputSetting=new InputSetting();

    #region UI通信

    //和外部的通信
    public GameSettingManager()
    {
        EventManager.UIMainStageEvent.AddEventHandler(UIMainStageEvent);
        EventManager.UISettingEvent.AddEventHandler(UISettingEvent);
        EventManager.UIActorSelectEvent.AddEventHandler(UIActorSelectEvent);
        EventManager.UIGameEnterSettingEvent.AddEventHandler(UIGameStartSettingEvent);
    }

    public void MyDestroy()
    {
        EventManager.UIMainStageEvent.RemoveEventHandler(UIMainStageEvent);
        EventManager.UISettingEvent.RemoveEventHandler(UISettingEvent);
        EventManager.UIActorSelectEvent.RemoveEventHandler(UIActorSelectEvent);
        EventManager.UIGameEnterSettingEvent.RemoveEventHandler(UIGameStartSettingEvent);
    }

    private void UIMainStageEvent(MainStateParam mainStateParam)
    {
        switch (mainStateParam.mainStageType)
        {
            case 1:
                //选择PVP
                GameModeSetting.GameMode = GameModeTypeEnum.playerToPlayer;
                break;
            case 2:
                //选择PVC
                GameModeSetting.GameMode = GameModeTypeEnum.playerToComputer;
                break;
            default:
                break;
        }
    }

    private void UISettingEvent(SettingScreenPram settingScreenPram)
    {
        //保存声音大小
        GameCommonSetting.SoundSetting = settingScreenPram.SoundSetting;
        GameCommonSetting.SaveSoundSetting();

        //保存其他设置
        GameCommonSetting.OtherSetting = settingScreenPram.OtherSetting;
        GameCommonSetting.SaveOtherSetting();

        //保存按键设置
        InputSetting.KeySettingSave(settingScreenPram.KeyMapSetting);
        
        //进行修改
        SoundSetting();
        QualitySetting();
        ResolutionSetting();
    }

    private void UIActorSelectEvent(ActorSelectScreenParam actorSelectScreenParam)
    {
        GameStartSetting.Player_1 = actorSelectScreenParam.player1;
        GameStartSetting.Player_2 = actorSelectScreenParam.player2;
    }

    private void UIGameStartSettingEvent(GameEnterSettingParams settingParams)
    {
        GameStartSetting.gameTime = settingParams.GameTimeType;
        GameStartSetting.healthScale = settingParams.HeathScaleType;
    }

    #endregion

    #region 设置控制

    public void Init()
    {
        QualitySetting();
        ResolutionSetting();
    }

    private void QualitySetting()
    {
        switch (GameCommonSetting.OtherSetting.gameQuality)
        {
            case QualitySettingTypeEnum.VeryLow:
                QualitySettings.SetQualityLevel(0,true);
                break;
            case QualitySettingTypeEnum.Low:
                QualitySettings.SetQualityLevel(1,true);
                break;
            case QualitySettingTypeEnum.Medium:
                QualitySettings.SetQualityLevel(2,true);
                break;
            case QualitySettingTypeEnum.High:
                QualitySettings.SetQualityLevel(3,true);
                break;
            case QualitySettingTypeEnum.VeryHigh:
                QualitySettings.SetQualityLevel(4,true);
                break;
            case QualitySettingTypeEnum.Ultra:
                QualitySettings.SetQualityLevel(5,true);
                break;
            default:
                break;
        }
    }

    private void ResolutionSetting()
    {
        switch (GameCommonSetting.OtherSetting.resolutionType)
        {
            case ResolutionTypeEnum.FullScreen:
                Screen.SetResolution(Screen.width,Screen.height,true);
                break;
            case ResolutionTypeEnum.w1920H1080:
                Screen.SetResolution(1920,1080,false);
                break;
            case ResolutionTypeEnum.W1080H720:
                Screen.SetResolution(1080,720,false);
                break;
            default:
                break;
        }
    }

    private void SoundSetting()
    {
        GameFramework.instance._service.SoundManager.VolumeSetting(GameCommonSetting.SoundSetting);
    }

    #endregion
}
