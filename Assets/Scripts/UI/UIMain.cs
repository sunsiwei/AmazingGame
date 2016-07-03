using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
	public class UIMain : UIBase {

		public Text txtScore;
        public Text txtLives;

		void Awake()
		{
            ScoreModule sm = ModuleManager.Instance.GetModule(ScoreModule.name) as ScoreModule;
            sm.EventScoreUpdate += new ScoreModule.ScoreUpdateHandler(EventScoreUpdate);
		}

        void Start()
        {

        }
		
		public void ShowMenu()
		{
			UIManager.GetInstance ().ShowUI ("UIMenu");
		}

        void EventScoreUpdate(int score)
		{
            txtScore.text = "score: " + score;
		}
        void EventLivesUpdate(int lives)
        {
            txtLives.text = "lives: " + lives;
        }
        
	}
}


