
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:网络服务
*************************************************************/

using PENet;
using PEProtocol;

public class MsgPack
{
    public ServerSession session;
    public GameMsg gameMsg;
    public MsgPack(ServerSession session, GameMsg gameMsg)
    {
        this.session = session;
        this.gameMsg = gameMsg;
    }
}

public class NetSvc:SingletonTemplate<NetSvc>
{

    public static readonly string obj = "lock";
    private Queue<MsgPack> msgPackQue=new Queue<MsgPack>();
    public void Init()
    {
         PESocket<ServerSession,GameMsg> server= new PESocket<ServerSession,GameMsg>();
        //连接服务器
        server.StartAsServer(SrvCfg.srvIP, SrvCfg.srcPort);

        PECommon.Log("NetSvc Init Done..");
    }
    //增加消息到队列中
    public void AddMsgQue(ServerSession session,GameMsg msg)
    {
        lock (obj)
        {
            msgPackQue.Enqueue(new MsgPack(session,msg));
        }
    }
    
    //更新网络数据包
    public void Update()
    {
        if (msgPackQue.Count > 0)
        {
            PECommon.Log("PackCount:"+msgPackQue.Count);
            lock(obj)
            {
                MsgPack msgPack = msgPackQue.Dequeue();
                HandleOutMsg(msgPack);
            }
        }
    }
    //消息分发处理
    private void HandleOutMsg(MsgPack msgPack)
    {
        switch ((CMD)msgPack.gameMsg.cmd)
        {
            case CMD.ReqLogin:
                LoginSys.Instance.ReqLogin(msgPack);
                break;
            case CMD.ReqRename:
                CreateSys.Instance.ReqRename(msgPack);
                break;
            case CMD.ReqExitLogin:
                LoginSys.Instance.ReqExitLogin(msgPack);
                break;

        }
    }
}
