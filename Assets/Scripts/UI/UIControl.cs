using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class UIControl : MonoBehaviour
    {
        public void OnBtnUp()
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "EventOnControlBtnDown", Vector2.up);
        }
        public void OnBtnRight()
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "EventOnControlBtnDown", Vector2.right);
        }
        public void OnBtnDown()
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "EventOnControlBtnDown", Vector2.down);
        }
        public void OnBtnLeft()
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "EventOnControlBtnDown", Vector2.left);
        }
    }
}


