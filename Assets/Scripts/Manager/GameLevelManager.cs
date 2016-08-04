using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System;

namespace PacmanGame
{
    public class GameLevelManager
    {
        JsonData levelsCfg;

        string fileName = "amazinggame.txt";
        string filePath;
        
        int passedMaxLevelIndex = 0;
        public int PassedMaxLevelIndex
        {
            get { return passedMaxLevelIndex; }
        }

        GameLevel level = null;
        public GameLevel Level { get { return level; } }

        public delegate void LevelLoadedHandle(int levelIndex);
        public event LevelLoadedHandle EventLevelLoaded;

        public void Init()
		{
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

        public void EnterLevel(int levelIndex)
        {
            int levelAmount = levelsCfg.Count;
            if (levelIndex + 1 > levelAmount)
            {
                Debug.Log("The End!!!");
                return;
            }

            level = new GameLevel(levelIndex);
            AmazingGame.Instance.LoadLevel(level.SceneName, OnLevelLoaded);
        }
        void OnLevelLoaded(int sceneIndex)
        {
            level.OnLevelLoaded(level.Index);

            if (EventLevelLoaded != null)
                EventLevelLoaded(level.Index);
        }

        public void RestartLevel()
        {
            int curLevelIndex = level.Index;
            EnterLevel(curLevelIndex);
        }

        public int LevelsCount
        {
            get { return levelsCfg.Count; }
        }

        public void LevelPassed()
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

        private static GameLevelManager _Instance = null;
        private GameLevelManager() { }
        public static GameLevelManager Instance
		{
            get
            {
                if (_Instance == null)
                    _Instance = new GameLevelManager();
                return _Instance;
            }
		}
    }
}




