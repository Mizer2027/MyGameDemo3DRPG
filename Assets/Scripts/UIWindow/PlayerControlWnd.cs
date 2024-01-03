using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��ҿ��ƽ���
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

    //����л��ӽǰ�ť
    private void ClickChangeView()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        CameraController.FpsOrTpsChange();
    }

    //������˵���ť
    private void ClickMenuBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        //�����˵�����
        MainGameSys.Instance.OpenMainMenuWnd();
    }

}
