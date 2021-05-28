using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 动画帧事件绑定
/// </summary>
public class VActorAnimationClipEventBind
{
    /// <summary>
    /// 技能对应的帧事件添加
    /// </summary>
    private Dictionary<VSkillAction, VSkillActionAniClipBind> clipEventDic;

    private Animator _animator;
    
    public VActorAnimationClipEventBind(VSkillActions skillActions,Animator animator)
    {
        _animator = animator;
        clipEventDic=new Dictionary<VSkillAction, VSkillActionAniClipBind>();
    }

    /// <summary>
    /// 添加动画帧事件
    /// </summary>
    /// <param name="skill">要添加的技能</param>
    /// <param name="func">添加的方法</param>
    /// <param name="clip">事件触发帧数</param>
    public void AddEvent(VSkillAction skill, UnityAction func, int clip)
    {
        if (clipEventDic.ContainsKey(skill))
        {
            clipEventDic[skill].AddEventByClip(func,clip);
        }
        else
        {
            clipEventDic.Add(skill,new VSkillActionAniClipBind(skill,_animator));
            clipEventDic[skill].AddEventByClip(func,clip);
        }
    }

    /// <summary>
    /// 最终绑定，在添加事件后
    /// </summary>
    public void Bind()
    {
        foreach (var clipEvent in clipEventDic)
        {
            clipEvent.Value.Bind(_animator);
        }
        
        //刷新
        _animator.Rebind();
    }
}

public class MyAnimationClipEvent : UnityEvent
{
    
}