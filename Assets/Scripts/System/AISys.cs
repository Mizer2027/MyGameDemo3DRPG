
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/************************************************************
作者:Mizer  
邮箱:2807665129@qq.com

功能:AI系统 控制AI寻路，攻击等等
*************************************************************/


public class AISys : MonoBehaviour
{
    public static AISys Instance;
    public enum AIState
    {
        Patrol,//巡逻
        Chase,//追逐
        Attack,//攻击
        Dead,//死亡
    }
    public AIState curstate=AIState.Patrol;//当前ai状态
    public bool isDead=false; //是否死亡
    private Transform playerTransform;//玩家的位置
    
    private GameObject[] pointArray; //位置列表
    private Vector3 desPos;//目标巡逻位置
    public float chaseDistance = 20f;//当小于追逐距离 开始追逐玩家
    public float attackDistance = 2f;//当小于攻击距离 开始攻击玩家

    private Vector3 initAIPos;
    private Quaternion initAIRot;
    private NavMeshAgent navMeshAgent;


    private void Awake()
    {
        Instance = this;
        initAIPos=transform.position;
        initAIRot=transform.rotation;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }
    private void Start()
    {
        AIInit();
    }
    public  void AIInit()
    {
        InitAItrans();
        curstate = AIState.Patrol;
        isDead = false;
        pointArray = GameObject.FindGameObjectsWithTag("PatrolPoints");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogError("没有玩家角色");
        }

        //寻找下一个巡逻点
        FindNextPoint();
    }

    private void InitAItrans()
    {
        transform.position = initAIPos;
        transform.rotation = initAIRot;
    }

    private void Update()
    {
        AIStateUpdate();
    }

    //AI状态更新
    private void AIStateUpdate()
    {
        switch (curstate)
        {
            case AIState.Patrol:
                //更新巡逻状态
                UpdatePatrolState();
                break;
            case AIState.Chase:
                UpdateChaseState();
                break;
            case AIState.Attack:
                UpdateAttackState();
                break;
            case AIState.Dead:
                break;
        }
    }
    //更新攻击状态
    private void UpdateAttackState()
    {
        //判断玩家死亡没
        if(PlayerController.Instance.isDead == false)
        {
            desPos = playerTransform.position;
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            if (dist >= attackDistance)//当前ai与玩家的距离在攻击范围之外和追逐范围之内
            {
                //切换为追逐状态
                curstate = AIState.Chase;
               
            }
            else  //攻击玩家 玩家死亡 游戏结束
            {
                PlayerController.Instance.isDead = true;
                MainGameSys.Instance.GameFailure();
            }
           
        }
        else
        {
            curstate = AIState.Dead;
        }
       
        //Debug.Log("攻击玩家中");
    }
    //更新追逐状态
    private void UpdateChaseState()
    {
        desPos=playerTransform.position;

        float dist=Vector3.Distance(transform.position,playerTransform.position);
        if (dist <= attackDistance)  //如果进入攻击范围 则立马攻击
        {
            curstate = AIState.Attack;
        }
        else if(dist>=chaseDistance) //如果玩家超出追逐距离 则放弃追逐继续巡逻
        {
            curstate = AIState.Patrol;
        }
        //导航网格寻路
        navMeshAgent.SetDestination(desPos);
    }

    //更新巡逻状态
    private void UpdatePatrolState()
    {
        //是否到达目标点
        if(Vector3.Distance(transform.position, desPos) <= navMeshAgent.stoppingDistance)
        {
            FindNextPoint();
        }
        else if(Vector3.Distance(transform.position,playerTransform.position)<=chaseDistance)
        {
            //追逐玩家
            curstate = AIState.Chase;
        }
        {
            //导航网格寻路
            navMeshAgent.SetDestination(desPos);
        }
       

       
    }
   
   

    //寻找下一个巡逻点
    private void FindNextPoint()
    {
        int randomPos= Random.Range(0, pointArray.Length);
        //设定目标巡逻位置

        desPos= pointArray[randomPos].transform.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, desPos);//绘制AI到目标点的位置

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);//绘制追逐半径

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);//绘制攻击半径


    }

   
}
