using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorPassiveColliderScript : MonoBehaviour
{
    public VActorColliderScriptBase ColliderScriptBase;
    
    /// <summary>
    /// 碰撞盒阵营
    /// </summary>
    public PlayerEnum player;

    /// <summary>
    /// 碰撞器代表的技能
    /// </summary>
    public VSkillAction currentSkill;
    
    /// <summary>
    /// 开始帧，-1表示技能开始开启
    /// </summary>
    public int startFrame;

    /// <summary>
    /// 结束帧，-1表示技能结束关闭
    /// </summary>
    public int endFrame;


    private string hitTag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];
    private string defenceTag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];
    
}
