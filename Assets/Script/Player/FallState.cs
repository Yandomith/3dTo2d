using UnityEngine;

public class FallState : PlayerState
{
    public FallState(PlayerStateMachine stateMachine, Rigidbody rb , Animator animator) : base(stateMachine , rb ,animator) { }
    
    
    private RaycastHit hit;
    private bool surfaceChecked;

    public override void Enter()
    {
        base.Enter();
        animator.Play("Falling Idle", 0, 0f);
        Debug.Log("Entering Fall State");
        surfaceChecked = false;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        if (!surfaceChecked && rb.linearVelocity.y < 0f)
        {
            Vector3 origin = stateMachine.transform.position;
            Vector3 direction = Vector3.down;
            float checkDistance = 5f; // Adjust as needed

            int combinedMask = stateMachine.environmentCheck.groundLayer | stateMachine.environmentCheck.waterLayer;

            if (Physics.Raycast(origin, direction, out hit, checkDistance, combinedMask))
            {
                int hitLayer = hit.collider.gameObject.layer;

                if (((1 << hitLayer) & stateMachine.environmentCheck.waterLayer) != 0)
                {
                    Debug.Log("Landing on WATER");
                    // trigger splash, set flag, etc.
                }
                else if (((1 << hitLayer) & stateMachine.environmentCheck.groundLayer) != 0)
                {
                    Debug.Log("Landing on GROUND");
                    // trigger dust, set flag, etc.
                }

                surfaceChecked = true; // Prevent multiple raycasts
            }

            // Debug ray
            Debug.DrawRay(origin, direction * checkDistance, Color.cyan, 0.1f);
        }

        
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
}
