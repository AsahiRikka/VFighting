using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制器和数据分离
/// </summary>
[Serializable]
public class VActorInfo
{
    public VActorPhysicInfo physicInfo;

    public VActorAnimationInfo animationInfo;

    public VActorBuffInfo buffInfo;

    public VActorSkillInfo skillInfo;

    public VActorSkillContinueInfo skillContinueInfo;

    public VActorColliderInfo colliderInfo;

    public VActorInfo(VActorChangeProperty property,VSkillActions skillActions,VActorReferanceGameObject referanceGameObject)
    {
        physicInfo=new VActorPhysicInfo();
        animationInfo=new VActorAnimationInfo(referanceGameObject);
        buffInfo=new VActorBuffInfo();
        skillInfo=new VActorSkillInfo(property);
        skillContinueInfo=new VActorSkillContinueInfo(skillActions);
        colliderInfo=new VActorColliderInfo(skillActions,referanceGameObject,property);
    }
}
