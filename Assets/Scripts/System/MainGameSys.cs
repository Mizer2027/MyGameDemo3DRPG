using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��������Ϸϵͳ
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
        //��ʼ����������Ϸ���洰��
        InitMainGameWnd();
        

    }
    public void GameFailure()
    {
        //����Ϸʧ�ܽ���
        gameFailureWnd.SetWndState(true);
        //�ر���ҿ���
        PlayerController.Instance.IsControl(false);
    }
    #region ��������й��߼�
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
