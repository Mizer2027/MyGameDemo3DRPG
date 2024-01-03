using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:玩家控制界面
*************************************************************/

public class PlayerControlWnd : WindowRoot
{
    private Button btn_Menu;
    private Button btn_ChangeView;
    private void Awake()
    {
        btn_Menu = transform.Find("btn_Menu").GetComponent<Button>();
        btn_Menu.onClick.AddListener(ClickMenuBtn);

        btn_ChangeView=transform.Find("btn_ChangeView").GetComponent <Button>();
        btn_ChangeView.onClick.AddListener(ClickChangeView);
    }

    //点击切换视角按钮
    private void ClickChangeView()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        CameraController.FpsOrTpsChange();
    }

    //点击主菜单按钮
    private void ClickMenuBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        //打开主菜单窗口
        MainGameSys.Instance.OpenMainMenuWnd();
    }

}
