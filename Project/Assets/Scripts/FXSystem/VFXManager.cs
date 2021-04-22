using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MackySoft.Pooling;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 特效管理，提供特效接口
/// </summary>
public class VFXManager
{
    /// <summary>
    /// 特效预设资源加载保存
    /// </summary>
    private Dictionary<string, VFXBase> fxDic = new Dictionary<string, VFXBase>();

    public VFXManager()
    {
        
    }

    public async UniTask VFXInit()
    {
        await LoadFX();
        
        //预加载特效
        foreach (var fx in fxDic)
        {
            Pool pool = PoolManager.AddPool(fx.Value.gameObject);
        }
    }

    private async UniTask LoadFX()
    {
        foreach (var fxReference in AddressableResources.instance.FXReferance)
        {
            GameObject assetAsync = await fxReference.LoadAssetAsync<GameObject>();
            VFXBase vfxBase = assetAsync.GetComponent<VFXBase>();
            fxDic.Add(vfxBase.Property.ID,vfxBase);
        }
    }

    /// <summary>
    /// 为角色提供的特效获取
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="skillActionFX"></param>
    public VFXBase ActorGetFXAndPlay(string ID, VSkillAction_FX skillActionFX, VActorChangeProperty property,
        GameObject actor, VSkillAction skillAction)
    {
        Pool pool = PoolManager.GetPoolSafe(fxDic[ID].gameObject);
        VFXBase fx = pool.Get<VFXBase>();
        fx.FXPrefabInit(skillActionFX, actor.transform, property, skillAction);
        return fx;
    }

    /// <summary>
    /// 如果设置为非自动关闭需要技能系统取消
    /// </summary>
    public void FXDestroy(VFXBase vfxBase)
    {
        vfxBase
            .gameObject.SetActive(false);
    }
}
