using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace PacmanGame
{
    public class ConfigManager
    {
		public static ConfigManager _Instance = null;
		private ConfigManager(){
		}
		public static ConfigManager Instance
		{
			get{
				if(_Instance == null)
					_Instance = new ConfigManager();
				return _Instance;
			}
		}

		Dictionary<string, JsonData> cfgMap;

		public JsonData GetCfg(string cfgName)
		{
			if (cfgMap == null)
				cfgMap = new Dictionary<string, JsonData> ();
			if (cfgMap.ContainsKey (cfgName))
				return cfgMap [cfgName];
			TextAsset ta = ResourcesLoader.LoadCfg (cfgName) as TextAsset;
			JsonData jd = JsonMapper.ToObject (ta.text);
			cfgMap.Add (cfgName, jd);
			return jd;
		}

    }
}








