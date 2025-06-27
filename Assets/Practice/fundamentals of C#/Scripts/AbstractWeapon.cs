using System;
using UnityEngine;

namespace Practice.fundamentals_of_C_.Scripts
{
    public class AbstractWeapon : MonoBehaviour
    {
        private void Start()
        {
            Weapon sword = new Sword();
            sword.Attack(); // Outputs: Swinging sword
            sword.Reload(); // Outputs: Reloading weapon
            
            Weapon bow = new Bow();
            bow.Attack(); // Outputs: Shooting arrow
            bow.Reload(); // Outputs: Reloading weapon
        }
    }
    public abstract class Weapon
    {
        public abstract void Attack();
        
        public void Reload()
        {
            Debug.Log("Reloading weapon");
        }
    }
    public class Sword : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Swinging sword");
        }
    }
    public class Bow : Weapon
    {
        public override void Attack()
        {
            Debug.Log("Shooting arrow");
        }
    }
}