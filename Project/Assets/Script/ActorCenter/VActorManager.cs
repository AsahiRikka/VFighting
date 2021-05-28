using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色间的交互管理
/// </summary>
public class VActorManager
{
    public VSkillManager SkillManager;

    public VActorDirManager ActorDirManager;

    public VActorColliderManager ActorColliderManager;

    public VActorManager()
    {

    }

    private bool isInit = false;
    
    public void VActorManagerInit(VActorBase player1,VActorBase player2)
    {
        ActorColliderManager=new VActorColliderManager(player1,player2);
        ActorDirManager=new VActorDirManager(player1,player2);
        isInit = true;
    }

    public void VActorManagerDisable()
    {
        isInit = false;
        ActorColliderManager?.Destroy();
        ActorColliderManager = null;
        ActorDirManager = null;
    }

    public void Update()
    {
        if(!isInit)
            return;

        ActorDirManager?.Update();
    }

    public void Destroy()
    {
        if(!isInit)
            return;
    }
}
