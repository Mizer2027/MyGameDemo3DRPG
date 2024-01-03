using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:���˵�����
*************************************************************/

public class MainMenuWnd : WindowRoot
{
    
    private GameObject menuPage;
    private Button btn_ContinueGame;
    private Button btn_ExitLogin;
    private Button btn_ExitGame;
    private void Awake()
    {
       
        menuPage = transform.Find("MenuPage").gameObject;
        btn_ContinueGame = transform.Find("MenuPage/btn_ContinueGame").GetComponent<Button>();
        btn_ContinueGame.onClick.AddListener(ClickContinueGameBtn);
        btn_ExitLogin = transform.Find("MenuPage/btn_ExitLogin").GetComponent<Button>();
        btn_ExitLogin.onClick.AddListener(ClickExitLoginBtn);
        btn_ExitGame = transform.Find("MenuPage/btn_ExitGame").GetComponent<Button>();
        btn_ExitGame.onClick.AddListener(ClickExitGameBtn);
    }
    //����˳���Ϸ��ť
    public  void ClickExitGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        Application.Quit();
    }

    //����˳����밴ť
    public  void ClickExitLoginBtn()
    {

        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqExitLogin
        };
        NetSvc.Instance.SendMsg(msg);
        //�ر����˵� 
        SetActive(gameObject, false);
        MainGameSys.Instance.ExitMainGameWnd();


    }

    //���������Ϸ��ť
    private void ClickContinueGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        SetActive(gameObject, false);
        PlayerController.Instance.IsControl(true);
    }
   
}
