using System.Collections;
using System.Collections.Generic;
using MackySoft.Pooling;
using UnityEngine;
/// <summary>
/// 单个角色的碰撞控制器，负责触发碰撞并通知总控，发生触发删除碰撞
/// </summary>
public class VActorColliderController
{
    public VActorColliderController(VActorInfo actorInfo)
    {
        _actorInfo = actorInfo;
        _animationInfo = actorInfo.animationInfo;
    }
    
    private VActorInfo _actorInfo;
    private VActorAnimationInfo _animationInfo;

    #region outDate

    #endregion
    
    //碰撞器列表
    private List<VActorHitColliderScript> hitList=new List<VActorHitColliderScript>();
    private List<VActorPassiveColliderScript> passiveList=new List<VActorPassiveColliderScript>();
    private List<VActorDefenceColliderScript> defenceList = new List<VActorDefenceColliderScript>();

    public void SkillStartEvent(VSkillAction lastSkill, VSkillAction currentSkill)
    {
        hitList.Clear();
        passiveList.Clear();
        defenceList.Clear();

        foreach (var hitColliderScript in _actorInfo.colliderInfo.hitBoxes[currentSkill])
        {
            if (hitColliderScript.startFrame == -1)
            {
                hitColliderScript.ColliderScriptBase.collider.enabled = true;
                hitList.Add(hitColliderScript);
            }
        }

        foreach (var passiveColliderScript in _actorInfo.colliderInfo.passiveBoxes[currentSkill])
        {
            if (passiveColliderScript.startFrame == -1)
            {
                passiveColliderScript.ColliderScriptBase.collider.enabled = true;
                passiveList.Add(passiveColliderScript);
            }
        }

        foreach (var defenceColliderScript in _actorInfo.colliderInfo.defenceBoxes[currentSkill])
        {
            if (defenceColliderScript.startFrame == -1)
            {
                defenceColliderScript.ColliderScriptBase.collider.enabled = true;
                defenceList.Add(defenceColliderScript);
            }
        }
    }

    public void SkillUpdateEvent(VSkillAction skillAction)
    {
        foreach (var hitColliderScript in _actorInfo.colliderInfo.hitBoxes[skillAction])
        {
            if (hitColliderScript.startFrame <= _animationInfo.currentFrame && hitColliderScript.startFrame != -1 &&
                !hitList.Contains(hitColliderScript)) 
            {
                hitColliderScript.ColliderScriptBase.collider.enabled = true;
                hitList.Add(hitColliderScript);
            }
            else if (hitColliderScript.endFrame <= _animationInfo.currentFrame && hitColliderScript.endFrame != -1) 
            {
                hitColliderScript.ColliderScriptBase.collider.enabled = false;
            }
        }

        foreach (var passiveColliderScript in _actorInfo.colliderInfo.passiveBoxes[skillAction])
        {
            if (passiveColliderScript.startFrame <= _animationInfo.currentFrame &&
                passiveColliderScript.startFrame != -1 && !passiveList.Contains(passiveColliderScript))  
            {
                passiveColliderScript.ColliderScriptBase.collider.enabled = true;
                passiveList.Add(passiveColliderScript);
            }
            else if (passiveColliderScript.endFrame <= _animationInfo.currentFrame &&
                     passiveColliderScript.endFrame != -1)  
            {
                passiveColliderScript.ColliderScriptBase.collider.enabled = false;
            }
        }
        
        foreach (var defenceColliderScript in _actorInfo.colliderInfo.defenceBoxes[skillAction])
        {
            if (defenceColliderScript.startFrame <= _animationInfo.currentFrame &&
                defenceColliderScript.startFrame != -1 && !defenceList.Contains(defenceColliderScript))  
            {
                defenceColliderScript.ColliderScriptBase.collider.enabled = true;
                defenceList.Add(defenceColliderScript);
            }
            else if (defenceColliderScript.endFrame <= _animationInfo.currentFrame &&
                     defenceColliderScript.endFrame != -1)  
            {
                defenceColliderScript.ColliderScriptBase.collider.enabled = false;
            }
        }
    }

    public void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        foreach (var hitColliderScript in _actorInfo.colliderInfo.hitBoxes[currentSkill])
        {
            hitColliderScript.ColliderScriptBase.collider.enabled = false;
        }

        foreach (var passiveColliderScript in _actorInfo.colliderInfo.passiveBoxes[currentSkill])
        {
            passiveColliderScript.ColliderScriptBase.collider.enabled = false;
        }
        
        foreach (var defenceColliderScript in _actorInfo.colliderInfo.defenceBoxes[currentSkill])
        {
            defenceColliderScript.ColliderScriptBase.collider.enabled = false;
        }
    }
}
