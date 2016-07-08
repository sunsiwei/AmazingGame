using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class Food : MonoBehaviour
    {
        public enum FoodType
        { 
            Normal = 0,
            Accelerate = 1,
            EnemyAfriad = 2
        }

        public FoodType foodType = FoodType.Normal;

        public float accelerateRate = 0.1f;
        public float accelerateDuration = 4.0f;

        void OnTriggerEnter2D(Collider2D co)
        {
            if (co.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}


