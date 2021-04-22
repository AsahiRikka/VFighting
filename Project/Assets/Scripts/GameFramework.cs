using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
/// <summary>
/// 游戏循环控制
/// </summary>
public class GameFramework : MonoBehaviour
{
    public static GameFramework instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public Service _service;
    private GameFactory _gameFactory; 

    // Start is called before the first frame update
    void Start()
    {
        _service=new Service();
        _gameFactory=new GameFactory(
            new VActorFactory(),
            new GameStartSetting());

        _service.SoundManager.SoundInit();

        _gameFactory.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        _service.Update();
    }

    private void OnDestroy()
    {
        _gameFactory.Destroy();
    }
}
