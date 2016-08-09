using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class LevelEndPage : PageBase
    {

        public LevelEndPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {

        }

        protected override void Awake(GameObject go)
        {
            Button btnRestart = transform.Find("BtnRestart").GetComponent<Button>();
            btnRestart.onClick.AddListener(OnBtnRestartClick);
            Button btnNext = transform.Find("BtnNext").GetComponent<Button>();
            btnNext.onClick.AddListener(OnBtnNextClick);
            Button btnMainUI = transform.Find("BtnMainUI").GetComponent<Button>();
            btnMainUI.onClick.AddListener(OnBtnMainUI);
        }

        void OnBtnRestartClick()
        { 
            
        }
        void OnBtnNextClick()
        { 
        
        }
        void OnBtnMainUI()
        { 
            
        }
    }
}


