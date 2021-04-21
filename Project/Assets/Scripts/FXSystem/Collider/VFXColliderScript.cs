using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXColliderScript : MonoBehaviour
{
    public BoxCollider BoxCollider;

    private void Start()
    {
        //默认设置关闭
        enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
