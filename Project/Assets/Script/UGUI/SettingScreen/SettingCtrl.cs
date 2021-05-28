using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : UIControlBase
{
    public Image backImage;
    public Button soundButton;
    public Button inputSettingBtn;
    public Button otherSettingBtn;
    public Button saveBtn;
    public Button cancelBtn;
    
    [Space(30)] [InfoBox("vector位置")] 
    public Image Vector;
    public float soundHeight;
    public float keyHeight;
    public float otherHeight;

    [Space(30)] [InfoBox("声音UI")] 
    public Slider BGMSoundSlider;
    public Slider EffectSoundSlider;
    public Slider ActorSoundSlider;
    public List<TextMeshProUGUI> soundText;
    public CanvasGroup soundGroup;

    

    [Space(30)] [InfoBox("其他设置")] 
    public Button QualityLeftArrow;
    public Button QualityRightArrow;
    public TextMeshProUGUI QualityShowText;
    public Button ResolutionLeftArrow;
    public Button ResolutionRightArrow;
    public TextMeshProUGUI ResolutionShowText;
    public List<TextMeshProUGUI> otherText;
    public CanvasGroup otherGroup;

    [Space(30)] [InfoBox("按键设置")] 
    public Button Player1;
    public Button Player2;
    public KeySettingDictionary KeySettingDictionary;
    public CanvasGroup KeyGroup;
}