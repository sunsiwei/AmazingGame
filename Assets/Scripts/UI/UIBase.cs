using UnityEngine;
using System.Collections;

namespace PacmanGame
{
	public class UIBase : MonoBehaviour {
        public void HideUI()
        {
            UIManager.GetInstance().HideUI(name);
        }
	}
}


