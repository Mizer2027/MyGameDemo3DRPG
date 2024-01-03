using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:加载进度界面
*************************************************************/

public class LoadingWnd : WindowRoot
{
    //引用
    private Text txt_Tips;
    private Text txt_LoadingProgress;
    private Image img_LoadingFieldBg;
    private Transform emp_LoadingPoint;

    //自定义成员变量
    private float LoadingFieldBgWidth;//进度条的宽度

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
        //设置进度条初始位置
        SetText(txt_Tips, "天上没有掉馅饼这回事");
        SetText(txt_LoadingProgress, "0%");
        img_LoadingFieldBg.fillAmount = 0;
        emp_LoadingPoint.transform.localPosition = new Vector3(-LoadingFieldBgWidth/2, 0, 0);
    }

    //设置进度条进度
    public void SetProgress(float progress)
    {
        SetText(txt_LoadingProgress, (int)(progress * 100) + "%");
        img_LoadingFieldBg.fillAmount = progress;

        float poxX = progress * LoadingFieldBgWidth - LoadingFieldBgWidth / 2;
        emp_LoadingPoint.transform.localPosition = new Vector3(poxX, 0, 0);
    }
}
