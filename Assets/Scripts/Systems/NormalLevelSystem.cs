using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class NormalLevelSystem : SystemBase
    {
        public static string name = "NormalLevelSystem";

        public NormalLevelSystem(string _name)
            :base(_name)
        {

        }

        JsonData levelsCfg;

        int passedMaxLevelIndex = 0;
        public int PassedMaxLevelIndex
        {
            get { return passedMaxLevelIndex; }
        }

        public override void Create()
        {
            base.Create();
            JsonData levelCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            levelsCfg = levelCfg[GameConst.GameLevelType_Normal]["levels"];

            passedMaxLevelIndex = IOManager.Instance.recordData.levelIndex;
        }

        public int GetLevelAmount()
        {
            return levelsCfg.Count;
        }
    }
}