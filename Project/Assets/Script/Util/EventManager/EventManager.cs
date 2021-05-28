using UnityEngine;
using System.Collections;

/// <summary>
/// 描述：事件系统，事件定义
/// </summary>

public class EventManager
{
    /// <summary>
    /// 方向转换事件，参数1：左边角色；参数2：右边角色
    /// </summary>
    public static FEvent<PlayerEnum,PlayerEnum> ActorDirTransEvent=new FEvent<PlayerEnum, PlayerEnum>();
    
    /// <summary>
    /// 检测敌人距离，float = player2.x-player1.x
    /// </summary>
    public static FEvent<float> ActorFarEvent=new FEvent<float>();
    
    /// <summary>
    /// 输入事件，参数1：player1能否输入，参数2：player2能否输入
    /// </summary>
    public static FEvent<bool,bool> ActorInputEvent=new FEvent<bool, bool>();

    /// <summary>
    /// 摄像机事件
    /// </summary>
    public static FEvent<PlayerEnum, Transform> ActorCameraFollow=new FEvent<PlayerEnum, Transform>();
    
    /// <summary>
    /// 胜利事件
    /// </summary>
    public static FEvent<PlayerEnum> WinnerEvent=new FEvent<PlayerEnum>();
    
    /// <summary>
    /// 场景进入事件
    /// </summary>
    public static FEvent SceneEnterCompleted=new FEvent();

    //对于碰撞事件，只有不同阵营碰撞才触发事件。

    #region 碰撞

    /// <summary>
    /// 攻击->身体
    /// </summary>
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> HitToPassiveEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    /// <summary>
    /// 身体->攻击
    /// </summary>
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> PassiveToHitEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> HitToDefenceEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();
    
    public static FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction> DefenceToHitEvent =
        new FEvent<PlayerEnum, VSkillAction, PlayerEnum, VSkillAction>();

    #endregion


    #region UI
    
    //主界面的UI
    public static FEvent<MainStateParam> UIMainStageEvent=new FEvent<MainStateParam>();

    //保存的UI
    public static FEvent<SettingScreenPram> UISettingEvent=new FEvent<SettingScreenPram>();
    
    //角色选择的UI
    public static FEvent<ActorSelectScreenParam> UIActorSelectEvent=new FEvent<ActorSelectScreenParam>();
    
    //进入游戏前的UI
    public static FEvent<GameEnterSettingParams> UIGameEnterSettingEvent=new FEvent<GameEnterSettingParams>();
    
    //加载UI
    public static FEvent<float> UIGameLoadingEvent=new FEvent<float>();
    
    //生命值UI
    public static FEvent<PlayerEnum,float> GamingHeathEvent=new FEvent<PlayerEnum, float>();
    
    #endregion
}
