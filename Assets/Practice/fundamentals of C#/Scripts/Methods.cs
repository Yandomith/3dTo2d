using System;
using UnityEngine;

public class Methods : MonoBehaviour
{
    int num1 = 10;
    int num2 = 20;
    
    private void Start()
    {
        Speak("Hello Traveller");
        int sum = Add(num1, num2);
        Debug.Log("Sum of " + num1 + " and " + num2 + " is: " + sum);
    }
    private void Speak(string dialogue)
    {
        Debug.Log("Dialogue: " + dialogue);
    }
    
    int Add(int a, int b)
    {
        return a + b;
    }
}
