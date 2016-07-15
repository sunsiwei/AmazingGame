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
        
        protected override void Awake(GameObject go)
        {
            Button btnStart = transform.Find("BtnStart").GetComponent<Button>();
            btnStart.onClick.AddListener(OnBtnStartClick);
        }

        void OnBtnStartClick()
        {
            //AmazingGame.Instance.Restart();
            PageManager.Instance.ShowPage("UIMenu");
        }
    }
}


