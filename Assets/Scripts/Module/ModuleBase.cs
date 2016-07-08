using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class ModuleBase
    {
        string name;
        public string Name
        {
            get { return name; }
        }

        public ModuleBase(string _name)
        {
            name = _name;
        }

		//excute it when every restart game
        public virtual void Init()
        { }

		// excute it when every game level loaded
        public virtual void OnLevelLoaded(int index)
        { }
    }
}


