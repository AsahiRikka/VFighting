using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 给场景物体添加该脚本，让摄像机跟踪
/// </summary>
public class CameraFollowObj : MonoBehaviour
{
    public PlayerEnum playerType;
    
    private Transform actor;

    private void Start()
    {
        gameObject.SetActive(false);
        EventManager.ActorCameraFollow.AddEventHandler(ActorSet);
    }

    private void OnDestroy()
    {
        EventManager.ActorCameraFollow.RemoveEventHandler(ActorSet);
    }

    private void ActorSet(PlayerEnum playerEnum, Transform trans)
    {
        if (playerType == playerEnum)
        {
            actor = trans;
            if(actor==null)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }
    
    public void Update()
    {
        if (actor is null) return;
        var trans = transform;
        trans.position = actor.position;
        trans.rotation = actor.rotation;
    }
}