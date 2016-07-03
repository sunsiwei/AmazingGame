using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class AudioManager : MonoBehaviour
    {

        static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        public void PlayAudio(string name)
        {
            StartCoroutine(CPlayAudio(name));
        }

        IEnumerator CPlayAudio(string name)
        {
            AudioClip clip = ResourcesLoader.LoadAudioClip(name);
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(AudioManager.Instance.transform);
            AudioSource audio = obj.AddComponent<AudioSource>();
            audio.clip = clip;
            audio.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(obj);
        }
    }
}


