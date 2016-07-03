using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace PacmanGame
{
//    public class Level {
//        int index;
//        public int Index
//        {
//            get { return index; }
//        }
//        JsonData levelCfg;

//        GameObject pacman;
//        Dictionary<string, GameObject> ghosts;

//        GameObject dotRoot;

//        public Level(int _index)
//        {
//            index = _index;
//            levelCfg = ConfigManager.GetLevelCfg(index);
//        }

//        public void Init()
//        {
//            UIManager.GetInstance().ShowUI("UIMain");
//            //UIManager.GetInstance().ShowUI("UIControl");
//            ResourcesLoader.LoadOther("EasyTouchControlsCanvas");

//            DotModule dm = ModuleManager.Instance.GetModule(DotModule.name) as DotModule;
//            dm.InitDots(delegate()
//            {
//                AddPacman();
//                AddGhosts();
//                AddSuperPacdot();
//                AddFruitManager();

//                AudioManager.Instance.PlayAudio("intro");
//            });

            
//        }


//        public void ResetForPacmanDead()
//        {
//            foreach (GameObject obj in ghosts.Values)
//            {
//                GhostAIMove gim = obj.GetComponent<GhostAIMove>();
//                gim.MakeHome();
//            }
//            pacman.GetComponent<Pacman>().Relive(PacmanReliveCompleted);
//        }

//        public GameObject GetGhost(string gname)
//        {
//            return ghosts[gname];
//        }

//        public JsonData GetLevelCfg()
//        {
//            return levelCfg;
//        }
//        public string GetLevelName()
//        {
//            return (string)levelCfg["sceneName"];
//        }
//        public double GetPacmanExsitedSpeed()
//        {
//            return (double)levelCfg["pacmanExsitedSpeed"];
//        }
//        public int GetExsitedDuration()
//        {
//            return (int)levelCfg["exsitedDuration"];
//        }
//        public JsonData GetFruitsCfg()
//        {
//            return levelCfg["fruits"];
//        }

//// ----------------------- private function ---------------------
//        void PacmanReliveCompleted()
//        {
//            foreach (GameObject obj in ghosts.Values)
//            {
//                GhostAIMove gim = obj.GetComponent<GhostAIMove>();
//                gim.MakeHome();
//            }
//        }
//        void AddPacman()
//        {
//            pacman = ResourcesLoader.LoadActor((string)levelCfg["pacmanName"]);
//            pacman.name = (string)levelCfg["pacmanName"];
//            PacmanMove pm = pacman.GetComponent<PacmanMove>();
//            pm.SetPosition(new Vector2((int)levelCfg["pacmanPos"][0], (int)levelCfg["pacmanPos"][1]));
//            pm.ExpectDir = new Vector2((int)levelCfg["pacmanDir"][0], (int)levelCfg["pacmanDir"][1]);
//            pm.Speed = (double)levelCfg["pacmanSpeed"];
//        }

//        void AddGhosts()
//        {
//            JsonData ghostsCfg = levelCfg["ghosts"];
//            ghosts = new Dictionary<string, GameObject>();
//            for (int index = 0; index < ghostsCfg.Count; index++ )
//            {
//                JsonData ghostCfg = ghostsCfg[index];
//                GameObject ghost = ResourcesLoader.LoadActor((string)ghostCfg["name"]);
//                ghosts.Add((string)ghostCfg["name"], ghost);
                
//                GhostAIMove gim = ghost.GetComponent<GhostAIMove>();
//                gim.Init(ghostCfg);
//                gim.ScatterTarget = pacman.transform;
//                Ghost g = ghost.GetComponent<Ghost>();
//                g.score = (int)ghostCfg["score"];
//            }
//        }

//        void AddSuperPacdot()
//        {
//            JsonData superPacdotCfg = levelCfg["superPacdot"];
//            GameObject[] superPacdots = new GameObject[superPacdotCfg.Count];
//            for (int index = 0; index < superPacdotCfg.Count; index++)
//            {
//                JsonData dotCfg = superPacdotCfg[index];
//                GameObject dot = ResourcesLoader.LoadActor((string)dotCfg["name"]);
//                dot.name = (string)dotCfg["name"];
//                dot.transform.position = new Vector2((int)dotCfg["position"][0], (int)dotCfg["position"][1]);
//                superPacdots[index] = dot;
//            }
//        }
//        void AddFruitManager()
//        {
//            GameObject fm = GameObject.Find("FruitManager");
//            if (fm == null)
//            {
//                GameObject obj = new GameObject();
//                obj.name = "FruitManager";
//            }
//        }
//    }

	
}

