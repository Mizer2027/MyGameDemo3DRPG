using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��Ϸʧ�ܽ���
*************************************************************/

public class GameFailureWnd : WindowRoot
{
    private Button btn_Restart;
    private Button btn_ExitLogin;
    private Button btn_ExitGame;
    private void Awake()
    {
        btn_Restart= transform.Find("GameFailurePanel/btn_Restart").GetComponent<Button>();
        btn_Restart.onClick.AddListener(ClickRestartBtn);
        btn_ExitLogin = transform.Find("GameFailurePanel/btn_ExitLogin").GetComponent<Button>();
        btn_ExitLogin.onClick.AddListener(ClickExitLoginBtn);
        btn_ExitGame = transform.Find("GameFailurePanel/btn_ExitGame").GetComponent<Button>();
        btn_ExitGame.onClick.AddListener(ClickExitGameBtn);

    }

    private void ClickExitGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        Application.Quit();
    }

    private void ClickExitLoginBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqExitLogin
        };
        NetSvc.Instance.SendMsg(msg);
        MainGameSys.Instance.ExitMainGameWnd();
    }

    private void ClickRestartBtn()
    {
        Debug.Log("��������¿�ʼ��ť");
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
