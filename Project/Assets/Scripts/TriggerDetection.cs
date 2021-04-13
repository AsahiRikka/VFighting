using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 触发检测器，挂载在含碰撞器的物体上
/// </summary>
public class TriggerDetection : MonoBehaviour
{
    /// <summary>
    /// 触发器进入事件
    /// </summary>
    public UnityAction<TagEnum> TriggerEnterDetection;

    /// <summary>
    /// 触发器退出事件
    /// </summary>
    public UnityAction<TagEnum> TriggerExitDetection;

    private void Start()
    {
        #if DEBUGER
        Collider coll = gameObject.GetComponent<Collider>(); 
        if (coll == null || !coll.isTrigger)
        {
            DebugHelper.LogError("不存在对应的触发器" + gameObject.name);
        }
        #endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerEnterDetection != null)
        {
            TagEnum result = (TagEnum) TagType.GetInstance().tagStringDictionary[other.tag];
            TriggerEnterDetection.Invoke(result);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TriggerExitDetection != null)
        {
            TagEnum result = (TagEnum) TagType.GetInstance().tagStringDictionary[other.tag];
            TriggerExitDetection.Invoke(result);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
