
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:服务器初始化
*************************************************************/

public class ServerRoot:SingletonTemplate<ServerRoot>
{
    public void Init()
    {
        //数据层初始化
        DBMgr.Instance.Init();
        //服务层初始化
        CacheSvc.Instance.Init();
        NetSvc.Instance.Init();
        //系统层初始化
        LoginSys.Instance.InitSys();
        CreateSys.Instance.InitSys();
    }
  
    public void Update()
    {
        NetSvc.Instance.Update();
    }

    private int SessinID = 0;
    public int GetSessionID()
    {
        if(SessinID==int.MaxValue)
        {
            SessinID=0;
        }
        return SessinID+=1;
    }
}
