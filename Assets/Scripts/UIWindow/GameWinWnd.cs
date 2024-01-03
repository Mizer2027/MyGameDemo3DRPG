using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:游戏胜利界面
*************************************************************/

public class GameWinWnd : WindowRoot
{
    private Button btn_Restart;
    private Button btn_ExitGame;
    private void Awake()
    {
        btn_Restart=transform.Find("GameWinPanel/btn_Restart").GetComponent<Button>();
        btn_Restart.onClick.AddListener(ClickReStartBtn);
        btn_ExitGame = transform.Find("GameWinPanel/btn_ExitGame").GetComponent<Button>();
        btn_ExitGame.onClick.AddListener(ClickeExitGameBtn);

    }

    private void ClickeExitGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        Application.Quit();
    }

    private void ClickReStartBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        PlayerController.Instance.InitPlayer();
        AISys.Instance.AIInit();
        SetWndState(false);
    }

    protected override void InitWnd()
    {
        base.InitWnd();

    }
}
