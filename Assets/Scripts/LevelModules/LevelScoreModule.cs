using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class LevelScoreModule : LevelModuleBase
    {
        public static string name = "PlayerScoreModule";

        public LevelScoreModule(string _name)
            :base(_name)
        {

        }

        public delegate void ScoreUpdateHandler(int score);
        public event ScoreUpdateHandler EventScoreUpdate;


        int score;
        public int Score
        {
            get { return score; }
            set { 
                score = value;
                if (EventScoreUpdate != null)
                    EventScoreUpdate(score);
            }
        }

        public override void OnLevelLoaded(GameLevel level)
        {
            base.OnLevelLoaded(level);
            int levelIndex = level.Index;

			score = 0;

            EventScoreUpdate(score);
        }
    }
}


