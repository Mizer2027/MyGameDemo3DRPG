using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:ҵ��ϵͳ����
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
