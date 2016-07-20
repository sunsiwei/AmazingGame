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

        public GameLevel(int _index)
        {
            index = _index;
            JsonData levelsCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            levelCfg = levelsCfg["levels"][index];

            AmazingGame.Instance.LoadLevel((string)levelCfg["sceneName"], OnLevelLoaded);
        }

        void OnLevelLoaded(int levelSceneIndex)
        {
            ModuleManager.Instance.OnLevelLoaded(index);
        }
    }
}


