using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEnterSettingScreen : UIScreenBase
{
    public GameEnterSettingCtrl gameEnterSettingCtrl;
    
    private DoubleLinkList<GameTimeTypeEnum> gameTimeLink=new DoubleLinkList<GameTimeTypeEnum>();
    private DoubleLinkList<HeathScaleTypeEnum> heathScaleLink=new DoubleLinkList<HeathScaleTypeEnum>();
    
    //数据
    private GameEnterSettingParams _gameEnterSettingParams;
    
    //当前选项
    private Node<GameTimeTypeEnum> currentTimeType;
    private Node<HeathScaleTypeEnum> currentHeathScale;
    
    private void Start()
    {
        //初始化两个双向链表
        foreach (GameTimeTypeEnum time in Enum.GetValues(typeof(GameTimeTypeEnum)))
        {
            gameTimeLink.AddElem(time);
        }

        foreach (HeathScaleTypeEnum heath in Enum.GetValues(typeof(HeathScaleTypeEnum)))
        {
            heathScaleLink.AddElem(heath);
        }
        
        //消息注册
        gameEnterSettingCtrl.TimeLeft.onClick.AddListener(TimeLeftEvent);
        gameEnterSettingCtrl.TimeRight.onClick.AddListener(TimeRightEvent);
        gameEnterSettingCtrl.HealthLeft.onClick.AddListener(HeathLeftEvent);
        gameEnterSettingCtrl.HealthRight.onClick.AddListener(HeathRightEvent);
        gameEnterSettingCtrl.GameStart.onClick.AddListener(ConfirmBtn);
    }

    public override void OnDisplay()
    {
        base.OnDisplay();
        
        //新建数据
        _gameEnterSettingParams=new GameEnterSettingParams();
        
        //重新定位
        currentTimeType = gameTimeLink.Head;
        currentHeathScale = heathScaleLink.Head;
        
        //初始化文字
        gameEnterSettingCtrl.timeText.text = KeyCodeMapper.GameTimeTypeDic
            .FirstOrDefault(q => q.Value == _gameEnterSettingParams.GameTimeType).Key;
        gameEnterSettingCtrl.healthText.text = KeyCodeMapper.HeathScaleTypeDic
            .FirstOrDefault(q => q.Value == _gameEnterSettingParams.HeathScaleType).Key;
    }

    private void TimeLeftEvent()
    {
        currentTimeType = currentTimeType.PreNode;
        TextRefreshTime();
    }

    private void TimeRightEvent()
    {
        currentTimeType = currentTimeType.NextNode;
        TextRefreshTime();
    }

    private void HeathLeftEvent()
    {
        currentHeathScale = currentHeathScale.PreNode;
        TextRefreshHeath();
    }

    private void HeathRightEvent()
    {
        currentHeathScale = currentHeathScale.NextNode;
        TextRefreshHeath();
    }

    //刷新时间文字
    private void TextRefreshTime()
    {
        gameEnterSettingCtrl.timeText.text = KeyCodeMapper.GameTimeTypeDic
            .FirstOrDefault(q => q.Value == currentTimeType.Data).Key;
    }

    private void TextRefreshHeath()
    {
        gameEnterSettingCtrl.healthText.text = KeyCodeMapper.HeathScaleTypeDic
            .FirstOrDefault(q => q.Value == currentHeathScale.Data).Key;
    }

    private void ConfirmBtn()
    {
        //将当前确认进数据，并开始游戏
        _gameEnterSettingParams.GameTimeType = currentTimeType.Data;
        _gameEnterSettingParams.HeathScaleType = currentHeathScale.Data;
        
        //确认完成，发布事件
        EventManager.UIGameEnterSettingEvent.BoradCastEvent(_gameEnterSettingParams);
        
        GameFramework.instance.GameManager.GameStart();
    }
}

public class GameEnterSettingParams : UIParamBase
{
    public GameTimeTypeEnum GameTimeType = GameTimeTypeEnum.infinite;
    public HeathScaleTypeEnum HeathScaleType = HeathScaleTypeEnum.one;
}