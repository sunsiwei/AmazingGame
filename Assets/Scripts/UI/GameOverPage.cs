using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class GameOverPage : PageBase
    {

        public GameOverPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {

        }

        protected override void Awake(GameObject go)
        {
            Button btnRestart = transform.Find("BtnRestart").GetComponent<Button>();
            btnRestart.onClick.AddListener(OnBtnRestartClick);
        }

        void OnBtnRestartClick()
        {
            AmazingGame.Instance.Restart();
        }
    }
}



