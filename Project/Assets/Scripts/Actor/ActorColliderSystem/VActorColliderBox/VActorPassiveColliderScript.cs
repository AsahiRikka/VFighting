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

    private string hitTag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];
    private string defenceTag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];
    
    private void OnTriggerStay(Collider other)
    {
        //身体->攻击
        if (other.CompareTag(hitTag))
        {
            VActorHitColliderScript hitScript = other.gameObject.GetComponent<VActorHitColliderScript>();
            PlayerEnum enemy = hitScript.player;
            //不同阵营
            if (player != enemy)
            {
                EventManager.PassiveToHitEvent.BoradCastEvent(player, currentSkill, enemy, hitScript.currentSkill);
                gameObject.SetActive(false);
            }
        }
    }
}
