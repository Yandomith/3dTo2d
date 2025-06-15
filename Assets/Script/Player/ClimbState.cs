using UnityEngine;

public class ClimbState : PlayerState
{
    public ClimbState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering Climb State");
        animator.Play("Climbing Up Wall");
        rb.useGravity = false;

        if (stateMachine.animator != null)
            stateMachine.animator.SetBool("isClimbing", true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

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

        if (stateMachine.animator != null)
            stateMachine.animator.SetBool("isClimbing", false);
    }
}
