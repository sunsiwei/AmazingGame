using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class PlayerModule : ModuleBase
    {
        public static string name = "PlayerModule";
        public PlayerModule(string _name)
            : base(_name)
        {

        }

		JsonData playerCfg;
		GameObject player;

		public override void Init ()
		{
			base.Init ();

		}

		public override void OnLevelLoaded ()
		{
			base.OnLevelLoaded ();
		}

        public GameObject GetPlayer()
        {
            return null;
        }
    }
}


