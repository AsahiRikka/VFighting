using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VActorHitColliderScript : MonoBehaviour
{
    public VActorColliderScriptBase ColliderScriptBase;

    /// <summary>
    /// 碰撞盒角色
    /// </summary>
    public PlayerEnum player;

    /// <summary>
    /// 碰撞器代表的技能
    /// </summary>
    public VSkillAction currentSkill;

    private string hitTag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];
    private string defenceTag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];

    private void OnTriggerStay(Collider other)
    {
        //攻击->身体
        if (other.CompareTag(passiveTag))
        {
            VActorPassiveColliderScript passiveScript = other.gameObject.GetComponent<VActorPassiveColliderScript>();
            PlayerEnum enemy = passiveScript.player;
            //不同阵营
            if (player != enemy)
            {
                EventManager.HitToPassiveEvent.BoradCastEvent(player, currentSkill, enemy, passiveScript.currentSkill);
                gameObject.SetActive(false);
            }
        }
    }

}
