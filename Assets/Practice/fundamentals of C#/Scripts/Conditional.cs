using System;
using UnityEngine;

public class Conditional : MonoBehaviour
{
    int health = 100;

    private void Start()
    {

        if (health > 50)
        {
            Debug.Log("Player is alive and healthy");
        }
        else if (health > 0)
        {
            Debug.Log("Player is alive but low");
        }
        else
        {
            Debug.Log("Player is dead");
        }
        
    }
}
