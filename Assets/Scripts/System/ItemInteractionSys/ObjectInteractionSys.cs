using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;


/************************************************************
����:Mizer  
����:2807665129@qq.com

����:���彻��ϵͳ �������������Ľ����߼�
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

    //����������߻��е���Ϣ
    public  void HandelHitInfo(RaycastHit hit)
    {
        //�жϼ�⵽�Ķ���
        switch (hit.collider.tag)
        {
            case "Item":
                HandelItemInteraction(hit);
                break;
        }
    }

    //������Ʒ����
    private void HandelItemInteraction(RaycastHit hit)
    {
        switch (hit.collider.gameObject.name)
        {
            case "Book":                  
                //��Book����
                noteWnd.SetWndState(true);
                //�رս�ɫ����
                PlayerController.Instance.IsControl(false);
                Debug.Log("��ҵ����book");
                break;
            case "Door":
                //������Ž���
                HandelDoorInteraction(hit);
                Debug.Log("��ҵ����");
                break;

        }
            
    }

    private void HandelDoorInteraction(RaycastHit hit)
    {
        Animation anin = hit.collider.gameObject.GetComponent<Animation>();
        //�Ƿ����ڿ��Ź���
        if (!anin.isPlaying)
        {
            //�жϵ�ǰ���ǿ���״̬���ǹص�״̬
            if (hit.collider.transform.rotation == Quaternion.identity)//Ĭ��״̬ ��
            {
                AnimationState animationState = anin["Door"];
                animationState.speed = 1;//�����ٶ�Ϊ��ֵ����
                                         //animationState.normalizedTime = 1;
                anin.Play("Door");
                //����
                //hit.collider.transform.rotation= 
                //   Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -90, 0)),1f);
            }
            else
            {
                AnimationState animationState = anin["Door"];
                animationState.speed = -1;//�����ٶ�Ϊ��ֵ����
                animationState.normalizedTime = 1;
                anin.Play("Door");

                //hit.collider.transform.rotation = Quaternion.identity;
            }
        }
        else { return; }
        
    }
}
