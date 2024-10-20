using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    public Vector3 trans;     //¹ÖÎï×ø±ê
    public GameObject target;    //»ñÈ¡ÈËÎï×é¼þ

    [Header("»ù´¡ÊôÐÔ")]
    public float chaseRange;        //¼ì²â·¶Î§
    public Vector3 randomPosition;

    [Header("×´Ì¬")]
    public bool isChasing;          //ÊÇ·ñ·¢Æð×·»÷
    public bool isPatrol;           //ÊÇ·ñÔÚÑ²Âß
    public bool isArrive;

    [Header("×´Ì¬»ú")]
    private EnemyBaseState currentState;   //µ±Ç°×´Ì¬
    protected EnemyBaseState patrolState;   //Ñ²Âß×´Ì¬
    protected EnemyBaseState chaseState;   //×·»÷×´Ì¬
    protected EnemyBaseState attackState;  //¹¥»÷×´Ì¬

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
        currentState = patrolState;    //³õÊ¼»¯¸³ÖµÑ²Âß×´Ì¬
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

        //ÇÐ»»×´Ì¬
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    private void OnDrawGizmosSelected() // »æÖÆ¾¯½ä°ë¾¶
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}