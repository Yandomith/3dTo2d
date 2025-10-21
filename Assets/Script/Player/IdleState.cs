using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine, Rigidbody rb , Animator animator) : base(stateMachine , rb ,animator) { }

    public override void Enter()
    {
        base.Enter();
         animator.Play("Neutral Idle", 0, 0f);
        Debug.Log("Entering Idle State");
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
       
        Vector3 velocity = stateMachine.rb.linearVelocity;
        velocity.x = 0;
        stateMachine.rb.linearVelocity = velocity;
    }
    
}
