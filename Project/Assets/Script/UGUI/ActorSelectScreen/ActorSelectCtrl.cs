using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ActorSelectCtrl : UIControlBase
{

    public Image Player1_Select;
    public Image Player2_Select;

    public Image Vector_p1;
    public Image Vector_p2;

    public Image Player1_Gif;
    public Image Player2_Gif;

    public GameObject Player1Direct;
    public GameObject Player2Direct;

    public Image BackGroundImage;
    
    public ActorImageUIDictionary SelectUIDictionary;
}
