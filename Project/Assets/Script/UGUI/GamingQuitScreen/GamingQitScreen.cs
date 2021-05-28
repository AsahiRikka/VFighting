using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingQitScreen : UIScreenBase
{
    public GamingQuitCtrl GamingQuitCtrl;
    private void Start()
    {
        GamingQuitCtrl.QuitGameBtn.onClick.AddListener(QuitGameEvent);
        GamingQuitCtrl.ReturnGameBtn.onClick.AddListener(ReturnGameEvent);
    }

    private void QuitGameEvent()
    {
        GameFramework.instance.GameManager.GameQuit();
        Time.timeScale = 1;
    }

    private void ReturnGameEvent()
    {
        Time.timeScale = 1;
        OnHide();
    }
}
