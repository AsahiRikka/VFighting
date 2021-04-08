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
    /// 技能结束被触发事件
    /// </summary>
    public MySkillInputEvent skillEndTriggerEvent=new MySkillInputEvent();

    /// <summary>
    /// 播放技能事件，符合条件的技能被播放
    /// </summary>
    public MySkillInputEvent skillStartEvent = new MySkillInputEvent();
    
    /// <summary>
    /// 技能循环
    /// </summary>
    public MySkillInputEvent skillUpdateVent=new MySkillInputEvent();

    /// <summary>
    /// 技能结束事件，确认技能结束
    /// </summary>
    public MySkillInputEvent skillEndEvent = new MySkillInputEvent();
}

public class MySkillInputEvent : UnityEvent<VSkillAction>
{
    
}