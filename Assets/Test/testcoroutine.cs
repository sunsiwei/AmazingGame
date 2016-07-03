using UnityEngine;
using System.Collections;

public class testcoroutine : MonoBehaviour
{

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        print("Starting " + Time.time);
        coroutine = WaitAndPrint(5.0f);
        StartCoroutine(coroutine);
        print("Done " + Time.time);
    }

    public IEnumerator WaitAndPrint(float waitTime)
    {
        print("WaitAndPrint1 " + Time.time);
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint2 " + Time.time);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StopAllCoroutines();
            print("Stopped " + Time.time);
        }
    }
}