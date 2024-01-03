using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:����ҵ��ϵͳ
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
    /// ������볡��
    /// </summary>
    public void EnterLogin()
    {

        //�첽���ص��볡��
       resSvc.AsyncLoadScene(Constants.LoginScene, () =>
        {
            loginWnd.SetWndState(true);
            audioSvc.PlayBGMusic(Constants.BGStart);
            //GameRoot.AddTips("���볡�����");
        });
       

    }

    //�˺ŵ���
    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("����ɹ�");
        //�����������
        GameRoot.Instance.SetPlayerData(msg.rspLogin);
        //�Ƿ��Ѿ������˽�ɫ
        if (msg.rspLogin.playerData.name == "")
        {
            //�򿪽�ɫ��������
            CreateSys.Instance.EnterCreatWnd();
            
        }
        //������ɫ��������
        else
        {
            //ֱ�ӽ�����Ϸ������
            resSvc.AsyncLoadScene(Constants.GameDemoScene, () =>
            {
                audioSvc.PlayBGMusic(Constants.BGMainScence);
                MainGameSys.Instance.EnterMainGame();
                GameRoot.AddTips("��ӭ����!!" + GameRoot.Instance.PlayerData.name);
                //uiMainDemoWnd.SetWndState(true);
            });
           
        }
        //�رյ������
        loginWnd.SetWndState(false);
    }
    //�˺��˳�
    public void RspExitLogin(GameMsg msg)
    {
        
        resSvc.AsyncLoadScene(Constants.LoginScene, () =>
        {
            string name = msg.rspExitLogin.name;
            GameRoot.AddTips(name + ",�˳��˺ųɹ�");
            loginWnd.SetWndState(true);
        });
        
    }
    
   
}
