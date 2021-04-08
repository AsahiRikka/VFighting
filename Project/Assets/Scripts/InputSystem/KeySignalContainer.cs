using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 输入服务
/// </summary>
public class KeySignalContainer
{
    public readonly KeyBoardInputService keyboardInput;
    public readonly JoystickInputService joystickInput;
    
    private InputSetting _inputSetting;

    public KeySignalContainer()
    {
        _inputSetting=new InputSetting();
        
        this.keyboardInput=new KeyBoardInputService(_inputSetting.myKeyMapMapper);
        this.joystickInput=new JoystickInputService(_inputSetting.myJoystickMapper);
        
        this.keyboardInput.Init();
        this.joystickInput.Init();
    }

    public void Update()
    {
        keyboardInput.Update();
        joystickInput.Update();
    }
    
    #region 按下
    /// <summary>
    /// 按键按下
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsPressed(MyKeyCode key)
    {
        return keyboardInput.pressSignal[key] || joystickInput.pressSignal[key];
    }
    #endregion
    
    /// <summary>
    /// 任意按键按下
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public bool IsAnyPressed(params MyKeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (keyboardInput.pressSignal[key] || joystickInput.pressSignal[key])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 所有键按下
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public bool IsAllPress(params MyKeyCode[] keys)
    {
        // int length = keys.Length;
        // int temp = 0;
        // foreach (var key in keys)
        // {
        //     if (keyboardInput.holdSignal[key] || joystickInput.holdSignal[key])
        //     {
        //         temp++;
        //     }
        // }
        // //全部键按下时
        // if (temp == length)
        // {
        //     if (IsAnyPressed(keys))
        //     {
        //         return true;
        //     }
        // }
        // return false;

        if (IsAllHold(keys) && IsAnyPressed(keys))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region 按住

    /// <summary>
    /// 按键按住
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsHold(MyKeyCode key)
    {
        return keyboardInput.holdSignal[key] || joystickInput.holdSignal[key];
    }

    /// <summary>
    /// 任意按住
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public bool IsAnyHold(params MyKeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (keyboardInput.holdSignal[key] || joystickInput.holdSignal[key])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 所有按键按住
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public bool IsAllHold(params MyKeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (!keyboardInput.holdSignal[key] && !joystickInput.holdSignal[key])
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region 松开

    /// <summary>
    /// 按键松开
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsRelease(MyKeyCode key)
    {
        return keyboardInput.releaseSignal[key] || joystickInput.releaseSignal[key];
    }

    /// <summary>
    /// 任意按键松开
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    public bool IsAnyRelease(params MyKeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (keyboardInput.releaseSignal[key] || joystickInput.releaseSignal[key])
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region 双击

    /// <summary>
    /// 按键双击
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsCombo(MyKeyCode key)
    {
        return keyboardInput.comboSignal[key] || joystickInput.comboSignal[key];
    }
    #endregion
}