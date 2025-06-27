using System;
using UnityEngine;

public class Constructors : MonoBehaviour
{
    private string name;
    private int damage;

    private void Start()
    {
        Constructors weapon1 = new Constructors("Sword", 50);
        Debug.Log("Weapon1 Name: " + weapon1.name);
        Debug.Log("Weapon1 Damage: " + weapon1.damage);
        // Using the constructor without parameters
        
        Constructors weapon2 = new Constructors();
        Debug.Log("Weapon2 Name: " + weapon2.name);
        Debug.Log("Weapo2 Damage: " + weapon2.damage);
    }

    // Constructor to initialize the fields
    public Constructors(string name, int damage)
    {
        this.name = name;
        this.damage = damage;
    }
    //constructor without parameters
    public Constructors()
    {
        name = "Default Weapon";
        damage = 10;
    }
}
