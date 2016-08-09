using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PromptSystem : SystemBase
    {
        public static string name = "PromptSystem";

        public PromptSystem(string _name)
            :base(_name)
        {

        }

        public void Prompt(string txt)
        {
            PromptPage pp = PageManager.Instance.GetPage("UIPrompt") as PromptPage;
            Debug.Log(txt);
            pp.PromptText(txt);
        }
    }
}


