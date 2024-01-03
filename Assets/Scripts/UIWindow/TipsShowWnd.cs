using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:弹窗显示界面
*************************************************************/

public class TipsShowWnd : WindowRoot
{
    private Animation tipsAni;
    private Text tipsText;    

    private bool isTipShow =false;
    //弹窗提示文本队列
    private Queue<string> tipsQue= new Queue<string>();

    private void Awake()
    {
        tipsAni = transform.Find("txt_TipText").GetComponent<Animation>();
        tipsText= transform.Find("txt_TipText").GetComponent<Text>();
    }

    protected override void InitWnd()
    {
        base.InitWnd();
        SetActive(tipsText, false);

    }

    public void AddTips(string tips)
    {
        lock(tipsQue)
        {
            tipsQue.Enqueue(tips);
        }
    }

    private void Update()
    {
        if(tipsQue.Count > 0&&isTipShow==false)
        {
            lock (tipsQue)
            {
                string tips=tipsQue.Dequeue();
                isTipShow=true;
                SetTips(tips);

            }
        }
    }
    /// <summary>
    ///设置弹窗提示
    /// </summary>
    /// <param name="tips"></param>
    private void SetTips(string tips)
    {
        SetActive(tipsText, true);
        SetText(tipsText, tips);

        AnimationClip animationClip = tipsAni.GetClip("TipsShowAnimation");
        tipsAni.Play();
        //延时关闭激活状态
        StartCoroutine(AniPlayDone(animationClip.length, () =>
        {
            SetActive(tipsText, false);
            isTipShow = false;
        }));

    }

    private IEnumerator AniPlayDone(float sec,Action action)
    {
        yield return new WaitForSeconds(sec);
        if(action != null)
        {
            action();
        }
    }

}
