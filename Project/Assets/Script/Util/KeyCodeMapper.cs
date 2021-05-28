using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 字符串对应的keyCode，在此的可被自定义
/// </summary>
public static class KeyCodeMapper
{
    public static Dictionary<String, KeyCode> KeyMapDic = new Dictionary<string, KeyCode>()
    {
        {"A",KeyCode.A},
        {"W",KeyCode.W},
        {"S",KeyCode.S},
        {"D",KeyCode.D},
        {"J",KeyCode.J},
        {"K",KeyCode.K},
        {"L",KeyCode.L},
        {"U",KeyCode.U},
        {"I",KeyCode.I},
        {"O",KeyCode.O},
        
        {"上方向键",KeyCode.UpArrow},
        {"下方向键",KeyCode.DownArrow},
        {"左方向键",KeyCode.LeftArrow},
        {"右方向键",KeyCode.RightArrow},
        {"小键盘1",KeyCode.Keypad1},
        {"小键盘2",KeyCode.Keypad2},
        {"小键盘3",KeyCode.Keypad3},
        {"小键盘4",KeyCode.Keypad4},
        {"小键盘5",KeyCode.Keypad5},
        {"小键盘6",KeyCode.Keypad6},
        
        {"E",KeyCode.E},
        {"R",KeyCode.R},
        {"T",KeyCode.T},
        {"Y",KeyCode.Y},
        {"P",KeyCode.P},
        {"F",KeyCode.F},
        {"G",KeyCode.G},
        {"H",KeyCode.H},
        {"Z",KeyCode.Z},
        {"X",KeyCode.X},
        {"C",KeyCode.C},
        {"V",KeyCode.V},
        {"B",KeyCode.B},
        {"N",KeyCode.N},
        {"M",KeyCode.M},
        
        {"小键盘7",KeyCode.Keypad7},
        {"小键盘8",KeyCode.Keypad8},
        {"小键盘9",KeyCode.Keypad9},
    };
    
    public static Dictionary<KeyCode,String> KeyMapToStringDic = new Dictionary<KeyCode,String>()
    {
        {KeyCode.A,"A"},
        {KeyCode.W,"W"},
        {KeyCode.S,"S"},
        {KeyCode.D,"D"},
        {KeyCode.J,"J"},
        {KeyCode.K,"K"},
        {KeyCode.L,"L"},
        {KeyCode.U,"U"},
        {KeyCode.I,"I"},
        {KeyCode.O,"O"},
        
        {KeyCode.UpArrow,"上方向键"},
        {KeyCode.DownArrow,"下方向键"},
        {KeyCode.LeftArrow,"左方向键"},
        {KeyCode.RightArrow,"右方向键"},
        {KeyCode.Keypad1,"小键盘1"},
        {KeyCode.Keypad2,"小键盘2"},
        {KeyCode.Keypad3,"小键盘3"},
        {KeyCode.Keypad4,"小键盘4"},
        {KeyCode.Keypad5,"小键盘5"},
        {KeyCode.Keypad6,"小键盘6"},
        
        {KeyCode.E,"E"},
        {KeyCode.R,"R"},
        {KeyCode.T,"T"},
        {KeyCode.Y,"Y"},
        {KeyCode.P,"P"},
        {KeyCode.F,"F"},
        {KeyCode.G,"G"},
        {KeyCode.H,"H"},
        {KeyCode.Z,"Z"},
        {KeyCode.X,"X"},
        {KeyCode.C,"C"},
        {KeyCode.V,"V"},
        {KeyCode.B,"B"},
        {KeyCode.N,"N"},
        {KeyCode.M,"M"},
        
        {KeyCode.Keypad7,"小键盘7"},
        {KeyCode.Keypad8,"小键盘8"},
        {KeyCode.Keypad9,"小键盘9"},
    };

    public static Dictionary<String, ResolutionTypeEnum> ResolutionSettingDic =
        new Dictionary<string, ResolutionTypeEnum>()
        {
            {"全屏", ResolutionTypeEnum.FullScreen},
            {"1920 X 1080", ResolutionTypeEnum.w1920H1080},
            {"1080 X 720", ResolutionTypeEnum.W1080H720},
        };

    public static Dictionary<String, QualitySettingTypeEnum> QualitySettingDic =
        new Dictionary<string, QualitySettingTypeEnum>()
        {
            {"极低",QualitySettingTypeEnum.VeryLow},
            {"低",QualitySettingTypeEnum.Low},
            {"中",QualitySettingTypeEnum.Medium},
            {"高",QualitySettingTypeEnum.High},
            {"较高",QualitySettingTypeEnum.VeryHigh},
            {"最高",QualitySettingTypeEnum.Ultra}
        };

    public static Dictionary<String, GameTimeTypeEnum> GameTimeTypeDic = new Dictionary<string, GameTimeTypeEnum>()
    {
        {"无限时间", GameTimeTypeEnum.infinite},
        {"一分钟", GameTimeTypeEnum.min_1},
        {"两分钟", GameTimeTypeEnum.min_2},
    };

    public static Dictionary<String, HeathScaleTypeEnum> HeathScaleTypeDic =
        new Dictionary<string, HeathScaleTypeEnum>()
        {
            {"正常", HeathScaleTypeEnum.one},
            {"两倍生命值", HeathScaleTypeEnum.two},
            {"三倍生命值", HeathScaleTypeEnum.three},
        };
}