using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class UIGameOver : UIBase
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void RestartGame()
        {
            AmazingGame.Instance.Restart();
        }
    }
}


