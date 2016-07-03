using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PacmanGame
{
    public class Loading : MonoBehaviour
    {
        public Slider slider;
        // Use this for initialization
        void Start()
        {
            slider.value = 0;
            StartCoroutine(LoadLevel(AmazingGame.Instance.nextSceneName));
        }

        AsyncOperation loadLevelOperation;
        IEnumerator LoadLevel(string levelName)
        {
            loadLevelOperation = Application.LoadLevelAsync(levelName);

            while (loadLevelOperation.progress < 0.9f)
            {
                slider.value = loadLevelOperation.progress;
                yield return 0;
            }
            yield return loadLevelOperation;
            slider.value = 1;
        }
    }
}


