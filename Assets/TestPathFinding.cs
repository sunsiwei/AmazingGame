using UnityEngine;
using System.Collections;

public class TestPathFinding : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			Debug.Log("UpArrow is press");
		}
	}
}
