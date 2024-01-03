using UniversalMobileController;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System;
/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:玩家控制逻辑
*************************************************************/

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance=null;
    //玩家操作杆
    public FloatingJoyStick joystick;

    public float moveSpeed = 3; //移动速度
    public float upForce = 20; //跳跃力
    public float cameraSpeed = 300f;//相机灵敏度
    public Vector3 dir;
    public float horizontal;
    public float vertical;
  
    //是否在地面
    public bool isGround = true;
    //玩家能触摸的距离范围
    public float rayDistance = 5;
    //玩家初始位置
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
        //鼠标光标位置设置模式为游戏窗口中心 PC模式下
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

    //移动
    public void Move()
    {

#if UNITY_ANDROID
        //获取手机端操作杆轴向
        horizontal = joystick.GetHorizontalValue();
        vertical = joystick.GetVerticalValue();
        if (horizontal!=0||vertical !=0)
        {
            //赋值给一个向量
            dir = new Vector3(horizontal, 0, vertical);
            body.Translate(dir * Time.deltaTime * moveSpeed);
            ani.SetFloat("SpeedX", horizontal);
            ani.SetFloat("SpeedY", vertical);
        }
#endif
#if UNITY_STANDALONE
        //获取轴向 （PC端）
        horizontal = Input.GetAxis("Horizontal"); 
            vertical = Input.GetAxis("Vertical");
            //赋值给一个向量
            dir = new Vector3(horizontal, 0, vertical);
            body.Translate(dir * Time.deltaTime * moveSpeed);
            ani.SetFloat("SpeedX", horizontal);
            ani.SetFloat("SpeedY", vertical);
#endif
        
      

    }
#if UNITY_STANDALONE
    //相机旋转PC
    public void CameraRotatePC(Transform body, Transform focus)
    {
        //PC端
        float pcmousex = Input.GetAxis("Mouse X");
        if (pcmousex != 0)
        {
            //身体旋转
            body.Rotate(Vector3.up, pcmousex * cameraSpeed * Time.deltaTime);

        }
        //PC端
        float pcmousey = Input.GetAxis("Mouse Y");
        if (pcmousey != 0)
        {
            //焦点旋转
            focus.Rotate(Vector3.left, pcmousey * cameraSpeed * Time.deltaTime);


        }
        //如果身体到焦点的角度大于60，旋转速度减小为负
        if (Vector3.Angle(body.forward, focus.forward) > 50)
        {
            focus.Rotate(Vector3.left, -pcmousey * cameraSpeed * Time.deltaTime);
        }
    }
    //电脑端跳跃
    public void JumpPC()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {

            ani.SetBool("Jump", true);
            rb.AddForce(Vector3.up * upForce);

        }
    }
    //切换显示操作提示
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
    //打开菜单
    private void OpenItem()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            MainGameSys.Instance.OpenMainMenuWnd();
            IsControl(false);
        }
       
    }
    //玩家触摸交互
    private void ClickInteractionPC()
    {
        if (Input.GetMouseButtonDown(0))//判断鼠标左键是否被单击 或屏幕被点击
        {

            // 创建一条点击位置为光标位置的射线
            Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);

            //将射线以黄色的表示出来
            Debug.DrawRay(rays.origin, rays.direction * rayDistance, Color.yellow);

            //创建一个RayCast变量用于存储返回信息
            RaycastHit hit;
            //将创建的射线投射出去并将反馈信息存储到hit中
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
    //手机端跳跃
    public void Jump()
    {
        if (isGround == true)
        {

            ani.SetBool("Jump", true);
            rb.AddForce(Vector3.up * upForce);
            

        }
    }
    //玩家触摸交互
    private void ClickInteraction()
    {
        if (Input.touchCount==1)//判断鼠标左键是否被单击 或屏幕被点击
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 创建一条点击位置为光标位置的射线
                    Ray rays = Camera.main.ScreenPointToRay(Input.touches[0].position);

                    //将射线以黄色的表示出来
                    Debug.DrawRay(rays.origin, rays.direction * rayDistance, Color.yellow);

                    //创建一个RayCast变量用于存储返回信息
                    RaycastHit hit;
                    //将创建的射线投射出去并将反馈信息存储到hit中
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
            //鼠标光标位置设置模式为游戏窗口中心 PC模式下
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
        //判断第一次碰到的物体里面是不是地面
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
            //游戏Demo通过
            GameRoot.AddTips("恭喜你，成功逃离！");
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