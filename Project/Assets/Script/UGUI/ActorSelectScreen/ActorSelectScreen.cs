using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ActorSelectScreen : UIScreenBase
{
    public GameObject ActorPrefab;

    public float horizonSpace;
    public float verticalSpace;
    
    public ActorSelectCtrl selectCtrl;

    private DoubleLinkList<ActorSelectUIPrefab> _actorList=new DoubleLinkList<ActorSelectUIPrefab>();
    private KeySignalContainer _input;
    private ActorSelectScreenParam _actorSelectScreenParam;

    //两个角色的当前
    private Node<ActorSelectUIPrefab> _currentPlayer1;
    private Node<ActorSelectUIPrefab> _currentPlayer2;
    
    //是否选择完成
    private bool player1Confirm;
    private bool player2Confirm;
    public void Start()
    {
        _actorSelectScreenParam=new ActorSelectScreenParam();
        
        //初始化角色列表，调整对应位置
        RectTransform initRect = ActorPrefab.GetComponent<RectTransform>();
        var anchoredPosition = initRect.anchoredPosition;
        float initX=anchoredPosition.x;
        float initY = anchoredPosition.y;
        float posX = anchoredPosition.x;
        float posY = anchoredPosition.y;
        
        //长宽
        var sizeDelta = initRect.sizeDelta;
        float width = sizeDelta.x;
        float height = sizeDelta.y;
        
        //默认每行个数
        const int number = 4;
        int currentNum = 1;
        
        foreach (ActorEnum e in Enum.GetValues(typeof(ActorEnum)))
        {
            GameObject actorPrefab = Instantiate(ActorPrefab, selectCtrl.BackGroundImage.GetComponent<RectTransform>());
            ActorSelectUIPrefab selectUIPrefab = actorPrefab.GetComponent<ActorSelectUIPrefab>();
            _actorList.AddElem(selectUIPrefab);
            selectUIPrefab.Init(e,selectCtrl.SelectUIDictionary.actorImage[e]);
            
            //调整位置
            RectTransform trans = actorPrefab.GetComponent<RectTransform>();
            trans.anchoredPosition = new Vector2(posX, posY);
            //计算下一图片位置
            if (currentNum == number)
            {
                posX = initX;
                posY += height + verticalSpace;
                currentNum = 1;
            }
            else
            {
                posX += width + horizonSpace;
                currentNum++;
            }
        }
    }

    //设置GIF
    private void GIFSet(PlayerEnum e, bool state)
    {
        if (e == PlayerEnum.player_1)
        {
            //selectCtrl.Player1_Gif.GetComponent<GifToSprite>().enabled = state;
            selectCtrl.Player1_Gif.GetComponent<ActorSelectGif>().enabled = state;
        }else if (e == PlayerEnum.player_2)
        {
            //selectCtrl.Player2_Gif.GetComponent<GifToSprite>().enabled = state;
            selectCtrl.Player2_Gif.GetComponent<ActorSelectGif>().enabled = state;
        }
    }

    //根据角色变化更改画面指向和待选图片
    private void SetArrow(ActorSelectUIPrefab selectActor,PlayerEnum e)
    {
        if (e == PlayerEnum.player_1)
        {
            selectCtrl.Player1Direct.GetComponent<RectTransform>()
                .SetParent(selectActor.GetComponent<RectTransform>(), false);

            var sprite = _currentPlayer1.Data.ActorImage.sprite;
            selectCtrl.Player1_Select.sprite = sprite;
        }else if (e == PlayerEnum.player_2)
        {
            selectCtrl.Player2Direct.GetComponent<RectTransform>()
                .SetParent(selectActor.GetComponent<RectTransform>(), false);
            
            var sprite = _currentPlayer2.Data.ActorImage.sprite;
            selectCtrl.Player2_Select.sprite = sprite;
        }
        
    }
    
    private void ActorSelectJudge(PlayerEnum e)
    {
        if (e == PlayerEnum.player_1 && !player1Confirm)
        {
            player1Confirm = true;
            _actorSelectScreenParam.player1 = _currentPlayer1.Data.e;
            GIFSet(PlayerEnum.player_1,false);
        }else if (e == PlayerEnum.player_2 && !player2Confirm)
        {
            player2Confirm = true;
            _actorSelectScreenParam.player2 = _currentPlayer2.Data.e;
            GIFSet(PlayerEnum.player_2,false);
        }
        
        if (player1Confirm && player2Confirm)
        {
            //确认选择完成，发布事件
            EventManager.UIActorSelectEvent.BoradCastEvent(_actorSelectScreenParam);
            
            //弹出游戏进入前设置
            UIManager.instance.ShowUI<GameEnterSettingScreen>();
        }
    }
    public override void OnDisplay()
    {
        base.OnDisplay();
        
        if(_input is null)
                _input = GameFramework.instance._service.input;
       
        //设置未选择
        player1Confirm = false;
        player2Confirm = false;
        
        //两位角色的指针指向第一个角色
        selectCtrl.Player1Direct.GetComponent<RectTransform>()
            .SetParent(_actorList.Head.Data.GetComponent<RectTransform>(), false);
        selectCtrl.Player2Direct.GetComponent<RectTransform>()
            .SetParent(_actorList.Head.Data.GetComponent<RectTransform>(), false);
        
        //设置带选框图片
        var sprite = _actorList.Head.Data.ActorImage.sprite;
        selectCtrl.Player1_Select.sprite = sprite;
        selectCtrl.Player2_Select.sprite = sprite;
        
        //当前选择
        _currentPlayer1 = _actorList.Head;
        _currentPlayer2 = _actorList.Head;
        
        //恢复GIF
        GIFSet(PlayerEnum.player_1,true);
        GIFSet(PlayerEnum.player_2,true);
        
        //如果玩家2为电脑玩家，进行随机选择并锁死
        if (GameFramework.instance._service.SettingManager.GameModeSetting.GameMode ==
            GameModeTypeEnum.playerToComputer)
        {
            int num = Random.Range(0, _actorList.Count() - 1);
            _currentPlayer2 = _actorList.GetNode(num);
            SetArrow(_currentPlayer2.Data, PlayerEnum.player_2);
            ActorSelectJudge(PlayerEnum.player_2);
        }
    }

    public override void OnHide()
    {
        base.OnHide();
        
    }

    public override void MyUpdate()
    {
        base.MyUpdate();

        //在显示时根据按键输入调整
        if (!player1Confirm)
        {
            if (_input.IsPressed(MyKeyCode.leftArrow_1))
            {
                _currentPlayer1 = _currentPlayer1.PreNode;
                SetArrow(_currentPlayer1.Data,PlayerEnum.player_1);
            }else if (_input.IsPressed(MyKeyCode.rightArrow_1))
            {
                _currentPlayer1 = _currentPlayer1.NextNode;
                SetArrow(_currentPlayer1.Data,PlayerEnum.player_1);
            }else if (_input.IsPressed(MyKeyCode.attack1_1))
            {
                //确认选择
                ActorSelectJudge(PlayerEnum.player_1);
            }
        }

        if (!player2Confirm)
        {
            if (_input.IsPressed(MyKeyCode.leftArrow_2))
            {
                _currentPlayer2 = _currentPlayer2.PreNode;
                SetArrow(_currentPlayer2.Data,PlayerEnum.player_2);
            }else if (_input.IsPressed(MyKeyCode.rightArrow_2))
            {
                _currentPlayer2 = _currentPlayer2.NextNode;
                SetArrow(_currentPlayer2.Data,PlayerEnum.player_2);
            }else if (_input.IsPressed(MyKeyCode.attack1_2))
            {
                //确认选择
                ActorSelectJudge(PlayerEnum.player_2);
            }
        }
    }

    private ActorSelectUIPrefab player1;
    private ActorSelectUIPrefab player2;
}

public class ActorSelectScreenParam : UIParamBase
{
    public ActorEnum player1;
    public ActorEnum player2;
}
