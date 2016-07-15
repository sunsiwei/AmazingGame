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
        }

        public void OnLevelLoaded()
        {
            PageManager.Instance.ShowPage("UIMain");

            JsonData playerCfg = ConfigManager.Instance.GetCfg("playerCfg");
            int playerCtlType = (int)playerCfg["playerControlType"];
            if (playerCtlType == 0)
            {
                ResourcesLoader.LoadOther("SwipeControl");
            }
            else if (playerCtlType == 1)
            {
                ResourcesLoader.LoadOther("JoystickControl");
            }
            

            ModuleManager.Instance.OnLevelLoaded(index);
        }

        public string GetLevelName()
        {
            return (string)levelCfg["sceneName"];
        }
    }
}


