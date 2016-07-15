using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System;

namespace PacmanGame
{
    public class AmazingGame : MonoBehaviour
    {
        public static int MapLayer = 8;
        public static int FoodLayer = 9;
        public static int EnemyLayer = 10;
        public static int PlayerLayer = 11;

        AndroidJavaClass mJc;
        AndroidJavaObject mJo;

        GameLevel level;
        public GameLevel Level
        { get { return level; } }

        public static AmazingGame Instance
        {
            get { return GameObject.Find("AmazingGame").GetComponent<AmazingGame>(); }
        }

        string fileName = "amazinggame.txt";
        string filePath;
        void Start()
        {
            if (Application.platform == RuntimePlatform.Android)
                filePath = Application.persistentDataPath;
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                filePath = Application.dataPath;
            else
                filePath = Application.dataPath;


            GameObject.DontDestroyOnLoad(this);

            ModuleManager.Instance.RegisterModules();
            PageManager.Instance.RegisterPages();

            ConfigManager.LoadCfg();


            PageManager.Instance.ShowPage("UIStartMenu");




            // 广告相关
            //mJc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //mJo = mJc.GetStatic<AndroidJavaObject>("currentActivity");
            //mJo.Call("showBanner");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 如果开发者使用了插屏广告，那么当按返回键的时候，逻辑应该如下：
                // 1、如果插屏广告在展示时，返回键应该先关闭正在展示的插屏广告，在按一次返回键才执行开发者自己的逻辑（如：退出应用）
                // 2、如果插屏广告没有在展示时，就进行自己的逻辑（如：退出应用等）

                // 当插屏广告已经消失了，就执行后续逻辑（这里为退出应用）
                // Android示例项目中定义0为返回键
                if (mJo.Call<bool>("closeSpot", 0) == true)
                {
                    Application.Quit();
                }
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                // 按Home键时，调用尝试关闭插屏广告的代码，开发者可以实现后续逻辑
                // Android示例项目中定义1为Home键
                if (mJo.Call<bool>("closeSpot", 1) == true)
                {

                }
            }
        }


        public void Restart()
        {
            StopAllCoroutines();

            ModuleManager.Instance.InitModules();

            level = new GameLevel(0);
            LoadLevelAsync(level.GetLevelName());
        }

        public void ToNextLevel()
        {
            StopAllCoroutines();

            int currentLevelIndex = level.Index;
            JsonData levelCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            int levelAmount = levelCfg["levels"].Count;
            if (level.Index + 1 >= levelAmount)
            {
                Debug.Log("The End!!!");
                Restart();
                return;
            }

            level = new GameLevel(currentLevelIndex + 1);
            LoadLevelAsync(level.GetLevelName());
        }

        public void GameOver()
        {
            StopAllCoroutines();
            Debug.Log("Game over!!!");

            EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
            em.MakeAllPause(true);

            PageManager.Instance.ShowPage("UIGameOver");
        }

        void LoadLevel(string name)
        {
            Application.LoadLevel(name);
        }

        public string nextSceneName;
        void LoadLevelAsync(string name)
        {
            nextSceneName = name;
            Application.LoadLevel("Loading");
        }

        public delegate void LevelLoadedHandler(int index);
        public event LevelLoadedHandler EventLevelLoaded;
        void OnLevelWasLoaded(int index)
        {
            Debug.LogFormat("level {0} was loaded.", Application.loadedLevelName);

            if (Application.loadedLevelName != "Loading")
            {
                level.OnLevelLoaded();
                EventLevelLoaded(level.Index);
            }
        }

        public int ReadLevelFromFile()
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
        public void WriteLvelToFile(int levelIndex)
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
    }
}


