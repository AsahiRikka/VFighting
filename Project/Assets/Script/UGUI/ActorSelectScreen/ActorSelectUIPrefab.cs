using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorSelectUIPrefab : MonoBehaviour
{
    [HideInInspector]
    public ActorEnum e;
    
    public Image ActorImage;

    public void Init(ActorEnum actor,Sprite actorSprite)
    {
        e = actor;
        ActorImage.sprite = actorSprite;
    }
}
