using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 我的输入和外设输入字符串的映射
/// </summary>
public class KeyMapSetting:ICloneable
{
    /// <summary>
    /// 我的外设信号
    /// </summary>
    public List<KeyMapToJson> keyMapJsons=new List<KeyMapToJson>();

    /// <summary>
    /// 里面的值作为初始值
    /// </summary>
    public KeyMapSetting()
    {
        keyMapJsons.Add(AddValue(MyKeyCode.upArrow_1,"W"));
        keyMapJsons.Add(AddValue(MyKeyCode.downArrow_1,"S"));
        keyMapJsons.Add(AddValue(MyKeyCode.leftArrow_1,"A"));
        keyMapJsons.Add(AddValue(MyKeyCode.rightArrow_1,"D"));
        keyMapJsons.Add(AddValue(MyKeyCode.attack1_1,"J"));
        keyMapJsons.Add(AddValue(MyKeyCode.attack2_1,"K"));
        keyMapJsons.Add(AddValue(MyKeyCode.dodge_1,"L"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill1_1,"U"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill2_1,"I"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill3_1,"O"));
        
        keyMapJsons.Add(AddValue(MyKeyCode.upArrow_2,"上方向键"));
        keyMapJsons.Add(AddValue(MyKeyCode.downArrow_2,"下方向键"));
        keyMapJsons.Add(AddValue(MyKeyCode.leftArrow_2,"左方向键"));
        keyMapJsons.Add(AddValue(MyKeyCode.rightArrow_2,"右方向键"));
        keyMapJsons.Add(AddValue(MyKeyCode.attack1_2,"小键盘1"));
        keyMapJsons.Add(AddValue(MyKeyCode.attack2_2,"小键盘2"));
        keyMapJsons.Add(AddValue(MyKeyCode.dodge_2,"小键盘3"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill1_2,"小键盘4"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill2_2,"小键盘5"));
        keyMapJsons.Add(AddValue(MyKeyCode.skill3_2,"小键盘6"));
    }

    private KeyMapToJson AddValue(MyKeyCode key,string value)
    {
        KeyMapToJson json=new KeyMapToJson();
        json.myKeyCode = key;
        json.keyCodeMapperKey = value;
        return json;
    }


    public object Clone()
    {
        KeyMapSetting newSetting = new KeyMapSetting();

        return newSetting;
    }
}
