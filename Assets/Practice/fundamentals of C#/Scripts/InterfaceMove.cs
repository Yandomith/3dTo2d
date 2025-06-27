using System;
using UnityEngine;
using System.Collections.Generic;

namespace Practice.fundamentals_of_C_.Scripts
{
    public class InterfaceMove : MonoBehaviour
    {
        private void Start()
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            
            List<IMovable> movables = new List<IMovable>();
            movables.Add(player);
            movables.Add(enemy);
            foreach (var movable in movables)
            {
                movable.Move();
            }
        }
    }

    public interface IMovable
    {
        public void Move();
        
    }

    public class Player : IMovable
    {
        public void Move()
        {
            Debug.Log("Player is moving");
        }
    }
    
    public class Enemy : IMovable
    {
        public void Move()
        {
            Debug.Log("Enemy is moving");
        }
    }
}