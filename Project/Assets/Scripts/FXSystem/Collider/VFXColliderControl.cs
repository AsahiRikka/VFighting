using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 粒子系统碰撞控制
/// </summary>
[Serializable]
public class VFXColliderControl
{
    public VFXColliderControl(VFXProperty property,VFXControllerInfo controllerInfo,VFXActorProperty actorProperty)
    {
        _vfxProperty = property;
        _controllerInfo = controllerInfo;
        _actorProperty = actorProperty;
    }

    private VFXProperty _vfxProperty;
    private VFXControllerInfo _controllerInfo;
    private VFXActorProperty _actorProperty;

    private List<VFXColliderSegment> startList = new List<VFXColliderSegment>();
    private List<VFXColliderSegment> endList = new List<VFXColliderSegment>();
    
    public void OnEnable()
    {
        startList.Clear();
        endList.Clear();
        
        //修改layer
        
        
        ColliderClose();
    }

    public void Update()
    {
        //遍历碰撞，进行开启关闭
        foreach (var collider in _vfxProperty.FXCollider.FXColliders)
        {
            if (collider.startTime >= _controllerInfo.playTime && !startList.Contains(collider))
            {
                collider.VFXColliderScript.enabled = true;
                startList.Add(collider);
            }

            if (collider.endTime >= _controllerInfo.playTime && !endList.Contains(collider))
            {
                collider.VFXColliderScript.enabled = false;
                endList.Add(collider);
            }
        }
    }

    public void Disable()
    {
        
    }

    private void ColliderClose()
    {
        foreach (var collider in _vfxProperty.FXCollider.FXColliders)
        {
            collider.VFXColliderScript.enabled = false;
        }
    }
}
