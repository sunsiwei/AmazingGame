using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;

namespace PacmanGame
{
	public class LevelMenuPage : PageBase
	{
		public LevelMenuPage(UIHierarchy _hierarchy, string _path)
			:base(_hierarchy, _path)
		{
			
		}

		Transform[] levelItems;
		protected override void Awake(GameObject go)
		{
			Button btnClose = transform.Find("BtnClose").GetComponent<Button>();
			btnClose.onClick.AddListener(OnBtnClose);

			LevelModule lm = ModuleManager.Instance.GetModule (LevelModule.name) as LevelModule;
			JsonData levelsData = lm.GetLevelsData ();
			Transform levelList = transform.Find ("LevelList");
			levelItems = new Transform[levelsData.Count];

			for(int index=0; index<levelItems.Length; index++)
			{
				GameObject item = ResourcesLoader.LoadUI("LevelItem");
				item.name = "LevelItem"+index;
				item.transform.SetParent(levelList, false);
				item.transform.localPosition = new Vector2(index * 300, 0);
				Text txt = item.transform.Find("Button").Find("Text").GetComponent<Text>();
				txt.text = "level: " + index;
				levelItems[index] = item.transform;
			}
		}

		protected override void Refresh ()
		{
			LevelModule lm = ModuleManager.Instance.GetModule (LevelModule.name) as LevelModule;
			int curLevelIndex = lm.GetCurrentLevelIndex ();
			for(int i=0; i<levelItems.Length; i++)
			{
				Transform imgLock = levelItems[i].Find("ImgLock");
				if(i <= curLevelIndex)
				{
					imgLock.gameObject.SetActive(false);
				}else
				{
					imgLock.gameObject.SetActive(true);
				}
			}
		}
		
		void OnBtnClose()
		{
			Hide ();
		}


	}
}


