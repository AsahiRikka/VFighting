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
    }

    private RectTransform _transform;

    // Update is called once per frame
    void Update()
    {
        //活动状态持续进行rotate
        if (Time.frameCount % 5 == 0)
        {
            var rotation = _transform.rotation;

            _transform.Rotate(new Vector3(rotation.x, rotation.y,
                rotation.z - 90));
        }
    }
}
