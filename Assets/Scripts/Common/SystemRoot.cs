using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:业务系统基类
*************************************************************/

public class SystemRoot : MonoBehaviour
{
    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;
    protected NetSvc netSvc = null;

    public  virtual void InitSys()
    {
        resSvc= ResSvc.Instance;
        audioSvc= AudioSvc.Instance;
        netSvc= NetSvc.Instance;
    }
}
