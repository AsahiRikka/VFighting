using System.Collections;
using System.Collections.Generic;
using MackySoft.Pooling;
using UnityEngine;
/// <summary>
/// 单个角色的碰撞控制器，负责触发碰撞并通知总控，发生触发删除碰撞
/// </summary>
public class VActorColliderController:VSkillEventBase
{
    public VActorColliderController(VActorEvent actorEvent,VActorInfo actorInfo,VActorReferanceGameObject referance,VActorChangeProperty property) : base(actorEvent)
    {
        _actor = referance.actor;
        _parent = referance.parent;
        _referance = referance;
        _colliderInfo = actorInfo.colliderInfo;
        _actorInfo = actorInfo;
        _property = property;
    }

    private VActorReferanceGameObject _referance;
    private GameObject _actor;
    private GameObject _parent;
    private VActorColliderInfo _colliderInfo;
    private VActorInfo _actorInfo;
    private VActorChangeProperty _property;

    public void AddBoxes(VSkillAction skillAction,ColliderBoxTypeEnum e)
    {
        if (e == ColliderBoxTypeEnum.hit)
        {
            if(skillAction.motion.hitBoxes.Count==0)
                return;
        }else if (e == ColliderBoxTypeEnum.passive)
        {
            if(skillAction.motion.passiveBoxes.Count==0)
                return;
        }else if (e == ColliderBoxTypeEnum.defence)
        {
            if(skillAction.motion.defenseBoxes.Count==0)
                return;
        }

        switch (e)
        {
            case ColliderBoxTypeEnum.hit:
                List<VActorHitColliderScript> colliderList=new List<VActorHitColliderScript>();
                foreach (var hit in skillAction.motion.hitBoxes)
                {
                    VActorHitBox hitBox = hit;
                    Pool pool = PoolManager.GetPool(_referance.hitBoxPrefab);
                    GameObject hitObj = pool.Get(_parent.transform);
                    hitObj.tag = TagType.GetInstance().tagDictionary[(int) TagEnum.hitCollider];

                    VActorHitColliderScript hitScript = hitObj.GetComponent<VActorHitColliderScript>();

                    hitScript.ColliderScriptBase.collider.center = hitBox.collider.center;
                    hitScript.ColliderScriptBase.collider.size = hitBox.collider.size;
                    hitScript.ColliderScriptBase.collider.isTrigger = hitBox.collider.trigger;

                    hitScript.player = _property.playerEnum;
                    hitScript.currentSkill = skillAction;
                    
                    colliderList.Add(hitScript);
                }
                _colliderInfo.hitBoxes[skillAction] = colliderList;
                break;
            case ColliderBoxTypeEnum.passive:
                VActorPassiveBox passiveBox = skillAction.motion.passiveBoxes[0];
                
                Pool passivePool = PoolManager.GetPool(_referance.passiveBoxPrefab);
                GameObject passiveObj = passivePool.Get(_parent.transform);
                passiveObj.tag = TagType.GetInstance().tagDictionary[(int) TagEnum.passiveCollider];

                VActorPassiveColliderScript passiveScript = passiveObj.GetComponent<VActorPassiveColliderScript>();

                passiveScript.ColliderScriptBase.collider.center = passiveBox.collider.center;
                passiveScript.ColliderScriptBase.collider.size = passiveBox.collider.size;
                passiveScript.ColliderScriptBase.collider.isTrigger = passiveBox.collider.trigger;
                
                passiveScript.player = _property.playerEnum;
                passiveScript.currentSkill = skillAction;

                _colliderInfo.passiveBoxes[skillAction] = passiveScript;
                
                break;
            case ColliderBoxTypeEnum.defence:
                VActorDefenseBox defenceBox = skillAction.motion.defenseBoxes[0];
                
                Pool defencePool = PoolManager.GetPool(_referance.defenceBoxPrefab);
                GameObject defenceObj = defencePool.Get(_parent.transform);
                defenceObj.tag = TagType.GetInstance().tagDictionary[(int) TagEnum.defenceCollider];

                VActorDefenceColliderScript defenceScript = defenceObj.GetComponent<VActorDefenceColliderScript>();

                defenceScript.ColliderScriptBase.collider.center = defenceBox.collider.center;
                defenceScript.ColliderScriptBase.collider.size = defenceBox.collider.size;
                defenceScript.ColliderScriptBase.collider.isTrigger = defenceBox.collider.trigger;
                
                defenceScript.player = _property.playerEnum;
                defenceScript.currentSkill = skillAction;

                _colliderInfo.defenceBoxes[skillAction] = defenceScript;
                
                break;
        }
    }

    public void RemoveBoxes(VSkillAction skillAction, ColliderBoxTypeEnum e)
    {
        switch (e)
        {
            case ColliderBoxTypeEnum.hit:
                if (_colliderInfo.hitBoxes.ContainsKey(skillAction))
                {
                    foreach (var hitBox in _colliderInfo.hitBoxes[skillAction])
                    {
                        if (hitBox)
                        {
                            hitBox.gameObject.SetActive(false);
                        }
                    }
                }
                break;
            case ColliderBoxTypeEnum.passive:
                if (_colliderInfo.passiveBoxes.ContainsKey(skillAction) && _colliderInfo.passiveBoxes[skillAction]) 
                {
                    _colliderInfo.passiveBoxes[skillAction].gameObject.SetActive(false);
                }
                break;
            case ColliderBoxTypeEnum.defence:
                if (_colliderInfo.defenceBoxes.ContainsKey(skillAction) && _colliderInfo.defenceBoxes[skillAction]) 
                {
                    _colliderInfo.passiveBoxes[skillAction].gameObject.SetActive(false);
                }
                break;
        }
    }
}
