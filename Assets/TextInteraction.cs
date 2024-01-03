using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteraction : MonoBehaviour
{
    public float smooth = 3f;
    public float RayDistance = 10f;
    Transform currentObject;
    Vector3 mouse3DPosition;

    

    // Update is called once per frame
    void Update()
    {
        ClickInteraction();

    }

    private void ClickInteraction()
    {
        if (Input.GetMouseButtonDown(0))//�ж��������Ƿ񱻵���
        {
            // ����һ�����λ��Ϊ���λ�õ�����
            Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);
            //�������Ի�ɫ�ı�ʾ����
            Debug.DrawRay(rays.origin, rays.direction * RayDistance, Color.yellow);

            //����һ��RayCast�������ڴ洢������Ϣ
            RaycastHit hit;
            //������������Ͷ���ȥ����������Ϣ�洢��hit��
            if (Physics.Raycast(rays, out hit, RayDistance))
            {

                if (hit.collider != null)
                {
                    ObjectInteractionSys.Instance.HandelHitInfo(hit);

                }

            }

        }
    }
}
