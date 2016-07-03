using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public interface IModule
    {

        void Init();
        void OnLevelLoaded();
    }
}


