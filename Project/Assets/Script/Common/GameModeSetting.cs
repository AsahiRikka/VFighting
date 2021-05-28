using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏模式
/// </summary>
public class GameModeSetting
{
    /// <summary>
    /// 游戏模式
    /// </summary>
    public GameModeTypeEnum GameMode = GameModeTypeEnum.playerToPlayer;
}

public enum GameModeTypeEnum
{
    playerToPlayer,
    playerToComputer,
}