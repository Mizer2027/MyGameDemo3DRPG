
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:登入系统
*************************************************************/

using PEProtocol;

public class LoginSys :SystemRoot<LoginSys>
{
    public  override void InitSys()
    {
        base.InitSys();
        PECommon.Log("LoginSys Init Done");
    }

    //处理请求登入消息
    public void ReqLogin(MsgPack msgPack)
    {
        ReqLogin data = msgPack.gameMsg.reqLogin;
        
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspLogin
            
        };
        // 当前账号是否上线
        if (cacheSvc.IsAcctOnline(data.account))
        {   
            //已上线：返回错误信息
            msg.err = (int)ErrorCode.AcctIsOnline; 
        }
        else
        {
            //未上线：
            //账号是否存在
            PlayerData pd = cacheSvc.GetPalyerData(data.account, data.password);
            if (pd == null)
            {
                //不存在：创建输入的账号密码
                msg.err= (int)ErrorCode.WrongPassword;
            }
            else
            {
                //存在：检测密码
                msg.rspLogin = new RspLogin
                {
                    
                    playerData = pd
                };
                //缓存账号数据
                cacheSvc.AcctOnline(data.account, msgPack.session, pd);

            }


        }


        //回应客户端

        msgPack.session.SendMsg(msg);
    }

    //处理请求退出登入消息
    public void ReqExitLogin(MsgPack msgPack)
    {
        string name= cacheSvc.GetPlayerDataBySession(msgPack.session).name;
        cacheSvc.AcctOffline(msgPack.session);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspExitLogin,
            rspExitLogin=new RspExitLogin { name = name } ,
            
        };
        msgPack.session.SendMsg(msg);
        
    }

    
    //清除缓存下线数据
    public void ClearOfflineData(ServerSession session)
    {
        cacheSvc.AcctOffline(session);
    }
}

