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

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="colliderCenter"></param>
    /// <param name="colliderSize"></param>
    /// <param name="colliderTrigger"></param>
    /// <param name="e"></param>
    /// <param name="skill"></param>
    /// <param name="startF"></param>
    /// <param name="endF"></param>
    public void ActorPassiveColliderInit(Vector3 colliderCenter, Vector3 colliderSize, bool colliderTrigger, PlayerEnum e,
        VSkillAction skill, int startF, int endF)
    {
        ColliderScriptBase.collider.center = colliderCenter;
        ColliderScriptBase.collider.size = colliderSize;
        ColliderScriptBase.collider.isTrigger = colliderTrigger;
        player = e;
        currentSkill = skill;
        startFrame = startF;
        endFrame = endF;
    }

    private string hitTag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];
    private string defenceTag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];
    
}
