using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 角色身上的事件
/// </summary>
public class VActorEvent
{
    //动画帧绑定
    private VActorAnimationClipEventBind _animationClipEventBind;
    
    //调用动画帧绑定，将碰撞，动画硬直，技能事件绑定进动画
    public VActorAnimationEvent AnimationEvent;
    
    //技能事件
    public readonly VActorSkillEvent SkillEvent;


    public VActorEvent(VSkillActions skillActions, VActorReferanceGameObject referance, VActorInfo actorInfo)
    {
        SkillEvent=new VActorSkillEvent();
        
        //动画帧绑定功能
        _animationClipEventBind=new VActorAnimationClipEventBind(skillActions,referance.actorAnimator);
        
        //各部分绑定
        AnimationEvent=new VActorAnimationEvent(skillActions,actorInfo,_animationClipEventBind,this);
        
        //最终绑定，在各部分添加事件后
        _animationClipEventBind.Bind();
    }
}

