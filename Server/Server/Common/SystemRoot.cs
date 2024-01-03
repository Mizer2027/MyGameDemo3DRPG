
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:服务端系统基类
*************************************************************/

public class SystemRoot<T> where T : new()
{
    public NetSvc netSvc = null;
    public CacheSvc cacheSvc = null;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }
    public  virtual void InitSys()
    {
        netSvc = NetSvc.Instance;
        cacheSvc = CacheSvc.Instance;
    }

}