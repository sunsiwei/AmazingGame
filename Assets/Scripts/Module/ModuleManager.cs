using UnityEngine;
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
            RegisterModule(new PromptModule(PromptModule.name));
            //RegisterModule(new DotModule(DotModule.name));
        }

		//excute it when every restart game
        public void InitModules()
        { 
            foreach(ModuleBase mb in modules.Values)
            {
                mb.Init();
            }
        }

		// excute it when every game level loaded
        public void OnLevelLoaded()
        {
            foreach (ModuleBase mb in modules.Values)
            {
                mb.OnLevelLoaded();
            }
        }

        public ModuleBase GetModule(string moduleName)
        {
            if (modules.ContainsKey(moduleName) == false)
                return null;
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


