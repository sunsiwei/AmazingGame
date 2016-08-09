using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class TestIO : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            IOManager.Instance.Init();
            Debug.Log(IOManager.Instance.recordData.levelIndex);
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "write"))
            {
                IOManager.Instance.recordData.levelIndex = 1220;
                IOManager.Instance.FlushToFile();
            }
            if (GUI.Button(new Rect(200, 10, 150, 100), "read"))
            {
                Debug.Log(IOManager.Instance.recordData.levelIndex);
            }
        }
    }
}


