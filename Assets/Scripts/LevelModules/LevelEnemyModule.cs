using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace PacmanGame
{
    public class LevelEnemyModule : LevelModuleBase
    {
        public static string name = "LevelEnemyCtl";

        public LevelEnemyModule(string _name)
            : base(_name)
        {

        }

        List<GameObject> enemys;
        JsonData enemysCfg;

        public override void OnLevelLoaded(GameLevel level)
        {
            base.OnLevelLoaded(level);
            enemys = new List<GameObject>();

            enemysCfg = level.JsonEnemys;
            JsonData enemyBehaviors = level.JsonEnemysBehaviors;

            for (int index = 0; index < enemysCfg.Count; index++)
            {
                JsonData enemyCfg = enemysCfg[index];
                GameObject enemyObj = ResourcesLoader.LoadActor((string)enemyCfg["name"]);
                enemyObj.name = (string)enemyCfg["name"];
                enemys.Add(enemyObj);

                EnemyAISearch eas = enemyObj.GetComponent<EnemyAISearch>();
                eas.InitialPosition = new Vector2((int)enemyCfg["position"][0], (int)enemyCfg["position"][1]);
                eas.InitialSpeed = (double)enemyCfg["speed"];
                eas.AfraidSpeed = (double)enemyCfg["afraidSpeed"];
                eas.ScatterPosition = new Vector2((int)enemyCfg["scatterPosition"][0], (int)enemyCfg["scatterPosition"][1]);
                eas.SearchType = (SearchType)(int)enemyCfg["searchType"];
                eas.SetBehaviors(enemyBehaviors);

                ActorScore es = enemyObj.GetComponent<ActorScore>();
                es.Score = (int)enemyCfg["score"];
            }
        }


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

        public void MakeAllHome()
        {
            foreach (GameObject obj in enemys)
            {
                EnemyAISearch eas = obj.GetComponent<EnemyAISearch>();
                eas.MakeHome();
            }
        }

        public void MakeAllContivueSearch()
        {
            foreach (GameObject obj in enemys)
            {
                EnemyAISearch eas = obj.GetComponent<EnemyAISearch>();
                eas.ContinueOriginalBehavior();
            }
        }

        public void MakeAllPause(bool pause)
        {
            foreach (GameObject obj in enemys)
            {
                EnemyAISearch eas = obj.GetComponent<EnemyAISearch>();
                eas.PauseSearch = pause;
            }
        }
    }
}


