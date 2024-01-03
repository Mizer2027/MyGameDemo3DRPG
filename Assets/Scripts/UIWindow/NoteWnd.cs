using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:笔记界面 显示笔记具体内容
*************************************************************/

public class NoteWnd : WindowRoot
{
    private Button btn_CloseWnd;
    private void Awake()
    {
        btn_CloseWnd=transform.Find("NotePanel/btn_CloseWnd").GetComponent<Button>();
        btn_CloseWnd.onClick.AddListener(ClickCloseWnd);
    }

    private void ClickCloseWnd()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        PlayerController.Instance.IsControl(true);
        SetActive(gameObject,false);
    }

    protected override void InitWnd()
    {
        base.InitWnd();

    }
}
