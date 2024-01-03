
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:服务器入口
*************************************************************/


public  class ServerStart:SingletonTemplate<ServerStart>
{
    private static void Main(string[] args)
    {
        ServerRoot.Instance.Init();

        while (true)
        {
             ServerRoot.Instance.Update();
        }
    }
}