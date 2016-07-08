using UnityEngine;
using System.Collections;
using PacmanGame;
public class testtouch : MonoBehaviour {

    //// Use this for initialization
    //void Start () {
    //    Vector2 v = new Vector2(-11, 22);
    //    Debug.Log(v.normalized);
    //    ModuleManager.Instance.Init();
    //    PromptModule p = ModuleManager.Instance.GetModule(PromptModule.name) as PromptModule;
    //    p.Prompt("123123123");
    //}
	
    //// Update is called once per frame
    //void Update () {
    //    if (Input.touchCount > 0)
    //        Debug.Log(Input.touchCount);
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        PromptModule p = ModuleManager.Instance.GetModule(PromptModule.name) as PromptModule;
    //        p.Prompt("12312eeeeeeeeeeeeeeeeeeee3123");
    //    }
    //}

    void Start()
    {
        GetComponent<BaseMove>().ImmediateMoveTo(Vector2.zero);
    }
}
