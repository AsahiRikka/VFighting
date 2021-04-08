﻿using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameFactory _gameFactory; 

    // Start is called before the first frame update
    void Start()
    {
        _service=new Service();
        _gameFactory=new GameFactory(
            new VActorFactory(),
            new GameStartSetting());
        
        _gameFactory.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        _service.Update();
    }
}