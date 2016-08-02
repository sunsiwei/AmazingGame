using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class ModuleManager
    {

        Dictionary<string, ModuleBase> modules;

		// when application start excute once
        public void RegisterModules()
        {
            // here init each module.
            RegisterModule(new PlayerModule(PlayerModule.name));
            RegisterModule(new PromptModule(PromptModule.name));
            RegisterModule(new PlayerScoreModule(PlayerScoreModule.name));
            RegisterModule(new EnemyModule(EnemyModule.name));
            RegisterModule(new FoodModule(FoodModule.name));
        }

		// excute it when every game level loaded
        public void OnLevelLoaded(int index)
        {
            foreach (ModuleBase mb in modules.Values)
            {
                mb.OnLevelLoaded(index);
            }
        }

        public ModuleBase GetModule(string moduleName)
        {
            if (modules.ContainsKey(moduleName) == false)
            {
                Debug.LogErrorFormat("error: module: {0} has not register!!!", moduleName);
                return null;
            }
            return modules[moduleName];
        }

        void RegisterModule(ModuleBase module)
        {
            if (modules == null)
                modules = new Dictionary<string, ModuleBase>();

            if (modules.ContainsKey(module.Name))
            {
                Debug.LogErrorFormat("module {0} already registered!!!", module.Name);
                return;
            }
            modules.Add(module.Name, module);
        }

        private static ModuleManager _Instance = null;
        private ModuleManager() { }
        public static ModuleManager Instance
		{
            get
            {
                if (_Instance == null)
                    _Instance = new ModuleManager();
                return _Instance;
            }
		}
    }
}


