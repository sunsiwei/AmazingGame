using UnityEngine;
using System.Collections;

using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace PacmanGame
{
    public class GameLevelManager
    {
        

        GameLevel level = null;
        public GameLevel Level { get { return level; } }

        public delegate void LevelLoadedHandle(int levelIndex);
        public event LevelLoadedHandle EventLevelLoaded;

        public void Init()
		{
			
		}

        public void EnterLevel(string levelType, int levelIndex)
        {
            int levelAmount = 0;
            if (levelType == GameConst.GameLevelType_Normal)
            { 
                NormalLevelSystem nls = SystemManager.Instance.GetSystem(NormalLevelSystem.name) as NormalLevelSystem;
                levelAmount = nls.GetLevelAmount();
            }
            
            if (levelIndex + 1 > levelAmount)
            {
                Debug.Log("The End!!!");
                return;
            }

            level = new GameLevel(levelType, levelIndex);
            AmazingGame.Instance.LoadLevel(level.SceneName, OnLevelLoaded);
        }

        public void EnterNextLevel(string levelType, int levelIndex)
        {
            EnterLevel(levelType, levelIndex + 1);
        }

        public void RestartLevel()
        {
            EnterLevel(level.Type, level.Index);
        }

        void OnLevelLoaded(int sceneIndex)
        {
            level.OnLevelLoaded(level.Index);

            if (EventLevelLoaded != null)
                EventLevelLoaded(level.Index);
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




