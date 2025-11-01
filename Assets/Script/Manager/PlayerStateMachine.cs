using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    [SerializeField] private GameObject playerObject;

    public PlayerInputHandler inputHandler;
    public EnvironmentCheck environmentCheck;


    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float climbSpeed = 3f;

    public float swimSpeed = 3f;
    public float currentWaterSurfaceY;

    public float minDrag;
    public float maxDrag = 5f;
    
    private float smoothedSpeed = 0f;
    private float speedSmoothTime = 0.2f; // Time to smooth the speed parameter


    public PlayerState CurrentState { get; private set; }
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState { get; private set; }
    public FallState FallState { get; private set; }
    public ClimbState ClimbState { get; private set; }
    public SwimState SwimState { get; private set; }


    public bool facingRight = true;
    private float jumpTimeElapsed = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = playerObject.GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        environmentCheck = GetComponent<EnvironmentCheck>();


        IdleState = new IdleState(this, rb, animator);
        MoveState = new MoveState(this, rb, animator);
        JumpState = new JumpState(this, rb, animator);
        FallState = new FallState(this, rb, animator);
        ClimbState = new ClimbState(this, rb, animator);
        SwimState = new SwimState(this, rb, animator);


        ChangeState(IdleState);
    }

    void Update()
    {
        HandleTransitions();
        CurrentState?.LogicUpdate();
        jumpTimeElapsed += Time.deltaTime;
        UpdateSpeedParameter(); 
    }

    void FixedUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }

    void HandleTransitions()
    {
        float safeLandingDistance = 5f;
        float maxFallCheckDistance = 10f;
        RaycastHit hit;


        bool groundDetected = Physics.Raycast(transform.position, Vector3.down, out hit, maxFallCheckDistance,
            environmentCheck.groundLayer);


        #region Debugging

        

        
// Determine if landing is safe (ground detected AND close enough)
        bool isSafeLanding = groundDetected && hit.distance <= safeLandingDistance;

// Draw ray:
// - Green if safe landing
// - Yellow if ground detected but far (unsafe landing)
// - Red if no ground detected
        Color rayColor;

        if (!groundDetected)
            rayColor = Color.red;         // No ground, danger!
        else if (isSafeLanding)
            rayColor = Color.green;       // Safe landing!
        else
            rayColor = Color.yellow;      // Ground detected but too far to be safe

        Debug.DrawRay(transform.position, Vector3.down * maxFallCheckDistance, rayColor);
        
        
        #endregion


        bool shouldFall = true;


        if (groundDetected && hit.distance <= safeLandingDistance)
        {
            shouldFall = false;
        }


        if (CurrentState == SwimState && !environmentCheck.IsInWater)
        {
            if (environmentCheck.IsGrounded)
            {
                if (Mathf.Abs(inputHandler.MoveInput) > 0.1f)
                    ChangeState(MoveState);
                else
                    ChangeState(IdleState);
            }
            else
            {
                ChangeState(FallState);
            }

            return;
        }


        if (environmentCheck.IsInWater)
        {
            if (CurrentState != SwimState)
            {
                ChangeState(SwimState);
                return;
            }
        }


        if (environmentCheck.IsClimbing)
        {
            if (CurrentState != ClimbState && Mathf.Abs(inputHandler.ClimbInput) > 0.1f)
            {
                ChangeState(ClimbState);
                return;
            }

            if (CurrentState == ClimbState && inputHandler.JumpPressed)
            {
                ChangeState(JumpState);
                return;
            }
        }
        else if (CurrentState == ClimbState)
        {
            ChangeState(Mathf.Abs(inputHandler.MoveInput) > 0.1f ? MoveState : FallState);
            return;
        }


        if (CurrentState == IdleState)
        {
            if (Mathf.Abs(inputHandler.MoveInput) > 0.1f)
            {
                ChangeState(MoveState);
            }
            else if (inputHandler.JumpPressed && environmentCheck.IsGrounded)
            {
                ChangeState(JumpState);
            }
            else if (!environmentCheck.IsGrounded && rb.linearVelocity.y < 0 && jumpTimeElapsed > 0.1f)
            {
                if (shouldFall)
                    ChangeState(FallState);
            }
        }
        else if (CurrentState == MoveState)
        {
            if (Mathf.Abs(inputHandler.MoveInput) < 0.1f)
            {
                ChangeState(IdleState);
            }
            else if (inputHandler.JumpPressed && environmentCheck.IsGrounded)
            {
                ChangeState(JumpState);
            }
            else if (!environmentCheck.IsGrounded && rb.linearVelocity.y < 0 && jumpTimeElapsed > 0.1f)
            {
                if (shouldFall)
                    ChangeState(FallState);
            }
        }
        else if (CurrentState == JumpState)
        {
            if (rb.linearVelocity.y <= 0 && jumpTimeElapsed > 0.1f)
            {
                if (environmentCheck.IsInWater)
                {
                    ChangeState(SwimState);
                }
                else if (environmentCheck.IsGrounded)
                {
                    if (Mathf.Abs(inputHandler.MoveInput) > 0.1f)
                        ChangeState(MoveState);
                    else
                        ChangeState(IdleState);
                }
                else if (shouldFall)
                {
                    ChangeState(FallState);
                }
            }
        }

        else if (CurrentState == FallState)
        {
            if (environmentCheck.IsGrounded)
            {
                if (Mathf.Abs(inputHandler.MoveInput) > 0.1f)
                    ChangeState(MoveState);
                else
                    ChangeState(IdleState);
            }
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;

        if (CurrentState != null)
            CurrentState.Enter();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
<<<<<<< Updated upstream
    
    protected void UpdateSpeedParameter()
    {
        float maxSpeed = moveSpeed; // or whatever your top speed is
        float currentSpeed = Mathf.Abs(rb.linearVelocity.x);
=======

>>>>>>> Stashed changes

        float normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeed);
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, normalizedSpeed, Time.deltaTime / speedSmoothTime);// 0 to 1
        animator.SetFloat("Speed", smoothedSpeed);
    }
}