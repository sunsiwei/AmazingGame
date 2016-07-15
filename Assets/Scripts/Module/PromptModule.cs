using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PromptModule : ModuleBase
    {
        public static string name = "PromptModule";

        public PromptModule(string _name)
            :base(_name)
        {

        }

        public void Prompt(string txt)
        {
            PromptPage pp = PageManager.Instance.GetPage("UIPrompt") as PromptPage;
            pp.PromptText(txt);
        }
    }
}


