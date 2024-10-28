using UnityEngine;

public class GoHundred_Players : Singleton<GoHundred_Players>
{
    // [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isRunning;        //�Ƿ����ܲ�
    Animator animator;
    private Rigidbody rb;

    [Header("��������")]
    float horizontal, vertical;                     //��ά����
    float stopDistance;                             //ֹͣ����

    public float WalkSpeed;                         //��·�ٶ�
    public float currentSpeed;                      //��ǰ�ٶ�(����agent)

    SpriteRenderer playerSprite;                    //��ת

    protected override void Awake()
    {
        base.Awake();

        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();

        //stopDistance = agent.stoppingDistance;
        currentSpeed = WalkSpeed;
    }

    void Update()
    {
        //agent.SetDestination(transform.position);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", rb.velocity.magnitude);
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
    public void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
        if (inputDirection != Vector3.zero)
        {
            MovePlayer(inputDirection);
            if (!AudioManager.Instance.footStepSource.isPlaying)
            {
                AudioManager.Instance.PlayFootStep(AudioManager.Instance.PlayerWalk_solid);
            }
        }
        else
        {
            AudioManager.Instance.footStepSource.Stop();
        }

    }

    //�ƶ�
    public void MovePlayer(Vector3 inputDirection)
    {
        rb.velocity = inputDirection * currentSpeed;    //�ƶ�     
    }
}
