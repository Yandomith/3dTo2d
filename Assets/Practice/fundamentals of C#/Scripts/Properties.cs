using UnityEngine;

namespace Practice.fundamentals_of_C_.Scripts
{
    public class Properties : MonoBehaviour
    {
        private int damage;
        public int Damage
        {
            get { return damage; }
            set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Damage cannot be negative. Setting to 0.");
                    damage = 0;
                }
                else
                {
                    damage = value;
                }
            }
        }
        
        private void Start()
        {
            Properties enemy = new Properties();
            enemy.Damage = 50; 
            Debug.Log("Weapon Damage: " + enemy.Damage);

            enemy.Damage = -10; 
            Debug.Log("Weapon Damage after invalid assignment: " + enemy.Damage);
        }
        
    }
}