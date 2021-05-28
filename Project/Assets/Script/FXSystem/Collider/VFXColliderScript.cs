using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class VFXColliderScript : MonoBehaviour
{
    private BoxCollider BoxCollider;

    /// <summary>
    /// 碰撞盒角色
    /// </summary>
    private PlayerEnum player;

    /// <summary>
    /// 碰撞器代表的技能
    /// </summary>
    [ShowInInspector]
    private VSkillAction currentSkill;
    
    /// <summary>
    /// 开始时间,duration
    /// </summary>
    public float startTime;

    /// <summary>
    /// 关闭时间，duration
    /// </summary>
    public float endTime;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <param name="skill"></param>
    public void FXColliderInit(PlayerEnum e, VSkillAction skill)
    {
        player = e;
        currentSkill = skill;
    }
    
    private string passiveTag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];
    
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //特效->身体
        if (other.CompareTag(passiveTag)) 
        {
            VActorPassiveColliderScript passiveScript = other.gameObject.GetComponent<VActorPassiveColliderScript>();
            PlayerEnum enemy = passiveScript.player;
            
            //不同阵营
            if (player == enemy) return;
            
            //停止检测
            BoxCollider.enabled = false;

            //攻击方通知
            EventManager.HitToPassiveEvent.BoradCastEvent(player, currentSkill, enemy, passiveScript.currentSkill);

            //受击方通知
            EventManager.PassiveToHitEvent.BoradCastEvent(enemy,passiveScript.currentSkill,player,currentSkill);
        }
    }

    public void SetBoxCollider(bool state)
    {
        BoxCollider = GetComponent<BoxCollider>();
        BoxCollider.enabled = state;
    }
}
