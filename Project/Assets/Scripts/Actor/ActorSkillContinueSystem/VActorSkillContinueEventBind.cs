using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 技能连携的帧绑定
/// </summary>
public class VActorSkillContinueEventBind:VSkillEventBase
{
    private VActorInfo _actorInfo;
    private VActorSkillContinueInfo _continueInfo;

    public VActorSkillContinueEventBind(VSkillActions skillActions,VActorInfo actorInfo,
        VActorAnimationClipEventBind bind,VActorEvent actorEvent) : base(actorEvent)
    {
        _actorInfo = actorInfo;
        _continueInfo = actorInfo.skillContinueInfo;
        //
        // //添加技能事件响应
        // BindAdding(skillActions.defaultSkillActions,bind);
        // BindAdding(skillActions.beAttackSkillAction,bind);
        //
        // foreach (VSkillAction skill in skillActions.actorSkillActions)
        // {
        //     BindAdding(skill,bind);
        // }
    }

    // private void BindAdding(VSkillAction skill, VActorAnimationClipEventBind bind)
    // {
    //     foreach (var myCon in skill.preConditionData.skillContinues)
    //     {
    //         bind.AddEvent(skill,AddConEvent,(int)myCon.frame.x);
    //         bind.AddEvent(skill,RemoveConEvent,(int)myCon.frame.y);
    //     }
    // }

    protected override void SkillEndEvent(VSkillAction currentSkill, VSkillAction nextSkill)
    {
        base.SkillEndEvent(currentSkill,nextSkill);
        
    }
}
