using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isRunning;
    Animator animator;
    float horizontal, vertical;
    float stopDistance;

    [Header("Player Settings")]
    public float RunSpeedMultiple;
    float originalSpeed;

    SpriteRenderer playerSprite;

    [Header("Stash Settings")]
    public float SlashStaminaCost;
    public GameObject playerSlashEffect;
    public Transform slashEffectPos_left;
    public Transform slashEffectPos_right;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        stopDistance = agent.stoppingDistance;
        originalSpeed = agent.speed;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        if (inputDirection != Vector3.zero)
        {
            Vector3 CamRelativeMove = ConvertToCameraSpace(inputDirection);
            MovePlayer(CamRelativeMove);
        }

        animator.SetFloat("Speed", agent.speed);
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
                agent.speed *= RunSpeedMultiple;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || PlayerNumController.Instance.mModel.PlayerStamina.Value <= 0)
        {
            isRunning = false;
            agent.speed = originalSpeed;
        }
        if (PlayerNumController.Instance.mModel.PlayerStamina.Value >= 10.0f && !Input.GetKeyDown(KeyCode.LeftShift))
            PlayerNumController.Instance.PlayerStaminaBar.gameObject.SetActive(false);

        // Player Attacking
        if (Input.GetKeyDown(KeyCode.E) && PlayerNumController.Instance.mModel.PlayerStamina.Value > 3.0f)
        {
            animator.SetTrigger("Attack");
            PlayerNumController.Instance.PlayerStaminaBar.gameObject.SetActive(true);
            PlayerNumController.Instance.StaminaChange(SlashStaminaCost);
            GameObject effect;
            if (horizontal < 0)
                effect = Instantiate(playerSlashEffect, slashEffectPos_left.position, Quaternion.identity);
            else
                effect = Instantiate(playerSlashEffect, slashEffectPos_right.position, Quaternion.Euler(0, 180, 0));
            Destroy(effect, 0.5f);
        }
    }

    public void MovePlayer(Vector3 inputDirection)
    {
        Vector3 targetPosition = transform.position + inputDirection;
        MoveToTarget(targetPosition);
    }

    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.stoppingDistance = stopDistance;
        agent.isStopped = false;
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
}
