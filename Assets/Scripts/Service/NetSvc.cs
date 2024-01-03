using PENet;
using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:网络服务
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
        //设置日志
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
        //客户端连接
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
    //发送消息
    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
            
        }
        else
        {
            GameRoot.AddTips("服务器未连接！！");
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
    //处理消息 
    private void ProcessMsg(GameMsg msg)
    {
        if (msg.err != (int)ErrorCode.None)
        {
           switch((ErrorCode)msg.err)
            {
                case ErrorCode.UpdataDBError:
                    PECommon.Log("数据库更新异常",LogTypes.Error);
                    GameRoot.AddTips("网络不稳定");
                    break;
                case ErrorCode.AcctIsOnline://账号在线
                    GameRoot.AddTips("当前账号已经上线！");
                    break;
                case ErrorCode.WrongPassword://密码错误
                    GameRoot.AddTips("密码错误！");
                    break;
                case ErrorCode.NameIsExit:
                    GameRoot.AddTips("昵称已被使用");
                    break;
            }
            return;
        }
        switch ((CMD)msg.cmd)
        {
            case CMD.RspLogin: //服务器回复 登入
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
