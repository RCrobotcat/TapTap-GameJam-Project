using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;

    [Header("Rabbit辅助参数")]
    public Vector3 trans;                //怪物坐标
    [HideInInspector] public GameObject target;            //获取人物组件
    public Vector3 attackPosition;       //要攻击的坐标
    public GameObject MagicPrefab;       //魔法攻击预制体
    public float MagicAttackDuration;      //控制攻击间隔
    [HideInInspector] public float MagicAttackCounter;        //魔法攻击时间计数器

    [Header("基础属性")]
    public float chaseRange;        //检测范围
    public float baseAttackRange;       //基础物理攻击范围
    public Vector3 randomPosition;
    public float EnemyMaxHealth; //怪物生命值
    float EnemyCurrentHealth;
    public float baseAttackDuration;    //基础攻击间隔
    float baseAttackCounter;
    public float baseAttackDamage;      //基础攻击伤害

    [Header("状态")]
    public bool isArrive;
    public bool isAttack;          //是否在攻击
    public bool beAttacking;       //是否可以释放一次攻击

    [Header("状态机")]
    private EnemyBaseState currentState;   //当前状态
    protected EnemyBaseState patrolState;   //巡逻状态
    protected EnemyBaseState chaseState;   //追击状态
    protected EnemyBaseState attackState;  //攻击状态

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
    bool isUpdateQuest = false;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player");
        playerCam = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        trans = gameObject.GetComponent<Transform>().position;

        EnemyCurrentHealth = EnemyMaxHealth;
        enemySprite = GetComponent<SpriteRenderer>();

        baseAttackCounter = baseAttackDuration;
    }

    private void OnEnable()
    {
        currentState = patrolState;    //初始化赋值巡逻状态
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

        if (EnemyCurrentHealth <= 0)
        {
            if (!isUpdateQuest)
            {
                QuestManager.Instance.UpdateQuestProgress(gameObject.name, 1);
                PlayerNumController.Instance.MaxLightUpdate();
                isUpdateQuest = true;
            }
            InventoryManager.Instance.EnemyHealthPanel.SetActive(false);
            StartCoroutine(AdjustFOVAndDeactivate());
        }

        //攻击计数器
        if (MagicAttackCounter <= MagicAttackDuration)
        {
            MagicAttackCounter += Time.deltaTime;
        }
        else
        {
            beAttacking = true;
        }

        HandleBaseAttackPlayer();
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

        //切换状态
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

    public void HandleBaseAttackPlayer()
    {
        float distanceToPlayer = Vector3.Distance(target.transform.position, transform.position);

        if (baseAttackCounter > 0)
        {
            baseAttackCounter -= Time.deltaTime;
        }
        else
        {
            if (distanceToPlayer <= baseAttackRange)
            {
                PlayerController.Instance.TakeDamage(baseAttackDamage);
            }
            baseAttackCounter = baseAttackDuration;
        }
    }

    private void OnDrawGizmosSelected() // 绘制警戒半径
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private IEnumerator AdjustFOVAndDeactivate()
    {
        float duration = 1.0f;
        float startFOV = playerCam.m_Lens.FieldOfView;
        float targetFOV = 40f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            playerCam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        playerCam.m_Lens.FieldOfView = targetFOV;
        gameObject.SetActive(false); // Deactivate the enemy after FOV adjustment
    }
}