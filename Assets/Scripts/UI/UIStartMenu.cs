using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PacmanGame
{

    public class UIStartMenu : UIBase
    {
		
		void Start()
		{
			
		}
		
		public void StartGame()
		{
            AmazingGame.Instance.Restart();
		}
		
	}

}
