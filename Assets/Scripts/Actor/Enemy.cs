using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class Enemy : MonoBehaviour
    {
        EnemyAISearch eas;
        void Start()
        {
            eas = GetComponent<EnemyAISearch>();
        }

        void OnTriggerEnter2D(Collider2D co)
        {
            if (co.CompareTag("Player"))
            {
                if (eas.BehaviorType == BehaviorType.Afraid)
                {
                    eas.MakeHomeForDead();
                }
            }
        }
    }
}


