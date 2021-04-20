using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储角色自身的活动碰撞盒
/// </summary>
public class VActorColliderInfo
{
    public Dictionary<VSkillAction, List<VActorHitColliderScript>> hitBoxes;

    public Dictionary<VSkillAction, List<VActorPassiveColliderScript>> passiveBoxes;

    public Dictionary<VSkillAction, List<VActorDefenceColliderScript>> defenceBoxes;

    public VActorColliderInfo(VSkillActions skillActions,VActorReferanceGameObject referance,VActorChangeProperty property)
    {
        hitBoxes = new Dictionary<VSkillAction, List<VActorHitColliderScript>>();
        passiveBoxes = new Dictionary<VSkillAction, List<VActorPassiveColliderScript>>();
        defenceBoxes = new Dictionary<VSkillAction, List<VActorDefenceColliderScript>>();

        _referance = referance;
        _property = property;

        //遍历技能初始化所有碰撞器并设置碰撞器 enable=false
        VSkillAction makeAction = skillActions.defaultSkillActions;
        HitColliderInit(makeAction);
        PassiveColliderInit(makeAction);

        makeAction = skillActions.beAttackSkillAction;
        HitColliderInit(makeAction);
        PassiveColliderInit(makeAction);

        foreach (var action in skillActions.actorSkillActions)
        {
            HitColliderInit(action);
            PassiveColliderInit(action);
        }
    }

    private VActorReferanceGameObject _referance;
    private VActorChangeProperty _property;

    private void HitColliderInit(VSkillAction skillAction)
    {
        List<VActorHitColliderScript> hitColliderScripts=new List<VActorHitColliderScript>();

        foreach (var hitBox in skillAction.motion.hitBoxes)
        {
            VActorHitColliderScript hitColliderScript = _referance.GetHitColliderScript();
            Transform transform;
            (transform = hitColliderScript.transform).SetParent(_referance.transform);
            transform.localPosition=Vector3.zero;
            transform.localRotation=Quaternion.Euler(Vector3.zero);

            hitColliderScript.ColliderScriptBase.collider.enabled = false;
            
            if (hitBox.segmentMotion.type == VSegmentMotionType.allskill)
            {
                hitColliderScript.startFrame = -1;
                hitColliderScript.endFrame = -1;
            }else if (hitBox.segmentMotion.type == VSegmentMotionType.keyFrame)
            {
                hitColliderScript.startFrame = hitBox.segmentMotion.startFrame;
                hitColliderScript.endFrame = hitBox.segmentMotion.endFrame;
            }

            hitColliderScript.player = _property.playerEnum;
            hitColliderScript.ColliderScriptBase.collider.center = hitBox.collider.center;
            hitColliderScript.ColliderScriptBase.collider.size = hitBox.collider.size;
            hitColliderScript.ColliderScriptBase.collider.isTrigger = hitBox.collider.trigger;

            hitColliderScript.currentSkill = skillAction;
            
            hitColliderScripts.Add(hitColliderScript);
        }
        
        hitBoxes.Add(skillAction,hitColliderScripts);
    }

    private void PassiveColliderInit(VSkillAction skillAction)
    {
        List<VActorPassiveColliderScript> passiveColliderScripts=new List<VActorPassiveColliderScript>();
        
        foreach (var passiveBox in skillAction.motion.passiveBoxes)
        {
            VActorPassiveColliderScript passiveColliderScript = _referance.GetPassiveColliderScript();

            passiveColliderScript.ColliderScriptBase.collider.enabled = false;
            Transform transform;
            (transform = passiveColliderScript.transform).SetParent(_referance.transform);
            transform.localPosition=Vector3.zero;
            transform.localRotation=Quaternion.Euler(Vector3.zero);

            if (passiveBox.segmentMotion.type == VSegmentMotionType.allskill)
            {
                passiveColliderScript.startFrame = -1;
                passiveColliderScript.endFrame = -1;
            }else if (passiveBox.segmentMotion.type == VSegmentMotionType.keyFrame)
            {
                passiveColliderScript.startFrame = passiveBox.segmentMotion.startFrame;
                passiveColliderScript.endFrame = passiveBox.segmentMotion.endFrame;
            }

            passiveColliderScript.player = _property.playerEnum;
            passiveColliderScript.ColliderScriptBase.collider.center = passiveBox.collider.center;
            passiveColliderScript.ColliderScriptBase.collider.size = passiveBox.collider.size;
            passiveColliderScript.ColliderScriptBase.collider.isTrigger = passiveBox.collider.trigger;

            passiveColliderScript.currentSkill = skillAction;
            
            passiveColliderScripts.Add(passiveColliderScript);
        }
        
        passiveBoxes.Add(skillAction,passiveColliderScripts);
    }
}

public enum ColliderBoxTypeEnum
{
    hit,
    passive,
    defence,
}
