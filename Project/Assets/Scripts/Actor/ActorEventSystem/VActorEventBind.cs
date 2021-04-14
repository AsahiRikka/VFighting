using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动画帧事件绑定
/// </summary>
public class VActorEventBind
{
    //动画帧绑定
    private VActorAnimationClipEventBind _animationClipEventBind;
    
    //调用动画帧绑定，将碰撞，动画硬直，技能事件绑定进动画
    private VActorAnimationEvent _animationEvent;
    private VActorSkillContinueEvent _continueEvent;
    private VActorPhysicEvent _physicEvent;

    public VActorEventBind(VSkillActions skillActions, VActorReferanceGameObject referance, VActorInfo actorInfo,
        VActorEvent actorEvent, VActorController actorController)
    {
        
        
        //动画帧绑定功能
        _animationClipEventBind=new VActorAnimationClipEventBind(skillActions,referance.actorAnimator);
        
        //各部分绑定
        _animationEvent=new VActorAnimationEvent(skillActions,actorInfo,_animationClipEventBind,actorEvent);
        _continueEvent = new VActorSkillContinueEvent(skillActions,actorInfo,
            _animationClipEventBind,actorEvent);
        _physicEvent=new VActorPhysicEvent(_animationClipEventBind,skillActions,actorController,actorInfo,actorEvent);
        
        //最终绑定，在各部分添加事件后
        _animationClipEventBind.Bind();
    }
}
