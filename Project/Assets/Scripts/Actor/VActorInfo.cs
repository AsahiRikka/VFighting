using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制器和数据分离
/// </summary>
public class VActorInfo
{
    public VActorPhysicInfo physicInfo;

    public VActorAnimationInfo animationInfo;

    public VActorBuffInfo buffInfo;

    public VActorSkillInfo skillInfo;

    public VActorSkillContinueInfo skillContinueInfo;

    public VActorInfo(VActorChangeProperty property,VSkillActions skillActions)
    {
        physicInfo=new VActorPhysicInfo();
        animationInfo=new VActorAnimationInfo();
        buffInfo=new VActorBuffInfo();
        skillInfo=new VActorSkillInfo(property);
        skillContinueInfo=new VActorSkillContinueInfo(skillActions);
    }
}
