using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源名称获取
/// </summary>
public static class ResourcesName
{
    public static Dictionary<ActorEnum, ActorResourcesName> ActorResources =
        new Dictionary<ActorEnum, ActorResourcesName>()
        {
            {
                ActorEnum.Fuka,
                new ActorResourcesName("Fuka")
            },
            {
                ActorEnum.XiaoXi,
                new ActorResourcesName("XiaoXi")
            },
            {
                ActorEnum.XiaoTao,
                new ActorResourcesName("XiaoTao")
            }
        };
}

public class ActorResourcesName
{
    //角色
    private const string ActorPrefabNamePrefix = "Actor_Prefab_";
    private const string ActorConfigNamePrefix = "Actor_Config_";

    public readonly string Name;
    public readonly string PrefabName;
    public readonly string ConfigName;

    public ActorResourcesName(string name)
    {
        Name = name;
        PrefabName = ActorPrefabNamePrefix + Name;
        
        ConfigName = ActorConfigNamePrefix + Name;
    }
}