using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public GameObject thirdPersonPlayer;    //角色
    public GameObject FollowCamera;         //跟随相机
    public float CameraSmoothTime = 0;
    public float RotateSpeed = 5;
    public float CameraY = 2;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        //相机跟随旋转
        float x = RotateSpeed * Input.GetAxis("Mouse X");
        //以下为相机与角色同步旋转是
        FollowCamera.transform.rotation = Quaternion.Euler(
            FollowCamera.transform.rotation.eulerAngles +
            Quaternion.AngleAxis(x, Vector3.up).eulerAngles
        );//原理： 物体当前的欧拉角 + 鼠标x轴上的增量所产生的夹角

        thirdPersonPlayer.transform.rotation = Quaternion.Euler(
            thirdPersonPlayer.transform.rotation.eulerAngles +
            Quaternion.AngleAxis(x, Vector3.up).eulerAngles
        );//同理
          //------------------------------------------------------>>>>>>>>
          //相机跟随移动
        Vector3 TargetCameraPosition = thirdPersonPlayer.transform.TransformPoint(new Vector3(0,CameraY, -5.5f));//获取相机跟随的相对位置，再转为世界坐标

        FollowCamera.transform.position = Vector3.SmoothDamp(
            FollowCamera.transform.position,
            TargetCameraPosition,
            ref velocity,
            CameraSmoothTime, //最好为0
            Mathf.Infinity,
            Time.deltaTime
        );
    }
}
