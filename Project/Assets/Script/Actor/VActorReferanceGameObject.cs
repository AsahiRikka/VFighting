using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 预设引用
/// </summary>
public class VActorReferanceGameObject : MonoBehaviour
{
    public GameObject parent;

    public GameObject actor;

    public GameObject actorRoot;

    public Rigidbody actorRig;
    
    public Animator actorAnimator;

    public TriggerDetection actorPhysicDetect;
    
    [SerializeField]
    private GameObject hitBoxPrefab;
    [SerializeField]
    private GameObject passiveBoxPrefab;
    [SerializeField]
    private GameObject defenceBoxPrefab;

    public VActorHitColliderScript GetHitColliderScript()
    {
        GameObject obj = Instantiate(hitBoxPrefab);
        return obj.GetComponent<VActorHitColliderScript>();
    }
    
    public VActorPassiveColliderScript GetPassiveColliderScript()
    {
        GameObject obj = Instantiate(passiveBoxPrefab);
        return obj.GetComponent<VActorPassiveColliderScript>();
    }
    
    public VActorDefenceColliderScript GetDefenceColliderScript()
    {
        GameObject obj = Instantiate(defenceBoxPrefab);
        return obj.GetComponent<VActorDefenceColliderScript>();
    }

    private void Start()
    {
        
    }
}
