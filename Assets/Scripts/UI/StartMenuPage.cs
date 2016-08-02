using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using LitJson;

namespace PacmanGame
{
    public class StartMenuPage : PageBase
    {
        public StartMenuPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {

        }

        int selectedLevelIndex = 0;
        public int SelectedLevelIndex
        {
            set {

                int levelAmount = GameLevelManager.Instance.LevelsCount;
                if(value > levelAmount - 1)
                    selectedLevelIndex = levelAmount - 1;
                else
                    selectedLevelIndex = value;
                Refresh();
            }
        }

		Text txtLevel;
        protected override void Awake(GameObject go)
        {
            Button btnStart = transform.Find("BtnStart").GetComponent<Button>();
            btnStart.onClick.AddListener(OnBtnStartClick);

			Button btnLevelMenu = transform.Find ("BtnLevelMenu").GetComponent<Button> ();
			btnLevelMenu.onClick.AddListener (OnBtnLevelMenu);

			txtLevel = transform.Find ("TxtLevel").GetComponent<Text> ();
        }

        protected override void Refresh()
        {
            if (gameObject == null)
                return;

            txtLevel.text = "cur level: " + (selectedLevelIndex + 1);
        }

        void OnBtnStartClick()
        {
            GameLevelManager.Instance.EnterLevel(selectedLevelIndex);
            //PageManager.Instance.ShowPage("UIMenu");
        }
		void OnBtnLevelMenu()
		{
			PageManager.Instance.ShowPage ("UILevelMenu");
		}
    }
}


