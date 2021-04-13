using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VActorSkillEvent
{
    /// <summary>
    /// 技能输入被触发的事件
    /// </summary>
    public MySkillInputEvent skillPlayTriggerEvent = new MySkillInputEvent();

    /// <summary>
    /// 播放技能事件，符合条件的技能被播放
    /// </summary>
    public MySkillInputEventForTwo skillStartEvent = new MySkillInputEventForTwo();
    
    /// <summary>
    /// 技能循环
    /// </summary>
    public MySkillInputEvent skillUpdateEvent=new MySkillInputEvent();

    /// <summary>
    /// fixupdate
    /// </summary>
    public MySkillInputEvent skillFixUpdateEvent=new MySkillInputEvent();
    
    /// <summary>
    /// 技能结束事件，自然结束的情况下
    /// </summary>
    public MySkillInputEvent skillEndNormalEvent = new MySkillInputEvent();
    
    /// <summary>
    /// 技能结束事件，确认技能结束
    /// </summary>
    public MySkillInputEventForTwo skillEndEvent = new MySkillInputEventForTwo();
}

public class MySkillInputEvent : UnityEvent<VSkillAction>
{
    
}

public class MySkillInputEventForTwo : UnityEvent<VSkillAction, VSkillAction>
{
    
}