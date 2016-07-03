using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PacmanGame
{
	
	public class UIMenu : UIBase {
		
		void Start()
		{
			
		}
		
		public void RestartGame()
		{
			AmazingGame.Instance.Restart ();
		}
		
	}

}
