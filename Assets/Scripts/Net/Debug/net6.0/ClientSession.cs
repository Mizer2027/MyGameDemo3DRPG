using PENet;
using PEProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSession : PESession<GameMsg> 
{
    protected override void OnConnected()
    {
        base.OnConnected();
        GameRoot.AddTips("���������ӳɹ���");
        PECommon.Log("Server Connect Success");

    }
    protected override void OnReciveMsg(GameMsg msg)
    {
        base.OnReciveMsg(msg);
        PECommon.Log("RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddNetPkg(msg);
    }
    protected override void OnDisConnected()
    {
        base.OnDisConnected();
        GameRoot.AddTips("�������Ͽ����ӣ�");
        PECommon.Log("Server DisConnect");
    }
}
