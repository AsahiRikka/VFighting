using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
/// <summary>
/// 自定义输入设置与保存
/// </summary>
public class InputSetting
{
    private KeyMapSetting keyMapSetting;
    private JoystickSetting joystickSetting;

    /// <summary>
    /// 维护一个字典用于KeyMapInput访问
    /// </summary>
    public Dictionary<MyKeyCode, KeyCode> myKeyMapMapper;

    /// <summary>
    /// 用于手柄信号访问
    /// </summary>
    public Dictionary<MyKeyCode, KeyCode> myJoystickMapper;
    
    private const string gameCommonSettingPath = "/Resources/GameCommonSetting/";
    private string keyMapPath = gameCommonSettingPath + "KeyMapSetting.txt";
    private string joystickPath = gameCommonSettingPath + "JoystickSetting.txt";
    
    public InputSetting()
    {
        keyMapSetting=new KeyMapSetting();
        joystickSetting=new JoystickSetting();

        myKeyMapMapper=new Dictionary<MyKeyCode, KeyCode>();
        myJoystickMapper=new Dictionary<MyKeyCode, KeyCode>();
        
        LoadKeyMapSetting();
        LoadJoystickSetting();
    }
    
        private void LoadKeyMapSetting()
    {
        //进入时通过文件初始化
        FileManager fileManager=new FileManager();
        string data = fileManager.DataLoad(keyMapPath);
        if (data.Length != 0)  
            keyMapSetting.keyMapJsons = JsonMapper.ToObject<List<KeyMapToJson>>(fileManager.DataLoad(keyMapPath));
        else
            SaveKeyMapSetting();
        
        //管理我的按键映射，获取或者保存时从文件添加进游戏中
        myKeyMapMapper.Clear();
        foreach (var v in keyMapSetting.keyMapJsons)
        {
            myKeyMapMapper.Add(v.myKeyCode,KeyCodeMapper.KeyMapDic[v.keyCodeMapperKey]);
        }
    }

    public void SaveKeyMapSetting()
    {
        string keyMapJson = JsonMapper.ToJson(keyMapSetting.keyMapJsons);
        FileManager fileManager=new FileManager();
        fileManager.DataSave(keyMapPath,keyMapJson);
        
        //管理我的按键映射，获取或者保存时从文件添加进游戏中
        myKeyMapMapper.Clear();
        foreach (var v in keyMapSetting.keyMapJsons)
        {
            myKeyMapMapper.Add(v.myKeyCode,KeyCodeMapper.KeyMapDic[v.keyCodeMapperKey]);
        }
    }

    private void LoadJoystickSetting()
    {
        //进入时通过文件初始化
        FileManager fileManager=new FileManager();
        string data = fileManager.DataLoad(joystickPath);
        if (data.Length != 0)  
            joystickSetting.keyMapJsons = JsonMapper.ToObject<List<KeyMapToJson>>(fileManager.DataLoad(joystickPath));
        else
            SaveJoystickSetting();
        
        //管理我的按键映射，获取或者保存时从文件添加进游戏中
        myJoystickMapper.Clear();
        foreach (var v in joystickSetting.keyMapJsons)
        {
            myJoystickMapper.Add(v.myKeyCode,KeyCodeMapper.KeyMapDic[v.keyCodeMapperKey]);
        }
    }

    public void SaveJoystickSetting()
    {
        string joyStickJson = JsonMapper.ToJson(joystickSetting.keyMapJsons);
        FileManager fileManager=new FileManager();
        fileManager.DataSave(joystickPath,joyStickJson);
        
        //管理我的按键映射，获取或者保存时从文件添加进游戏中
        myJoystickMapper.Clear();
        foreach (var v in joystickSetting.keyMapJsons)
        {
            myJoystickMapper.Add(v.myKeyCode,KeyCodeMapper.KeyMapDic[v.keyCodeMapperKey]);
        }
    }
}
