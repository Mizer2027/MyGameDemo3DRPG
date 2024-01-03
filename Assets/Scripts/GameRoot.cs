using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:游戏启动入口 初始化各个系统 保存核心数据
*************************************************************/


public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance=null;

    public LoadingWnd loadingWnd;
    public TipsShowWnd tipShowWnd;
    private void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        Debug.Log("Game Start..");
        ClearUIRoot();
        Init();
    }


    private void ClearUIRoot()
    {
        Transform canvas = transform.Find("GameCanvas");
        for(int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);
        }
        tipShowWnd.SetWndState(true);
    }
    private  void  Init()
    {
        //服务模块初始化
        NetSvc netSvc =GetComponent<NetSvc>();
        netSvc.InitSvc();
        ResSvc resSvc= GetComponent<ResSvc>();
        resSvc.InitSvc();
        AudioSvc audioSvc= GetComponent<AudioSvc>();
        audioSvc.InitSvc();
        //业务系统初始化
        LoginSys loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();
        CreateSys createSys = GetComponent<CreateSys>();
        createSys.InitSys();
        MainGameSys mainGameSys=GetComponent<MainGameSys>();
        mainGameSys.InitSys();
        ObjectInteractionSys objectInteractionSys = GetComponent<ObjectInteractionSys>();
        objectInteractionSys.InitSys(); 
        
        //进入登入场景并加载相应UI
        loginSys.EnterLogin();
       
    }

    //增加弹窗提示
    public static void AddTips(string tips)
    {
        Instance.tipShowWnd.AddTips(tips);
    }

    //当前客户端玩家数据
    private PlayerData playerData = null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    //设置当前玩家数据
    public void SetPlayerData(RspLogin data)
    {
        this.playerData = data.playerData;
    }
    //设置玩家名字
    public void SetPlayerName(string name)
    {
        this.playerData.name = name;
    }

}
