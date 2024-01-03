            
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:网络通信协议 客户端与服务端通用
*************************************************************/

using PENet;
using System.Net;

namespace PEProtocol
{
    [Serializable]
    public class GameMsg:PEMsg
    {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqRename reqRename;
        public RspRename rspRename;
        public ReqExitLogin reqExitLogin;
        public RspExitLogin rspExitLogin;
        //public string text;
    }
    #region 登入相关
    [Serializable]
    public class ReqLogin
    {
        public string account;
        public string password;

    }
    [Serializable]
    public class RspLogin
    {
        //TODO
        public  PlayerData playerData;
    }

    [Serializable]
    public class ReqExitLogin
    {
        public string name;
    }
    [Serializable]
    public class RspExitLogin
    {
        public string name;
    }
    [Serializable]
    public class PlayerData
    {
        public int id;
        public string name;

    }

    [Serializable]
    public class ReqRename
    {
        public string name;
    }
    [Serializable]
    public class RspRename
    {
        public string name;
    }
    #endregion
    public enum ErrorCode
    {                    
        None=0,//没有错误
        UpdataDBError,//更新数据库出错
        AcctIsOnline,//账号已经上线
        WrongPassword,//密码错误
        NameIsExit,//名字已经存在
    }

    public enum CMD
    {
        None=0,
        
        ReqLogin= 101,
        RspLogin=102,
        
        ReqRename=103,
        RspRename=104,

        ReqExitLogin=105,
        RspExitLogin=106,

    }
    public class SrvCfg
    {

        public const string srvIP ="127.0.0.1";
        public const int srcPort = 8080;
    }
}