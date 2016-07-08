using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
	public class UIMain : UIBase {
        [SerializeField]
		private Text txtScore;
        [SerializeField]
        private Text txtLives;

		void Awake()
		{
            PlayerScoreModule sm = ModuleManager.Instance.GetModule(PlayerScoreModule.name) as PlayerScoreModule;
            sm.EventScoreUpdate += EventScoreUpdate;

            PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
            pm.EnentPlayerLivesUpdate += EventLivesUpdate;
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
            //txtLives.text = "lives: " + lives;
        }
        
	}
}


