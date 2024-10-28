using Cinemachine;
using QFramework;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isRunning;
    Animator animator;
    float horizontal, vertical;
    float stopDistance;

    [Header("Camera Settings")]
    CinemachineVirtualCamera PlayerCam;
    public Transform followPoint;
    public Transform lookAtPoint;

    [Header("Player Settings")]
    public float RunSpeedMultiple;
    float originalSpeed;
    [HideInInspector] public bool combatWithEnemy; // Player combat with enemy

    SpriteRenderer playerSprite;

    [Header("Slash Settings")]
    public float SlashStaminaCost;
    [HideInInspector] public GameObject playerSlashEffect;
    public Transform slashEffectPos_left;
    public Transform slashEffectPos_right;
    public float detectRange; // Player detect range

    [HideInInspector] public bool isGluttonyCompleted;
    [HideInInspector] public bool isJealousCompleted;

    [HideInInspector] public bool equipRage;

    private Coroutine attackCoroutine;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        stopDistance = agent.stoppingDistance;
        originalSpeed = agent.speed;

        PlayerCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Start()
    {
        if (PlayerCam != null)
        {
            PlayerCam.Follow = followPoint;
            PlayerCam.LookAt = lookAtPoint;
        }
    }

    void Update()
    {
        if (isDead) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        if (inputDirection != Vector3.zero)
        {
            Vector3 CamRelativeMove = ConvertToCameraSpace(inputDirection);
            MovePlayer(CamRelativeMove);
        }
        else
        {
            AudioManager.Instance.footStepSource.Stop();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (horizontal < 0)
        {
            playerSprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            playerSprite.flipX = false;
        }

        // Player Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (PlayerNumController.Instance.mModel.PlayerStamina.Value > 0)
            {
                PlayerNumController.Instance.PlayerStaminaBar.gameObject.SetActive(true);
                isRunning = true;
                animator.SetBool("Running", true);
                agent.speed *= RunSpeedMultiple;

                AudioManager.Instance.footStepSource.Stop();
                AudioManager.Instance.PlayFootStep(AudioManager.Instance.PlayerRun_solid);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerNumController.Instance.mModel.PlayerStamina.Value <= 0
            || agent.velocity.magnitude <= 0.1f)
        {
            isRunning = false;
            animator.SetBool("Running", false);
            agent.speed = originalSpeed;
            AudioManager.Instance.footStepSource.Stop();
        }
        if (PlayerNumController.Instance.mModel.PlayerStamina.Value >= 15.0f && !Input.GetKeyDown(KeyCode.LeftShift))
            PlayerNumController.Instance.PlayerStaminaBar.gameObject.SetActive(false);

        // Player Attacking
        if (Input.GetKeyDown(KeyCode.E) && PlayerNumController.Instance.mModel.PlayerStamina.Value > 3.0f
            && playerSlashEffect != null)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(PlayerAttack());
            }
        }

        HandlePlayerDead();
    }


    bool isDead;
    void HandlePlayerDead()
    {
        if (PlayerNumController.Instance.mModel.PlayerLight.Value <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("Dead", true);
            StartCoroutine(HandlePlayerDeadTimer(1.5f));
        }
    }

    IEnumerator HandlePlayerDeadTimer(float time)
    {
        yield return new WaitForSeconds(time);

        isDead = false;

        if (!string.IsNullOrEmpty(SaveManager.Instance.SceneName))
        {
            SceneController.Instance.HandleRespawn(SaveManager.Instance.SceneName);
        }
        else
        {
            SceneManager.LoadSceneAsync("MenuScene");
        }
    }

    #region Weapon Equip/UnEquip
    public void EquipWeapon()
    {
        var weapon = InventoryManager.Instance.actionData.items[0].itemData;


        if (weapon != null)
        {
            if (weapon.name == "Rage")
                equipRage = true;
            else equipRage = false;
            playerSlashEffect = weapon.WeaponPrefab;
        }
    }

    public void UnEquipWeapon()
    {
        if (equipRage) equipRage = false;
        playerSlashEffect = null;
    }
    #endregion

    public void MovePlayer(Vector3 inputDirection)
    {
        Vector3 targetPosition = transform.position + inputDirection;
        MoveToTarget(targetPosition);
        if (!AudioManager.Instance.footStepSource.isPlaying)
        {
            AudioManager.Instance.PlayFootStep(AudioManager.Instance.PlayerWalk_solid);
        }
    }

    public void MoveToTarget(Vector3 target)
    {
        // StopAllCoroutines()
        agent.stoppingDistance = stopDistance;
        agent.destination = target;
    }

    private Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        Vector3 CamForwardZProduct = vectorToRotate.z * camForward;
        Vector3 CamRightXProduct = vectorToRotate.x * camRight;

        Vector3 vectorRotateToCameraSpace = CamForwardZProduct + CamRightXProduct;
        return vectorRotateToCameraSpace;
    }

    IEnumerator PlayerAttack()
    {
        agent.isStopped = true;
        animator.SetTrigger("Attack");
        AudioManager.Instance.PlaySfx(AudioManager.Instance.PlayerSlash);
        PlayerNumController.Instance.PlayerStaminaBar.gameObject.SetActive(true);
        PlayerNumController.Instance.StaminaChange(SlashStaminaCost);
        GameObject effect;

        yield return new WaitForSeconds(0.5f);
        if (playerSprite.flipX)
            effect = Instantiate(playerSlashEffect, slashEffectPos_left.position, Quaternion.identity);
        else
            effect = Instantiate(playerSlashEffect, slashEffectPos_right.position, Quaternion.Euler(0, 180, 0));

        Destroy(effect, 0.3f);
        yield return new WaitForSeconds(0.2f);
        agent.isStopped = false;

        attackCoroutine = null; // clear the coroutine reference
    }

    #region Slash Attack Nums Logic
    // Get Player Slashing Target
    public GameObject GetSlashTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    Enemy enemy;
    ArrogantController arrogant;
    // Player Slash Animation Event
    public void PlayerSlashAttack()
    {
        GameObject target = GetSlashTarget();
        var attackData = InventoryManager.Instance.actionData.items[0].itemData.WeaponData;
        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackData.attackRange)
            {

                float damage = currentDamage();
                if (target.GetComponent<Enemy>() != null)
                {
                    enemy = target.GetComponent<Enemy>();
                    enemy.TakeDamage(damage);
                }
                if (target.GetComponent<ArrogantController>() != null)
                {
                    arrogant = target.GetComponent<ArrogantController>();
                    arrogant.TakeDamage(damage);
                }
                InventoryManager.Instance.EnemyHealthPanel.GetComponent<EnemyHealthUI>()
                    .UpdateHealthBar(enemy.currentHealth, enemy.EnemyMaxHealth);
            }
        }
    }

    // Calculate the current damage
    private float currentDamage()
    {
        AttackData_SO attackData = InventoryManager.Instance.actionData.items[0].itemData.WeaponData;
        float coreDamage = Random.Range(attackData.minDamage, attackData.maxDamage);

        /*if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            // Debug.Log("Critical Hit: " + coreDamage);
        }*/

        return (float)coreDamage;
    }
    #endregion

    public void TakeDamage(float damage)
    {
        PlayerNumController.Instance.SendCommand(new PlayerLightChangeCommand(-damage));
        CinemachineShake.Instance.shakingCamera(3f, 0.3f);
    }

    // Draw the detection range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}