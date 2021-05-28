using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// 技能BUFF事件
/// </summary>
[Serializable]
public class VSkillAction_Buff:VSkillActionBase
{
    [InfoBox("true:添加该buff;false:删除该buff")]
    public bool addOrRemoveBuff;

    public string buffID;

    [InfoBox("buff层级")]
    public int buffLevel;
}
