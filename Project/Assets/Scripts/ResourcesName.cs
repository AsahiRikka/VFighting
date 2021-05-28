using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源名称获取
/// </summary>
public static class ResourcesName
{
    private const string ActorPrefabNamePrefix = "Actor_Prefab_";
    private const string ActorConfigNamePrefix = "Actor_Config_";

    public const string FukaName = "Fuka";
    public const string FukaPrefabName = ActorPrefabNamePrefix + FukaName;
    public const string FukaConfigName = ActorConfigNamePrefix + FukaName;
}
