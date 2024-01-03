using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:�ʼǽ��� ��ʾ�ʼǾ�������
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
