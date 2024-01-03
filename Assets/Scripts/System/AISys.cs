
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/************************************************************
����:Mizer  
����:2807665129@qq.com

����:AIϵͳ ����AIѰ·�������ȵ�
*************************************************************/


public class AISys : MonoBehaviour
{
    public static AISys Instance;
    public enum AIState
    {
        Patrol,//Ѳ��
        Chase,//׷��
        Attack,//����
        Dead,//����
    }
    public AIState curstate=AIState.Patrol;//��ǰai״̬
    public bool isDead=false; //�Ƿ�����
    private Transform playerTransform;//��ҵ�λ��
    
    private GameObject[] pointArray; //λ���б�
    private Vector3 desPos;//Ŀ��Ѳ��λ��
    public float chaseDistance = 20f;//��С��׷����� ��ʼ׷�����
    public float attackDistance = 2f;//��С�ڹ������� ��ʼ�������

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
            Debug.LogError("û����ҽ�ɫ");
        }

        //Ѱ����һ��Ѳ�ߵ�
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

    //AI״̬����
    private void AIStateUpdate()
    {
        switch (curstate)
        {
            case AIState.Patrol:
                //����Ѳ��״̬
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
    //���¹���״̬
    private void UpdateAttackState()
    {
        //�ж��������û
        if(PlayerController.Instance.isDead == false)
        {
            desPos = playerTransform.position;
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            if (dist >= attackDistance)//��ǰai����ҵľ����ڹ�����Χ֮���׷��Χ֮��
            {
                //�л�Ϊ׷��״̬
                curstate = AIState.Chase;
               
            }
            else  //������� ������� ��Ϸ����
            {
                PlayerController.Instance.isDead = true;
                MainGameSys.Instance.GameFailure();
            }
           
        }
        else
        {
            curstate = AIState.Dead;
        }
       
        //Debug.Log("���������");
    }
    //����׷��״̬
    private void UpdateChaseState()
    {
        desPos=playerTransform.position;

        float dist=Vector3.Distance(transform.position,playerTransform.position);
        if (dist <= attackDistance)  //������빥����Χ ��������
        {
            curstate = AIState.Attack;
        }
        else if(dist>=chaseDistance) //�����ҳ���׷����� �����׷�����Ѳ��
        {
            curstate = AIState.Patrol;
        }
        //��������Ѱ·
        navMeshAgent.SetDestination(desPos);
    }

    //����Ѳ��״̬
    private void UpdatePatrolState()
    {
        //�Ƿ񵽴�Ŀ���
        if(Vector3.Distance(transform.position, desPos) <= navMeshAgent.stoppingDistance)
        {
            FindNextPoint();
        }
        else if(Vector3.Distance(transform.position,playerTransform.position)<=chaseDistance)
        {
            //׷�����
            curstate = AIState.Chase;
        }
        {
            //��������Ѱ·
            navMeshAgent.SetDestination(desPos);
        }
       

       
    }
   
   

    //Ѱ����һ��Ѳ�ߵ�
    private void FindNextPoint()
    {
        int randomPos= Random.Range(0, pointArray.Length);
        //�趨Ŀ��Ѳ��λ��

        desPos= pointArray[randomPos].transform.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, desPos);//����AI��Ŀ����λ��

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);//����׷��뾶

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);//���ƹ����뾶


    }

   
}
