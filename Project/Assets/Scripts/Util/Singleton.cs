using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T _instance;

    protected Singleton()
    {
        
    }
    
    private static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance=new T();
            }
            return _instance;
        }
    }

    public static T GetInstance()
    {
        return Instance;
    }
}
