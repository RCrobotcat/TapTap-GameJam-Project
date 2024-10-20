using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    public Vector3 trans;     //��������
    public GameObject target;    //��ȡ�������

    [Header("��������")]
    public float chaseRange;        //��ⷶΧ
    public Vector3 randomPosition;

    [Header("״̬")]
    public bool isChasing;          //�Ƿ���׷��
    public bool isPatrol;           //�Ƿ���Ѳ��
    public bool isArrive;

    [Header("״̬��")]
    private EnemyBaseState currentState;   //��ǰ״̬
    protected EnemyBaseState patrolState;   //Ѳ��״̬
    protected EnemyBaseState chaseState;   //׷��״̬
    protected EnemyBaseState attackState;  //����״̬

    protected virtual void Awake() { }
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player");
        trans = gameObject.GetComponent<Transform>().position;
    }

    private void OnEnable()
    {
        currentState = patrolState;    //��ʼ����ֵѲ��״̬
        currentState.OnEnter(this);
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    private void Update()
    {
        currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        currentState.PhysicsUpdate();
    }

    public bool FoundPlayer()
    {
        float distanceToPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToPlayer <= chaseRange)
        {
            return true;
        }
        return false;
    }

    public void SwitchState(EnemyState state)
    {
        var newState = state switch
        {
            EnemyState.Chasing => chaseState,
            EnemyState.Patroling => patrolState,
            EnemyState.Attackiing => attackState,
            _ => null
        }; ;

        //�л�״̬
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    private void OnDrawGizmosSelected() // ���ƾ���뾶
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}