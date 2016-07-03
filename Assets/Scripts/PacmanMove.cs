using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
	public class PacmanMove : MonoBehaviour {

        double speed = 0.1;
		Vector2 dest;
        
        Vector2 expectDir = Vector2.right;
        Vector2 curDir = Vector2.right;

		void Start()
		{
			dest = transform.position;
            NotificationCenter.DefaultCenter().AddObserver(this, "EventOnControlBtnDown");
            NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickStart");
            NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickMove");
            NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickEnd");
		}

        public Vector2 ExpectDir
        {
            set { expectDir = value; }
        }
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public void SetPosition(Vector2 pos)
        {
            transform.position = pos;
            dest = transform.position;
        }
        public Vector2 GetDirection()
        {
            return curDir;
        }

        Vector2 touchStartPos;
        float minTouchDistance = 1;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && valid(Vector2.up))
                expectDir = Vector2.up;
            if (Input.GetKeyDown(KeyCode.RightArrow) && valid(Vector2.right))
                expectDir = Vector2.right;
            if (Input.GetKeyDown(KeyCode.DownArrow) && valid(Vector2.down))
                expectDir = Vector2.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && valid(Vector2.left))
                expectDir = Vector2.left;

            //if (Input.touchCount == 1)
            //{
            //    Touch touch = Input.GetTouch(0);
            //    switch (touch.phase)
            //    { 
            //        case TouchPhase.Began:
            //            touchStartPos = touch.position;
            //            break;
            //        case TouchPhase.Moved:
            //            break;
            //        case TouchPhase.Ended:
            //            Vector2 touchDirection = touch.position - touchStartPos;
            //            if (touchDirection.magnitude > minTouchDistance)
            //            {
            //                Vector2 dir = GetTouchDirection(touchStartPos, touch.position);
            //                if (valid(dir))
            //                {
            //                    expectDir = dir;
            //                    PromptModule.Instance.Prompt(string.Format("startPos:{0}, endPos{1}. dir:{2}", touchStartPos, touch.position, expectDir));
            //                }
            //            }
            //            break;
            //    }
            //}
        }
		
		void FixedUpdate()
		{
			if ((Vector2)transform.position == dest)
			{
                if (valid(expectDir))
                {
                    dest = (Vector2)transform.position + expectDir;
                    curDir = expectDir;
                }
                else
                {
                    if (valid(curDir))
                    {
                        dest = (Vector2)transform.position + curDir;
                    }
                    else
                    {
                        curDir = -curDir;
                        expectDir = curDir;
                        dest = (Vector2)transform.position + curDir;
                    }
                }
			}
            //Debug.LogFormat("position: {0}, destation: {1}. direction:{2}, expectDir:{3}", (Vector2)transform.position, dest, curDir, expectDir);
            // Move closer to Destination
            Vector2 p = Vector2.MoveTowards(transform.position, dest, (float)speed);
            GetComponent<Rigidbody2D>().MovePosition(p);

			// Animation Parameters
            Vector2 dir = dest - (Vector2)transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
		}

        

		bool valid(Vector2 dir)
		{
			Vector2 pos = transform.position;
            int layerMask = 1 << 9 | 1 << 12;
            //RaycastHit2D hit = Physics2D.Linecast(pos + dir*1.2f, pos, layerMask);
            RaycastHit2D circleHit = Physics2D.CircleCast(pos + dir * 1.2f, 0.2f, -dir, 1f, layerMask);
            bool b = circleHit.collider == GetComponent<Collider2D>();
			return b;
		}

        void EventOnControlBtnDown(Notification n)
        {
            Vector2 dir = (Vector2)n.data;
            if (valid(Vector2.up))
                expectDir = dir;
        }

        bool bJoyStickMoving = false;
        void EventJoystickStart()
        {
            bJoyStickMoving = true;
        }
        void EventJoystickMove(Notification noti)
        {
            Vector2 direction = (Vector2)noti.data;
            if (direction.x * curDir.x > 0 || direction.y * curDir.y > 0)
            {
                Vector2 dir;
                if (direction.x * curDir.x > 0)
                {
                    dir = direction.y > 0 ? Vector2.up : Vector2.down;
                }
                else
                {
                    dir = direction.x > 0 ? Vector2.right : Vector2.left;
                }
                if (valid(dir))
                {
                    if (valid(curDir) == true)
                    {
                        float degree = Vector2.Angle(curDir, direction);
                        if(degree > 45)
                            expectDir = dir;
                    }
                    else
                    {
                        expectDir = dir;
                    }
                }
            }
            else
            {
                Vector2 dir = GetTouchDirection(direction);
                if (dir != Vector2.zero && valid(dir))
                {
                    expectDir = dir;
                }
            }
        }
        void EventJoystickEnd()
        {
            bJoyStickMoving = false;
        }

       
        Vector2 GetTouchDirection(Vector2 start, Vector2 end)
        {
            Vector2 direction = end - start;
            return GetTouchDirection(direction);
        }
        Vector2 GetTouchDirection(Vector2 direction)
        {
            float angle;
            float side = direction.x > 0 ? 1 : -1;
            float num = Vector2.Angle(direction, -Vector2.up);
            if (side == 1)
            {
                angle = num;
            }
            else
            {
                angle = 360 - num;
            }

            if (angle > 315 || angle <= 45)
            {
                return Vector2.down;
            }
            else
            if (angle > 45 && angle <= 135)
            {
                return Vector2.right;
            }
            else
            if (angle > 135 && angle <= 225)
            {
                return Vector2.up;
            }
            else
            if (angle > 225 && angle <= 315)
            {
                return Vector2.left;
            }
            return Vector2.zero;
        }
	}
}


