using Cinemachine;
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
    public float EnemyMaxHealth; //��������ֵ
    float EnemyCurrentHealth;

    [Header("״̬")]
    public bool isChasing;          //�Ƿ���׷��
    public bool isPatrol;           //�Ƿ���Ѳ��
    public bool isArrive;

    [Header("״̬��")]
    private EnemyBaseState currentState;   //��ǰ״̬
    protected EnemyBaseState patrolState;   //Ѳ��״̬
    protected EnemyBaseState chaseState;   //׷��״̬
    protected EnemyBaseState attackState;  //����״̬

    CinemachineVirtualCamera playerCam;
    SpriteRenderer enemySprite;

    float horizontal, vertical;

    public float currentHealth
    {
        get
        {
            return EnemyCurrentHealth;
        }
    }

    protected virtual void Awake() { }
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player");
        playerCam = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        trans = gameObject.GetComponent<Transform>().position;

        EnemyCurrentHealth = EnemyMaxHealth;
        enemySprite = GetComponent<SpriteRenderer>();
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

        horizontal = target.transform.position.x - transform.position.x;

        if (horizontal < 0)
        {
            enemySprite.flipX = false;
        }
        else if (horizontal > 0)
        {
            enemySprite.flipX = true;
        }
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
            playerCam.m_Lens.FieldOfView = Mathf.Lerp(playerCam.m_Lens.FieldOfView, 70, Time.deltaTime * 1.5f);
            InventoryManager.Instance.EnemyHealthPanel.SetActive(true);
            InventoryManager.Instance.EnemyHealthPanel.GetComponent<EnemyHealthUI>().UpdateHealthBar(EnemyCurrentHealth, EnemyMaxHealth);
            return true;
        }
        else
        {
            playerCam.m_Lens.FieldOfView = Mathf.Lerp(playerCam.m_Lens.FieldOfView, 40, Time.deltaTime * 1.5f);
            InventoryManager.Instance.EnemyHealthPanel.SetActive(false);
            return false;
        }
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

    public void TakeDamage(float damage)
    {
        float currentHealth = EnemyCurrentHealth - damage;
        EnemyCurrentHealth = Mathf.Clamp(currentHealth, 0, EnemyMaxHealth);
        anim.SetTrigger("GetHit");
        CinemachineShake.Instance.shakingCamera(5f, 0.3f);
    }

    private void OnDrawGizmosSelected() // ���ƾ���뾶
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}