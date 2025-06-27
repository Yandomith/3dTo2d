using System;
using UnityEngine;

namespace Practice.fundamentals_of_C_.Scripts
{
    public enum PetType
    {
        Dog,
        Cat,
        Bird,
        
    }
    public class PetEnum : MonoBehaviour
    {
        private void Start()
        {
            
            Pet pet = new Pet();
            pet.Name = "Buddy";
            pet.Type = PetType.Dog;
            pet.Describe(); // Outputs: this is pet name Buddy and type is Dog
            
            Pet cat = new Pet();
            cat.Name = "Whiskers";
            cat.Type = PetType.Cat;
            cat.Describe(); // Outputs: this is pet name Whiskers and type is Cat
            
            Pet bird = new Pet();
            bird.Name = "Tweety";
            bird.Type = PetType.Bird;
            bird.Describe(); // Outputs: this is pet name Tweety and type is Bird
        }
    }

    public class Pet
    {
        public string Name;
        public PetType Type;
        
        public void Describe()
        {
            Debug.Log("this is pet name " + Name + " and type is " + Type);
        }
        
    }
}