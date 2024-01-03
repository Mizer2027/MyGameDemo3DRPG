using PEProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:������ɫ����
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
        //����ý���ʱ��ʾһ���������
        ipt_Name.text = resSvc.GetRDNameData(false);
    }

    //���������ɫ��ť
    private void ClickCreateRoleBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        if (ipt_Name.text != "")
        {
            //TODO:�����������ݵ�������  ��֤ ������Ϸ������
            GameMsg msg = new GameMsg
            {
                cmd=(int)CMD.ReqRename,
                reqRename = new ReqRename
                {
                    name = ipt_Name.text,
                }
            };
            netSvc.SendMsg(msg);
            //GameRoot.AddTips("������ɫ�ɹ�");
        }
        else
        {
            GameRoot.AddTips("��ɫ�����淶����");
        }
        
    }
    //������������ť
    private void ClickRandomNameBtn()
    {
        audioSvc.PlayeUIMusic(Constants.UIClickBtn);
        //�����ʾһ������
        ipt_Name.text= resSvc.GetRDNameData(false);
    }

}
