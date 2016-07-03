using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
	public class UIManager {
		
		GameObject _rootCanvas = null;
		Dictionary<string, GameObject> uiDic = null;
		
		public GameObject ShowUI(string uiname)
		{
			if (GameObject.Find("RootCanvas") == null)
			{
				_rootCanvas = ResourcesLoader.LoadOther("RootCanvas");
				_rootCanvas.transform.name = "RootCanvas";

			}
			
			if (GameObject.Find ("EventSystem") == null) {
				GameObject eventSystem = ResourcesLoader.LoadOther("EventSystem");
				eventSystem.transform.name = "EventSystem";
			}

            if (uiDic != null && uiDic.ContainsKey(uiname))
                return uiDic[uiname];

            GameObject ui = ResourcesLoader.LoadUI(uiname);
            ui.name = uiname;
			ui.transform.SetParent (_rootCanvas.transform, false);
			
			if (uiDic == null)
				uiDic = new Dictionary<string, GameObject> ();
            uiDic.Add(uiname, ui);
			return ui;
		}
		
		public void HideUI(string name)
		{
			if (uiDic != null && uiDic.ContainsKey (name)) 
			{
				GameObject ui = uiDic[name];
				GameObject.Destroy(ui);
				uiDic.Remove(name);
			}
		}
		
		public GameObject GetUI(string name)
		{
			if (uiDic.ContainsKey (name) == false)
				return null;
			return uiDic [name];
		}
		
		public void Clear()
		{
			uiDic = new Dictionary<string, GameObject> ();
		}
		

		
		private static UIManager _Instance = null;
		private UIManager() { }
		public static UIManager GetInstance()
		{
			if (_Instance == null)
				_Instance = new UIManager();
			return _Instance;
		}
	}
}

