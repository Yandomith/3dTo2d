using UnityEngine;


public class EnvironmentCheck : MonoBehaviour
{
    [Header("Ground Check Settings")] public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Ladder Check Settings")] public Transform ladderCheckPoint;
    public float ladderCheckRadius = 0.3f;
    public LayerMask ladderLayer;


    [Header("water Check Settings")] public LayerMask waterLayer;

    public bool IsGrounded { get; private set; }
    public bool IsNearLadder { get; private set; }
    public bool IsInWater { get; private set; }


    public bool IsClimbing => IsNearLadder;

    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        IsNearLadder = Physics.CheckSphere(ladderCheckPoint.position, ladderCheckRadius, ladderLayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            IsInWater = true;
            Debug.Log("Player entered water.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            IsInWater = false;
            Debug.Log("Player exited water.");
        }
    }


    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }

        if (ladderCheckPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(ladderCheckPoint.position, ladderCheckRadius);
        }
    }
}