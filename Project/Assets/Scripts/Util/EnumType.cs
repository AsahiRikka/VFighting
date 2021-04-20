using Sirenix.OdinInspector;


/// <summary>
/// 阵营分类
/// </summary>
public enum CampTypeEnum
{
    red,
    green,
}

/// <summary>
/// 玩家，目前只能存在两名玩家，可为电脑
/// </summary>
public enum PlayerEnum
{
    player_1,
    player_2,
}

/// <summary>
/// 玩家类型
/// </summary>
public enum PlayerTypeEnum
{
    immortal,
    computer,
}

/// <summary>
/// 下划线后代表第1，2位玩家的输入设置
/// </summary>
public enum MyKeyCode
{
    leftArrow_1,
    rightArrow_1,
    upArrow_1,
    downArrow_1,
    attack1_1,
    attack2_1,
    dodge_1,
    skill1_1,
    skill2_1,
    skill3_1,
    
    leftArrow_2,
    rightArrow_2,
    upArrow_2,
    downArrow_2,
    attack1_2,
    attack2_2,
    dodge_2,
    skill1_2,
    skill2_2,
    skill3_2,
}

/// <summary>
/// 操作难度
/// </summary>
public enum DifficultOfOperationEnum
{
    simple,
    medium,
    hard,
}

/// <summary>
/// tag枚举
/// </summary>
[System.Flags]
public enum TagEnum
{
    untagged,
    ground,
    wall,
    player1,
    player2,
    hitCollider,
    passiveCollider,
    defenceCollider,
}