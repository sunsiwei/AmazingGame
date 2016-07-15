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
    void Update()
    {
        //if (input.touchcount > 0)
        //    debug.log(input.touchcount);
        //if (input.getmousebuttondown(0))
        //{
        //    promptmodule p = modulemanager.instance.getmodule(promptmodule.name) as promptmodule;
        //    p.prompt("12312eeeeeeeeeeeeeeeeeeee3123");
        //}
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
