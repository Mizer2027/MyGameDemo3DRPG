using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:������ʾ����
*************************************************************/

public class TipsShowWnd : WindowRoot
{
    private Animation tipsAni;
    private Text tipsText;    

    private bool isTipShow =false;
    //������ʾ�ı�����
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
    ///���õ�����ʾ
    /// </summary>
    /// <param name="tips"></param>
    private void SetTips(string tips)
    {
        SetActive(tipsText, true);
        SetText(tipsText, tips);

        AnimationClip animationClip = tipsAni.GetClip("TipsShowAnimation");
        tipsAni.Play();
        //��ʱ�رռ���״̬
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
