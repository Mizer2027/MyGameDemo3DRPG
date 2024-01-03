using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:创建角色界面
*************************************************************/

public class CreateWnd : WindowRoot
{
    private InputField ipt_Name;
    private Button btn_RandomName;
    private Button btn_CreateRole;
    private void Awake()
    {
        ipt_Name= transform.Find("ipt_Name").GetComponent<InputField>();   
        btn_RandomName= transform.Find("btn_RandomName").GetComponent <Button>();
        btn_RandomName.onClick.AddListener(ClickRandomNameBtn);
        btn_CreateRole= transform.Find("btn_CreateRole").GetComponent<Button>();
        btn_CreateRole.onClick.AddListener(ClickCreateRoleBtn);
    }

    

    protected override void InitWnd()
    {
        base.InitWnd();
        //进入该界面时显示一个随机名字
        ipt_Name.text = resSvc.GetRDNameData(false);
    }

    //点击创建角色按钮
    private void ClickCreateRoleBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        if (ipt_Name.text != "")
        {
            //TODO:发送名字数据到服务器  验证 进入游戏主场景
            GameMsg msg = new GameMsg
            {
                cmd=(int)CMD.ReqRename,
                reqRename = new ReqRename
                {
                    name = ipt_Name.text,
                }
            };
            netSvc.SendMsg(msg);
            //GameRoot.AddTips("创建角色成功");
        }
        else
        {
            GameRoot.AddTips("角色名不规范！！");
        }
        
    }
    //点击随机命名按钮
    private void ClickRandomNameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        //随机显示一个名字
        ipt_Name.text= resSvc.GetRDNameData(false);
    }

}
