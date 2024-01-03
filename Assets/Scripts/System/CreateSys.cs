
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:创建角色系统
*************************************************************/

using PEProtocol;
using UnityEngine;

public class CreateSys : SystemRoot
{
    public CreateWnd createWnd;
    public static CreateSys Instance = null;
    public override void InitSys()
    {
        
        base.InitSys();
        Instance = this;
        Debug.Log("CreatSys Init...");

    }
    public void EnterCreatWnd()
    {
        createWnd.SetWndState(true);

    }
    //建立角色
    public void RspRename(GameMsg msg)
    {
        //设置客户端玩家名字
        GameRoot.Instance.SetPlayerName(msg.rspRename.name);
        GameRoot.AddTips("创建角色成功");
        //跳转到游戏主场景
        resSvc.AsyncLoadScene(Constants.GameDemoScene, () =>
        {
            audioSvc.PlayBGMusic(Constants.BGMainScence);
            //进入游戏主界面
            MainGameSys.Instance.EnterMainGame();
            GameRoot.AddTips("你好！！" + GameRoot.Instance.PlayerData.name);
            //关闭场景角色界面
            createWnd.SetWndState(false);
        });
        
    }
}
