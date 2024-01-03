using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��Դ���ط���
*************************************************************/

public class ResSvc : MonoBehaviour
{
    public static ResSvc Instance = null;

    private Action prgCB = null;//���ȼ�����ɻص�
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
    /// �첽���س���
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="loaded"></param>
    public void AsyncLoadScene(string sceneName,Action loaded)
    {
        //���ؽ���������
        GameRoot.Instance.loadingWnd.SetWndState(true);

        AsyncOperation asyncOperation= SceneManager.LoadSceneAsync(sceneName);

        //����������߼�
        prgCB = () =>
        {

            float progress = asyncOperation.progress;//�����Ľ���
            GameRoot.Instance.loadingWnd.SetProgress(progress);
            if (progress == 1)
            {
                //�������
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
    /// ������Ч
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

    //��ʼ���������ñ�
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
