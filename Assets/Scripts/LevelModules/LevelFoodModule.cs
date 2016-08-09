using UnityEngine;
using System.Collections;
using LitJson;
using System;

namespace PacmanGame
{
    public class LevelFoodModule : LevelModuleBase
    {
        public static string name = "LevelFoodModule";

        public LevelFoodModule(string _name)
            : base(_name)
        { 
            
        }

        int normalFoodAmount;
        int alreadyEatFoodCount;

        public int AlreadyEatFoodCount
        {
            get { return alreadyEatFoodCount; }
            set {
                alreadyEatFoodCount = value;
                if (alreadyEatFoodCount >= normalFoodAmount)
                    level.Passed();

            }
        }

        public override void OnLevelLoaded(GameLevel level)
        {
            base.OnLevelLoaded(level);
            int index = level.Index;
            normalFoodAmount = (int)level.JsonLevel["foodAmount"];

            alreadyEatFoodCount = 0;

            AmazingGame.Instance.StartCoroutine(DelayAppearSpecialFood(index));
        }

        IEnumerator DelayAppearSpecialFood(int index)
        {
            Debug.Log("ready add food.");
            JsonData specialFoods = level.JsonSpecialFoods;
            JsonData specialFoodPositions = level.JsonSpecialFoodPositions;

            float time = 0;
            while (true)
            {
                time = time + Time.deltaTime;
                if (time > 5)
                {
                    int randomNameIndex = UnityEngine.Random.Range(0, specialFoods.Count);
                    string foodName = (string)specialFoods[randomNameIndex];
                    int randomPositionIndex = UnityEngine.Random.Range(0, specialFoodPositions.Count);
                    Vector2 foodPosition = new Vector2((int)specialFoodPositions[randomPositionIndex][0], (int)specialFoodPositions[randomPositionIndex][1]);
                    GameObject food = ResourcesLoader.LoadFood(foodName);
                    food.transform.position = foodPosition;
                    Debug.LogFormat("add food: {0}.", foodName);
                    time = 0;
                }
                yield return 0;

				while(pause)
				{
					yield return 0;
				}
            }
        }

		bool pause = false;
		public void MakePause(bool b)
		{
			pause = b;
		}
    }
}


