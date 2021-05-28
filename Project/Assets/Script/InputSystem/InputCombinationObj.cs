using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 组合键的对象，一个对象有对应的键位集合，以及组合键判断字典
/// </summary>
public class InputCombinationObj
{
    /// <summary>
    /// 所有按键
    /// </summary>
    public readonly MyKeyCode[] allKeys;

    /// <summary>
    /// 组合键
    /// </summary>
    public readonly List<MyKeyCode[]> combinationKeys;

    /// <summary>
    /// 所有按键按列表删除组合键的键位
    /// </summary>
    public readonly List<MyKeyCode[]> notCombinationKeys;

    /// <summary>
    /// 组合键判断长度
    /// </summary>
    public readonly int combinationLength;
    
    /// <summary>
    /// 当前进行组合键判断list的位置
    /// </summary>
    public int currentKeys = 0;

    /// <summary>
    /// 上次判断时间，超时归0
    /// </summary>
    public float lastTime = 0;

    /// <summary>
    /// 输入模式
    /// </summary>
    public readonly SkillSignalEnum signalEnum;
    
    public InputCombinationObj(List<MyKeyCode[]> keyCodeses,MyKeyCode[] allKeys,SkillSignalEnum e)
    {
        combinationKeys = keyCodeses;
        combinationLength = combinationKeys.Count;
        signalEnum = e;

        this.allKeys = allKeys;
        
        notCombinationKeys=new List<MyKeyCode[]>();
        NotCombinationKeysInit();
    }
    private void NotCombinationKeysInit()
    {
        foreach (MyKeyCode[] removeKeys in combinationKeys)
        {
            MyKeyCode[] keyCodes = new MyKeyCode[allKeys.Length-removeKeys.Length];
            int flag = 0;
            foreach (MyKeyCode value in allKeys)
            {
                if (!removeKeys.Contains(value))
                {
                    keyCodes[flag] = value;
                    flag++;
                }
            }
            notCombinationKeys.Add(keyCodes);
        }
    }
    public void Clear()
    {
        currentKeys = 0;
    }
}
