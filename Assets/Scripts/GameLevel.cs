using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class GameLevel
    {
        int index;
        public int Index
        {
            get { return index; }
        }

        JsonData levelCfg;

        public string SceneName
        {
            get { return (string)levelCfg["sceneName"]; }
        }

        public GameLevel(int _index)
        {
            index = _index;
            JsonData levelsCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            levelCfg = levelsCfg["levels"][index];
        }

        public void MakePause(bool b)
        {
            EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
            em.MakeAllPause(b);
            PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
            pm.MakePause(b);
        }
    }
}


