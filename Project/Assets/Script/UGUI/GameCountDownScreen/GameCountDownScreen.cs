using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameCountDownScreen : UIScreenBase
{
    public GameCountDownCtrl GameCountDownCtrl;

    private float initTime;
    public override void OnDisplay()
    {
        base.OnDisplay();

        two = false;
        one = false;
        
        //开始倒数
        GameCountDownCtrl.CountDownText.text = "3";
        initTime = Time.time;
    }

    private bool two;
    private bool one;
    public override void MyUpdate()
    {
        base.MyUpdate();

        if (Time.time - initTime > 1f && !two)
        {
            two = true;
            GameCountDownCtrl.CountDownText.text = "2";
        }

        if (Time.time - initTime > 2f && !one)
        {
            one = true;
            GameCountDownCtrl.CountDownText.text = "1";
        }

        if (Time.time - initTime > 3f)
        {
            OnHide();
        }
    }
}
