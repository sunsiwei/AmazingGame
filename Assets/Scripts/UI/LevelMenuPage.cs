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

            InitLevelItem();
		}

		protected override void Refresh ()
		{
            NormalLevelSystem nls = SystemManager.Instance.GetSystem(NormalLevelSystem.name) as NormalLevelSystem;
            int passedMaxLevelIndex = nls.PassedMaxLevelIndex;
			for(int i=0; i<levelItems.Length; i++)
			{
				Transform imgLock = levelItems[i].Find("ImgLock");
                if (i <= passedMaxLevelIndex+1)
				{
					imgLock.gameObject.SetActive(false);
				}else
				{
					imgLock.gameObject.SetActive(true);
				}

                if (i <= passedMaxLevelIndex+1)
                {
                    Button btn = levelItems[i].Find("Button").GetComponent<Button>();
                    int levelIndex = i;
					btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(delegate()
                    {
                        OnLevelItemClick(levelIndex);
                    });
                }
			}
		}

        protected override void Active()
        {
            base.Active();
            DoTween();
        }

        void InitLevelItem()
        {
            NormalLevelSystem nls = SystemManager.Instance.GetSystem(NormalLevelSystem.name) as NormalLevelSystem;
            int levelAmount = nls.GetLevelAmount();
            int passedMaxLevelIndex = nls.PassedMaxLevelIndex;
            levelItems = new Transform[levelAmount];
            
            Transform levelList = transform.Find("LevelList");
            
            for (int index = 0; index < levelItems.Length; index++)
            {
                GameObject item = ResourcesLoader.LoadUI("LevelItem");
                item.name = "LevelItem" + index;
                item.transform.SetParent(levelList, false);

                Text txt = item.transform.Find("Button").Find("Text").GetComponent<Text>();
                txt.text = "level: " + (index + 1);
                levelItems[index] = item.transform;
            }
        }
        void OnLevelItemClick(int index)
        {
            Debug.Log(index);
            StartMenuPage smp = PageManager.Instance.ShowPage("UIStartMenu") as StartMenuPage;
            smp.SelectedLevelIndex = index;

            Hide();
        }
		
		void OnBtnClose()
		{
			Hide ();
		}


	}
}


