using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:主场景游戏系统
*************************************************************/

public class MainGameSys : SystemRoot
{
    public static MainGameSys Instance = null;

    public MainMenuWnd mainMenuWnd;
    public PlayerControlWnd playerControlWnd;
    public GameFailureWnd gameFailureWnd;
    public GameWinWnd gameWinWnd;
    public GameOperationTipWnd gameOperationTipWnd;
    //public 
    public override void InitSys()
    {  
        base.InitSys();
        Instance = this;
        Debug.Log("MainGameSys Init...");

    }
    public void EnterMainGame()
    {
        //初始化主场景游戏界面窗口
        InitMainGameWnd();
        

    }
    public void GameFailure()
    {
        //打开游戏失败界面
        gameFailureWnd.SetWndState(true);
        //关闭玩家控制
        PlayerController.Instance.IsControl(false);
    }
    #region 处理界面有关逻辑
    public void InitMainGameWnd()
    {
        gameFailureWnd.SetWndState(false);
#if UNITY_STANDALONE

        gameOperationTipWnd.SetWndState(true);
        
#endif
#if UNITY_ANDROID
         playerControlWnd.SetWndState(true);
#endif
    }
    public void ExitMainGameWnd()
    {
        playerControlWnd.SetWndState(false);
        mainMenuWnd.SetWndState(false);
        gameFailureWnd.SetWndState(false);
    }
    public void OpenMainMenuWnd()
    {
        mainMenuWnd.SetWndState(true);
    }

   
#endregion
}
