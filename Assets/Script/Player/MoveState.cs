using UnityEngine;


public class MoveState : PlayerState
{
    private float moveInput;

    public MoveState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator) : base(stateMachine, rb, animator)

    {
        this.stateMachine = stateMachine;
    }


    public override void Enter()
    {
        base.Enter();
         animator.Play("Running", 0, 0f);
        Debug.Log("Entered Move State");


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = stateMachine.inputHandler.MoveInput;


        if (moveInput > 0 && !stateMachine.facingRight)
            stateMachine.Flip();
        else if (moveInput < 0 && stateMachine.facingRight)
            stateMachine.Flip();




    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 velocity = stateMachine.rb.linearVelocity;
        velocity.x = moveInput * stateMachine.moveSpeed;
        stateMachine.rb.linearVelocity = velocity;
    }

    public override void Exit()
    {
        base.Exit();

    }


}
