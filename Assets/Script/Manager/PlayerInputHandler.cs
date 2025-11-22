using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerInputHandler : MonoBehaviour
{
    
    public float MoveInput { get; private set; }

    
    public bool JumpPressed { get; private set; }

    
    public float ClimbInput { get; private set; }

    
    public bool DashPressed { get; private set; }
    public Joystick joystick;
    //void Update()
    //{

    //    if (Application.isMobilePlatform)
    //    {
    //        MoveInput = joystick.Horizontal;
    //        ClimbInput = joystick.Vertical;
    //    }
    //    else
    //    {

    //        MoveInput = Input.GetAxisRaw("Horizontal");


    //        JumpPressed = Input.GetButtonDown("Jump");


    //        ClimbInput = Input.GetAxisRaw("Vertical");


    //        DashPressed = Input.GetKeyDown(KeyCode.LeftShift);
    //    }
    //}
    void Update()
    {

  
    
            MoveInput = joystick.Horizontal;
            ClimbInput = joystick.Vertical;


     


            JumpPressed = Input.GetButtonDown("Jump");



            DashPressed = Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
