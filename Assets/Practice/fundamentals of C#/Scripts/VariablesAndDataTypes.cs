using System;
using UnityEngine;

public class VariablesAndDataTypes : MonoBehaviour
{
    private int level = 1; // Integer variable to store the level
    private float health = 75.5f;
    private bool hasKey = false;
    private string playerName = "Smith Yando";

    private void Start()
    {
        Debug.Log("Player Name: " + playerName);
        Debug.Log("Level: " + level);
        Debug.Log("Health: " + health);
        Debug.Log("Has Key: " + hasKey);
    }
}
