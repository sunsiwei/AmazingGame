using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class GameConst
    {
        public static string GameLevelType_Normal = "normal";
    }

    public class GameLevel
    {
        string type;
        int index;
        JsonData jsonLevel;
        int levelAmount;

        public int Index { get { return index; } }
        public string Type { get { return type; } }

        public JsonData JsonLevel { get { return jsonLevel; } }
        public string SceneName { get { return (string)jsonLevel["sceneName"]; } }
        public JsonData JsonPlayer { get { return jsonLevel["player"]; } }
        public JsonData JsonEnemys { get { return jsonLevel["enemys"]; } }
        public JsonData JsonEnemysBehaviors { get { return jsonLevel["enemyBehaviors"]; } }
        public JsonData JsonSpecialFoods { get { return jsonLevel["specialFoods"]; } }
        public JsonData JsonSpecialFoodPositions { get { return jsonLevel["specialFoodPositions"]; } }

        public GameLevel(string _type, int _index)
        {
            type = _type;
            index = _index;

            levelAmount = ConfigManager.Instance.GetCfg("gameLevelCfg")[_type]["levels"].Count;
            jsonLevel = ConfigManager.Instance.GetCfg("gameLevelCfg")[_type]["levels"][_index];
        }

        public void OnLevelLoaded(int index)
        {
            LevelModuleManager.Instance.OnLevelLoaded(this);
        }

        public void Passed()
        {
            MakePause(true);
            IOManager.Instance.recordData.levelIndex = Index;
            IOManager.Instance.FlushToFile();

            if (Index + 1 >= levelAmount)
            {
                Debug.Log("The End!!!");
                return;
            }

            PageManager.Instance.ShowPage("UILevelEnd");
        }

        public void MakePause(bool b)
        {
            LevelEnemyModule em = LevelModuleManager.Instance.GetModule(LevelEnemyModule.name) as LevelEnemyModule;
            em.MakeAllPause(b);
            LevelPlayerModule pm = LevelModuleManager.Instance.GetModule(LevelPlayerModule.name) as LevelPlayerModule;
            pm.MakePause(b);
			LevelFoodModule fm = LevelModuleManager.Instance.GetModule (LevelFoodModule.name) as LevelFoodModule;
			fm.MakePause (b);
        }
    }
}


