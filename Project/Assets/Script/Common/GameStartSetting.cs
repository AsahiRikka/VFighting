using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏开始时的参数设置
/// </summary>
public class GameStartSetting
{
    /// <summary>
    /// 游戏场景
    /// </summary>
    public string SceneID;

    /// <summary>
    /// 游戏时间设置，-1代表无限
    /// </summary>
    public GameTimeTypeEnum gameTime = GameTimeTypeEnum.infinite;

    /// <summary>
    /// 生命值倍率
    /// </summary>
    public HeathScaleTypeEnum healthScale = HeathScaleTypeEnum.one;

    /// <summary>
    /// 玩家1角色
    /// </summary>
    public ActorEnum Player_1;

    /// <summary>
    /// 玩家2角色
    /// </summary>
    public ActorEnum Player_2;
}

public enum GameTimeTypeEnum
{
    infinite,
    min_1,
    min_2,
}

public enum HeathScaleTypeEnum
{
    one,
    two,
    three,
}