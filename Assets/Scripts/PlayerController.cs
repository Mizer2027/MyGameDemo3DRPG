using UniversalMobileController;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System;
/************************************************************
����:Mizer  
����:2807665129@qq.com

����:��ҿ����߼�
*************************************************************/

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance=null;
    //��Ҳ�����
    public FloatingJoyStick joystick;

    public float moveSpeed = 3; //�ƶ��ٶ�
    public float upForce = 20; //��Ծ��
    public float cameraSpeed = 300f;//���������
    public Vector3 dir;
    public float horizontal;
    public float vertical;
  
    //�Ƿ��ڵ���
    public bool isGround = true;
    //����ܴ����ľ��뷶Χ
    public float rayDistance = 5;
    //��ҳ�ʼλ��
    private Vector3 playerInitPos;
    private Quaternion playerInitRot;

    public bool isDead;
    private Transform body;
    private Rigidbody rb;
    private Animator ani;
        

    private void Awake()
    {
        Instance= this;
        playerInitPos = transform.position;
        playerInitRot = transform.rotation;

        joystick = GameObject.Find("GameRoot/GameCanvas/PlayerControlWnd/JoyStick Touch Pad").GetComponent<FloatingJoyStick>();
#if UNITY_STANDALONE
        //�����λ������ģʽΪ��Ϸ�������� PCģʽ��
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }
    
    private void Start()
    {
        body = transform;
       
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
       Move();
#if UNITY_STANDALONE
        JumpPC();
        OpenTip();
        OpenItem();
        ClickInteractionPC();
#endif
#if UNITY_ANDROID
        ClickInteraction();
#endif



    }

    //�ƶ�
    public void Move()
    {

#if UNITY_ANDROID
        //��ȡ�ֻ��˲���������
        horizontal = joystick.GetHorizontalValue();
        vertical = joystick.GetVerticalValue();
        if (horizontal!=0||vertical !=0)
        {
            //��ֵ��һ������
            dir = new Vector3(horizontal, 0, vertical);
            body.Translate(dir * Time.deltaTime * moveSpeed);
            ani.SetFloat("SpeedX", horizontal);
            ani.SetFloat("SpeedY", vertical);
        }
#endif
#if UNITY_STANDALONE
        //��ȡ���� ��PC�ˣ�
        horizontal = Input.GetAxis("Horizontal"); 
            vertical = Input.GetAxis("Vertical");
            //��ֵ��һ������
            dir = new Vector3(horizontal, 0, vertical);
            body.Translate(dir * Time.deltaTime * moveSpeed);
            ani.SetFloat("SpeedX", horizontal);
            ani.SetFloat("SpeedY", vertical);
#endif
        
      

    }
#if UNITY_STANDALONE
    //�����תPC
    public void CameraRotatePC(Transform body, Transform focus)
    {
        //PC��
        float pcmousex = Input.GetAxis("Mouse X");
        if (pcmousex != 0)
        {
            //������ת
            body.Rotate(Vector3.up, pcmousex * cameraSpeed * Time.deltaTime);

        }
        //PC��
        float pcmousey = Input.GetAxis("Mouse Y");
        if (pcmousey != 0)
        {
            //������ת
            focus.Rotate(Vector3.left, pcmousey * cameraSpeed * Time.deltaTime);


        }
        //������嵽����ĽǶȴ���60����ת�ٶȼ�СΪ��
        if (Vector3.Angle(body.forward, focus.forward) > 50)
        {
            focus.Rotate(Vector3.left, -pcmousey * cameraSpeed * Time.deltaTime);
        }
    }
    //���Զ���Ծ
    public void JumpPC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {

            ani.SetBool("Jump", true);
            rb.AddForce(Vector3.up * upForce);

        }
    }
    //�л���ʾ������ʾ
    private void OpenTip()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {

            if (MainGameSys.Instance.gameOperationTipWnd.gameObject.activeSelf)
            {
                MainGameSys.Instance.gameOperationTipWnd.SetWndState(false);
                       }
            else
            {
                MainGameSys.Instance.gameOperationTipWnd.SetWndState(true);
              
            }
            
        }
    }
    //�򿪲˵�
    private void OpenItem()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            MainGameSys.Instance.OpenMainMenuWnd();
            IsControl(false);
        }
       
    }
    //��Ҵ�������
    private void ClickInteractionPC()
    {
        if (Input.GetMouseButtonDown(0))//�ж��������Ƿ񱻵��� ����Ļ�����
        {

            // ����һ�����λ��Ϊ���λ�õ�����
            Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);

            //�������Ի�ɫ�ı�ʾ����
            Debug.DrawRay(rays.origin, rays.direction * rayDistance, Color.yellow);

            //����һ��RayCast�������ڴ洢������Ϣ
            RaycastHit hit;
            //������������Ͷ���ȥ����������Ϣ�洢��hit��
            if (Physics.Raycast(rays, out hit, rayDistance))
            {

                if (hit.collider != null)
                {
                    ObjectInteractionSys.Instance.HandelHitInfo(hit);

                }

            }

        }
    }
#endif
#if UNITY_ANDROID
    //�ֻ�����Ծ
    public void Jump()
    {
        if (isGround == true)
        {

            ani.SetBool("Jump", true);
            rb.AddForce(Vector3.up * upForce);
            

        }
    }
    //��Ҵ�������
    private void ClickInteraction()
    {
        if (Input.touchCount==1)//�ж��������Ƿ񱻵��� ����Ļ�����
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // ����һ�����λ��Ϊ���λ�õ�����
                    Ray rays = Camera.main.ScreenPointToRay(Input.touches[0].position);

                    //�������Ի�ɫ�ı�ʾ����
                    Debug.DrawRay(rays.origin, rays.direction * rayDistance, Color.yellow);

                    //����һ��RayCast�������ڴ洢������Ϣ
                    RaycastHit hit;
                    //������������Ͷ���ȥ����������Ϣ�洢��hit��
                    if (Physics.Raycast(rays, out hit, rayDistance))
                    {

                        if (hit.collider != null)
                        {
                            ObjectInteractionSys.Instance.HandelHitInfo(hit);

                        }

                    }
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
            

        }
    }
#endif




    public  void IsControl(bool state=true)
    {
        if (state)
        {
            enabled = true;
#if UNITY_STANDALONE
            //�����λ������ģʽΪ��Ϸ�������� PCģʽ��
            Cursor.lockState = CursorLockMode.Locked;
#endif

        }
        else
        {
#if UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.None;
#endif
            enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //�жϵ�һ�����������������ǲ��ǵ���
        if (collision.collider.tag == "Ground")
        {
            isGround = true;

        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.collider.tag == "Ground")
        {
            isGround = false;
            ani.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DemoOver")
        {
            //��ϷDemoͨ��
            GameRoot.AddTips("��ϲ�㣬�ɹ����룡");
            MainGameSys.Instance.gameWinWnd.SetWndState(true);
            PlayerController.Instance.IsControl(false);
        }
    }

    public  void InitPlayer()
    {
        isDead = false;
        IsControl(true);
        transform.position = playerInitPos;
        transform.rotation = playerInitRot;
        
    }
}