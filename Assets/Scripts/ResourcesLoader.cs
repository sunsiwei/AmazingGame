using UnityEngine;
using System.Collections;

namespace PacmanGame
{
	public class ResourcesLoader {
		
		public static GameObject LoadUI(string name)
		{
            return Object.Instantiate(Resources.Load("UI/" + name)) as GameObject;
		}
		public static GameObject LoadOther(string name)
		{
            return Object.Instantiate(Resources.Load("Others/" + name), Vector3.zero, Quaternion.identity) as GameObject;
		}
		public static GameObject LoadActor(string name)
		{
			return Object.Instantiate(Resources.Load("Actors/" + name), Vector3.zero, Quaternion.identity) as GameObject;
		}
		public static GameObject LoadMap(string name)
		{
            return Object.Instantiate(Resources.Load("Maps/" + name), Vector3.zero, Quaternion.identity) as GameObject;
		}
		public static Object LoadCfg(string name)
		{
			return Resources.Load("Configs/" + name);
		}
        public static AudioClip LoadAudioClip(string name)
        {
            return Resources.Load("Audio/" + name) as AudioClip;
        }
        public static GameObject LoadFood(string name)
        {
            return Object.Instantiate(Resources.Load("Foods/" + name), Vector3.zero, Quaternion.identity) as GameObject;
        }
	}
}


