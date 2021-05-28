using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// AI技能单元，单个技能的执行控制
/// </summary>
public class VActorSkillUnit
{
    public UnityEvent skillNormalEnding = new UnityEvent();
    public VActorAISkillData _skillData;
    public bool CanInterrupt;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="skillEvent">技能事件</param>
    /// <param name="skillAction">技能</param>
    /// <param name="physicComponentDic"></param>
    public VActorSkillUnit(VActorSkillEvent skillEvent, VSkillAction skillAction,
        Dictionary<VActorPhysicComponentEnum, bool> physicComponentDic,VActorSkillInfo skillInfo)
    {
        _skillEvent = skillEvent;
        _physicComponentDic = physicComponentDic;
        _skillInfo = skillInfo;

        _skillData = new VActorAISkillData(skillAction);
        CanInterrupt = false;
        if (skillAction.skillProperty.PlayType == SkillAnimPlayType.autoQuit)
            CanInterrupt = true;

        if (skillAction.signalData.SignalEnum == SkillSignalEnum.none)
        {
            DebugHelper.LogWarning("非主动技能，不应添加进AI" + skillAction);
        }

        holdTime = -1;
        //只要不是自动退出的技能
        if (skillAction.skillProperty.PlayType != SkillAnimPlayType.autoQuit)
        {
            //设置属性
            holdTime = Random.Range(_skillData.HoldTime.x, _skillData.HoldTime.y);
        }
    }

    private VActorSkillEvent _skillEvent;
    private Dictionary<VActorPhysicComponentEnum, bool> _physicComponentDic;
    private VActorSkillInfo _skillInfo;

    private float holdTime;

    public VActorSkillUnit SkillPlay()
    {
        //已在当前技能返回
        if (_skillInfo.currentSkill == _skillData._skillAction)
            return null;
        
        _skillEvent.skillPlayTriggerEvent.Invoke(_skillData._skillAction);

        //如果成功释放
        if (_skillInfo.currentSkill == _skillData._skillAction)
        {
            //技能系统可以保证持续技能被打断的情况下触发中断
            if (holdTime < 0f) 
            {
                //不使用协程停止技能，但是可被其他技能打断
                
            }
            //持续技能释放后
            else
            {
                //设置属性
                holdTime = Random.Range(_skillData.HoldTime.x, _skillData.HoldTime.y);
                GameFramework.instance.StartCoroutine(StopSkill(holdTime));
            }
            return this;
        }
        else
        {
            return null;
        }
    }

    private void RemovePhysicComponent()
    {
        //技能被打断再取消物理效果，当前技能发动后只能设置一次物理效果
        // foreach (VActorPhysicComponentEnum e in Enum.GetValues(typeof(VActorPhysicComponentEnum)))
        // {
        //     _physicComponentDic[e] = false;
        // }
    }

    /// <summary>
    /// 选择性的设置，前提是拥有，否则无效
    /// </summary>
    /// <param name="move"></param>
    /// <param name="retreat"></param>
    /// <param name="dash"></param>
    /// <param name="jump"></param>
    public void PhysicComponentSet(bool move, bool retreat, bool dash, bool jump)
    {
        VActorPhysicComponent component = _skillData._skillAction.physicComponent;
        
        if (component.isEnable)
        {
            if (component.isBack)
                _physicComponentDic[VActorPhysicComponentEnum.retreat] = retreat;
            if (component.isDash)
                _physicComponentDic[VActorPhysicComponentEnum.dash] = dash;
            if (component.isJump)
                _physicComponentDic[VActorPhysicComponentEnum.jump] = jump;
            if (component.isMove)
                _physicComponentDic[VActorPhysicComponentEnum.move] = move;
        }
    }

    IEnumerator StopSkill(float time)
    {
        yield return new WaitForSeconds(time);
        
        RemovePhysicComponent();
        _skillEvent.skillEndNormalEvent.Invoke(_skillData._skillAction);
        skillNormalEnding.Invoke();
    }

    /// <summary>
    /// 角色销毁时需关闭在进行的技能协程，提前结束技能
    /// </summary>
    public void MyDestroy()
    {
        RemovePhysicComponent();
        _skillEvent.skillEndNormalEvent.Invoke(_skillData._skillAction);
        GameFramework.instance.StopCoroutine(StopSkill(holdTime));
    }
}