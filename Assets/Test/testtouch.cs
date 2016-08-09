using UnityEngine;
using System.Collections;
using PacmanGame;
public class testtouch : MonoBehaviour {

    // Use this for initialization

	
    //// Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            StopAllCoroutines();
            Debug.Log("/////////////////////////////");
        }
    }

    void Start()
    {
        //GetComponent<BaseMove>().ImmediateMoveTo(Vector2.zero);
        StartCoroutine(testC());
    }
    IEnumerator testC()
    {
        while (true)
        {
            Debug.Log("--");
            yield return 0;
        }
    }

}
