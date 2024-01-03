
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:服务端创建角色系统
*************************************************************/
using PEProtocol;

public class CreateSys : SystemRoot<CreateSys>
{
    public override void InitSys()
    {
        base.InitSys();
        PECommon.Log("CreateSys Init Done");

    }
    //处理命名消息
    public void ReqRename(MsgPack msgPack)
    {
        ReqRename data = msgPack.gameMsg.reqRename;

        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRename
        };

        //当前名字是否存在
        if (cacheSvc.IsNameExit(data.name))
        {
            //存在 返回错误码
            msg.err = (int)ErrorCode.NameIsExit;
        }
        else
        {
            //不存在 更新缓存，以及数据库，再返回给客户端
            //更新缓存
            PlayerData playerData = cacheSvc.GetPlayerDataBySession(msgPack.session);
            playerData.name = data.name;
            
            //更新数据库
            if (!cacheSvc.UpdatePlayerData(playerData.id, playerData))
            {
                //更新数据库出错
                msg.err = (int)ErrorCode.UpdataDBError;
            }
            else
            {
                msg.rspRename = new RspRename
                {
                    name = data.name,
                };
            }
        }
        msgPack.session.SendMsg(msg);

    }

}
