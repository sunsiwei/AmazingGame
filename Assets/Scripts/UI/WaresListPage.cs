using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class WaresListPage : PageBase
    {
        public WaresListPage(UIHierarchy _hierarchy, string _path)
			:base(_hierarchy, _path)
		{
			
		}

        Transform[] wareItem;
		protected override void Awake(GameObject go)
		{
			Button btnClose = transform.Find("BtnClose").GetComponent<Button>();
			btnClose.onClick.AddListener(OnBtnClose);

            InitWareItem();
		}

        protected override void Refresh()
        { 
            
        }
        void InitWareItem()
        {
            
        }
        void OnBtnClose()
        {
            Hide();
        }
    }
}

