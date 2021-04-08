using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 游戏通用设置，游戏时实例化，常驻存在
/// </summary>

public class GameCommonSetting
{
    public ResolutionTypeEnum resolutionType;

    public SoundSetting soundSetting;

    public QualitySettingTypeEnum gameQuality;

    private const string gameCommonSettingPath = "/Resources/GameCommonSetting/";
    private string soundPath = gameCommonSettingPath + "SoundSetting.txt";

    public GameCommonSetting()
    {
        soundSetting=new SoundSetting();

        LoadSoundSetting();
    }

    private void LoadSoundSetting()
    {
        //进入时通过文件初始化
        FileManager fileManager=new FileManager();
        string data = fileManager.DataLoad(soundPath);
        if (data.Length != 0)  
            soundSetting = JsonMapper.ToObject<SoundSetting>(fileManager.DataLoad(soundPath));
        else
            SaveSoundSetting();
    }
    
    public void SaveSoundSetting()
    {
        string soundJson = JsonMapper.ToJson(soundSetting);
        FileManager fileManager=new FileManager();
        fileManager.DataSave(soundPath,soundJson);
    }

    private void LoadResolutionType()
    {
        
    }
    
    public void SaveResolutionType()
    {
        
    }

    public void SaveQualitySetting()
    {
        
    }
}

/// <summary>
/// 分辨率设置
/// </summary>
public enum ResolutionTypeEnum
{
    FullScreen,
    w1920H1080,
    
}

/// <summary>
/// 画面等级设置
/// </summary>
public enum QualitySettingTypeEnum
{
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh,
    Ultra
}
