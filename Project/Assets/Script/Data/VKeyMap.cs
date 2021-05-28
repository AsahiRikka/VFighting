using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 忽略玩家位置，玩家的输入信号
/// </summary>
public enum PlayerKeyCode
{
    leftArrow,
    rightArrow,
    upArrow,
    downArrow,
    attack1,
    attack2,
    dodge,
    skill1,
    skill2,
    skill3,
    
    /// <summary>
    /// 面向敌人的方向键
    /// </summary>
    anamyArrow,
    
    /// <summary>
    /// 背向敌人方向键
    /// </summary>
    invertAnamyArrow,
}