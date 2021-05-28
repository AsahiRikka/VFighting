using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 游戏服务中心
/// </summary>
public class Service
{
    public KeySignalContainer input;
    public SoundManager SoundManager;
    public GameSettingManager SettingManager;

    public Service()
    {
        SettingManager=new GameSettingManager();
        SoundManager=new SoundManager();
    }

    public async UniTask Init()
    {
        input=new KeySignalContainer();
        await SoundManager.SoundInit();
    }

    public void Update()
    {
        input.Update();
    }

    public void Destroy()
    {
        SettingManager.MyDestroy();
    }
}
