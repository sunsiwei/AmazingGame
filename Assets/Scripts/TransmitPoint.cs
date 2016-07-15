using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class TransmitPoint : MonoBehaviour
    {

        public Transform toTransmitPoint;

        void OnTriggerEnter2D(Collider2D co)
        {
            Transform target = co.transform;
            if (target.GetComponent<BaseMove>())
                target.GetComponent<BaseMove>().ImmediateMoveTo(toTransmitPoint.position);
            else
                target.position = toTransmitPoint.position;
        }
    }
}


