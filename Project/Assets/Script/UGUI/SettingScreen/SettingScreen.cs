using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingScreen : UIScreenBase
{
    private SettingCtrl _settingCtrl;

    private Dictionary<KeyCode,int> _dropdownValueDic=new Dictionary<KeyCode, int>();
    private Dictionary<int,KeyCode> _dropdownValueForIntDic=new Dictionary<int, KeyCode>();
    private void Start()
    {
        _settingCtrl = GetComponent<SettingCtrl>();
        _currentGroup = _settingCtrl.soundGroup;
        _currentSettingBtn = _settingCtrl.soundButton;

        //起始时，按键的dropDown列表数据构建
        foreach (var dropdown in _settingCtrl.KeySettingDictionary.KeyDropDown)
        {
            dropdown.Value.options.Clear();
            foreach (var keyDic in KeyCodeMapper.KeyMapDic)
            {
                dropdown.Value.options.Add(new TMP_Dropdown.OptionData(keyDic.Key));
            }
        }

        int flag = 0;
        foreach (var keyDic in KeyCodeMapper.KeyMapDic)
        {
            _dropdownValueDic.Add(keyDic.Value,flag);
            _dropdownValueForIntDic.Add(flag,keyDic.Value);
            flag++;
        }
        
        SettingBtnAddListener();
        SoundAddListener();
        OtherAddListener();
    }

    #region 设置界面控制

    private SettingBtnTypeEnum _btnTypeEnum = SettingBtnTypeEnum.sound;
    private SettingKeyMapPlayerEnum _keyMapPlayerEnum = SettingKeyMapPlayerEnum.player1;
    private Button _currentSettingBtn;
    private CanvasGroup _currentGroup;

    private void SettingBtnAddListener()
    {
        //界面控制类的按钮
        _settingCtrl.soundButton.onClick.AddListener(SoundSettingPush);
        _settingCtrl.inputSettingBtn.onClick.AddListener(KeySettingPush);
        _settingCtrl.otherSettingBtn.onClick.AddListener(OtherSettingPush);
        _settingCtrl.Player1.onClick.AddListener(KeySettingPlayer1Push);
        _settingCtrl.Player2.onClick.AddListener(KeySettingPlayer2Push);
        _settingCtrl.cancelBtn.onClick.AddListener(SettingCancelBtnPush);
        _settingCtrl.saveBtn.onClick.AddListener(SettingSaveBtnPush);
    }
    
    private void SoundSettingPush()
    {
        SettingBtnChange();
        
        //关闭交互
        _settingCtrl.soundButton.GetComponent<Button>().interactable = false;
        
        //显示声音设置
        _settingCtrl.soundGroup.alpha = 1;
        _settingCtrl.soundGroup.blocksRaycasts = true;

        //调整Vector位置
        var rectTransform = _settingCtrl.Vector.rectTransform;
        rectTransform.anchoredPosition=new Vector3(
            rectTransform.anchoredPosition.x,
            _settingCtrl.soundHeight
        );

        //修改当前版面和当前组
        _btnTypeEnum = SettingBtnTypeEnum.sound;
        _currentGroup = _settingCtrl.soundGroup;
        _currentSettingBtn = _settingCtrl.soundButton;
        
        SoundSettingInit();
    }

    private void KeySettingPush()
    {
        SettingBtnChange();
        
        //关闭交互
        _settingCtrl.inputSettingBtn.GetComponent<Button>().interactable = false;
        
        _settingCtrl.KeyGroup.alpha = 1;
        _settingCtrl.KeyGroup.blocksRaycasts = true;
            
        var rectTransform = _settingCtrl.Vector.rectTransform;
        rectTransform.anchoredPosition=new Vector3(
            rectTransform.anchoredPosition.x,
            _settingCtrl.keyHeight
        );
        
        _btnTypeEnum = SettingBtnTypeEnum.keyMap;
        _currentGroup = _settingCtrl.KeyGroup;
        _currentSettingBtn = _settingCtrl.inputSettingBtn;
        
        //默认角色1按键数据
        KeySettingPlayer1Push();
    }

    private void OtherSettingPush()
    {
        SettingBtnChange();
        
        //关闭交互
        _settingCtrl.otherSettingBtn.GetComponent<Button>().interactable = false;

        _settingCtrl.otherGroup.alpha = 1;
        _settingCtrl.otherGroup.blocksRaycasts = true;
            
        var rectTransform = _settingCtrl.Vector.rectTransform;
        rectTransform.anchoredPosition=new Vector3(
            rectTransform.anchoredPosition.x,
            _settingCtrl.otherHeight
        );

        _btnTypeEnum = SettingBtnTypeEnum.other;
        _currentGroup = _settingCtrl.otherGroup;
        _currentSettingBtn = _settingCtrl.otherSettingBtn;
        
        OtherSettingInit();
    }

    /// <summary>
    /// 对前一组的设置
    /// </summary>
    private void SettingBtnChange()
    {
        //当前的设置版面隐藏
        _currentGroup.alpha = 0;
        _currentGroup.blocksRaycasts = false;

        //恢复交互
        _currentSettingBtn.GetComponent<Button>().interactable = true;
    }

    private void KeySettingPlayer1Push()
    {
        //关闭当前按钮交互，恢复另一个按钮交互
        _settingCtrl.Player1.GetComponent<Button>().interactable = false;
        _settingCtrl.Player2.GetComponent<Button>().interactable = true;

        _keyMapPlayerEnum = SettingKeyMapPlayerEnum.player1;
        
        //将UI数据绑定角色1
        KeyMapInit(PlayerEnum.player_1);
    }

    private void KeySettingPlayer2Push()
    {
        _settingCtrl.Player2.GetComponent<Button>().interactable = false;
        _settingCtrl.Player1.GetComponent<Button>().interactable = true;

        _keyMapPlayerEnum = SettingKeyMapPlayerEnum.player2;
        
        //将UI数据绑定角色2
        KeyMapInit(PlayerEnum.player_2);
    }

    //取消按钮
    private void SettingCancelBtnPush()
    {
        OnHide();
    }

    //保存按钮
    private void SettingSaveBtnPush()
    {
        EventManager.UISettingEvent.BoradCastEvent(_settingScreenPram);
        OnHide();
    }

    #endregion

    #region 按键部份控制

    //调用此方法会修改UI层的按键数据
    private void KeyMapInit(PlayerEnum player)
    {
        KeyMapEventRemoveListener(player);
        
        if(_settingScreenPram?.KeyMapSetting == null)
            return;
        
        foreach (var keyMapJson in _settingScreenPram.KeyMapSetting.keyMapJsons)
        {
            //根据角色不同筛选keyMapJson
            TMP_Dropdown drop = null;
            if (player == PlayerEnum.player_1)
            {
                if (_settingCtrl.KeySettingDictionary.KeyDropDown.ContainsKey(keyMapJson.myKeyCode))
                {
                    drop = _settingCtrl.KeySettingDictionary.KeyDropDown[keyMapJson.myKeyCode];
                }
            }else if (player == PlayerEnum.player_2)
            {
                if (_settingCtrl.KeySettingDictionary.KeyDropDown2.ContainsKey(keyMapJson.myKeyCode))
                {
                    drop = _settingCtrl.KeySettingDictionary.KeyDropDown2[keyMapJson.myKeyCode];
                }
            }
            if (drop != null)
            {
                KeyCode keyCode = KeyCodeMapper.KeyMapDic[keyMapJson.keyCodeMapperKey];
                drop.value = _dropdownValueDic[keyCode];
            }
        }
        
        KeyMapEventAddListener(player);
    }
    
    private void KeyChanged(int arg,MyKeyCode keyCode)
    {
        KeyMapToJson myJson = null;
        foreach (var json in _settingScreenPram.KeyMapSetting.keyMapJsons)
        {
            if (json.myKeyCode == keyCode)
            {
                myJson = json;
            }
        }
        //修改字典的索引
        if (myJson != null)
            myJson.keyCodeMapperKey =
                KeyCodeMapper.KeyMapToStringDic[_dropdownValueForIntDic[arg]];
    }

    private void KeyMapEventAddListener(PlayerEnum e)
    {
        if (e == PlayerEnum.player_1)
        {
            Dictionary<MyKeyCode, TMP_Dropdown> player1Dropdown = _settingCtrl.KeySettingDictionary.KeyDropDown;

            player1Dropdown[MyKeyCode.leftArrow_1].onValueChanged.AddListener(Player1Left);
            player1Dropdown[MyKeyCode.rightArrow_1].onValueChanged.AddListener(Player1Right);
            player1Dropdown[MyKeyCode.upArrow_1].onValueChanged.AddListener(Player1Up);
            player1Dropdown[MyKeyCode.downArrow_1].onValueChanged.AddListener(Player1Down);
            player1Dropdown[MyKeyCode.attack1_1].onValueChanged.AddListener(Player1Skill1);
            player1Dropdown[MyKeyCode.attack2_1].onValueChanged.AddListener(Player1Skill2);
            player1Dropdown[MyKeyCode.dodge_1].onValueChanged.AddListener(Player1Skill3);
            player1Dropdown[MyKeyCode.skill1_1].onValueChanged.AddListener(Player1Skill4);
            player1Dropdown[MyKeyCode.skill2_1].onValueChanged.AddListener(Player1Skill5);
            player1Dropdown[MyKeyCode.skill3_1].onValueChanged.AddListener(Player1Skill6);
        }
        else if (e == PlayerEnum.player_2)
        {
            Dictionary<MyKeyCode, TMP_Dropdown> player2Dropdown = _settingCtrl.KeySettingDictionary.KeyDropDown2;
            
            player2Dropdown[MyKeyCode.leftArrow_2].onValueChanged.AddListener(Player2Left);
            player2Dropdown[MyKeyCode.rightArrow_2].onValueChanged.AddListener(Player2Right);
            player2Dropdown[MyKeyCode.upArrow_2].onValueChanged.AddListener(Player2Up);
            player2Dropdown[MyKeyCode.downArrow_2].onValueChanged.AddListener(Player2Down);
            player2Dropdown[MyKeyCode.attack1_2].onValueChanged.AddListener(Player2Skill1);
            player2Dropdown[MyKeyCode.attack2_2].onValueChanged.AddListener(Player2Skill2);
            player2Dropdown[MyKeyCode.dodge_2].onValueChanged.AddListener(Player2Skill3);
            player2Dropdown[MyKeyCode.skill1_2].onValueChanged.AddListener(Player2Skill4);
            player2Dropdown[MyKeyCode.skill2_2].onValueChanged.AddListener(Player2Skill5);
            player2Dropdown[MyKeyCode.skill3_2].onValueChanged.AddListener(Player2Skill6);
        }
    }

    private void KeyMapEventRemoveListener(PlayerEnum e)
    {
        //关闭监听
        foreach (var keyDropdown in _settingCtrl.KeySettingDictionary.KeyDropDown)
        {
            keyDropdown.Value.onValueChanged.RemoveAllListeners();
        }
        foreach (var keyDropdown in _settingCtrl.KeySettingDictionary.KeyDropDown2)
        {
            keyDropdown.Value.onValueChanged.RemoveAllListeners();
        }
    }

    #region 角色1事件回调

    private void Player1Left(int arg)
    {
        KeyChanged(arg,MyKeyCode.leftArrow_1);
    }

    private void Player1Right(int arg)
    {
        KeyChanged(arg,MyKeyCode.rightArrow_1);
    }

    private void Player1Up(int arg)
    {
        KeyChanged(arg,MyKeyCode.upArrow_1);
    }

    private void Player1Down(int arg)
    {
        KeyChanged(arg,MyKeyCode.downArrow_1);
    }

    private void Player1Skill1(int arg)
    {
        KeyChanged(arg,MyKeyCode.attack1_1);
    }

    private void Player1Skill2(int arg)
    {
        KeyChanged(arg,MyKeyCode.attack2_1);
    }

    private void Player1Skill3(int arg)
    {
        KeyChanged(arg,MyKeyCode.dodge_1);
    }

    private void Player1Skill4(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill1_1);
    }

    private void Player1Skill5(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill2_1);
    }

    private void Player1Skill6(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill3_1);
    }

    #endregion

    #region  角色2事件回调

    private void Player2Left(int arg)
    {
        KeyChanged(arg,MyKeyCode.leftArrow_2);
    }

    private void Player2Right(int arg)
    {
        KeyChanged(arg,MyKeyCode.rightArrow_2);
    }

    private void Player2Up(int arg)
    {
        KeyChanged(arg,MyKeyCode.upArrow_2);
    }

    private void Player2Down(int arg)
    {
        KeyChanged(arg,MyKeyCode.downArrow_2);
    }

    private void Player2Skill1(int arg)
    {
        KeyChanged(arg,MyKeyCode.attack1_2);
    }

    private void Player2Skill2(int arg)
    {
        KeyChanged(arg,MyKeyCode.attack2_2);
    }

    private void Player2Skill3(int arg)
    {
        KeyChanged(arg,MyKeyCode.dodge_2);
    }

    private void Player2Skill4(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill1_2);
    }

    private void Player2Skill5(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill2_2);
    }

    private void Player2Skill6(int arg)
    {
        KeyChanged(arg,MyKeyCode.skill3_2);
    }

    #endregion
    
    #endregion

    #region 音效部份控制

    private void SoundAddListener()
    {
        _settingCtrl.BGMSoundSlider.onValueChanged.AddListener(BGMSoundChange);
        _settingCtrl.EffectSoundSlider.onValueChanged.AddListener(EffectSoundChange);
        _settingCtrl.ActorSoundSlider.onValueChanged.AddListener(ActorSoundChange);
    }

    //声音设置初始化
    private void SoundSettingInit()
    {
        _settingCtrl.BGMSoundSlider.value = (float)_settingScreenPram.SoundSetting.musicSound;
        _settingCtrl.EffectSoundSlider.value = (float) _settingScreenPram.SoundSetting.effectSound;
        _settingCtrl.ActorSoundSlider.value = (float) _settingScreenPram.SoundSetting.characterSound;
    }

    private void BGMSoundChange(float volume)
    {
        _settingScreenPram.SoundSetting.musicSound = volume;
    }

    private void EffectSoundChange(float volume)
    {
        _settingScreenPram.SoundSetting.effectSound = volume;
    }

    private void ActorSoundChange(float volume)
    {
        _settingScreenPram.SoundSetting.effectSound = volume;
    }

    #endregion

    #region 其他设置控制

    private void OtherAddListener()
    {
        _settingCtrl.QualityLeftArrow.onClick.AddListener(QualityLeftBtnPush);
        _settingCtrl.QualityRightArrow.onClick.AddListener(QualityRightBtnPush);
        _settingCtrl.ResolutionLeftArrow.onClick.AddListener(ResolutionLeftBtnPush);
        _settingCtrl.ResolutionRightArrow.onClick.AddListener(ResolutionRightBtnPush);
    }
    
    DoubleLinkList<QualitySettingTypeEnum> qualityList=new DoubleLinkList<QualitySettingTypeEnum>();
    DoubleLinkList<ResolutionTypeEnum> resolutionList=new DoubleLinkList<ResolutionTypeEnum>();
    
    //其他设置初始化
    private void OtherSettingInit()
    {
        string text = KeyCodeMapper.QualitySettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.gameQuality).Key;
        _settingCtrl.QualityShowText.text = text;

        string resolutionText = KeyCodeMapper.ResolutionSettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.resolutionType).Key;
        _settingCtrl.ResolutionShowText.text = resolutionText;

        foreach (QualitySettingTypeEnum type in Enum.GetValues(typeof(QualitySettingTypeEnum)))
        {
            qualityList.AddElem(type);
        }

        foreach (ResolutionTypeEnum type in Enum.GetValues(typeof(ResolutionTypeEnum)))
        {
            resolutionList.AddElem(type);
        }
    }

    private void QualityLeftBtnPush()
    {
        _settingScreenPram.OtherSetting.gameQuality =
            qualityList.GetNode(_settingScreenPram.OtherSetting.gameQuality).PreNode.Data;
        
        string text = KeyCodeMapper.QualitySettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.gameQuality).Key;
        _settingCtrl.QualityShowText.text = text;
    }

    private void QualityRightBtnPush()
    {
        _settingScreenPram.OtherSetting.gameQuality =
            qualityList.GetNode(_settingScreenPram.OtherSetting.gameQuality).NextNode.Data;
        
        string text = KeyCodeMapper.QualitySettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.gameQuality).Key;
        _settingCtrl.QualityShowText.text = text;
    }

    private void ResolutionLeftBtnPush()
    {
        _settingScreenPram.OtherSetting.resolutionType =
            resolutionList.GetNode(_settingScreenPram.OtherSetting.resolutionType).PreNode.Data;
        
        string resolutionText = KeyCodeMapper.ResolutionSettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.resolutionType).Key;
        _settingCtrl.ResolutionShowText.text = resolutionText;
    }

    private void ResolutionRightBtnPush()
    {
        _settingScreenPram.OtherSetting.resolutionType =
            resolutionList.GetNode(_settingScreenPram.OtherSetting.resolutionType).NextNode.Data;
        
        string resolutionText = KeyCodeMapper.ResolutionSettingDic
            .FirstOrDefault(q => q.Value == _settingScreenPram.OtherSetting.resolutionType).Key;
        _settingCtrl.ResolutionShowText.text = resolutionText;
    }

    #endregion
    
    private SettingScreenPram _settingScreenPram;
    public override void OnDisplay()
    {
        base.OnDisplay();
        
        //显示时加载数据
        _settingScreenPram = new SettingScreenPram();
        
        //默认声音设置
        SoundSettingPush();
    }

    public override void OnHide()
    {
        base.OnHide();
    }
}

public class SettingScreenPram : UIParamBase
{
    //构造时新建声音，按键和其他设置的设置，并进行加载
    public SettingScreenPram()
    {
        SoundSetting = (SoundSetting) GameFramework.instance._service.SettingManager.GameCommonSetting.SoundSetting.Clone();
        KeyMapSetting = (KeyMapSetting) GameFramework.instance._service.SettingManager.InputSetting.KeyMapSetting.Clone();
        OtherSetting = (OtherSetting) GameFramework.instance._service.SettingManager.GameCommonSetting.OtherSetting.Clone();
    }

    //对声音的引用
    public SoundSetting SoundSetting;
    
    //新建按键设置，在开始时按配置加载，设置被修改后用此替代旧设置
    public KeyMapSetting KeyMapSetting;
    
    //其他设置
    public OtherSetting OtherSetting;
}

public enum SettingBtnTypeEnum
{
    sound,
    keyMap,
    other,
}

public enum SettingKeyMapPlayerEnum
{
    player1,
    player2,
}