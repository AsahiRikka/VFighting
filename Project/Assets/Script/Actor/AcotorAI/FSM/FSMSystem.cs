using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// FSM状态机管理，负责状态切换控制
/// </summary>
public class FSMSystem
{
    private Dictionary<FSMStateEnum, FSMState> _fsmStates = new Dictionary<FSMStateEnum, FSMState>();

    private FSMState _currentState;
    private FSMStateEnum _currentStateEnum;

    private float actorFar = 5;
    private const float FarDistance = 1;

    private VActorAISkillEmitLogic Logic=new VActorAISkillEmitLogic();
    private VActorSkillUnit _skillUnit;
    
    public FSMSystem(VActorSkillInfo skillInfo,VSkillActions skillActions,VActorAISkillClassify classify)
    {
        _skillInfo = skillInfo;
        _skillActions = skillActions;
        _classify = classify;
        
        _stateDic.Add(FSMStateEnum.attacked,new FSMState_attacked());
        _stateDic.Add(FSMStateEnum.farEnemy,new FSMState_farEnemy());
        _stateDic.Add(FSMStateEnum.hitInTarget, new FSMState_hitInTarget());
        _stateDic.Add(FSMStateEnum.inAir,new FSMState_inAir());
        _stateDic.Add(FSMStateEnum.nearEnemy,new FSMState_nearEnemy());

        //初始默认farEnemy
        _currentState = _stateDic[FSMStateEnum.farEnemy];
        _currentStateEnum = FSMStateEnum.farEnemy;
        
        EventManager.ActorFarEvent.Subscribe(ActorFarEvent);
    }
    
    private VActorSkillInfo _skillInfo;
    private VSkillActions _skillActions;
    private VActorAISkillClassify _classify;

    private Dictionary<FSMStateEnum, FSMState> _stateDic=new Dictionary<FSMStateEnum, FSMState>();

    public void MyUpdate()
    {
        //降低刷新频率
        if (Time.frameCount % 10 == 0)
        {
            StateTrans(_skillInfo,_skillActions);
            _currentState.Act(_classify,_skillInfo,Logic);
        }
    }

    private void StateTrans(VActorSkillInfo skillInfo,VSkillActions skillActions)
    {
        if ((skillInfo.currentSkill == skillActions.specialSkillDic[ActorStateTypeEnum.attacked] ||
             skillInfo.currentSkill == skillActions.specialSkillDic[ActorStateTypeEnum.attackedFall] ||
             skillInfo.currentSkill == skillActions.specialSkillDic[ActorStateTypeEnum.attackedInAir]) &&
            _currentStateEnum != FSMStateEnum.attacked) 
        {
            StateTrans(FSMStateEnum.attacked);
        }else if (skillInfo.currentSkill == skillActions.specialSkillDic[ActorStateTypeEnum.inAir] &&
                  _currentStateEnum != FSMStateEnum.inAir) 
        {
            StateTrans(FSMStateEnum.inAir);
        }
        //距离判断优先级最低
        else if (Mathf.Abs(actorFar) > FarDistance && _currentStateEnum != FSMStateEnum.farEnemy) 
        {
            StateTrans(FSMStateEnum.farEnemy);
        }
        else if (Mathf.Abs(actorFar) <= FarDistance && _currentStateEnum != FSMStateEnum.nearEnemy)  
        {
            StateTrans(FSMStateEnum.nearEnemy);
        }
    }

    private void StateTrans(FSMStateEnum e)
    {
        _currentState.Quit(_classify,Logic);
        _currentState = _stateDic[e];
        _currentStateEnum = e;
    }

    private void ActorFarEvent(float far)
    {
        actorFar = far;
    }
    
    public void MyDestroy()
    {
        foreach (var fsmState in _stateDic)
        {
            fsmState.Value.Quit(_classify,Logic);
        }

        EventManager.ActorFarEvent.RemoveEventHandler(ActorFarEvent);
    }
}