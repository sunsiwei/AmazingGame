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
            //NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickStart");
            NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickMove");
            //NotificationCenter.DefaultCenter().AddObserver(this, "EventJoystickEnd");
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
    }
}


