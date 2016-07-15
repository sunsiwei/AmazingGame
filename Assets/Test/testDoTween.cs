using UnityEngine;
using System.Collections;
using DG.Tweening;

public class testDoTween : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOMoveX(100, 40).From() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
