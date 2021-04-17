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

    public VActorManager(VActorBase player1,VActorBase player2)
    {
        ActorColliderManager=new VActorColliderManager(player1,player2);
    }

    public void Destory()
    {
        ActorColliderManager.Destory();
    }
}
