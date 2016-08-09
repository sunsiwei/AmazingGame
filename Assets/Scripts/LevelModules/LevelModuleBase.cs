using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class LevelModuleBase
    {
        string name;
        public string Name
        {
            get { return name; }
        }
        public GameLevel level;

        public LevelModuleBase(string _name)
        {
            name = _name;
        }

		// excute it when every game level loaded
        public virtual void OnLevelLoaded(GameLevel _level)
        {
            level = _level;
        }
    }
}


