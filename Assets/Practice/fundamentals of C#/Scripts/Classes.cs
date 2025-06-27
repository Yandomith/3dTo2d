using System;
using UnityEngine;

public class Classes : MonoBehaviour
{
    public string petName;
    public int petAge;

    private void Start()
    {
        Classes dog = new Classes();
        dog.petName = "Buddy";
        dog.petAge = 3;
        Debug.Log("Pet Name: " + dog.petName);

        
        Classes cat = new Classes();
        cat.petName = "Whiskers";
        cat.petAge = 2;
        Debug.Log("Cat Name: " + cat.petName);
   
        Debug.Log("DOg Name: " + dog.petName);
        
        
    }

    private void Speak()
    {
        Debug.Log("Woof!");
    }
}
