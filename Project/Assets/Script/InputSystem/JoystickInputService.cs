using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInputService:IInputService
{
    private const float DOUBLE_PRESS_INTERVAL = 0.5f;
    private const int CAPACITY = 20;

    public Dictionary<MyKeyCode, float> keyTimeMap = new Dictionary<MyKeyCode, float>(CAPACITY);   //按键被按下的时间
    public Dictionary<MyKeyCode, bool> pressSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> holdSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> releaseSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);
    public Dictionary<MyKeyCode, bool> comboSignal = new Dictionary<MyKeyCode, bool>(CAPACITY);

    private Dictionary<MyKeyCode, KeyCode> _keyMapBtn;
    
    
    public JoystickInputService(Dictionary<MyKeyCode, KeyCode> keyMapBtn)
    {
        _keyMapBtn = keyMapBtn;
    }
    
    Dictionary<string, MyKeyCode> keyMapAxis = new Dictionary<string, MyKeyCode>(CAPACITY)
    {
        {"Left", MyKeyCode.leftArrow_1},
        {"Right", MyKeyCode.rightArrow_1},
    };
    
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
        foreach (var item in keyMapAxis)
        {
            InputUpdateAxis(item.Key, item.Value);
        }

        foreach (var item in _keyMapBtn)
        {
            InputUpdateBtn(item.Value, item.Key);
        }
    }
    

    void InputUpdateBtn(KeyCode key, MyKeyCode val)
    {
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

    void InputUpdateAxis(string key, MyKeyCode val)
    {
        bool signal = Input.GetAxisRaw(key) == 1;

        bool press = !pressSignal[val] && signal;
        pressSignal[val] = press;

        if (press)
        {
            if (Time.time - keyTimeMap[val] <= DOUBLE_PRESS_INTERVAL)
            {
                comboSignal[val] = true;
            }
            keyTimeMap[val] = Time.time;
        }

        holdSignal[val] = signal;

        bool release = pressSignal[val] && !signal;
        releaseSignal[val] = release;
        if (release)
        {
            comboSignal[val] = false;
        }

    }
    
}
