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
        GameRoot.AddTips("服务器连接成功！");
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
        GameRoot.AddTips("服务器断开连接！");
        PECommon.Log("Server DisConnect");
    }
}
