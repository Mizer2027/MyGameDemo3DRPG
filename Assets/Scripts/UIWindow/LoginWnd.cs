using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:登入注册界面
*************************************************************/

public class LoginWnd : WindowRoot
{
    private InputField ipt_Count;
    private InputField ipt_Password;
    private Button btn_Login;
    private Button btn_ExitGame;
    private Toggle tg_DisplayPassword;
    private Button btn_SingleplayerMode;

    private void Awake()
    {
        ipt_Count = transform.Find("emp_Login/ipt_Count").GetComponent<InputField>();
        ipt_Password = transform.Find("emp_Login/ipt_Password").GetComponent<InputField>();

        btn_Login = transform.Find("emp_Login/btn_Login").GetComponent<Button>();
        btn_Login.onClick.AddListener(ClickEnterGame);

        tg_DisplayPassword = transform.Find("emp_Login/ipt_Password/tg_DisplayPassword").GetComponent<Toggle>();
        tg_DisplayPassword.onValueChanged.AddListener(ClickDisplayPassBtn);

        btn_ExitGame =transform.Find("btn_ExitGame").GetComponent<Button> ();
        btn_ExitGame.onClick.AddListener(ClickExitGameBtn);

        btn_SingleplayerMode = transform.Find("btn_SingleplayerMode").GetComponent<Button>();
        btn_SingleplayerMode.onClick.AddListener(ClickSingleplayerModeBtn);


    }

    //点击单机模式按钮
    private void ClickSingleplayerModeBtn()
    {

        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        resSvc.AsyncLoadScene(Constants.GameDemoScene, () =>
        {
            audioSvc.PlayBGMusic(Constants.BGMainScence);
            GameRoot.AddTips("欢迎进入游戏！");
            SetActive(gameObject, false);
            MainGameSys.Instance.InitMainGameWnd();
        });
    }

    //点击退出游戏按钮
    private void ClickExitGameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        Application.Quit();
    }
    //点击显示密码按钮
    public  void ClickDisplayPassBtn(bool isShow)
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        ipt_Password.contentType = isShow ? InputField.ContentType.Standard : InputField.ContentType.Password;
        ipt_Password.Select();
    }

    protected override void InitWnd()
    {
        base.InitWnd();
        //获取本地存储的账号密码
        if (PlayerPrefs.HasKey("Account") && PlayerPrefs.HasKey("Password"))
        {
            ipt_Count.text = PlayerPrefs.GetString("Account");
            ipt_Password.text = PlayerPrefs.GetString("Password");
        }
        else
        {
            ipt_Count.text = "";
            ipt_Password.text = "";
        }
        
    }

    /// <summary>
    /// 点击进入游戏按钮
    /// </summary>
    public void ClickEnterGame()
    {
        
        audioSvc.PlayeUIMusic(Constants.UILoginBtn);

        string ipt_Account=ipt_Count.text;
        string ipt_Password= this.ipt_Password.text;
        if(ipt_Account!="" && ipt_Password!="")
        {
            //更新本地存储的数据
            PlayerPrefs.SetString("Account", ipt_Account);
            PlayerPrefs.SetString("Password", ipt_Password);

            //TODO:发生网络消息，请求登入
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqLogin,
                reqLogin = new ReqLogin
                {
                    account = ipt_Account,
                    password = ipt_Password
                }
            };
            netSvc.SendMsg(msg);
            Debug.Log("点击登入按钮");
        }
        else
        {
            GameRoot.AddTips("请输入账号密码!");
        }

    }

}
