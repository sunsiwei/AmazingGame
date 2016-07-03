using UnityEngine;
using System.Collections;

public class tetjoystick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMoveStart()
    {
        Debug.Log("OnMoveStart");
    }
    public void OnMove(Vector2 v)
    {
        Debug.Log("OnMove" + v);
    }
    public void OnMoveSpeed(Vector2 v)
    {
        Debug.Log("OnMoveSpeed" + v);
    }
    public void OnMoveEnd()
    {
        Debug.Log("OnMoveEnd");
    }
}
