using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 负责角色的技能触发信号，不进行成功判断
/// </summary>
public class VActorSkillSignal
{
    private readonly KeySignalContainer _inputService = GameFramework.instance._service.input;
    private VActorChangeProperty _property;
    private readonly VSkillActions _skillActions;
    private VActorInfo _actorInfo;
    private readonly VCombinationSignal _combinationSignal;
    private VActorEvent _actorEvent;
    private readonly VActorState _actorState;

    /// <summary>
    /// 物理组件信号
    /// </summary>
    public Dictionary<VActorPhysicComponentEnum, bool> physicComponentDic=new Dictionary<VActorPhysicComponentEnum, bool>();
    InputCombinationObj dash;
    private PlayerKeyCode anamy = PlayerKeyCode.rightArrow;
    private PlayerKeyCode invertAnamy = PlayerKeyCode.leftArrow;
    
    public VActorSkillSignal(VActorChangeProperty property, VSkillActions skillActions, VActorEvent actorEvent,
        VActorInfo actorInfo, VActorState actorState)
    {
        _property = property;
        _skillActions = skillActions;
        _actorEvent = actorEvent;
        _actorInfo = actorInfo;
        _actorState = actorState;

        _combinationSignal=new VCombinationSignal();

        foreach (VActorPhysicComponentEnum e in Enum.GetValues(typeof(VActorPhysicComponentEnum)))
        {
            physicComponentDic.Add(e,false);
        }
    }

    public void Init()
    {
        //根据角色初始化键位
        KeyCodeInit();

        //玩家电脑判断
        if (_property.playerTypeEnum == PlayerTypeEnum.computer)
        {
            DebugHelper.LogError("电脑玩家，使用内部技能信号");
        }
        
        //将组合键添加进输入系统中
        AddCombination();
        
        //冲刺
        if (_property.playerEnum == PlayerEnum.player_2)
        {
            anamy = PlayerKeyCode.leftArrow;
            invertAnamy = PlayerKeyCode.rightArrow;
        }
        dash = new InputCombinationObj(new List<MyKeyCode[]>()
        {
            {
                new[]
                {
                    MyKeyCodeForPlayer[anamy]
                }
            },
            {
                new[]
                {
                    MyKeyCodeForPlayer[anamy]
                }
            },
        }, allkeys, SkillSignalEnum.combinationHold);
        _combinationSignal.InsertCombinationSignal(dash);
    }

