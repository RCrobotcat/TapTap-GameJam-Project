using UnityEngine;
using UnityEngine.AI;

public class PacManController : Singleton<PacManController>
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isRunning;
    Animator animator;
    float horizontal, vertical;
    float stopDistance;

    SpriteRenderer playerSprite;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        stopDistance = agent.stoppingDistance;
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
    }

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
}