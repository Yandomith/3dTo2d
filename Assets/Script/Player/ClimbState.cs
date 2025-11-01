using UnityEngine;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes


public class ClimbState : PlayerState
{
    public ClimbState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Climb State");
        rb.useGravity = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float vertical = stateMachine.inputHandler.ClimbInput;
        // Check if the player is moving up or down
        if (vertical > 0.01f || vertical < -0.01f )
        {
            // Climbing (up or down)
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Climbing Up Wall"))
            {
                animator.Play("Climbing Up Wall" , 0,0f);
            }
        }
        else
        {
            // Idle on wall (not moving)
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.Play("Neutral Idle",0,0f);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        float vertical = stateMachine.inputHandler.ClimbInput;

        rb.linearVelocity = new Vector3(0f, vertical * stateMachine.climbSpeed, 0f);
       
    }

    public override void Exit()
    {
        base.Exit();

        rb.useGravity = true;

    }
}
