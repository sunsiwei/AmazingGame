using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class TransmitPoint : MonoBehaviour
    {

        public Transform toTransmitPoint;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D co)
        {
            Transform target = co.transform;
            if (target.GetComponent<PacmanMove>())
                target.GetComponent<PacmanMove>().SetPosition(toTransmitPoint.position);
            else
                target.position = toTransmitPoint.position;
        }
    }
}


