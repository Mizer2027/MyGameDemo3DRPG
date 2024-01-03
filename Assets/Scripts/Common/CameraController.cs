using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniversalMobileController;
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:相机控制
*************************************************************/

public class CameraController : MonoBehaviour
{
    //joystick小眼睛
    public Eyejoystick joy;
    //移动端触摸板
    public SpecialTouchPad touchPad;
    //相机自身位置
    private static Transform cameratran;
    //人物身体位置
    public Transform body;
    //人物中心焦点位置
    public Transform focus;
    //人物头的位置
    public Transform head;
    //小眼睛位置
    public   Transform eye;
    public Transform cameraFllowPos;
    //第三人称的位置
    private static Vector3 thirdPosition;
    
    //手机旋转角度速度
    public float andriodCameraSpeed = 120;
    public float mousex;
    public float mousey;

    private void Awake()
    {
        joy= GameObject.Find("GameRoot/GameCanvas/PlayerControlWnd/EyeJoyStick Touch Pad").GetComponent<Eyejoystick>();
        touchPad = GameObject.Find("GameRoot/GameCanvas/PlayerControlWnd/TurnAndLookTouchpad").GetComponent<SpecialTouchPad>();
    }
    private void Start()
    {
        cameratran = transform;
        //第三人称相机的本地位置
        thirdPosition = cameratran.localPosition;
        cameratran.localPosition = Vector3.zero;


    }
    void Update()
    {

        FpsOrTpsChangePC();
        //CameraRotate();
        CameraRotatePC();
     
    }
    public void CameraRotatePC()
    {
        if(PlayerController.Instance.gameObject.GetComponent<PlayerController>().isActiveAndEnabled)
        {
#if UNITY_STANDALONE
            PlayerController.Instance.CameraRotatePC(body, focus);
#endif
#if UNITY_ANDROID
            CameraRotate();
#endif
        }
       
    }
    //相机旋转Android
    public void CameraRotate()
    {
        //移动端
        mousex = touchPad.GetHorizontalValue();
        //小眼睛
        float mousex1 = joy.GetHorizontalValue();
        float mousey1 = joy.GetVerticalValue();


        if (mousex != 0)
        {
            //身体旋转
            body.Rotate(Vector3.up, mousex * andriodCameraSpeed * Time.deltaTime);

        }
        if (mousex1 != 0)
        {
            //身体旋转
            eye.Rotate(Vector3.up, mousex1 * andriodCameraSpeed * Time.deltaTime);
            
        }

        //float mousey = Input.GetAxis("Mouse Y");
        //移动端
        mousey = touchPad.GetVerticalValue();
       
        if (mousey != 0)
        {
            //焦点旋转
            focus.Rotate(Vector3.left, mousey * andriodCameraSpeed * Time.deltaTime);
            //头旋转
            //head.Rotate(Vector3.left, mousey * andriodCameraSpeed * Time.deltaTime);

        }
        //如果身体到焦点的角度大于60，旋转速度减小为负
        if (Vector3.Angle(body.forward, focus.forward) > 50)
        {
            focus.Rotate(Vector3.left, -mousey * andriodCameraSpeed * Time.deltaTime);
        }
        //如果身体到头的角度大于60，旋转速度减小为负
        if (Vector3.Angle(body.forward, head.forward) > 50)
        {
            head.Rotate(Vector3.left, -mousey * andriodCameraSpeed * Time.deltaTime);
        }
    }
    //第三人称第一人称切换
    public static void FpsOrTpsChange()
    {

#if UNITY_ANDROID
        if (cameratran.localPosition == thirdPosition)
        {
            cameratran.localPosition = Vector3.zero;
        }
        else
        {
            cameratran.localPosition = thirdPosition;
        }
#endif
    }

    public void FpsOrTpsChangePC()
    {
#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (cameratran.localPosition == thirdPosition)
            {
                cameratran.localPosition = Vector3.zero;
            }
            else
            {
                cameratran.localPosition = thirdPosition;
            }


        }
#endif
    }


}
