using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System;

namespace PacmanGame
{
	public class LevelModule : ModuleBase {
		
		public static string name = "LevelModule";


		JsonData levelsCfg;

		string fileName = "amazinggame.txt";
		string filePath;

		GameLevel level;
		int currentLevelIndex = 0;
		int passedLevelIndex = 0;

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
			passedLevelIndex = ReadPassedLevelIndexFromFile ();
		}

		public override void OnLevelLoaded (int index)
		{
			base.OnLevelLoaded (index);

		}

		public void EnterLevel(int levelIndex)
		{
			int levelAmount = levelsCfg["levels"].Count;
			if (levelIndex + 1 >= levelAmount)
			{
				Debug.Log("The End!!!");
				return;
			}

			level = new GameLevel(levelIndex);
			AmazingGame.Instance.EnterLevel (level.GetLevelName());
		}

		public JsonData GetLevelsData()
		{
			return levelsCfg;
		}

		public int GetCurrentLevelIndex()
		{
			return currentLevelIndex;
		}


		public int ReadPassedLevelIndexFromFile()
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
		public void WritePassedLvelIndexToFile(int levelIndex)
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
			int levelAmount = levelsCfg["levels"].Count;
			if (currentLevelIndex + 1 >= levelAmount)
			{
				Debug.Log("The End!!!");
				return;
			}
			currentLevelIndex += 1;
			EnterLevel (currentLevelIndex);
		}
	}
}


