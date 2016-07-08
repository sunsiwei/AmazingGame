using UnityEngine;
using System.Collections;

namespace PacmanGame
{

    public class BaseMove : MonoBehaviour
    {
        protected Vector2 nextPos;
        public void ImmediateMoveTo(Vector2 to)
        {
            transform.position = to;
            nextPos = to;
        }
    }
}

