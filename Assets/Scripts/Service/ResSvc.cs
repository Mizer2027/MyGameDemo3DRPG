using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:资源加载服务
*************************************************************/

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance = null;

    private Action prgCB = null;//进度加载完成回调
    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    public  void  InitSvc()
    {
        InitRDNameCfg();
        Instance = this;
        Debug.Log("ResSvc Init..");
    }



    private void Update()
    {
        if (prgCB != null)
        {
            prgCB();
        }
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="loaded"></param>
    public void AsyncLoadScene(string sceneName,Action loaded)
    {
        //加载进度条界面
        GameRoot.Instance.loadingWnd.SetWndState(true);

        AsyncOperation asyncOperation= SceneManager.LoadSceneAsync(sceneName);

        //处理进度条逻辑
        prgCB = () =>
        {

            float progress = asyncOperation.progress;//操作的进度
            GameRoot.Instance.loadingWnd.SetProgress(progress);
            if (progress == 1)
            {
                //加载完成
                if(loaded != null)
                {
                    loaded();
                }
                prgCB = null;
                asyncOperation = null;

                GameRoot.Instance.loadingWnd.SetWndState(false);
            }
        };
       
    
    }

    /// <summary>
    /// 加载音效
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public AudioClip LoadAudio(string path,bool cache= false)
    {
        AudioClip audioClip = null;
        if(!adDic.TryGetValue(path, out audioClip))
        {
            audioClip = Resources.Load<AudioClip>(path);
            if(cache)
            {
                adDic.Add(path, audioClip);
            }
        }
        
        return audioClip;
    }

    #region InitCfgs

    private List<string> surnameList = new List<string>();
    private List<string> manList = new List<string>();
    private List<string> womanList = new List<string>();

    //初始化名字配置表
    private void InitRDNameCfg()
    {
        TextAsset xml = Resources.Load<TextAsset>(PathDefine.RDNameCfg);
        if(xml==null)
        {
            Debug.LogError("xml file:" + PathDefine.RDNameCfg + "not exist!");
        }
        else
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml.text);
            XmlNodeList nodList = xmlDocument.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < nodList.Count; i++)
            {             
                XmlElement xmlElement = nodList[i] as XmlElement;
                if (xmlElement == null)
                {
                    continue;
                }
                int ID = Convert.ToInt32(xmlElement.GetAttributeNode("ID").InnerText);
                foreach(XmlElement xmlEle in nodList[i].ChildNodes)
                {
                    switch (xmlEle.Name)
                    {
                        case "surname":
                            surnameList.Add(xmlEle.InnerText);
                            break;
                        case "man":
                            manList.Add(xmlEle.InnerText);
                            break;
                        case "woman":
                            womanList.Add(xmlEle.InnerText);
                            break;

                    }
                }
            }
        }
    }

    public string GetRDNameData(bool man= true)
    {
        System.Random random = new System.Random();
        string rdName = surnameList[Tool.RDInt(0,surnameList.Count-1)];
        if(man)
        {
            rdName += manList[Tool.RDInt(0, manList.Count - 1)];
        }
        else
        {
            rdName += womanList[Tool.RDInt(0, womanList.Count - 1)];
        }
        return rdName;
    }

    #endregion
}
