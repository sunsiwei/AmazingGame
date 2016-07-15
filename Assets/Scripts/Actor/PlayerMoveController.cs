using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PlayerMoveController : MonoBehaviour
    {
        PlayerMove pm;
        void Start()
        {
            pm = GetComponent<PlayerMove>();

            //joystick
            NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickMove");

            //swipe
            EasyTouch.On_SwipeEnd += On_SwipeEnd;
        }

        void EventJoystickMove(Notification noti)
        {
            Vector2 offset = (Vector2)noti.data;
            Vector2 direction = GetTouchDirection(offset);
            if (direction != Vector2.zero)
                pm.ExpectDirection = direction;  
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

        void On_SwipeEnd(Gesture gesture)
        {
            switch (gesture.swipe)
            { 
                case EasyTouch.SwipeDirection.Up:
                    pm.ExpectDirection = Vector2.up;
                    break;
                case EasyTouch.SwipeDirection.Right:
                    pm.ExpectDirection = Vector2.right;
                    break;
                case EasyTouch.SwipeDirection.Down:
                    pm.ExpectDirection = Vector2.down;
                    break;
                case EasyTouch.SwipeDirection.Left:
                    pm.ExpectDirection = Vector2.left;
                    break;
            }
        }
    }
}


