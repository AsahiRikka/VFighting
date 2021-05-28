using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class VActorAISkillData
{
    /// <summary>
    /// 技能
    /// </summary>
    public VSkillAction _skillAction;

    /// <summary>
    /// 对于持续性技能，holdTime为持续时间，到达时间后触发结束，范围随机。其他技能无效
    /// </summary>
    public readonly Vector2 HoldTime;

    public VActorAISkillData(VSkillAction skillAction)
    {
        _skillAction = skillAction;
        HoldTime.x = Random.Range(1.5f, 2f);
        HoldTime.y = Random.Range(3f, 3.5f);
    }
}
