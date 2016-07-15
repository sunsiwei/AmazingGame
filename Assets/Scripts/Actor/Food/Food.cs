using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public enum FoodType
    {
        Normal = 0,//only add score
        Accelerate = 1,//only accelerate
        EnemyAfriad = 2,//make enemy afraid and make player accelerate
        EnemyPause = 3,//make enemy pause for a moment
    }

    public class Food : MonoBehaviour
    {
        //do nothing
        protected void OnTriggerEnter2D(Collider2D co)
        {
            if (co.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}


