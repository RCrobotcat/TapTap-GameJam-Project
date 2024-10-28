using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ArrogantController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    Animator animator;
    float stopDistance;

    float horizontal, vertical;

    [Header("Enemy Settings")]
    public float detectRange;       // 敌人检测范围
    public float attackRange;        // 敌人攻击范围
    public float attackCooldown;   // 攻击冷却时间
    public float RunSpeedMultiple = 1.5f; // 运行速度倍数
    float originalSpeed;

    SpriteRenderer enemySprite;

    [Header("Slash Settings")]
    public GameObject enemySlashEffect;   // 敌人攻击特效
    public Transform slashEffectPos_left;
    public Transform slashEffectPos_right;

    [HideInInspector] public bool equipWeapon;

    private Coroutine attackCoroutine;

    private Transform playerTransform;

    bool isDead;

    [Header("Enemy Stats")]
    public float maxHealth;
    float currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();

        stopDistance = agent.stoppingDistance;
        originalSpeed = agent.speed;
        currentHealth = maxHealth;

        // 找到玩家
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        if (isDead) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // 计算与玩家的距离
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectRange)
        {
            // 移动到玩家位置
            MoveToTarget(playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                // 攻击玩家
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(EnemyAttack());
                }
            }
        }
        else
        {
            // 停止移动
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }

        // 更新动画参数
        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // 根据移动方向翻转精灵
        if (agent.velocity.x < 0)
        {
            enemySprite.flipX = true;
        }
        else if (agent.velocity.x > 0)
        {
            enemySprite.flipX = false;
        }

        // 处理敌人死亡
        HandleEnemyDead();
    }

    void MoveToTarget(Vector3 target)
    {
        agent.isStopped = false;
        agent.stoppingDistance = stopDistance;
        agent.destination = target;
    }

    IEnumerator EnemyAttack()
    {
        agent.isStopped = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        GameObject effect;
        if (enemySprite.flipX)
            effect = Instantiate(enemySlashEffect, slashEffectPos_left.position, Quaternion.identity);
        else
            effect = Instantiate(enemySlashEffect, slashEffectPos_right.position, Quaternion.Euler(0, 180, 0));

        Destroy(effect, 0.3f);

        // 对玩家造成伤害
        EnemySlashAttack();

        yield return new WaitForSeconds(attackCooldown);
        agent.isStopped = false;

        attackCoroutine = null; // 清除协程引用
    }

    // 敌人攻击逻辑
    public void EnemySlashAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            float damage = CalculateDamage();
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    // 计算当前伤害
    private float CalculateDamage()
    {
        // 简化处理，直接指定伤害值
        float minDamage = 0.5f;
        float maxDamage = 1f;
        float damage = Random.Range(minDamage, maxDamage);
        return damage;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // 播放受击动画
        animator.SetTrigger("Hit");

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            HandleEnemyDead();
        }
    }

    void HandleEnemyDead()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("Dead", true);
            agent.isStopped = true;
            // 禁用碰撞器等组件
            Destroy(gameObject, 2f); // 2秒后销毁
        }
    }

    // 在场景中绘制检测和攻击范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
