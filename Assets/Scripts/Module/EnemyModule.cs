using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class EnemyModule : ModuleBase
    {
        public static string name = "EnemyModule";

        public EnemyModule(string _name)
            : base(_name)
        {

        }



        public void Init()
        {

        }
        List<GameObject> enemys;
        public GameObject GetEnemy(SearchType st)
        {
            foreach (GameObject obj in enemys)
            {
                EnemyAISearch eas = obj.GetComponent<EnemyAISearch>();
                if (eas.SearchType == st)
                    return obj;
            }
            return null;
        }
    }
}


