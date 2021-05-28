using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 有限状态机基类，子类负责具体的技能触发逻辑
/// </summary>
public abstract class FSMState
{
    /// <summary>
    /// 状态识别
    /// </summary>
    public FSMStateEnum StateEnum;

    #region 状态生命周期

    /// <summary>
    /// 转换到本状态前的逻辑
    /// </summary>
    public virtual void DoBeforeEntering(){}
    
    /// <summary>
    /// 离开本状态执行的逻辑
    /// </summary>
    public virtual void DoBeforeLeaving(){}

    /// <summary>
    /// 处在状态中的逻辑
    /// </summary>
    /// <param name="go"></param>
    public virtual void Act(VActorAISkillClassify classify, VActorSkillInfo skillInfo, VActorAISkillEmitLogic logic)
    {
        if (logic.ActorSkillUnit==null || logic.ActorSkillUnit.CanInterrupt)
        {
            //尝试释放技能
            SkillSetting(classify,skillInfo,logic);

            //尝试添加回调，技能自然结束置空
            logic.ActorSkillUnit?.skillNormalEnding.AddListener(logic.Reset);
        }
    }

    protected abstract void SkillSetting(VActorAISkillClassify classify, VActorSkillInfo skillInfo,
        VActorAISkillEmitLogic Logic);

    /// <summary>
    /// 退出状态的逻辑
    /// </summary>
    /// <param name="go"></param>
    public virtual void Quit(VActorAISkillClassify classify, VActorAISkillEmitLogic logic)
    {
        //不为空直接结束当前技能
        logic.ActorSkillUnit?.skillNormalEnding.RemoveListener(logic.Reset);
        logic.ActorSkillUnit?.MyDestroy();
        logic.ActorSkillUnit = null;
    }

    #endregion
}

/// <summary>
/// AI状态枚举
/// </summary>
public enum FSMStateEnum
{
    attacked,
    nearEnemy,
    farEnemy,
    hitInTarget,
    inAir,
}