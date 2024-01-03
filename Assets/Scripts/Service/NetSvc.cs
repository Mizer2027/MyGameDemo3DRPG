using PENet;
using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:�������
*************************************************************/

public class NetSvc : MonoBehaviour
{
    public static NetSvc Instance;

    public static readonly string obj = "lock";
    PESocket<ClientSession,GameMsg> client= null;
    private Queue<GameMsg> msgQue = new Queue<GameMsg>();
    public void InitSvc()
    {
        Instance = this;
        client = new PESocket<ClientSession, GameMsg> ();
        //������־
        client.SetLog(true, (string msg, int lv) =>
        {
            switch (lv)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
        //�ͻ�������
        client.StartAsClient(SrvCfg.srvIP, SrvCfg.srcPort);
        Debug.Log("NetSvc Init..");


    }

    public void AddNetPkg(GameMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        }
    }
    //������Ϣ
    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
            
        }
        else
        {
            GameRoot.AddTips("������δ���ӣ���");
            InitSvc(); 
        }
    }
    private void Update()
    {
        if (msgQue.Count > 0)
        {
            //PECommon.Log("PackCount:" + msgQue.Count);
            lock (obj)
            {
                GameMsg gameMsg = msgQue.Dequeue();
                ProcessMsg(gameMsg);
            }
        }
    }
    //������Ϣ 
    private void ProcessMsg(GameMsg msg)
    {
        if (msg.err != (int)ErrorCode.None)
        {
           switch((ErrorCode)msg.err)
            {
                case ErrorCode.UpdataDBError:
                    PECommon.Log("���ݿ�����쳣",LogTypes.Error);
                    GameRoot.AddTips("���粻�ȶ�");
                    break;
                case ErrorCode.AcctIsOnline://�˺�����
                    GameRoot.AddTips("��ǰ�˺��Ѿ����ߣ�");
                    break;
                case ErrorCode.WrongPassword://�������
                    GameRoot.AddTips("�������");
                    break;
                case ErrorCode.NameIsExit:
                    GameRoot.AddTips("�ǳ��ѱ�ʹ��");
                    break;
            }
            return;
        }
        switch ((CMD)msg.cmd)
        {
            case CMD.RspLogin: //�������ظ� ����
                LoginSys.Instance.RspLogin(msg);
                break;
            case CMD.RspRename:
                CreateSys.Instance.RspRename(msg);
                break;
            case CMD.RspExitLogin:
                LoginSys.Instance.RspExitLogin(msg);
                break;

        }

        
    }
}
