using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class PageManager
    {
        Dictionary<string, PageBase> pages;


        public void RegisterPages()
        {
            RegisterPage(new StartMenuPage(UIHierarchy.Panel, "UI/UIStartMenu"));
            RegisterPage(new MainPage(UIHierarchy.Main, "UI/UIMain"));
            RegisterPage(new GameOverPage(UIHierarchy.Panel, "UI/UIGameOver"));
            RegisterPage(new MenuPage(UIHierarchy.Panel, "UI/UIMenu"));
            RegisterPage(new PromptPage(UIHierarchy.Popup, "UI/UIPrompt"));
			RegisterPage(new LevelMenuPage(UIHierarchy.Panel, "UI/UILevelMenu"));
            RegisterPage(new LevelMenuPage(UIHierarchy.Panel, "UI/UILevenEnd"));
            RegisterPage(new TestPayPage(UIHierarchy.Panel, "UI/UITestPay"));
        }

        public void Reset()
        { 

        }

        public PageBase ShowPage(string pageName, object data = null)
        {
            if (pages.ContainsKey(pageName) == false)
            {
                Debug.LogErrorFormat("error: uipage: {0} has not register!!!", pageName);
                return null;
            }
            PageBase page = pages[pageName];
            page.Show(data);
            return page;
        }

        public void HidePage(string pageName)
        {
            if (pages.ContainsKey(pageName) == false)
            {
                Debug.LogErrorFormat("error: uipage: {0} has not register!!!", pageName);
                return;
            }
            PageBase page = pages[pageName];
            page.Hide();
        }

        public PageBase GetPage(string pageName)
        {
            if (pages.ContainsKey(pageName) == false)
            {
                Debug.LogErrorFormat("error: uipage: {0} has not register!!!", pageName);
                return null;
            }
            PageBase page = pages[pageName];
            if(page.gameObject == null)
                 PageManager.Instance.ShowPage(pageName);
            return page;
        }

        void RegisterPage(PageBase page)
        {
            if (pages == null)
                pages = new Dictionary<string, PageBase>();

            if (pages.ContainsKey(page.Name))
            {
                Debug.LogErrorFormat("page {0} already registered!!!", page.Name);
                return;
            }
            pages.Add(page.Name, page);
        }

        private static PageManager _Instance = null;
        private PageManager() { }
        public static PageManager Instance
		{
            get
            {
                if (_Instance == null)
                    _Instance = new PageManager();
                return _Instance;
            }
		}
    }
}


