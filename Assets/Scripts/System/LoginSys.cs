using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:登入业务系统
*************************************************************/

public class LoginSys : SystemRoot
{
    public static LoginSys Instance;

    public LoginWnd loginWnd;
    public  override void InitSys()
    {
        base.InitSys();
        Instance = this;
        Debug.Log("LoginSys Init...");
    }

    /// <summary>
    /// 进入登入场景
    /// </summary>
    public void EnterLogin()
    {

        //异步加载登入场景
       resSvc.AsyncLoadScene(Constants.LoginScene, () =>
        {
            loginWnd.SetWndState(true);
            audioSvc.PlayBGMusic(Constants.BGStart);
            //GameRoot.AddTips("进入场景完成");
        });
       

    }

    //账号登入
    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登入成功");
        //设置玩家数据
        GameRoot.Instance.SetPlayerData(msg.rspLogin);
        //是否已经创建了角色
        if (msg.rspLogin.playerData.name == "")
        {
            //打开角色创建界面
            CreateSys.Instance.EnterCreatWnd();
            
        }
        //跳过角色创建界面
        else
        {
            //直接进入游戏主场景
            resSvc.AsyncLoadScene(Constants.GameDemoScene, () =>
            {
                audioSvc.PlayBGMusic(Constants.BGMainScence);
                MainGameSys.Instance.EnterMainGame();
                GameRoot.AddTips("欢迎回来!!" + GameRoot.Instance.PlayerData.name);
                //uiMainDemoWnd.SetWndState(true);
            });
           
        }
        //关闭登入界面
        loginWnd.SetWndState(false);
    }
    //账号退出
    public void RspExitLogin(GameMsg msg)
    {
        
        resSvc.AsyncLoadScene(Constants.LoginScene, () =>
        {
            string name = msg.rspExitLogin.name;
            GameRoot.AddTips(name + ",退出账号成功");
            loginWnd.SetWndState(true);
        });
        
    }
    
   
}
