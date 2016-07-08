using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public enum BehaviorType
    {
        Scatter = 0,//分散check sequence: up right down left.
        Chase = 1,
        Afraid = 2,
        Home = 3
    }

    struct Behavior
    {
        public BehaviorType type;
        public float duration;
        public Behavior(BehaviorType _type, float _duration)
        {
            type = _type;
            duration = _duration;
        }
    }

    public class EnemyBehaviorController : MonoBehaviour
    {
        List<Behavior> behaviors;
        Behavior curBehavior;
        bool pause;

        public void AddBehavior(BehaviorType t, float d)
        {
            if (behaviors == null)
                behaviors = new List<Behavior>();
            Behavior b = new Behavior(t, d);
            behaviors.Add(b);
        }

        public delegate void EnterBehaviorHandler(BehaviorType behaviorType);
        public EnterBehaviorHandler onEnterBehavior;
        public void StartBehavior()
        {
            StartCoroutine(CStartBehavior());
        }

        public bool Pause
        {
            set 
            { 
                pause = value;
                if (value == false)
                {
                    onEnterBehavior(curBehavior.type);
                }
            }
        }


        IEnumerator CStartBehavior()
        {
            for (int i = 0; i < behaviors.Count; i++)
            {
                //Debug.LogFormat("enter behavior: {0}", behaviors[i].type);
                yield return StartCoroutine(COnEnterBehavior(behaviors[i]));
            }
        }
        IEnumerator COnEnterBehavior(Behavior b)
        {
            curBehavior = b;
            onEnterBehavior(curBehavior.type);

            for (float t = 0; t <= b.duration; t += Time.deltaTime)
            {
                while (pause)
                {
                    yield return 0;
                }
                yield return 0;
            }
        }
    }
}


