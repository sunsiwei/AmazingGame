using UnityEngine;
using System.Collections;

public class testswipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EasyTouch.On_SwipeEnd += On_SwipeEnd;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void On_SwipeEnd(Gesture gesture)
    {
        switch (gesture.swipe)
        {
            case EasyTouch.SwipeDirection.Left:
                Debug.Log("left");
                break;
            case EasyTouch.SwipeDirection.Up:
                Debug.Log("Up");
                break;
            case EasyTouch.SwipeDirection.Right:
                Debug.Log("Right");
                break;
            case EasyTouch.SwipeDirection.Down:
                Debug.Log("Down");
                break;
        }
    }
}
