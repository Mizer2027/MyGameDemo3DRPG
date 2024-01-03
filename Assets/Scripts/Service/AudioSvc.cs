using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:�������ŷ���
*************************************************************/

public class AudioSvc : MonoBehaviour
{
    public static AudioSvc Instance;

    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitSvc()
    {
        Instance = this;
        Debug.Log("AudioSvc Init..");
    }

    public void PlayBGMusic(string name,bool isLoop = true)
    {
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        if (bgAudio.clip == null || bgAudio.clip.name !=audio.name)
        {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }

    }
    public void PlayeUIMusic(string name)
    {
        AudioClip audio =ResSvc.Instance.LoadAudio("ResAudio/"+name, true);
        uiAudio.clip = audio;
        uiAudio.Play(); 
    }
}                                                           
