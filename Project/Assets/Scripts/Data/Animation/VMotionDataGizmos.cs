using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// VmotionEditor的Gizmos
/// </summary>
public class VMotionDataGizmos : MonoBehaviour
{
    private VMotion _motion;
    public int _currentFrame = 0;
    
    public void Init(VMotion motion)
    {
        _motion = motion;
    }

    public void CurrentFrameRefrsh(int i)
    {
        _currentFrame = i;
    }

    /// <summary>
    /// 根据currentFrame绘制受击框和攻击框
    /// </summary>
    private void OnDrawGizmos()
    {
        if(_motion==null)
            return;
    
        foreach (var VARIABLE in _motion.passiveBoxes)
        {
            if (_currentFrame >= VARIABLE.segmentMotion.startFrame && _currentFrame <= VARIABLE.segmentMotion.endFrame)
            {
                Gizmos.color=Color.green;
                Gizmos.DrawWireCube(VARIABLE.collider.center + transform.position, VARIABLE.collider.size);
            }
        }
    
        foreach (var VARIABLE in _motion.hitBoxes)
        {
            if (_currentFrame >= VARIABLE.segmentMotion.startFrame && _currentFrame <= VARIABLE.segmentMotion.endFrame)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(VARIABLE.collider.center + transform.position, VARIABLE.collider.size);
            }
        }
        
        foreach (var VARIABLE in _motion.defenseBoxes)
        {
            if (_currentFrame >= VARIABLE.segmentMotion.startFrame && _currentFrame <= VARIABLE.segmentMotion.endFrame)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(VARIABLE.collider.center + transform.position, VARIABLE.collider.size);
            }
        }
    }
}
