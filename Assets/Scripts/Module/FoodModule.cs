using UnityEngine;
using System.Collections;
using LitJson;
using System;

namespace PacmanGame
{
    public class FoodModule : ModuleBase
    {
        public static string name = "FoodModule";

        public FoodModule(string _name)
            : base(_name)
        { 
            
        }

        //public delegate void FoodEatUpHandler();
        //public event FoodEatUpHandler EventEatUp;

        int normalFoodAmount;
        int alreadyEatFoodCount;

        public int AlreadyEatFoodCount
        {
            get { return alreadyEatFoodCount; }
            set {
                alreadyEatFoodCount = value;
                if (alreadyEatFoodCount >= normalFoodAmount)
                    EnterNextLevel();
            }
        }

        public override void OnLevelLoaded(int index)
        {
            base.OnLevelLoaded(index);
            JsonData levelCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            normalFoodAmount = (int)levelCfg["levels"][index]["FoodAmount"];
            
        }

        void EnterNextLevel()
        {
            AmazingGame.Instance.ToNextLevel();
        }
    }
}


