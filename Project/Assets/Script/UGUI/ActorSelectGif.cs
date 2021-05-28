using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ActorSelectGif : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        lastTime = Time.fixedTime;
    }

    private RectTransform _transform;

    private float lastTime;
    private void FixedUpdate()
    {
        //活动状态持续进行rotate
        if (Time.fixedTime - lastTime > 0.05f) 
        {
            var rotation = _transform.rotation;

            _transform.Rotate(new Vector3(rotation.x, rotation.y,
                rotation.z - 90));

            lastTime = Time.fixedTime;
        }
    }
}
