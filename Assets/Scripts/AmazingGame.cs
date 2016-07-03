using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class AmazingGame : MonoBehaviour
    {
        AndroidJavaClass mJc;
        AndroidJavaObject mJo;

        GameLevel level;

        public static AmazingGame Instance
        {
            get { return GameObject.Find("AmazingGame").GetComponent<AmazingGame>(); }
        }

        void Start()
        {
            GameObject.DontDestroyOnLoad(this);

            ModuleManager.Instance.RegisterModules();

            ConfigManager.LoadCfg();

            UIManager.GetInstance().ShowUI("UIStartMenu");


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
            UIManager.GetInstance().Clear();
            ModuleManager.Instance.InitModules();

            level = new GameLevel(0);
            LoadLevelAsync(level.GetLevelName());
        }

        public void ToNextLevel()
        {
            int currentLevelIndex = level.Index;
            int totalLevelCount = ConfigManager.GetLevelsCfg().Count;
            if (level.Index + 1 >= totalLevelCount)
            {
                Debug.Log("The End!!!");
                Restart();
                return;
            }

            UIManager.GetInstance().Clear();

            level = new GameLevel(currentLevelIndex + 1);
            LoadLevelAsync(level.GetLevelName());
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

        void OnLevelWasLoaded(int index)
        {
            Debug.LogFormat("level {0} was loaded.", Application.loadedLevelName);

            if (Application.loadedLevelName != "Loading")
            {
                level.OnLevelLoaded();
            }
        }
    }
}


