using UnityEngine;
using System;

public class Event : MonoBehaviour
{
    public static event Action OnCubeCollide;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Debug.Log("Cube collided with " + gameObject.name);
            OnCubeCollide?.Invoke();
        }
    }
}