using System;
using System.Collections.Generic;
using UnityEngine;


namespace PacmanGame
{
    public enum UIHierarchy
    { 
        Main = 0,
        Panel = 1,
        Popup = 2
    }
	public class PageBase
	{
        
        UIHierarchy hierarchy = UIHierarchy.Panel;
        string path;
        string name;
        public string Name
        {
            get { return name; }
        }

        public GameObject gameObject;
        public Transform transform;
        object data;

        public PageBase(UIHierarchy _hierarchy, string _path)
        {
            hierarchy = _hierarchy;
            path = _path;
            name = _path.Substring(path.LastIndexOf("/") + 1);
        }

        //only once when prefeb loaded.
        protected virtual void Awake(GameObject go)
        { }
        //each time when show.
        protected virtual void Refresh()
        { }
        //active ui
        protected virtual void Active()
        {
            gameObject.SetActive(true);
        }
        //deactive ui
        protected virtual void Deactive()
        {
            gameObject.SetActive(false);
        }

        public void Show(object _data)
        {
            data = _data;
            if (gameObject == null)
            {
                gameObject = ResourcesLoader.LoadUI(name);
                if (gameObject == null)
                {
                    Debug.LogErrorFormat("[ui] can not find {0}.", name);
                    return;
                }
                transform = gameObject.transform;
                gameObject.name = name;
                AnchorPage(gameObject);

                Awake(gameObject);
            }
            Active();
            Refresh();
        }
        public void Hide()
        {
            Deactive();
        }
        public void Destroy()
        {
            GameObject.Destroy(gameObject);
            gameObject = null;
        }

        public bool IsActive
        {
            get { return gameObject != null && gameObject.activeSelf; }
        }

        void AnchorPage(GameObject go)
        {
            GameObject _rootCanvas = GameObject.Find("RootCanvas");
            if (_rootCanvas == null)
            {
                _rootCanvas = ResourcesLoader.LoadOther("RootCanvas");
                _rootCanvas.transform.name = "RootCanvas";
            }

            if (GameObject.Find("EventSystem") == null)
            {
                GameObject eventSystem = ResourcesLoader.LoadOther("EventSystem");
                eventSystem.transform.name = "EventSystem";
            }

            if (hierarchy == UIHierarchy.Main)
            {
                Transform MainRoot = _rootCanvas.transform.Find("MainRoot");
                go.transform.SetParent(MainRoot.transform, false);
            }else if(hierarchy == UIHierarchy.Panel)
            {
                Transform PanelRoot = _rootCanvas.transform.Find("PanelRoot");
                go.transform.SetParent(PanelRoot.transform, false);
            }
            else if (hierarchy == UIHierarchy.Panel)
            {
                Transform PopupRoot = _rootCanvas.transform.Find("PopupRoot");
                go.transform.SetParent(PopupRoot.transform, false);
            }

        }
	}
}
