using UnityEngine;
using System.Collections;
using LitJson;
using System;

namespace PacmanGame
{
    //public class DotModule : ModuleBase
    //{
    //    public static string name = "DotModule";

    //    public DotModule(string _name)
    //        : base(_name)
    //    { 
            
    //    }

    //    GameObject dotRoot;
    //    public delegate void InitDotsCompleted();
    //    public void InitDots(InitDotsCompleted c)
    //    {
    //        if (GameObject.Find("dotRoot") == null)
    //        {
    //            dotRoot = new GameObject("dotRoot");
    //        }

    //        Game.Instance.StartCoroutine(AddDots(c));
    //    }

    //    IEnumerator AddDots(InitDotsCompleted c)
    //    {
    //        JsonData levelCfg = Game.Instance.GetLevel().GetLevelCfg();
    //        for (int h = (int)levelCfg["mazeHeight"] / 8; h >= 0; h--)
    //        {
    //            for (int w = 0; w <= (int)levelCfg["mazeWidth"] / 8; w++)
    //            {
    //                if (Physics2D.Raycast(new Vector2(w, h), Vector2.up, 0.5f) == false)
    //                {
    //                    GameObject dot = ResourcesLoader.LoadActor("pacdot");
    //                    dot.transform.SetParent(dotRoot.transform, false);
    //                    dot.transform.position = new Vector3(w, h, 0);
    //                    yield return 0;
    //                }
    //            }
    //        }
    //        c();
    //    }
    //}
}


