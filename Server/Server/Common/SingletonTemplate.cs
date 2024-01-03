
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:单例模板
*************************************************************/

public class SingletonTemplate<T> where T : new()
{
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

}