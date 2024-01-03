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
        if (Input.GetMouseButtonDown(0))//判断鼠标左键是否被单击
        {
            // 创建一条点击位置为光标位置的射线
            Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);
            //将射线以黄色的表示出来
            Debug.DrawRay(rays.origin, rays.direction * RayDistance, Color.yellow);

            //创建一个RayCast变量用于存储返回信息
            RaycastHit hit;
            //将创建的射线投射出去并将反馈信息存储到hit中
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
