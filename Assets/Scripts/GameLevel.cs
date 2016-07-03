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
            levelCfg = ConfigManager.GetLevelCfg(index);
        }

        public void OnLevelLoaded()
        {
            UIManager.GetInstance().ShowUI("UIMain");
            ResourcesLoader.LoadOther("EasyTouchControlsCanvas");


            ModuleManager.Instance.OnLevelLoaded();
        }

        public string GetLevelName()
        {
            return (string)levelCfg["sceneName"];
        }
    }
}


