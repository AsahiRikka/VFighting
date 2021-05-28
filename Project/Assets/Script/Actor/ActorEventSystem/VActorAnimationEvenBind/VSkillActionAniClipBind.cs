using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using zFrame.Event;

/// <summary>
/// 一个技能的绑定
/// </summary>
public class VSkillActionAniClipBind
{
    private Dictionary<int, MyAnimationClipEvent> skillActionAniClipEvents;
    private int maxClip;

    private VSkillAction _skillAction;
    
    public VSkillActionAniClipBind(VSkillAction skillAction,Animator animator)
    {
        skillActionAniClipEvents=new Dictionary<int, MyAnimationClipEvent>();

        _skillAction = skillAction;
        
        //动画片段长度
        float animLength = skillAction.motion.animationClip.length;
        //获取动画片段帧频
        float frameRate = skillAction.motion.animationClip.frameRate;
        //计算动画片段总帧数
        maxClip = (int) ((int)animLength / (1 / frameRate));
    }
    
    /// <summary>
    /// 外部添加事件进指定帧
    /// </summary>
    /// <param name="func"></param>
    /// <param name="clip"></param>
    public void AddEventByClip(UnityAction func,int clip)
    {
        if (clip < maxClip)
        {
            //字典不存在该帧的事件，先添加
            if (!skillActionAniClipEvents.ContainsKey(clip))
            {
                skillActionAniClipEvents.Add(clip,new MyAnimationClipEvent());
            }
            
            //添加事件
            skillActionAniClipEvents[clip].AddListener(func);
        }
    }

    /// <summary>
    /// 绑定
    /// </summary>
    /// <param name="animator"></param>
    public void Bind(Animator animator)
    {
        //
        //遍历字典，添加事件
        //
        foreach (var skill in skillActionAniClipEvents)
        {
            animator.SetTarget(_skillAction.motion.animationClip.name, skill.Key).OnProcess(v =>
            {
                skill.Value.Invoke();
            });
        }
    }
}
