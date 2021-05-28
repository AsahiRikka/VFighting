using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingScreen : UIScreenBase
{
    public GamingCtrl GamingCtrl;
    public ActorImageUIDictionary ImageUIDictionary;

    private void Start()
    {
        EventManager.GamingHeathEvent.AddEventHandler(PlayerHeathChange);
    }

    private void OnDestroy()
    {
        EventManager.GamingHeathEvent.RemoveEventHandler(PlayerHeathChange);
    }

    private float initTime;
    private float largeTime;
    
    public override void OnDisplay()
    {
        base.OnDisplay();
        
        //初始化生命值和图片
        GamingCtrl.Slider_1.value = 0;
        GamingCtrl.Slider_2.value = 1;

        GameStartSetting startSetting = GameFramework.instance._service.SettingManager.GameStartSetting;

        GamingCtrl.Sprite_1.sprite = ImageUIDictionary.actorImage[startSetting.Player_1];
        GamingCtrl.Sprite_2.sprite = ImageUIDictionary.actorImage[startSetting.Player_2];
        
        //开始游戏计时
        initTime = Time.time;
        
        GamingCtrl.time.text = largeTime.ToString();
        initTime += 3;

        if (startSetting.gameTime == GameTimeTypeEnum.infinite)
        {
            largeTime = -1;
            
            //无限时间的文字设置
            GamingCtrl.time.text = "无 限";
        }else if (startSetting.gameTime == GameTimeTypeEnum.min_1)
        {
            largeTime = 60;
        }else if (startSetting.gameTime == GameTimeTypeEnum.min_2)
        {
            largeTime = 120;
        }
    }

    public override void MyUpdate()
    {
        base.MyUpdate();
        
        //无限时间
        if (largeTime < 0) 
        {
            //活动时如果按下esc弹出UI
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.ShowUI<GamingQitScreen>();
                Time.timeScale = 0;
            }
        }
        else if (initTime < Time.time) 
        {
            if (Time.time - initTime >= largeTime)
            {
                //计时结束，游戏结束
                GameFramework.instance.GameManager.GameFinish();
                return;
            }
            //刷新时间
            GamingCtrl.time.text = ((int) largeTime - (int) (Time.time - initTime) - 1).ToString();
            
            //活动时如果按下esc弹出UI
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.instance.ShowUI<GamingQitScreen>();
                Time.timeScale = 0;
            }
        }
    }

    private void PlayerHeathChange(PlayerEnum e,float scale)
    {
        if (e == PlayerEnum.player_1)
        {
            GamingCtrl.Slider_1.value = 1 - scale;
        }else if (e == PlayerEnum.player_2)
        {
            GamingCtrl.Slider_2.value = scale;
        }
    }
}
