using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理UI组的生命周期，每个类型的组只存在一个实例
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    /// <summary>
    /// UI的可点击控制
    /// </summary>
    public GameObject UIShadow;

    private int _screenCount;
    public int ScreenCount
    {
        get
        {
            _screenCount = transform.childCount;
            return _screenCount;
        }
        private set
        { }
    }
    
    /// <summary>
    /// 根据UI组显示UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ShowUI<T>()where T: UIScreenBase
    {
        UIScreenBase screenBase = GetComponentInChildren<T>();
        if (screenBase == null)
        {
            DebugHelper.LogError("不存在该UI组件：" + typeof(T));
            return;
        }
        screenBase.OnDisplay();
    }

    /// <summary>
    /// 根据UI组隐藏UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void HideUI<T>()where T: UIScreenBase
    {
        UIScreenBase screenBase = GetComponentInChildren<T>();
        if (screenBase == null)
        {
            DebugHelper.LogError("不存在该UI组件：" + typeof(T));
            return;
        }
        screenBase.OnHide();
    }

    public bool GetUIState<T>() where T : UIScreenBase
    {
        UIScreenBase screenBase = GetComponentInChildren<T>();
        if (screenBase == null)
        {
            DebugHelper.LogError("不存在该UI组件：" + typeof(T));
            return false;
        }

        return screenBase.isActive;
    }

    /// <summary>
    /// 只显示某个UI组，会同时隐藏其他组UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void OnlyShowUI<T>()where T: UIScreenBase
    {
        UIScreenBase[] list = GetComponentsInChildren<UIScreenBase>();
        foreach (UIScreenBase screen in list)
        {
            if (screen != null) 
            {
                screen.OnHide();
            }
        }
        ShowUI<T>();
    }
}