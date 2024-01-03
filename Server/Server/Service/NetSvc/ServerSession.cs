
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:网络会话连接
*************************************************************/
using PENet;
using PEProtocol;


public class ServerSession :PESession<GameMsg>
{
    public int SessionID = 0;
    protected override void OnConnected()
    {
        base.OnConnected();
        SessionID= ServerRoot.Instance.GetSessionID();
        PECommon.Log("SessionID:"+SessionID+"   Client Connect");
        
    }
    protected override void OnReciveMsg(GameMsg msg)
    {
        base.OnConnected();
        PECommon.Log("SessionID:" + SessionID + "Client resMsg:" +((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddMsgQue(this,msg);
       
    }
    protected override void OnDisConnected()
    {
        base.OnConnected();
        LoginSys.Instance.ClearOfflineData(this);
        PECommon.Log("SessionID:" + SessionID + "Client DisConnect");
       
    }
}
