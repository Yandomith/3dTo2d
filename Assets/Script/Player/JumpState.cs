using UnityEngine;

public class JumpState : PlayerState
{
    private bool jumpStarted;

    public JumpState(PlayerStateMachine stateMachine, Rigidbody rb , Animator animator) : base(stateMachine , rb ,animator)
    {
        jumpStarted = false;
    }

    public override void Enter()
        {
        base.Enter();
        Debug.Log("Entering Jump State");
        jumpStarted = false;
        animator.Play("Jump");
        
        stateMachine.rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        stateMachine.rb.AddForce(Vector3.up * stateMachine.jumpForce, ForceMode.Impulse);
        jumpStarted = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        float moveInput = stateMachine.inputHandler.MoveInput;
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput * stateMachine.moveSpeed;
        rb.linearVelocity = velocity;

        
        if (moveInput > 0 && !stateMachine.facingRight)
            stateMachine.Flip();
        else if (moveInput < 0 && stateMachine.facingRight)
            stateMachine.Flip();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    
    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting Jump State");
        jumpStarted = false;
    }
}
