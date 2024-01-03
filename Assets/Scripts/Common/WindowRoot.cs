using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:UI界面基类
*************************************************************/

public class WindowRoot : MonoBehaviour
{
    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;
    public void SetWndState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            gameObject.SetActive(isActive);
        }
        if(isActive)
        {
            InitWnd();
        }
        else
        {
            ClearWnd();
        }
    }

    protected virtual void InitWnd()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
        netSvc = NetSvc.Instance;
    }
    protected virtual void ClearWnd()
    {
        resSvc = null;
        audioSvc = null;
        netSvc = null;
    }

    #region Tool Functions
    protected void SetText(Text text,string context = "")
    {
        text.text = context;
    }
    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(),num);
    }
    protected void SetText(Transform trans, string context = "")
    { 
        SetText(trans.GetComponent<Text>(), context); 
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }



    protected void SetActive(GameObject go,bool state = true)
    {
        go.SetActive(state);
    }
    protected void SetActive(Transform trans, bool state = true)
    { 
        trans.gameObject.SetActive(state); 
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    { 
        rectTrans.gameObject.SetActive(state); 
    }
    protected void SetActive(Image img, bool state = true)
    { 
        img.transform.gameObject.SetActive(state); 
    }
    protected void SetActive(Text txt, bool state = true)
    {
        txt.transform.gameObject.SetActive(state); 
    }

    #endregion
}
