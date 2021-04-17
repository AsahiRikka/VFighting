using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 预设引用
/// </summary>
public class VActorReferanceGameObject : MonoBehaviour
{
    public GameObject parent;

    public GameObject actor;
    
    public Animator actorAnimator;

    public TriggerDetection actorPhysicDetect;

    public GameObject hitBoxPrefab;
    public GameObject passiveBoxPrefab;
    public GameObject defenceBoxPrefab;
    
    private void Start()
    {
        
    }
}
