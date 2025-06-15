using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.right * 5f, ForceMode.Impulse);  
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}