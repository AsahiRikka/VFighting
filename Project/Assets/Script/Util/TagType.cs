using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagType:Singleton<TagType>
{
    public Dictionary<int, string> tagDictionary = new Dictionary<int, string>()
    {
        {(int)TagEnum.untagged,"Untagged" },
        {(int)TagEnum.ground,"Ground" },
        {(int)TagEnum.wall,"Wall" },
        {(int)TagEnum.player1,"Player1" },
        {(int)TagEnum.player2,"Player2" },
        {(int)TagEnum.hitCollider,"HitCollider" },
        {(int)TagEnum.passiveCollider,"PassiveCollider" },
        {(int)TagEnum.defenceCollider,"DefenceCollider" },
        {(int)TagEnum.FXCollider,"FXCollider" },
    };
    
    public Dictionary<string,int> tagStringDictionary=new Dictionary<string, int>()
    {
        {"Untagged",(int)TagEnum.untagged },
        {"Ground",(int)TagEnum.ground },
        {"Wall",(int)TagEnum.wall },
        {"Player1",(int)TagEnum.player1 },
        {"Player2",(int)TagEnum.player2 },
        {"HitCollider",(int)TagEnum.hitCollider },
        {"PassiveCollider",(int)TagEnum.passiveCollider },
        {"DefenceCollider",(int)TagEnum.defenceCollider },
        {"FXCollider",(int)TagEnum.FXCollider },
    };
}