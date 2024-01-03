using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:���ؽ��Ƚ���
*************************************************************/

public class LoadingWnd : WindowRoot
{
    //����
    private Text txt_Tips;
    private Text txt_LoadingProgress;
    private Image img_LoadingFieldBg;
    private Transform emp_LoadingPoint;

    //�Զ����Ա����
    private float LoadingFieldBgWidth;//�������Ŀ��

    private void Awake()
    {
        txt_Tips= transform.Find("MiddleDownPin/txt_Tips").GetComponent<Text>();
        txt_LoadingProgress = transform.Find("MiddleDownPin/img_LoadingFieldBg/emp_LoadingPoint/txt_LoadingProgress").GetComponent<Text>();
        img_LoadingFieldBg = transform.Find("MiddleDownPin/img_LoadingFieldBg").GetComponent<Image>();
        emp_LoadingPoint = transform.Find("MiddleDownPin/img_LoadingFieldBg/emp_LoadingPoint").GetComponent<Transform>();
    }
    protected override void InitWnd()
    {
        base.InitWnd();
        LoadingFieldBgWidth=img_LoadingFieldBg.rectTransform.sizeDelta.x;
        //���ý�������ʼλ��
        SetText(txt_Tips, "����û�е��ڱ������");
        SetText(txt_LoadingProgress, "0%");
        img_LoadingFieldBg.fillAmount = 0;
        emp_LoadingPoint.transform.localPosition = new Vector3(-LoadingFieldBgWidth/2, 0, 0);
    }

    //���ý���������
    public void SetProgress(float progress)
    {
        SetText(txt_LoadingProgress, (int)(progress * 100) + "%");
        img_LoadingFieldBg.fillAmount = progress;

        float poxX = progress * LoadingFieldBgWidth - LoadingFieldBgWidth / 2;
        emp_LoadingPoint.transform.localPosition = new Vector3(poxX, 0, 0);
    }
}
