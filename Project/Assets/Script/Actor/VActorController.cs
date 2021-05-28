using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色控制器中心
/// </summary>
public class VActorController
{
    /// <summary>
    /// 角色状态控制
    /// </summary>
    public VActorStateController stateController;
    
    /// <summary>
    /// 物理效果控制
    /// </summary>
    public VActorPhysicController physicController;

    /// <summary>
    /// 动画控制
    /// </summary>
    public VActorAnimationController animationController;
    
    /// <summary>
    /// 技能控制
    /// </summary>
    public VActorSkillController skillController;
    
    /// <summary>
    /// 技能连携控制
    /// </summary>
    public VActorSkillContinueController skillContinueController;

    /// <summary>
    /// 碰撞控制
    /// </summary>
    public VActorColliderController colliderController;

    /// <summary>
    /// 角色声音控制
    /// </summary>
    public VActorSoundController soundController;
    
    /// <summary>
    /// 特效控制器
    /// </summary>
    public VActorFXController FXController;

    /// <summary>
    /// 伤害控制器
    /// </summary>
    public VActorDamageController DamageController;

    /// <summary>
    /// 技能释放控制
    /// </summary>
    public VActorSkillSignal skillSignal;

    public VActorController(VActorChangeProperty property, VActorState state, VSkillActions skillActions,
        VActorInfo actorInfo, VActorEvent actorEvent,VActorReferanceGameObject referance)
    {
        //输入信号初始化
        skillSignal = new VActorSkillSignal(property, skillActions, actorEvent,actorInfo,state);
        
        //控制器初始化
        stateController = new VActorStateController(actorEvent, state, actorInfo, property, referance,skillActions);
        physicController = new VActorPhysicController(property, state, actorEvent, skillSignal, referance, actorInfo);
        animationController = new VActorAnimationController(actorEvent, actorInfo, referance, state);
        skillContinueController = new VActorSkillContinueController(actorInfo);
        colliderController = new VActorColliderController(actorInfo);
        FXController=new VActorFXController(actorEvent,property,state,referance);
        soundController=new VActorSoundController(actorEvent,actorInfo);
        DamageController = new VActorDamageController(actorEvent, stateController);
        
        skillController = new VActorSkillController(skillActions, actorEvent, actorInfo, state,property,referance,this);

        _actorInfo = actorInfo;
        _actorEvent = actorEvent;
    }

    public void ControllerInit()
    {
        skillSignal.Init();
    }


    private VActorInfo _actorInfo;
    private VActorEvent _actorEvent;
    public void ControllerUpdate()
    {
        skillSignal.Update();

        skillController.Update();
        physicController.Update();
    }

    public void ControllerFixUpdate()
    {
        skillController.FixUpdate();
        physicController.FixUpdate();
    }

    public void ControllerExit()
    {
        skillSignal.Destroy();
        skillController.Destroy();
    }
}
