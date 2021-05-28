using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 场景管理
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public GameObject myScene;

    public Vector3 initPos;

    private GameObject scene;
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
       scene = Instantiate(myScene);
        myScene.transform.position = initPos;
    }

    private void OnDisable()
    {
        if(scene!=null)
            Destroy(scene);
    }
}
