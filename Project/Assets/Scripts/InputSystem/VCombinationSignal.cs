using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
/// <summary>
/// 基于角色的组合键信号
/// </summary>
public class VCombinationSignal
{
    private const float DOUBLE_PRESS_INTERVAL = 0.5f;
    
    ///
    /// 组合键判断
    ///
    private readonly KeySignalContainer _keySignalContainer;

    //所有在进行组合键判断的按键，需要在游戏开始时通过角色技能填充，游戏结束时清空
    public Dictionary<InputCombinationObj, bool> combinationSignal = new Dictionary<InputCombinationObj, bool>();

    //组合键释放信号
    public Dictionary<InputCombinationObj, bool> combinationReleaseSignal = new Dictionary<InputCombinationObj, bool>();
    
    //组合键的超时判断
    private Dictionary<InputCombinationObj,bool> combinationTimeSignal=new Dictionary<InputCombinationObj, bool>();

    public VCombinationSignal()
    {
        _keySignalContainer = GameFramework.instance._service.input;
        
    }
    
    /// <summary>
    /// 填充组合键判断
    /// </summary>
    /// <param name="combinationObj"></param>
    private void InsertCombination(InputCombinationObj combinationObj)
    {
        combinationSignal.Add(combinationObj, false);
        combinationTimeSignal.Add(combinationObj, false);
    }

    /// <summary>
    /// 清除所有组合键判断
    /// </summary>
    private void RemoveCombination(List<InputCombinationObj> combinationObjs)
    {
        foreach (var removeObj in combinationObjs)
        {
            if (combinationSignal.ContainsKey(removeObj))
                combinationSignal.Remove(removeObj);
            if (combinationReleaseSignal.ContainsKey(removeObj))
                combinationReleaseSignal.Remove(removeObj);
        }
    }

    private void Reset(InputCombinationObj obj)
    {
        obj.Clear();
        combinationSignal[obj] = false;
    }
    
    public void Update()
    {
        for (int i = 0; i < combinationSignal.Count; i++)
        {
            var combination = combinationSignal.ElementAt(i);
            // if (combination.Key.signalEnum == SkillSignalEnum.combinationPress)
            // {
            //     //上一帧已经判断为true，此时判断初始化
            //     if (combination.Key.currentKeys >= combination.Key.combinationLength)
            //     {
            //         Reset(combination.Key);
            //     }
            //     else
            //     {
            //         CombinationLogic(combination);
            //     }
            // }else if (combination.Key.signalEnum == SkillSignalEnum.combinationHold)
            // {
            //     //上一帧为true，如果release任意键则初始化
            //     if (combination.Key.currentKeys >= combination.Key.combinationLength)
            //     {
            //         if (_keySignalContainer.IsAnyRelease(
            //             combination.Key.combinationKeys[combination.Key.combinationLength - 1]))
            //         {
            //             Reset(combination.Key);
            //         }
            //     }
            //     else
            //     {
            //         CombinationLogic(combination);
            //     }
            // }
            //上一帧已经判断为true，此时判断初始化
            if (combination.Key.currentKeys >= combination.Key.combinationLength)
            {
                //添加进release字典，当发现字典按键被释放返回true
                if(!combinationReleaseSignal.ContainsKey(combination.Key))
                    combinationReleaseSignal.Add(combination.Key,false);

                Reset(combination.Key);
            }
            else
            {
                CombinationLogic(combination);
            }
        }

        for (int j = 0; j < combinationReleaseSignal.Count; j++)
        {
            var combination = combinationReleaseSignal.ElementAt(j);
            if (_keySignalContainer.IsAnyRelease(combination.Key.combinationKeys[combination.Key.combinationLength - 1]))
            {
                combinationReleaseSignal[combination.Key] = true;
            }
        }
    }

    private void CombinationLogic(KeyValuePair<InputCombinationObj, bool> combination)
    {
        int current = combination.Key.currentKeys;

        //如果当前组合键判断list的位置输入正确
        if (_keySignalContainer.IsAllPress(combination.Key.combinationKeys[current]) && 
            !_keySignalContainer.IsAnyPressed(combination.Key.notCombinationKeys[current]))
        {
            //如果相对上次已经超时，初始化
            if (Time.time - combination.Key.lastTime > DOUBLE_PRESS_INTERVAL && current != 0) 
            {
                Reset(combination.Key);
                return;
            }

            //已到最后一位判断，返回正确
            combination.Key.currentKeys++;
            if (combination.Key.currentKeys == combination.Key.combinationLength)
            {
                combinationSignal[combination.Key] = true;
            }
            //如果未到最后判断，则判断位置+1
            else if(combination.Key.currentKeys<combination.Key.combinationLength)
            {
                
            }

            //计时保存
            combination.Key.lastTime = Time.time;
        }
        //判断错误
        else if(_keySignalContainer.IsAnyPressed(combination.Key.notCombinationKeys[current]))
        {
            Reset(combination.Key);
        }
    }

    #region  组合键

    /// <summary>
    /// 组合键判断
    /// </summary>
    /// <param name="combinationObj"></param>
    /// <returns></returns>
    public bool IsCombination(InputCombinationObj combinationObj)
    {
        if(combinationSignal.ContainsKey(combinationObj))
            return combinationSignal[combinationObj];
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 组合键释放判断
    /// </summary>
    /// <param name="combinationObj"></param>
    /// <returns></returns>
    public bool IsCombinationRelease(InputCombinationObj combinationObj)
    {
        if (combinationReleaseSignal.ContainsKey(combinationObj))
        {
            if (combinationReleaseSignal[combinationObj])
            {
                combinationReleaseSignal[combinationObj] = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
            return false;
    }
    
    #endregion

    public void InsertCombinationSignal(InputCombinationObj combinationObj)
    {
        InsertCombination(combinationObj);
    }

    public void RemoveCombinationSignal(List<InputCombinationObj> combinationObjs)
    {
        RemoveCombination(combinationObjs);
    }
    
    
}
