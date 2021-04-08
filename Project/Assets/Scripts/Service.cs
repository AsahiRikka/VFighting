using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏服务中心
/// </summary>
public class Service
{
    public KeySignalContainer input;

    public Service()
    {
        input=new KeySignalContainer();
    }

    public void Init()
    {
        
    }

    public void Update()
    {
        input.Update();
    }
}
