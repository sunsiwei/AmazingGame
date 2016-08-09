using UnityEngine;
using System.Collections;
using LitJson;


namespace PacmanGame
{
    public class AmazingGame : MonoBehaviour
    {
        public static int MapLayer = 8;
        public static int FoodLayer = 9;
        public static int EnemyLayer = 10;
        public static int PlayerLayer = 11;

        public static AmazingGame Instance
        {
            get { return GameObject.Find("AmazingGame").GetComponent<AmazingGame>(); }
        }

        void Start()
        {
            GameObject.DontDestroyOnLoad(this);

            ConfigManager.LoadCfg();
            IOManager.Instance.Init();

            LevelModuleManager.Instance.RegisterLevelModules();
            PageManager.Instance.RegisterPages();
            SystemManager.Instance.SystemsRegister();
            SystemManager.Instance.SystemsCreate();


            //YouMiManager.Instance.Init();
            GameLevelManager.Instance.Init();

            StartMenuPage smp = PageManager.Instance.ShowPage("UIStartMenu") as StartMenuPage;
            NormalLevelSystem nls = SystemManager.Instance.GetSystem(NormalLevelSystem.name) as NormalLevelSystem;
            smp.SelectedLevelIndex = nls.PassedMaxLevelIndex + 1;
        }

        void Update()
        {
            //YouMiManager.Instance.Update();
        }

        public delegate void LevelLoadedHandler(int index);
        LevelLoadedHandler LevelLoaded;
		public void LoadLevel(string levelName, LevelLoadedHandler loadedBack)
		{
			StopAllCoroutines();
			LoadLevelAsync (levelName);
            LevelLoaded = loadedBack;
		}
        void OnLevelWasLoaded(int index)
        {
            Debug.LogFormat("level {0} was loaded.", Application.loadedLevelName);

            if (Application.loadedLevelName != "Loading")
            {
                LevelLoaded(index);
            }
        }

        void LoadLevelSync(string name)
        {
            Application.LoadLevel(name);
        }

        public string nextSceneName;
        void LoadLevelAsync(string name)
        {
            nextSceneName = name;
            Application.LoadLevel("Loading");
        }

        

        
    }
}


