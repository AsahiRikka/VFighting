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
    public SoundSetting SoundSetting;
    public OtherSetting OtherSetting;

    private readonly string gameCommonSettingPath;

    private readonly string soundPath;
    private readonly string otherPath;

    public GameCommonSetting()
    {
        gameCommonSettingPath = "/GameCommonSetting/";
        soundPath = gameCommonSettingPath + "SoundSetting.txt";
        otherPath = gameCommonSettingPath + "OtherSetting.txt";

        SoundSetting=new SoundSetting();
        OtherSetting=new OtherSetting();
        LoadSoundSetting();
        LoadOtherSetting();
    }

    private void LoadSoundSetting()
    {
        //进入时通过文件初始化
        FileManager fileManager=new FileManager();
        string data = fileManager.DataLoad(soundPath);
        if (data.Length != 0)  
            SoundSetting = JsonMapper.ToObject<SoundSetting>(fileManager.DataLoad(soundPath));
        else
            SaveSoundSetting();
    }
    
    public void SaveSoundSetting()
    {
        string soundJson = JsonMapper.ToJson(SoundSetting);
        FileManager fileManager=new FileManager();
        fileManager.DataSave(soundPath,soundJson);
    }

    private void LoadOtherSetting()
    {
        //进入时通过文件初始化
        FileManager fileManager=new FileManager();
        string data = fileManager.DataLoad(otherPath);
        if (data.Length != 0)  
            OtherSetting = JsonMapper.ToObject<OtherSetting>(fileManager.DataLoad(otherPath));
        else
            SaveOtherSetting();
    }
    
    public void SaveOtherSetting()
    {
        string otherJson = JsonMapper.ToJson(OtherSetting);
        FileManager fileManager=new FileManager();
        fileManager.DataSave(otherPath,otherJson);
    }
}
