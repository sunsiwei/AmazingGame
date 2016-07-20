using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System;

namespace PacmanGame
{
	public class LevelModule : ModuleBase {
		
		public static string name = "LevelModule";

        public delegate void LevelLoadedHandle(int levelIndex);
        public event LevelLoadedHandle EventLevelLoaded;

		JsonData levelsCfg;

		string fileName = "amazinggame.txt";
		string filePath;

		GameLevel level;
		int passedMaxLevelIndex = 0;
        public int PassedMaxLevelIndex
        {
            get { return passedMaxLevelIndex; }
        }

		public LevelModule(string _name)
			: base(_name)
		{
			FoodModule fm = ModuleManager.Instance.GetModule (FoodModule.name) as FoodModule;
			fm.EventFoodsEatUp += EventFoodsEatUp;

			JsonData levelCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
			levelsCfg = levelCfg["levels"];

			if (Application.platform == RuntimePlatform.Android)
				filePath = Application.persistentDataPath;
			else if (Application.platform == RuntimePlatform.WindowsEditor)
				filePath = Application.dataPath;
			else
				filePath = Application.dataPath;

            passedMaxLevelIndex = ReadPassedLevelIndexFromFile();
		}

        
		public override void OnLevelLoaded (int index)
		{
            EventLevelLoaded(index);
            PageManager.Instance.ShowPage("UIMain");
		}

		public void EnterLevel(int levelIndex)
		{
            int levelAmount = levelsCfg.Count;
			if (levelIndex + 1 > levelAmount)
			{
				Debug.Log("The End!!!");
				return;
			}

			level = new GameLevel(levelIndex);
		}

        public void RestartLevel()
        {
            int curLevelIndex = level.Index;
            EnterLevel(curLevelIndex);
        }

        public GameLevel GetLevel()
        {
            return level;
        }

		public JsonData GetLevelsData()
		{
			return levelsCfg;
		}
        
		int ReadPassedLevelIndexFromFile()
		{
			StreamReader sr = null;
			try
			{
				sr = File.OpenText(filePath + "/" + fileName);
			}
			catch (Exception e)
			{
				return 0;
			}
			string str = sr.ReadToEnd();
			JsonData jd = JsonMapper.ToObject(str);
			int level = (int)jd["level"];
			sr.Close();
			sr.Dispose();
			return level;
		}
		void WritePassedLvelIndexToFile(int levelIndex)
		{
			StreamWriter sw;
			FileInfo f = new FileInfo(filePath + "/" + fileName);
			if (f.Exists)
			{
				sw = f.CreateText();
			}
			else
			{
				sw = f.CreateText();
			}
			sw.WriteLine("{'level':" + levelIndex + "}");
			sw.Close();
			sw.Dispose();
		}

		void EventFoodsEatUp()
		{
            WritePassedLvelIndexToFile(level.Index);

			int levelAmount = levelsCfg.Count;
			if (level.Index + 1 >= levelAmount)
			{
				Debug.Log("The End!!!");
				return;
			}
           
            int index = level.Index + 1;
            EnterLevel(index);
		}
	}
}


