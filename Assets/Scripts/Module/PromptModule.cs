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
            UIPrompt up = UIManager.GetInstance().ShowUI("UIPrompt").GetComponent<UIPrompt>();
            up.PromptText(txt);
        }
    }
}


