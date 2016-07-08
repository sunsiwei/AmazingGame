using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PlayerMove : BaseMove
    {

        double speed = 0.1;
        
        Vector2 expectDirection = Vector2.right;
        Vector2 curDirection = Vector2.right;
        bool pause;

        void Start()
        {
            nextPos = transform.position;
        }

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public Vector2 ExpectDirection
        {
            set { expectDirection = value; }
        }
        public Vector2 CurDirection
        {
            get { return curDirection; }
        }



        void FixedUpdate()
        {
            if (pause)
                return;

            if ((Vector2)transform.position == nextPos)
            {
                if (valid(expectDirection))
                {
                    nextPos = (Vector2)transform.position + expectDirection;
                    curDirection = expectDirection;
                }
                else
                {
                    if (valid(curDirection))
                    {
                        nextPos = (Vector2)transform.position + curDirection;
                    }
                    else
                    {
                        curDirection = -curDirection;
                        expectDirection = curDirection;
                        nextPos = (Vector2)transform.position + curDirection;
                    }
                }
            }
            //Debug.LogFormat("position: {0}, destation: {1}. direction:{2}, expectDir:{3}", (Vector2)transform.position, dest, curDir, expectDir);
            // Move closer to Destination
            Vector2 p = Vector2.MoveTowards(transform.position, nextPos, (float)speed);
            GetComponent<Rigidbody2D>().MovePosition(p);

            // Animation Parameters
            Vector2 dir = nextPos - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
        bool valid(Vector2 dir)
        {
            Vector2 pos = transform.position;
            int layerMask = 1 << AmazingGame.MapLayer | 1 << AmazingGame.PlayerLayer;
            //RaycastHit2D hit = Physics2D.Linecast(pos + dir*1.2f, pos, layerMask);
            RaycastHit2D circleHit = Physics2D.CircleCast(pos + dir * 1.2f, 0.2f, -dir, 1f, layerMask);
            bool b = circleHit.collider == GetComponent<Collider2D>();
            return b;
        }

        
    }
}


