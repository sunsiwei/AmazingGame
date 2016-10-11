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
                NormalLevelSystem nls = SystemManager.Instance.GetSystem(NormalLevelSystem.name) as NormalLevelSystem;
                int levelAmount = nls.GetLevelAmount();
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
            GameLevelManager.Instance.EnterLevel(GameConst.GameLevelType_Normal, selectedLevelIndex);
        }
		void OnBtnLevelMenu()
		{
            PageManager.Instance.ShowPage ("UILevelMenu");

            //PageManager.Instance.ShowPage("UITestPay");
		}
    }
}


