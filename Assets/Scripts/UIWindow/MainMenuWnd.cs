using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:主菜单窗口
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
    //点击退出游戏按钮
    public  void ClickExitGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        Application.Quit();
    }

    //点击退出登入按钮
    public  void ClickExitLoginBtn()
    {

        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqExitLogin
        };
        NetSvc.Instance.SendMsg(msg);
        //关闭主菜单 
        SetActive(gameObject, false);
        MainGameSys.Instance.ExitMainGameWnd();


    }

    //点击继续游戏按钮
    private void ClickContinueGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        SetActive(gameObject, false);
        PlayerController.Instance.IsControl(true);
    }
   
}
