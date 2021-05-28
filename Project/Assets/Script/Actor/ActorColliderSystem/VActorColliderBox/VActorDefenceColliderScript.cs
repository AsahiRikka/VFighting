using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorDefenceColliderScript : MonoBehaviour
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

    public int startFrame;

    public int endFrame;

    private string hitTag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];
    private string defenceTag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];
    
    public void ActorDefenceColliderInit(Vector3 colliderCenter, Vector3 colliderSize, bool colliderTrigger, PlayerEnum e,
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
}
