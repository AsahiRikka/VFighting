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
    public void ActorHitColliderInit(Vector3 colliderCenter, Vector3 colliderSize, bool colliderTrigger, PlayerEnum e,
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

    private void OnTriggerEnter(Collider other)
    {
        //攻击->防御，优先攻击->身体的判断
        if (other.CompareTag(defenceTag))
        {
            VActorPassiveColliderScript passiveScript = other.gameObject.GetComponent<VActorPassiveColliderScript>();
            PlayerEnum enemy = passiveScript.player;
            
            //不同阵营
            if (player == enemy) return;
            
            //停止检测
            ColliderScriptBase.collider.enabled = false;

            //攻击方通知
            EventManager.HitToDefenceEvent.BoradCastEvent(player, currentSkill, enemy, passiveScript.currentSkill);

            //受击方通知
            EventManager.DefenceToHitEvent.BoradCastEvent(enemy,passiveScript.currentSkill,player,currentSkill);
        }
        
        //攻击->身体
        if (other.CompareTag(passiveTag)) 
        {
            VActorPassiveColliderScript passiveScript = other.gameObject.GetComponent<VActorPassiveColliderScript>();
            PlayerEnum enemy = passiveScript.player;
            
            //不同阵营
            if (player == enemy) return;
            
            //停止检测
            ColliderScriptBase.collider.enabled = false;

            //攻击方通知
            EventManager.HitToPassiveEvent.BoradCastEvent(player, currentSkill, enemy, passiveScript.currentSkill);

            //受击方通知
            EventManager.PassiveToHitEvent.BoradCastEvent(enemy,passiveScript.currentSkill,player,currentSkill);
        }
    }
}
