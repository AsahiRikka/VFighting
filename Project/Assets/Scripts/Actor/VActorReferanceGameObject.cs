﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 预设引用
/// </summary>
public class VActorReferanceGameObject : MonoBehaviour
{
    public GameObject parent;

    public Animator actorAnimator;

    public void MyCoroutine(IEnumerator method)
    {
        StartCoroutine(method);
    }
}