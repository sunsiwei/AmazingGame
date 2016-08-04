using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace PacmanGame
{
    public class MenuPage : PageBase
    {

        public MenuPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {

        }

        protected override void Awake(GameObject go)
        {
            Button btnRestart = transform.Find("BtnRestart").GetComponent<Button>();
            btnRestart.onClick.AddListener(OnBtnRestartClick);

            Button btnClose = transform.Find("BtnClose").GetComponent<Button>();
            btnClose.onClick.AddListener(OnBtnClose);
        }

        protected override void Active()
        {
            base.Active();
            //transform.DOMove(new Vector2(0, 1500), 1).From();

            GameLevelManager.Instance.Level.MakePause(true);
        }

        void OnBtnRestartClick()
        {
            GameLevelManager.Instance.RestartLevel();
        }

        void OnBtnClose()
        {
            Hide();

            GameLevelManager.Instance.Level.MakePause(false);
        }
    }
}


