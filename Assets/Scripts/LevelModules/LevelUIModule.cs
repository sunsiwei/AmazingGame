using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class LevelUIModule : LevelModuleBase
    {

        public static string name = "LevelUIModule";
        public LevelUIModule(string _name)
            : base(_name)
        {

        }

        public override void OnLevelLoaded(GameLevel level)
        {
            base.OnLevelLoaded(level);

            ResourcesLoader.LoadMap((string)level.JsonLevel["mapName"]);
            PageManager.Instance.ShowPage("UIMain");

            PromptSystem ps = SystemManager.Instance.GetSystem(PromptSystem.name) as PromptSystem;
            ps.Prompt(AmazingGame.Instance.GetText(1000));
        }
    }
}


