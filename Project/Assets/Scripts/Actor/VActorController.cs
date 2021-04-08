using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色控制器中心
/// </summary>
public class VActorController
{
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
    /// 技能释放控制
    /// </summary>
    public VActorSkillSignal skillSignal;

    public VActorController(VActorChangeProperty property, VActorState state, VSkillActions skillActions,
        VActorInfo actorInfo, VActorEvent actorEvent)
    {
        //控制器初始化
        physicController = new VActorPhysicController(property, state,actorEvent);
        animationController=new VActorAnimationController(actorEvent);
        skillController = new VActorSkillController(skillActions, actorEvent, actorInfo, state);
        skillContinueController = new VActorSkillContinueController(actorEvent);
        
        //输入信号初始化
        skillSignal = new VActorSkillSignal(property, skillActions, actorEvent);

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
        
        //技能update
        if (_actorInfo.skillInfo.currentSkill is null)
        {
            _actorEvent.SkillEvent.skillUpdateVent.Invoke(_actorInfo.skillInfo.currentSkill);
        }
    }

    public void COntrollerExit()
    {
        skillSignal.Destroy();
    }
}
