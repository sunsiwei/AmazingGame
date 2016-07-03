using UnityEngine;
using System.Collections;

public class UIJoystick : MonoBehaviour {

    public void OnMoveStart()
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "EventJoystickStart");
    }
    public void OnMove(Vector2 v)
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "EventJoystickMove", v);
    }
    public void OnMoveEnd()
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "EventJoystickEnd");
    }
}
