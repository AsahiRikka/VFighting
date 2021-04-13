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
    };
    
    public Dictionary<string,int> tagStringDictionary=new Dictionary<string, int>()
    {
        {"Untagged",(int)TagEnum.untagged },
        {"Ground",(int)TagEnum.ground },
        {"Wall",(int)TagEnum.wall },
    };
}