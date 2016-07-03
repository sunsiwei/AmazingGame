using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class SuperPacdot : MonoBehaviour
    {

        public int score = 10;

        void OnTriggerEnter2D(Collider2D co)
        {
            if (co.CompareTag("Player"))
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "EventTriggerSuperPacdot");
                Destroy(gameObject);
            }
        }
    } 
}


