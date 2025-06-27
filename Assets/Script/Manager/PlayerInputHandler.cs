using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    
    public float MoveInput { get; private set; }

    
    public bool JumpPressed { get; private set; }

    
    public float ClimbInput { get; private set; }

    
    public bool DashPressed { get; private set; }

    void Update()
    {
        
        MoveInput = Input.GetAxisRaw("Horizontal");

        
        JumpPressed = Input.GetButtonDown("Jump");

        
        ClimbInput = Input.GetAxisRaw("Vertical");

        
        DashPressed = Input.GetKeyDown(KeyCode.LeftShift);
    }
}
