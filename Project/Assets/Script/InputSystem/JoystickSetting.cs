using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 手柄设置
/// </summary>
public class JoystickSetting
{
    /// <summary>
    /// 我的外设信号，按键部分
    /// </summary>
    public List<KeyMapToJson> keyMapJsons=new List<KeyMapToJson>();

    /// <summary>
    /// 外设信号，摇杆部分
    /// </summary>
    public List<KeyMapToJson> keyMapJsonsForAixs=new List<KeyMapToJson>();
    
    /// <summary>
    /// 里面的值作为初始值
    /// </summary>
    public JoystickSetting()
    {
        
    }

    private KeyMapToJson AddValue(MyKeyCode key,string value)
    {
        KeyMapToJson json=new KeyMapToJson();
        json.myKeyCode = key;
        json.keyCodeMapperKey = value;
        return json;
    }
}
