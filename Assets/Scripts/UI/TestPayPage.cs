using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class TestPayPage : PageBase
    {
        public TestPayPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {

        }

        Text txt;
        protected override void Awake(GameObject go)
        {
            PaySystem ps = SystemManager.Instance.GetSystem(PaySystem.name) as PaySystem;
            ps.EventPayReply += EventPayReply;

            Button btnRestart = transform.Find("BtnPay").GetComponent<Button>();
            btnRestart.onClick.AddListener(OnBtnPayClick);

            txt = transform.Find("Text").GetComponent<Text>();
        }

        protected override void Active()
        {
            base.Active();

        }

        void OnBtnPayClick()
        {
            PaySystem ps = SystemManager.Instance.GetSystem(PaySystem.name) as PaySystem;
            ps.Pay();
        }
        void EventPayReply(int code)
        {
            PromptSystem ps = SystemManager.Instance.GetSystem(PromptSystem.name) as PromptSystem;
            ps.Prompt("reply: " + code);

            txt.text = "reply: " + code;
        }
    }
}


