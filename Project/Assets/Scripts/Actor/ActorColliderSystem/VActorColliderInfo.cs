using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储角色自身的活动碰撞盒
/// </summary>
public class VActorColliderInfo
{
    public Dictionary<VSkillAction, List<VActorHitColliderScript>> hitBoxes;

    public Dictionary<VSkillAction, VActorPassiveColliderScript> passiveBoxes;

    public Dictionary<VSkillAction, VActorDefenceColliderScript> defenceBoxes;

    public VActorColliderInfo(VSkillActions skillActions)
    {
        hitBoxes = new Dictionary<VSkillAction, List<VActorHitColliderScript>>();
        passiveBoxes = new Dictionary<VSkillAction, VActorPassiveColliderScript>();
        defenceBoxes = new Dictionary<VSkillAction, VActorDefenceColliderScript>();
    }
}

public enum ColliderBoxTypeEnum
{
    hit,
    passive,
    defence,
}
