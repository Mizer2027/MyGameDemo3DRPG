using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��Ϸ������� ��ʼ������ϵͳ �����������
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
        //����ģ���ʼ��
        NetSvc netSvc =GetComponent<NetSvc>();
        netSvc.InitSvc();
        ResSvc resSvc= GetComponent<ResSvc>();
        resSvc.InitSvc();
        AudioSvc audioSvc= GetComponent<AudioSvc>();
        audioSvc.InitSvc();
        //ҵ��ϵͳ��ʼ��
        LoginSys loginSys = GetComponent<LoginSys>();
        loginSys.InitSys();
        CreateSys createSys = GetComponent<CreateSys>();
        createSys.InitSys();
        MainGameSys mainGameSys=GetComponent<MainGameSys>();
        mainGameSys.InitSys();
        ObjectInteractionSys objectInteractionSys = GetComponent<ObjectInteractionSys>();
        objectInteractionSys.InitSys(); 
        
        //������볡����������ӦUI
        loginSys.EnterLogin();
       
    }

    //���ӵ�����ʾ
    public static void AddTips(string tips)
    {
        Instance.tipShowWnd.AddTips(tips);
    }

    //��ǰ�ͻ����������
    private PlayerData playerData = null;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
    //���õ�ǰ�������
    public void SetPlayerData(RspLogin data)
    {
        this.playerData = data.playerData;
    }
    //�����������
    public void SetPlayerName(string name)
    {
        this.playerData.name = name;
    }

}
