using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniversalMobileController;
/************************************************************
����:Mizer  
����:2807665129@qq.com

����:�������
*************************************************************/

public class CameraController : MonoBehaviour
{
    //joystickС�۾�
    public Eyejoystick joy;
    //�ƶ��˴�����
    public SpecialTouchPad touchPad;
    //�������λ��
    private static Transform cameratran;
    //��������λ��
    public Transform body;
    //�������Ľ���λ��
    public Transform focus;
    //����ͷ��λ��
    public Transform head;
    //С�۾�λ��
    public   Transform eye;
    public Transform cameraFllowPos;
    //�����˳Ƶ�λ��
    private static Vector3 thirdPosition;
    
    //�ֻ���ת�Ƕ��ٶ�
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
        //�����˳�����ı���λ��
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
    //�����תAndroid
    public void CameraRotate()
    {
        //�ƶ���
        mousex = touchPad.GetHorizontalValue();
        //С�۾�
        float mousex1 = joy.GetHorizontalValue();
        float mousey1 = joy.GetVerticalValue();


        if (mousex != 0)
        {
            //������ת
            body.Rotate(Vector3.up, mousex * andriodCameraSpeed * Time.deltaTime);

        }
        if (mousex1 != 0)
        {
            //������ת
            eye.Rotate(Vector3.up, mousex1 * andriodCameraSpeed * Time.deltaTime);
            
        }

        //float mousey = Input.GetAxis("Mouse Y");
        //�ƶ���
        mousey = touchPad.GetVerticalValue();
       
        if (mousey != 0)
        {
            //������ת
            focus.Rotate(Vector3.left, mousey * andriodCameraSpeed * Time.deltaTime);
            //ͷ��ת
            //head.Rotate(Vector3.left, mousey * andriodCameraSpeed * Time.deltaTime);

        }
        //������嵽����ĽǶȴ���60����ת�ٶȼ�СΪ��
        if (Vector3.Angle(body.forward, focus.forward) > 50)
        {
            focus.Rotate(Vector3.left, -mousey * andriodCameraSpeed * Time.deltaTime);
        }
        //������嵽ͷ�ĽǶȴ���60����ת�ٶȼ�СΪ��
        if (Vector3.Angle(body.forward, head.forward) > 50)
        {
            head.Rotate(Vector3.left, -mousey * andriodCameraSpeed * Time.deltaTime);
        }
    }
    //�����˳Ƶ�һ�˳��л�
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
