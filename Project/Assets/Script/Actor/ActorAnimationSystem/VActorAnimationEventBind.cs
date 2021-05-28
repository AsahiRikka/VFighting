using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using zFrame.Event;

/// <summary>
/// 负责调用动画帧绑定功能，将与动画有关的帧事件进行绑定
/// </summary>
public class VActorAnimationEventBind
{
    public VActorAnimationEventBind(VSkillActions skillActions, VActorInfo actorInfo, VActorAnimationClipEventBind bind)
    {
        _skillActions = skillActions;
        _actorInfo = actorInfo;

        //动画硬直
        // BindAdding(skillActions.defaultSkillActions,bind);
        // BindAdding(skillActions.beAttackSkillAction,bind);
        // foreach (VSkillAction skill in _skillActions.actorSkillActions)
        // {
        //     BindAdding(skill,bind);
        // }
        
        
    }

    // private void BindAdding(VSkillAction skill, VActorAnimationClipEventBind bind)
    // {
    //     foreach (var straight in skill.motion.animationStraights)
    //     {
    //         bind.AddEvent(skill,SetAnimatorFalseSkill,straight.startFrame);
    //         bind.AddEvent(skill,SetAnimatorCanSkill,straight.endFrame);
    //     }
    // }

    private VSkillActions _skillActions;
    private VActorInfo _actorInfo;
    
    // private void SetAnimatorCanSkill()
    // {
    //     _actorInfo.animationInfo.canSkill = true;
    // }
    //
    // private void SetAnimatorFalseSkill()
    // {
    //     _actorInfo.animationInfo.canSkill = false;
    // }
}
