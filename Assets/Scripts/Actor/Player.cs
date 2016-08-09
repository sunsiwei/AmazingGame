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
                        LevelScoreModule psm = LevelModuleManager.Instance.GetModule(LevelScoreModule.name) as LevelScoreModule;
                        psm.Score += es.Score;
                    }
                }
                else
                {
                    LevelPlayerModule pm = LevelModuleManager.Instance.GetModule(LevelPlayerModule.name) as LevelPlayerModule;
                    pm.OnPlayerDead();
                    
                    Destroy(gameObject);
                }
            }else
            if (co.CompareTag("Food"))
            {
                ActorScore aScore = co.GetComponent<ActorScore>();
                if (aScore != null)
                {
                    LevelScoreModule psm = LevelModuleManager.Instance.GetModule(LevelScoreModule.name) as LevelScoreModule;
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
                    LevelFoodModule fm = LevelModuleManager.Instance.GetModule(LevelFoodModule.name) as LevelFoodModule;
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
            LevelEnemyModule em = LevelModuleManager.Instance.GetModule(LevelEnemyModule.name) as LevelEnemyModule;
            em.MakeAllPause(true);
            yield return new WaitForSeconds(dura);
            em.MakeAllPause(false);
        }


    }
}


