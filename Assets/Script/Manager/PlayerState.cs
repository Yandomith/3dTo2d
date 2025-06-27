using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Rigidbody rb;
    protected Animator animator;

    public PlayerState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
  
    {
        this.rb = rb;
        this.animator = animator;
        this.stateMachine = stateMachine;
    }

    
    public virtual void Enter() { }


    public virtual void LogicUpdate()
    {
      
    }


    public virtual void PhysicsUpdate()
    {
       
    }

    
    public virtual void Exit() { }


}
