using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;


/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:物体交互系统 处理玩家与物体的交互逻辑
*************************************************************/

public class ObjectInteractionSys : SystemRoot
{
    public static ObjectInteractionSys Instance=null;

    public NoteWnd noteWnd;
    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
    }

    //处理被玩家射线击中的信息
    public  void HandelHitInfo(RaycastHit hit)
    {
        //判断检测到的对象
        switch (hit.collider.tag)
        {
            case "Item":
                HandelItemInteraction(hit);
                break;
        }
    }

    //处理物品交互
    private void HandelItemInteraction(RaycastHit hit)
    {
        switch (hit.collider.gameObject.name)
        {
            case "Book":                  
                //打开Book界面
                noteWnd.SetWndState(true);
                //关闭角色控制
                PlayerController.Instance.IsControl(false);
                Debug.Log("玩家点击了book");
                break;
            case "Door":
                //玩家与门交互
                HandelDoorInteraction(hit);
                Debug.Log("玩家点击门");
                break;

        }
            
    }

    private void HandelDoorInteraction(RaycastHit hit)
    {
        Animation anin = hit.collider.gameObject.GetComponent<Animation>();
        //是否正在开门关门
        if (!anin.isPlaying)
        {
            //判断当前门是开的状态还是关的状态
            if (hit.collider.transform.rotation == Quaternion.identity)//默认状态 关
            {
                AnimationState animationState = anin["Door"];
                animationState.speed = 1;//设置速度为负值即可
                                         //animationState.normalizedTime = 1;
                anin.Play("Door");
                //打开门
                //hit.collider.transform.rotation= 
                //   Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -90, 0)),1f);
            }
            else
            {
                AnimationState animationState = anin["Door"];
                animationState.speed = -1;//设置速度为负值即可
                animationState.normalizedTime = 1;
                anin.Play("Door");

                //hit.collider.transform.rotation = Quaternion.identity;
            }
        }
        else { return; }
        
    }
}
