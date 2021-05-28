using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishScreen : UIScreenBase
{
    public GameFinishCtrl GameFinishCtrl;

    private const string player1 = "Player1 胜利";
    private const string player2 = "Player2 胜利";

    public override void OnDisplay()
    {
        base.OnDisplay();
        
        GameFinishCtrl.panelBtn.onClick.AddListener(PanelBtnEvent);
    }

    private void Start()
    {
        EventManager.WinnerEvent.AddEventHandler(TextSetting);
    }

    private void OnDestroy()
    {
        EventManager.WinnerEvent.RemoveEventHandler(TextSetting);
    }

    public override void MyUpdate()
    {
        base.MyUpdate();
    }

    private void PanelBtnEvent()
    {
        OnHide();
        GameFramework.instance.GameManager.GameQuit();
    }

    private void TextSetting(PlayerEnum e)
    {
        if (e == PlayerEnum.player_1)
        {
            GameFinishCtrl.finishText.text = player1;
        }else if (e == PlayerEnum.player_2)
        {
            GameFinishCtrl.finishText.text = player2;
        }
    }
}
