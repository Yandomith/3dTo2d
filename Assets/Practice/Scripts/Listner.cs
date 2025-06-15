using UnityEngine;

public class Listner : MonoBehaviour
{
    private void OnEnable()
    {
        Event.OnCubeCollide += HandleCubeCollision;
    }
    
    private void OnDisable()
    {
        Event.OnCubeCollide -= HandleCubeCollision;
    }

    private void HandleCubeCollision()
    {
        transform.position += Vector3.up * 2f;
    }
}