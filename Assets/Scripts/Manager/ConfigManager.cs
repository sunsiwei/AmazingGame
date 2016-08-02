using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace PacmanGame
{
    //public class LevelDataList
    //{
    //    public LevelData[] list;
    //}

    //public class LevelData
    //{
    //    public int mazeWidth;
    //    public int mazeHeight;
    //    public int index;
    //    public string sceneName;
    //    public int[] pacmanPos;
    //    public string pacmanName;
    //    public float pacmanSpeed;
    //    public float pacmanExsitedSpeed;
    //}

    public class ConfigManager
    {

        static JsonData gameCfg;
        static JsonData levelsCfg;

        public static void LoadCfg()
        {
            TextAsset cfg1 = ResourcesLoader.LoadCfg("GameCfg") as TextAsset;
            gameCfg = JsonMapper.ToObject(cfg1.text);

            TextAsset cfg2 = ResourcesLoader.LoadCfg("LevelCfg") as TextAsset;
            levelsCfg = JsonMapper.ToObject(cfg2.text);
        }

        public static JsonData GetGameCfgItem(string itemName)
        {
            return gameCfg[itemName];
        }

        public static JsonData GetLevelsCfg()
        {
            return levelsCfg["list"]; 
        }

        public static JsonData GetLevelCfg(int index)
        {
            return levelsCfg["list"][index];
        }


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








