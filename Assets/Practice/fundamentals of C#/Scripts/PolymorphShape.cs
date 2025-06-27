using System;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Practice.fundamentals_of_C_.Scripts
{
    public class PolymorphShape : MonoBehaviour
    {
        private void Start()
        {
            Shape shape = new Shape();
            shape.Draw(); // Outputs: Drawing a shape

            Circle circle = new Circle();
            circle.Draw(); // Outputs: Drawing a circle


            
        }
    }
    public class Shape
    {
        public virtual void Draw()
        {
            Debug.Log("Drawing a shape");
        }

        public void Add(int a )
        {
            
        }
        public void Add(int a, int b)
        {
            Debug.Log("Adding two numbers: " + (a + b));
        }
    }
    public class Circle : Shape
    {
        public override void Draw()
        {
            
            Debug.Log("Drawing a circle");
        }
    }
}