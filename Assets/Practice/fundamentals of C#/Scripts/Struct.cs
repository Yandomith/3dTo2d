using UnityEngine;


    public class Struct : MonoBehaviour
    {
        public struct WeaponStats
        
        {
            public int Damage { get; set; }
            public float Range { get; set; }
            public float Weight { get; set; }
            public WeaponStats(int damage, float range, float weight)
            {
                Damage = damage;
                Range = range;
                Weight = weight;
            }
            
            public void DisplayStats()
            {
                Debug.Log("Weapon Damage: " + Damage);
                Debug.Log("Weapon Range: " + Range);
                Debug.Log("Weapon Weight: " + Weight);
            }
        }
        
        private void Start()
        {
            WeaponStats sword = new WeaponStats(50, 1.5f, 2.0f);
            sword.DisplayStats();

            WeaponStats bow = sword;
            bow.Damage = 30;
            bow.DisplayStats();
            sword.DisplayStats();

        }
    }
