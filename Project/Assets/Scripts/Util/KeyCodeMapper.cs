using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 字符串对应的keyCode，在此的可被自定义
/// </summary>
public static class KeyCodeMapper
{
    public static Dictionary<String, KeyCode> KeyMapDic = new Dictionary<string, KeyCode>()
    {
        {"A",KeyCode.A},
        {"W",KeyCode.W},
        {"S",KeyCode.S},
        {"D",KeyCode.D},
        {"J",KeyCode.J},
        {"K",KeyCode.K},
        {"L",KeyCode.L},
        {"U",KeyCode.U},
        {"I",KeyCode.I},
        {"O",KeyCode.O},
        
        {"UpArrow",KeyCode.UpArrow},
        {"DownArrow",KeyCode.DownArrow},
        {"LeftArrow",KeyCode.LeftArrow},
        {"RightArrow",KeyCode.RightArrow},
        {"Keypad1",KeyCode.Keypad1},
        {"Keypad2",KeyCode.Keypad2},
        {"Keypad3",KeyCode.Keypad3},
        {"Keypad4",KeyCode.Keypad4},
        {"Keypad5",KeyCode.Keypad5},
        {"Keypad6",KeyCode.Keypad6},
        
        {"Z",KeyCode.Z},
        
        {"JSB0",KeyCode.Joystick1Button0},
    };
}