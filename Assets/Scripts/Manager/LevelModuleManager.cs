using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class LevelModuleManager
    {

        Dictionary<string, LevelModuleBase> modules;

		// when application start excute once
        public void RegisterLevelModules()
        {
            // here init each module.
            RegisterLevelModule(new LevelPlayerModule(LevelPlayerModule.name));
            RegisterLevelModule(new LevelScoreModule(LevelScoreModule.name));
            RegisterLevelModule(new LevelEnemyModule(LevelEnemyModule.name));
            RegisterLevelModule(new LevelFoodModule(LevelFoodModule.name));
            RegisterLevelModule(new LevelUIModule(LevelUIModule.name));
        }

		// excute it when every game level loaded
        public void OnLevelLoaded(GameLevel level)
        {
            foreach (LevelModuleBase mb in modules.Values)
            {
                mb.OnLevelLoaded(level);
            }
        }

        public LevelModuleBase GetModule(string moduleName)
        {
            if (modules.ContainsKey(moduleName) == false)
            {
                Debug.LogErrorFormat("error: module: {0} has not register!!!", moduleName);
                return null;
            }
            return modules[moduleName];
        }

        void RegisterLevelModule(LevelModuleBase module)
        {
            if (modules == null)
                modules = new Dictionary<string, LevelModuleBase>();

            if (modules.ContainsKey(module.Name))
            {
                Debug.LogErrorFormat("module {0} already registered!!!", module.Name);
                return;
            }
            modules.Add(module.Name, module);
        }

        private static LevelModuleManager _Instance = null;
        private LevelModuleManager() { }
        public static LevelModuleManager Instance
		{
            get
            {
                if (_Instance == null)
                    _Instance = new LevelModuleManager();
                return _Instance;
            }
		}
    }
}


