using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
/// <summary>
/// BUFF设置
/// </summary>\
[Serializable]
public class VBuffSetting
{
    /// <summary>
    /// 持续时间
    /// </summary>
    [InfoBox("持续时间")]
    public float time = 0;

    /// <summary>
    /// 是否可以移除
    /// </summary>
    [InfoBox("是否可以移除")]
    public bool isRemove = false;

    /// <summary>
    /// 是否有优先级，相同buff不同等级时（true：高等级覆盖；false：同时存在）
    /// </summary>
    [InfoBox("是否有优先级，相同buff不同等级时（true：高等级覆盖；false：同时存在）")]
    public bool isPriority = true;
}
