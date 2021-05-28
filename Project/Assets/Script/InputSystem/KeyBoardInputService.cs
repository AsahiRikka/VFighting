using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 键盘输入控制
/// </summary>
public class KeyBoardInputService :IInputService
{
    private const float DOUBLE_PRESS_INTERVAL = 0.5f;
    private const int CAPACITY = 20;  //预设按键列表大小
    
    private Dictionary<MyKeyCode, KeyCode> _keyMap;
    
    public KeyBoardInputService(Dictionary<MyKeyCode,KeyCode> keyMap)
    {
        _keyMap = keyMap;
    }

    public Dictionary<MyKeyCode, float> keyTimeMap = new Dictionary<MyKeyCode, float>(CAPACITY);   //按键被按下的时间
    public Dictionary<MyKeyCode, bool> pressSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> holdSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> releaseSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> comboSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);

    public void Init()
    {
        foreach (var item in Enum.GetValues(typeof(MyKeyCode)))
        {
            keyTimeMap.Add((MyKeyCode)item, 0f);
            pressSignal.Add((MyKeyCode)item, false);
            holdSignal.Add((MyKeyCode)item, false);
            releaseSignal.Add((MyKeyCode)item, false);
            comboSignal.Add((MyKeyCode)item, false);
        }
    }

    public void Update()
    {
        foreach (var item in _keyMap)
        {
            KeyCode key = item.Value;
            MyKeyCode val = item.Key;

            bool press = Input.GetKeyDown(key);
            pressSignal[val] = press;
            if (press)
            {
                if (Time.time - keyTimeMap[val] <= DOUBLE_PRESS_INTERVAL)
                {
                    comboSignal[val] = true;
                }
                keyTimeMap[val] = Time.time;
            }
            holdSignal[val] = Input.GetKey(key);

            bool release = Input.GetKeyUp(key);
            releaseSignal[val] = release;
            if (release)
            {
                comboSignal[val] = false;
            }
        }
    }
}
