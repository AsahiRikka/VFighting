using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色状态管理
/// </summary>
[Serializable]
public class VActorState
{
    /// <summary>
    /// 角色当前状态
    /// </summary>
    public ActorStateTypeEnum actorState = ActorStateTypeEnum.idle;

    /// <summary>
    /// 是否可以释放技能
    /// </summary>
    public bool canSkill = true;

    /// <summary>
    /// 是否可以移动
    /// </summary>
    public bool canMove = true;

    /// <summary>
    /// 是否可以冲刺
    /// </summary>
    public bool canDash = true;
    
    /// <summary>
    /// 是否可以跳跃
    /// </summary>
    public bool canJump = true;

    /// <summary>
    /// 角色朝向，角度值
    /// </summary>
    public Vector3 actorDir;

    /// <summary>
    /// 跟踪目标的朝向，角度值
    /// </summary>
    public Vector3 focusDir;

    public VActorState(PlayerEnum e)
    {
        //初始朝向设置，游戏中会变化
        if (e == PlayerEnum.player_1)
        {
            actorDir=Vector3.zero;
            focusDir=new Vector3(0,180,0);
        }else if (e == PlayerEnum.player_2)
        {
            actorDir=new Vector3(0,180,0);
            focusDir=Vector3.zero;
        }
    }
}

public enum ActorStateTypeEnum
{
    idle,
    move,
    dash,
    jump,
    crouch,
    attacked,
    skill,
}
