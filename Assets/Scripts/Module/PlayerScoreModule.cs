using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PlayerScoreModule : ModuleBase
    {
        public static string name = "PlayerScoreModule";

        public PlayerScoreModule(string _name)
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

        public override void Init()
        {
			base.Init ();
            score = 0;
        }

        public override void OnLevelLoaded(int levelIndex)
        {
            base.OnLevelLoaded(levelIndex);
            EventScoreUpdate(score);
        }
    }
}


