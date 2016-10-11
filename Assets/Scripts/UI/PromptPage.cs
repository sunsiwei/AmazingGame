using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace PacmanGame
{
    public class PromptPage : PageBase
    {
        public PromptPage(UIHierarchy _hierarchy, string _path)
            : base(_hierarchy, _path)
        {

        }

        List<Text> txtList = new List<Text>();

        public void PromptText(string txt)
        {
            AmazingGame.Instance.StartCoroutine(CPromptText(txt));
        }
        IEnumerator CPromptText(string txt)
        {
            foreach (Text t in txtList)
            {
                Vector2 originalPos = t.transform.localPosition;
                originalPos.y += t.rectTransform.rect.height + 2;
                t.transform.localPosition = originalPos;
            }

            GameObject obj = ResourcesLoader.LoadUI("TxtPrompt");
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            Text uiTxt = obj.GetComponent<Text>();
            uiTxt.text = txt;
            txtList.Add(uiTxt);

            yield return new WaitForSeconds(1.0f);

            txtList.Remove(uiTxt);
            GameObject.Destroy(obj);
        }
    }
}

