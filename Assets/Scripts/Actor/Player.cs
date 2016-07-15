using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class Player : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D co)
        {
            if (co.CompareTag("Enemy"))
            {
                EnemyAISearch eas = co.GetComponent<EnemyAISearch>();
                if (eas.BehaviorType == BehaviorType.Afraid)
                {
                    ActorScore es = co.GetComponent<ActorScore>();
                    if (es != null)
                    {
                        PlayerScoreModule psm = ModuleManager.Instance.GetModule(PlayerScoreModule.name) as PlayerScoreModule;
                        psm.Score += es.Score;
                    }
                }
                else
                {
                    PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
                    pm.OnPlayerDead();
                    
                    Destroy(gameObject);
                }
            }else
            if (co.CompareTag("Food"))
            {
                ActorScore aScore = co.GetComponent<ActorScore>();
                if (aScore != null)
                {
                    PlayerScoreModule psm = ModuleManager.Instance.GetModule(PlayerScoreModule.name) as PlayerScoreModule;
                    psm.Score += aScore.Score;
                }

                if (co.GetComponent<AccelerateFood>() != null)
                {
                    AccelerateFood af = co.GetComponent<AccelerateFood>();
                    StartCoroutine(Accelerate(af.accelerateRate, af.accelerateDuration));
                }
                else if (co.GetComponent<ExcitedFood>() != null)
                {
                    ExcitedFood ef = co.GetComponent<ExcitedFood>();
                    StartCoroutine(Accelerate((float)ef.accelerateRate, (float)ef.excitedDuration));
                    NotificationCenter.DefaultCenter().PostNotification(this, "EventPlayerExcited", (float)ef.excitedDuration);
                }
                else if (co.GetComponent<EnemyPauseFood>() != null)
                {
                    EnemyPauseFood epf = co.GetComponent<EnemyPauseFood>();
                    StartCoroutine(MakeAllEnemyPause(epf.pauseDuration));
                    Debug.Log("enemy all pause.");
                }
                else
                {
                    FoodModule fm = ModuleManager.Instance.GetModule(FoodModule.name) as FoodModule;
                    fm.AlreadyEatFoodCount++;
                }
            }
        }

        IEnumerator Accelerate(float accelerateRate, float accelerateDuration)
        {
            PlayerMove pm = GetComponent<PlayerMove>();
            double originalSpeed = pm.Speed;
            pm.Speed += pm.Speed * accelerateRate;
            yield return new WaitForSeconds(accelerateDuration);

            pm.Speed = originalSpeed;
        }
        IEnumerator MakeAllEnemyPause(int dura)
        {
            EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
            em.MakeAllPause(true);
            yield return new WaitForSeconds(dura);
            em.MakeAllPause(false);
        }


    }
}


