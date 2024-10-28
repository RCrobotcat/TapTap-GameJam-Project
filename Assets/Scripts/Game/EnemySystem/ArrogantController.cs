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
    public float detectRange;       // ���˼�ⷶΧ
    public float attackRange;        // ���˹�����Χ
    public float attackCooldown;   // ������ȴʱ��
    public float RunSpeedMultiple = 1.5f; // �����ٶȱ���
    float originalSpeed;

    SpriteRenderer enemySprite;

    [Header("Slash Settings")]
    public GameObject enemySlashEffect;   // ���˹�����Ч
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

        // �ҵ����
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

        // ��������ҵľ���
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectRange)
        {
            // �ƶ������λ��
            MoveToTarget(playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                // �������
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(EnemyAttack());
                }
            }
        }
        else
        {
            // ֹͣ�ƶ�
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }

        // ���¶�������
        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // �����ƶ�����ת����
        if (agent.velocity.x < 0)
        {
            enemySprite.flipX = true;
        }
        else if (agent.velocity.x > 0)
        {
            enemySprite.flipX = false;
        }

        // �����������
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

        // ���������˺�
        EnemySlashAttack();

        yield return new WaitForSeconds(attackCooldown);
        agent.isStopped = false;

        attackCoroutine = null; // ���Э������
    }

    // ���˹����߼�
    public void EnemySlashAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            float damage = CalculateDamage();
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    // ���㵱ǰ�˺�
    private float CalculateDamage()
    {
        // �򻯴���ֱ��ָ���˺�ֵ
        float minDamage = 0.5f;
        float maxDamage = 1f;
        float damage = Random.Range(minDamage, maxDamage);
        return damage;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // �����ܻ�����
        animator.SetTrigger("Hit");

        // ����Ƿ�����
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
            // ������ײ�������
            Destroy(gameObject, 2f); // 2�������
        }
    }

    // �ڳ����л��Ƽ��͹�����Χ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
