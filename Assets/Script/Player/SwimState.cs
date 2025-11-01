using System;

using UnityEngine;

public class SwimState : PlayerState
{
    private float moveInput;
    private float buoyancyForce = 0.1f;
    private float maxVerticalSpeed = 2f;

    private float waterSurfaceY = 0.2f;
    private float maxSubmergeDepth = 5f;

    private bool waitingToSubmerge = true;
    private float submergeThreshold = 0.2f;

    private string currentAnimation = "";


    private float submergeDelay = 0.5f;     // ⏳ time before submerging allowed
    private float submergeTimer = 0f;       // timer for delay


    public SwimState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Swim State");
        waterSurfaceY = stateMachine.currentWaterSurfaceY;

        rb.linearDamping = 1f;
        animator.SetBool("IsSwimming", true);
        animator.SetFloat("swimSpeed", 0f);
        animator.Play("Swim", 0, 0f);
        waitingToSubmerge = true;

        submergeTimer = 0f;
    }

    public override void Exit()
    {
        rb.useGravity = true;
        animator.SetBool("IsSwimming", false);
        animator.SetBool("waitingToSubmerge", false);
        
        rb.linearDamping = 0f;

    }

    public override void LogicUpdate()
    {
        float depth = waterSurfaceY - rb.position.y ;
        moveInput = stateMachine.inputHandler.MoveInput;

        #region Debugging

        Vector3 start = new Vector3(rb.position.x - 2f, waterSurfaceY, rb.position.z);
        Vector3 end = new Vector3(rb.position.x + 2f, waterSurfaceY, rb.position.z);
        Debug.DrawLine(start, end, Color.cyan);


        float thresholdY = waterSurfaceY - submergeThreshold;
        start = new Vector3(rb.position.x - 2f, thresholdY, rb.position.z);
        end = new Vector3(rb.position.x + 2f, thresholdY, rb.position.z);
        Debug.DrawLine(start, end, Color.blue);

 


        #endregion

        moveInput = stateMachine.inputHandler.MoveInput;


        if (moveInput > 0 && !stateMachine.facingRight)
            stateMachine.Flip();
        else if (moveInput < 0 && stateMachine.facingRight)
            stateMachine.Flip();

        if (!waitingToSubmerge && depth < submergeThreshold)
        {
            waitingToSubmerge = true;
            rb.useGravity = true;
        }



        if (waitingToSubmerge)
        {
            submergeTimer += Time.deltaTime;
            if (depth >= submergeThreshold && submergeTimer >= submergeDelay)
            {
                waitingToSubmerge = false;
                rb.useGravity = false;
                animator.SetBool("waitingToSubmerge", false);
            }
            else
            {
                Vector3 velocity = stateMachine.rb.linearVelocity;
                velocity.x = moveInput * stateMachine.moveSpeed * 0.5f;
                stateMachine.rb.linearVelocity = velocity;
                animator.SetBool("waitingToSubmerge", true);

            }
        }


        Vector3 input = new Vector3(stateMachine.inputHandler.MoveInput, stateMachine.inputHandler.ClimbInput, 0);
        Vector3 swimForce = input.normalized * stateMachine.swimSpeed;
        rb.AddForce(swimForce, ForceMode.Acceleration);
        // Smoothly interpolate swimSpeed between 0 (idle) and 1 (active)
        float targetSwimSpeed;

        if (rb.position.y > waterSurfaceY && (stateMachine.inputHandler.MoveInput == 0.0f))
        {
            targetSwimSpeed = -2f;
        }
        else if (rb.position.y > waterSurfaceY && (stateMachine.inputHandler.MoveInput > 0.1f || stateMachine.inputHandler.MoveInput < -0.1f))
        {
            targetSwimSpeed = -1f;
        }
        else
        {
            targetSwimSpeed = (!waitingToSubmerge &&
                (stateMachine.inputHandler.MoveInput > 0.1f ||
                stateMachine.inputHandler.ClimbInput < -0.1f ||
                stateMachine.inputHandler.MoveInput < -0.1f ||
                stateMachine.inputHandler.ClimbInput > 0.1f))
                ? 1f
                : 0f;
        }


        // Adjust how quickly the animation responds (tweak 5f to make it faster/slower)
        float currentSwimSpeed = animator.GetFloat("swimSpeed");
        float smoothSwimSpeed = Mathf.MoveTowards(currentSwimSpeed, targetSwimSpeed, Time.deltaTime * 5f);
        animator.SetFloat("swimSpeed", smoothSwimSpeed);


        if (stateMachine.inputHandler.ClimbInput < 0.1f)
        {
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);
        }


        Vector3 v = rb.linearVelocity;
        v.y = Mathf.Clamp(v.y, -maxVerticalSpeed, maxVerticalSpeed);
        rb.linearVelocity = v;

        ApplyDepthBasedDrag();
    }

    private void ApplyDepthBasedDrag()
    {
        float depth = waterSurfaceY - rb.position.y;
        float t = Mathf.Clamp01(depth / maxSubmergeDepth);
        float dragAmount = Mathf.Lerp(stateMachine.minDrag, stateMachine.maxDrag, t);

        Vector3 velocity = rb.linearVelocity;
        velocity.x *= (1f - dragAmount * Time.fixedDeltaTime);
        velocity.y *= (1f - dragAmount * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector3(velocity.x, velocity.y, velocity.z);
    }
}