    public void Update()
    {
        _combinationSignal.Update();

        //组合技能
        foreach (var skill in currentTriggerCombinationDic)
        {
            if (_actorInfo.skillInfo.currentSkill != skill.Value) 
            {
                if (_combinationSignal.IsCombination(skill.Key)) 
                {
                    _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(skill.Value);
                }
            }
            else
            {
                //hold类型退出判断
                if (!_combinationSignal.IsCombination(skill.Key) &&
                    skill.Value.signalData.SignalEnum == SkillSignalEnum.combinationHold)
                {
                    //hold型技能释放按键
                    _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(skill.Value);
                }
            }
        }
        
        //其他技能
        foreach (var trigger in currentNormalSignalDic)
        {
            if (_actorInfo.skillInfo.currentSkill != trigger.Value) 
            {
                if (_inputService.IsAllPress(trigger.Key) &&
                    trigger.Value.signalData.SignalEnum == SkillSignalEnum.allPress) 
                {
                    //press技能按键按键
                    _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(trigger.Value);

                }else if (trigger.Value.signalData.SignalEnum == SkillSignalEnum.allHold &&
                          _inputService.IsAllHold(trigger.Key)) 
                {
                    //hold按下按键
                    _actorEvent.SkillEvent.skillPlayTriggerEvent.Invoke(trigger.Value);
                }
            }
            else
            {
                //hold类型退出判断
                if (trigger.Value.signalData.SignalEnum == SkillSignalEnum.allHold &&
                    _inputService.IsAnyRelease(trigger.Key))
                {
                    //hold型技能释放按键
                    _actorEvent.SkillEvent.skillEndNormalEvent.Invoke(trigger.Value);
                }
            }
        }
        
        //
        //物理组件信号
        //

        #region MyRegion
        //前进
        if (_inputService.IsHold(MyKeyCodeForPlayer[anamy]))
        {
            physicComponentDic[VActorPhysicComponentEnum.move] = true;
        }
        else
        {
            physicComponentDic[VActorPhysicComponentEnum.move] = false;
        }
        
        //冲刺
        if (_combinationSignal.IsCombination(dash))
        {
            physicComponentDic[VActorPhysicComponentEnum.dash] = true;
        }
        else
        {
            physicComponentDic[VActorPhysicComponentEnum.dash] = false;
        }

        //后退
        if (_inputService.IsHold(MyKeyCodeForPlayer[invertAnamy]))
        {
            physicComponentDic[VActorPhysicComponentEnum.retreat] = true;
        }
        else
        {
            physicComponentDic[VActorPhysicComponentEnum.retreat] = false;
        }
        
        //跳跃
        if (_inputService.IsPressed(MyKeyCodeForPlayer[PlayerKeyCode.upArrow]))
        {
            physicComponentDic[VActorPhysicComponentEnum.jump] = true;
        }
        else
        {
            physicComponentDic[VActorPhysicComponentEnum.jump] = false;
        }

        #endregion
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    public void Destroy()
    {
        RemoveCombination();
    }

    /// <summary>
    /// 组合键技能字典，方便技能判断
    /// </summary>
    private Dictionary<InputCombinationObj, VSkillAction> skillTriggerCombinationDic;

    private Dictionary<InputCombinationObj, VSkillAction> invertSkillTriggerCombinationDic;

    private Dictionary<InputCombinationObj, VSkillAction> currentTriggerCombinationDic;
    
    /// <summary>
    /// 组合键列表，用于添加进inputSystem
    /// </summary>
    private List<InputCombinationObj> _combinationObjs;

    private List<InputCombinationObj> _invertCombinationObjs;

    /// <summary>
    /// 非组合技能字典
    /// </summary>
    private Dictionary<MyKeyCode[], VSkillAction> normalSignalDic;

    private Dictionary<MyKeyCode[], VSkillAction> invertNormalSignalDic;

    private Dictionary<MyKeyCode[], VSkillAction> currentNormalSignalDic;

    /// <summary>
    /// 单个按键的映射
    /// </summary>
    private Dictionary<PlayerKeyCode, MyKeyCode> MyKeyCodeForPlayer = new Dictionary<PlayerKeyCode, MyKeyCode>();
    
    /// <summary>
    /// 多按键映射，减少计算
    /// </summary>
    /// <returns></returns>
    private Dictionary<PlayerKeyCode[],MyKeyCode[]> MykeyCdoeForPlayerArray=new Dictionary<PlayerKeyCode[], MyKeyCode[]>();

    /// <summary>
    /// 当前角色的全按键
    /// </summary>
    private MyKeyCode[] allkeys;

    /// <summary>
    /// 按键数据添加
    /// </summary>
    private void AddCombination()
    {
        skillTriggerCombinationDic=new Dictionary<InputCombinationObj, VSkillAction>();
        invertSkillTriggerCombinationDic=new Dictionary<InputCombinationObj, VSkillAction>();

        normalSignalDic=new Dictionary<MyKeyCode[], VSkillAction>();
        invertNormalSignalDic=new Dictionary<MyKeyCode[], VSkillAction>();

        if (_property.playerEnum == PlayerEnum.player_1)
        {
            currentTriggerCombinationDic = skillTriggerCombinationDic;
            currentNormalSignalDic = normalSignalDic;
        }
        else if (_property.playerEnum == PlayerEnum.player_2)
        {
            currentTriggerCombinationDic = invertSkillTriggerCombinationDic;
            currentNormalSignalDic = invertNormalSignalDic;
        }

        _combinationObjs=new List<InputCombinationObj>();
        _invertCombinationObjs = new List<InputCombinationObj>();
        
        foreach (VSkillAction skillAction in _skillActions.actorSkillActions)
        {
            SkillSignalEnum e = skillAction.signalData.SignalEnum;
            //组合键
            if (skillAction.signalData.SignalEnum == SkillSignalEnum.combinationPress ||
                skillAction.signalData.SignalEnum == SkillSignalEnum.combinationHold)       
            {
                List<MyKeyCode[]> keyCodeses=new List<MyKeyCode[]>();
                List<MyKeyCode[]> invertKeyCodeses=new List<MyKeyCode[]>();
                foreach (var dataSegment in skillAction.signalData.skillSignalDataSegments)
                {
                    MyKeyCode[] codes = new MyKeyCode[dataSegment.playerKeyCodes.Count];
                    MyKeyCode[] invertcodes=new MyKeyCode[dataSegment.playerKeyCodes.Count];
                    int flag = 0;
                    foreach (var data in dataSegment.playerKeyCodes)
                    {
                        PlayerKeyCode temp = data;
                        if (temp == PlayerKeyCode.anamyArrow)
                        {
                            codes[flag] = MyKeyCodeForPlayer[PlayerKeyCode.rightArrow];
                            invertcodes[flag] = MyKeyCodeForPlayer[PlayerKeyCode.leftArrow];
                        }else if (temp == PlayerKeyCode.invertAnamyArrow)
                        {
                            codes[flag] = MyKeyCodeForPlayer[PlayerKeyCode.leftArrow];
                            invertcodes[flag] = MyKeyCodeForPlayer[PlayerKeyCode.rightArrow];
                        }
                        else
                        {
                            codes[flag] = MyKeyCodeForPlayer[temp];
                            invertcodes[flag] = MyKeyCodeForPlayer[temp];
                        }
                        flag++;
                    }
                    keyCodeses.Add(codes);
                    invertKeyCodeses.Add(invertcodes);
                }

                InputCombinationObj obj =
                    new InputCombinationObj(keyCodeses, allkeys, skillAction.signalData.SignalEnum);
                InputCombinationObj invertObj =
                    new InputCombinationObj(invertKeyCodeses, allkeys, skillAction.signalData.SignalEnum);
                
                skillTriggerCombinationDic.Add(obj,skillAction);
                invertSkillTriggerCombinationDic.Add(invertObj,skillAction);
                
                _combinationObjs.Add(obj);
                _invertCombinationObjs.Add(invertObj);
                _combinationSignal.InsertCombinationSignal(obj);
                _combinationSignal.InsertCombinationSignal(invertObj);
            }
            //非组合键
            else
            {
                MyKeyCode[] normalSignal=new MyKeyCode[skillAction.signalData.skillSignalDataSegments[0].playerKeyCodes.Count];
                MyKeyCode[] invertSignal=new MyKeyCode[skillAction.signalData.skillSignalDataSegments[0].playerKeyCodes.Count];

                int flag = 0;
                foreach (var key in skillAction.signalData.skillSignalDataSegments[0].playerKeyCodes)
                {
                    if (key == PlayerKeyCode.anamyArrow)
                    {
                        normalSignal[flag] = MyKeyCodeForPlayer[PlayerKeyCode.rightArrow];
                        invertSignal[flag] = MyKeyCodeForPlayer[PlayerKeyCode.leftArrow];
                    }else if (key == PlayerKeyCode.invertAnamyArrow)
                    {
                        normalSignal[flag] = MyKeyCodeForPlayer[PlayerKeyCode.leftArrow];
                        invertSignal[flag] = MyKeyCodeForPlayer[PlayerKeyCode.rightArrow];
                    }
                    else
                    {
                        normalSignal[flag] = MyKeyCodeForPlayer[key];
                        invertSignal[flag] = MyKeyCodeForPlayer[key];
                    }
                    flag++;
                }
                
                normalSignalDic.Add(normalSignal,skillAction);
                invertNormalSignalDic.Add(invertSignal,skillAction);
            }
        }
    }

    private void RemoveCombination()
    {
        _combinationSignal.RemoveCombinationSignal(_combinationObjs);
        _combinationSignal.RemoveCombinationSignal(_invertCombinationObjs);
        _combinationSignal.RemoveCombinationSignal(new List<InputCombinationObj>(){dash});
    }
    
    private void KeyCodeInit()
    {
        if (_property.playerEnum == PlayerEnum.player_1)
        {
            MyKeyCodeForPlayer.Add(PlayerKeyCode.leftArrow,MyKeyCode.leftArrow_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.rightArrow,MyKeyCode.rightArrow_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.upArrow,MyKeyCode.upArrow_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.downArrow,MyKeyCode.downArrow_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.attack1,MyKeyCode.attack1_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.attack2,MyKeyCode.attack2_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.dodge,MyKeyCode.dodge_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill1,MyKeyCode.skill1_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill2,MyKeyCode.skill2_1);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill3,MyKeyCode.skill3_1);

            allkeys = new[]
            {
                MyKeyCode.leftArrow_1,
                MyKeyCode.rightArrow_1,
                MyKeyCode.upArrow_1,
                MyKeyCode.downArrow_1,
                MyKeyCode.attack1_1,
                MyKeyCode.attack2_1,
                MyKeyCode.dodge_1,
                MyKeyCode.skill1_1,
                MyKeyCode.skill2_1,
                MyKeyCode.skill3_1,
            };
        }
        else if (_property.playerEnum == PlayerEnum.player_2)
        {
            MyKeyCodeForPlayer.Add(PlayerKeyCode.leftArrow,MyKeyCode.leftArrow_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.rightArrow,MyKeyCode.rightArrow_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.upArrow,MyKeyCode.upArrow_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.downArrow,MyKeyCode.downArrow_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.attack1,MyKeyCode.attack1_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.attack2,MyKeyCode.attack2_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.dodge,MyKeyCode.dodge_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill1,MyKeyCode.skill1_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill2,MyKeyCode.skill2_2);
            MyKeyCodeForPlayer.Add(PlayerKeyCode.skill3,MyKeyCode.skill3_2);
            
            allkeys = new[]
            {
                MyKeyCode.leftArrow_2,
                MyKeyCode.rightArrow_2,
                MyKeyCode.upArrow_2,
                MyKeyCode.downArrow_2,
                MyKeyCode.attack1_2,
                MyKeyCode.attack2_2,
                MyKeyCode.dodge_2,
                MyKeyCode.skill1_2,
                MyKeyCode.skill2_2,
                MyKeyCode.skill3_2,
            };
        }
        else
            DebugHelper.LogError("不存在该玩家!" + _property.playerEnum);
    }

    /// <summary>
    /// 方向变化的回调
    /// </summary>
    /// <param name="dir"></param>
    private void SkillDirChange(int dir)
    {
        if (dir == 1)
        {
            currentTriggerCombinationDic = skillTriggerCombinationDic;
            currentNormalSignalDic = normalSignalDic;

            anamy = PlayerKeyCode.rightArrow;
            invertAnamy = PlayerKeyCode.leftArrow;
        }
        else if (dir == -1)
        {
            currentTriggerCombinationDic = invertSkillTriggerCombinationDic;
            currentNormalSignalDic = invertNormalSignalDic;

            anamy = PlayerKeyCode.leftArrow;
            invertAnamy = PlayerKeyCode.rightArrow;
        }
    }
}