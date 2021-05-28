using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// UI控制的基类
/// </summary>
public abstract class UIScreenBase : MonoBehaviour
{
    public UIType UIType;
    public CanvasGroup canvasGroup;

    [HideInInspector] 
    public bool isActive = false;

    private void Awake()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDisplay()
    {
        //确认启动
        gameObject.SetActive(true);

        //通过canvasGroup控制显隐和输入
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        
        isActive = true;
        
        //如果是弹窗
        if (UIType == UIType.pop)
        {
            UIManager.instance.UIShadow.SetActive(true);
            UIManager.instance.UIShadow.transform.SetSiblingIndex(UIManager.instance.ScreenCount-1);
            transform.SetSiblingIndex(UIManager.instance.ScreenCount - 1);
        }
    }

    private void Update()
    {
        if(!isActive)
            return;
        
        MyUpdate();
    }

    public virtual void MyUpdate()
    {
        
    }

    public virtual void OnHide()
    {
        //通过canvasGroup控制显隐和输入
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        
        isActive = false;
        
        //如果是弹窗
        if (UIType == UIType.pop)
        {
            UIManager.instance.UIShadow.SetActive(false);
        }
    }
}

/// <summary>
/// UI参数基类，通过事件系统进行传递
/// </summary>
public class UIParamBase
{
    
}

public enum UIType
{
    /// <summary>
    /// 弹窗，弹窗在启动后产生遮罩避免其他正在显示的UI被点击
    /// </summary>
    pop,
    /// <summary>
    /// 其他
    /// </summary>
    normal,
}