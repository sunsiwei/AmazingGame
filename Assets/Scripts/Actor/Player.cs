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
                Food food = co.GetComponent<Food>();
                if (food.foodType == Food.FoodType.Accelerate)
                {
                    StartCoroutine(Accelerate(food.accelerateRate, food.accelerateDuration));
                }else if(food.foodType == Food.FoodType.EnemyAfriad)
                {
                    PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
                    float excitedDuration = pm.GetExsitedDuration();
                    StartCoroutine(Accelerate(food.accelerateRate, excitedDuration));
                    NotificationCenter.DefaultCenter().PostNotification(this, "EventPlayerHitAfraidFood");
                }else if(food.foodType == Food.FoodType.Normal)
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


    }
}


