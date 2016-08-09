using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class SystemManager
    {

        Dictionary<string, SystemBase> systems;

        public void SystemsRegister()
        {
            RegisterSystem(new PromptSystem(PromptSystem.name));
            RegisterSystem(new NormalLevelSystem(NormalLevelSystem.name));
            RegisterSystem(new DiamondSystem(DiamondSystem.name));
        }

        public void SystemsCreate()
        {
            foreach (SystemBase mb in systems.Values)
            {
                mb.Create();
            }
        }

        public SystemBase GetSystem(string systemName)
        {
            if (systems.ContainsKey(systemName) == false)
            {
                Debug.LogErrorFormat("error: system: {0} has not register!!!", systemName);
                return null;
            }
            return systems[systemName];
        }

        void RegisterSystem(SystemBase system)
        {
            if (systems == null)
                systems = new Dictionary<string, SystemBase>();

            if (systems.ContainsKey(system.Name))
            {
                Debug.LogErrorFormat("system {0} already registered!!!", system.Name);
                return;
            }
            systems.Add(system.Name, system);
        }



        public static SystemManager _Instance = null;
        private SystemManager()
        {
		}
        public static SystemManager Instance
		{
			get{
				if(_Instance == null)
                    _Instance = new SystemManager();
				return _Instance;
			}
		}
    }
}


