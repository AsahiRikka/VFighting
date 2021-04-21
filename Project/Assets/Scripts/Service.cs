using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏服务中心
/// </summary>
public class Service
{
    public KeySignalContainer input;
    public VFXManager VFXManager;

    public Service()
    {
        input=new KeySignalContainer();
        VFXManager=new VFXManager();
    }

    public void Init()
    {
        
    }

    public void Update()
    {
        input.Update();
    }
}
