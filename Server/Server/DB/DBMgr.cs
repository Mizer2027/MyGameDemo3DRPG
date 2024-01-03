
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:数据库管理类
*************************************************************/

using MySql.Data.MySqlClient;
using PEProtocol;
using System.Reflection.PortableExecutable;

public class DBMgr :SingletonTemplate<DBMgr>
{
    private MySqlConnection conn;
    public void Init()
    {
        conn = new MySqlConnection("server=localhost;User Id=root;password=;Database=mygamedemo;Charset=utf8");
        conn.Open();

        //QueryPlayerData("xx", "xxx");

        PECommon.Log("DBMgr Init Done..");
    }
    //数据库查询玩家数据
    public PlayerData QueryPlayerData(string acct,string pass)
    {
        bool isNew = true;//是否为新账号
        PlayerData playerData = null;
        //TODO
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from accounts where account = @account", conn);
            cmd.Parameters.AddWithValue("account", acct);
            reader = cmd.ExecuteReader();
            //验证密码
            if(reader.Read())
            {
                isNew = false;
                string _pass=reader.GetString("password");
                if(_pass.Equals(pass))
                {
                    //密码正确
                    playerData = new PlayerData
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name")
                        //TO ADD
                    }; 
                }
            }
        }
        catch (Exception ex)
        {
            PECommon.Log("Query PlayerData acc&pass Exception:" + ex, LogTypes.Error);
        }
        finally
        {       
            if (reader != null)
            {
                reader.Close();
            }
            if (isNew)
            {
                //不存在账号数据，创建新的默认账号数据，并返回
                playerData = new PlayerData
                {
                    id = -1,
                    name = ""
                };
                playerData.id= InsertNewAcctData(acct,pass, playerData);
            }
        }
       
        return playerData;
    }

    //插入新账号数据，返回主键id
    private int InsertNewAcctData(string acct,string pass,PlayerData pd)
    {
        int id = -1;
        try
        {
            MySqlCommand cmd = new MySqlCommand(
                "insert into accounts set account=@account,password=@password,name=@name", conn);
            cmd.Parameters.AddWithValue("account", acct);
            cmd.Parameters.AddWithValue("password", pass);
            cmd.Parameters.AddWithValue("name", pd.name);
            cmd.ExecuteNonQuery();
           
            id = (int)cmd.LastInsertedId;
           
        }
        catch (Exception ex)
        {
            PECommon.Log("nsertNewAcctData Exception:" + ex, LogTypes.Error);

        }
        return id;
    }

    //查询名字数据
    public bool QueryNameData(string name)
    {
        bool exist= false;
        MySqlDataReader reader = null;
        try
        {
            MySqlCommand cmd = new MySqlCommand("select*from accounts where name=@name", conn);
            cmd.Parameters.AddWithValue("name", name);
            reader= cmd.ExecuteReader();
            if(reader.Read())//读取到名字
            {
                exist = true;
            }
        }
        catch(Exception ex)
        {
            PECommon.Log("Query NameData  Exception:" + ex, LogTypes.Error);
        }
        finally
        {
            if(reader != null)
            {
                reader.Close();
            }
           
        }
        return exist;
    }
    
    //更新数据库
    public bool UpdatePlayerData(int id, PlayerData playerData)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(
                "update accounts set name=@name where id=@id",conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("name", playerData.name);

            cmd.ExecuteNonQuery();
        }
        catch( Exception ex )
        {
            PECommon.Log("UpdatePlayerData  Exception:" + ex, LogTypes.Error);
            return false;

        }
        return true;
    }
   
}