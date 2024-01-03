
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:缓存层
*************************************************************/

using PEProtocol;

public class CacheSvc : SingletonTemplate<CacheSvc>
{
    private DBMgr dbMgr;
    //在线的账号 key：账号 value：客户端session
    public Dictionary<string,ServerSession> onLineAcctDic=new Dictionary<string,ServerSession>();
    public Dictionary<ServerSession,PlayerData> onLineSessionDic=new Dictionary<ServerSession,PlayerData>();

    public void Init()
    {
        dbMgr = DBMgr.Instance;
        PECommon.Log("CacheSvc Init Done..");
    }

    //判断账号是否上线
    public bool IsAcctOnline(string acct)
    {
        return onLineAcctDic.ContainsKey(acct);
    }

    //根据账号密码返回对应账号数据，密码错误返回null，账号不存在则默认创建新账号
    public PlayerData GetPalyerData(string acct,string pass)
    {
        //TODO
        //从数据库中查找账号数据
        PlayerData playerData= dbMgr.QueryPlayerData(acct, pass);
        return playerData;
    }

    //缓存上线，缓存数据
    public void AcctOnline(string acct,ServerSession session,PlayerData playerData)
    {
        onLineAcctDic.Add(acct, session);
        onLineSessionDic.Add(session, playerData);
    }
    //名字是否存在
    public bool IsNameExit(string name)
    {
        return dbMgr.QueryNameData(name); 
    }
    //获取玩家数据
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if(onLineSessionDic.TryGetValue(session, out PlayerData playerData))
        {
            return playerData;
        }
        else
        {
            return null;
        }
    }
    //更新玩家数据
    public bool UpdatePlayerData(int id,PlayerData playerData)
    {
       
        return dbMgr.UpdatePlayerData(id,playerData);
    }
    //账号下线
    public void AcctOffline(ServerSession session)
    {
        foreach(var item in onLineAcctDic)
        {
            if(item.Value == session)
            {
                  onLineAcctDic.Remove(item.Key);
                break;
            }
        }
        bool succ= onLineSessionDic.Remove(session);
        PECommon.Log("Offline Result:" + succ + "   SessionID:" + session.SessionID);
        
    }
    //账号退出登入
    public void AcctExitLogin(ServerSession session)
    {
        foreach (var item in onLineAcctDic)
        {
            if (item.Value == session)
            {
                onLineAcctDic.Remove(item.Key);
                break;
            }
        }
    }
}
