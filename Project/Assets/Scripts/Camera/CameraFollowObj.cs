using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 给场景物体添加该脚本，让摄像机跟踪
/// </summary>
public class CameraFollowObj : MonoBehaviour
{
    [HideInInspector] public GameObject actor;

    public void Update()
    {
        if (!(actor is null))
        {
            gameObject.transform.position = actor.transform.position;
            gameObject.transform.rotation = actor.transform.rotation;
        }
    }
}
