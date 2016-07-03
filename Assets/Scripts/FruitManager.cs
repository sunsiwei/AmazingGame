using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class FruitManager : MonoBehaviour
    {
        GameObject root;
        GameObject[] fruits;

        //// Use this for initialization
        //void Start() {
        //    root = new GameObject();
        //    root.name = "fruits";
        //    JsonData fruitsCfg = Game.Instance.GetLevel().GetFruitsCfg();
        //    fruits = new GameObject[fruitsCfg.Count];
        //    for (int i = 0; i < fruitsCfg.Count; i++)
        //    {
        //        StartCoroutine(FruitsAppear(fruitsCfg[i]));
        //    }
        //}

        //IEnumerator FruitsAppear(JsonData data)
        //{
        //    yield return new WaitForSeconds((int)data["delayShowTime"]);
        //    GameObject f = ResourcesLoader.LoadActor((string)data["name"]);
        //    f.transform.SetParent(root.transform);
        //    f.transform.position = new Vector2((int)data["position"][0], (int)data["position"][1]);
        //    Pacdot pd = f.GetComponent<Pacdot>();
        //    pd.score = (int)data["score"];
        //}

    }
}


