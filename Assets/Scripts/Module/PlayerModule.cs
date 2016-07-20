using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class PlayerModule : ModuleBase
    {
        public static string name = "PlayerModule";
        public PlayerModule(string _name)
            : base(_name)
        {

        }

        public delegate void PlayerLivesUpdateHandler(int count);
        public event PlayerLivesUpdateHandler EnentPlayerLivesUpdate;

		JsonData playerCfg;
        JsonData levelPlayerCfg;
		GameObject player;

        int leftPlayerLives;
        public int LeftPlayerLives
        {
            get { return leftPlayerLives; }
            set {
                leftPlayerLives = value;
                EnentPlayerLivesUpdate(leftPlayerLives);
            }
        }

        public delegate void PlayerExpectDirectionUpdateHandle(Vector2 dir);
        public event PlayerExpectDirectionUpdateHandle EventPlayerExpectDirectionUpdate;
        Vector2 playerExpectDirection;
        public Vector2 PlayerExpectDirection
        {
            set {
                playerExpectDirection = value;
                EventPlayerExpectDirectionUpdate(value);
            }
        }

        public override void OnLevelLoaded(int levelIndex)
		{
			playerCfg = ConfigManager.Instance.GetCfg("playerCfg");
			leftPlayerLives = (int)playerCfg["playerCounts"];

            int playerCtlType = (int)playerCfg["playerControlType"];
            if (playerCtlType == 0)
            {
                ResourcesLoader.LoadOther("SwipeControl");
            }
            else if (playerCtlType == 1)
            {
                ResourcesLoader.LoadOther("JoystickControl");
            }
            
            JsonData levelCfg = ConfigManager.Instance.GetCfg("gameLevelCfg");
            levelPlayerCfg = levelCfg["levels"][levelIndex]["player"];

            EnentPlayerLivesUpdate(leftPlayerLives);
            AddPlayer();
		}

        public void OnPlayerDead()
        {
            EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
            if (LeftPlayerLives <= 0)
            {
                AmazingGame.Instance.StopAllCoroutines();
                Debug.Log("Game over!!!");

                em.MakeAllPause(true);

                PageManager.Instance.ShowPage("UIGameOver");
                return;
            }

            em.MakeAllHome();

            AmazingGame.Instance.StartCoroutine(DelayRelive());
        }
        IEnumerator DelayRelive()
        {
            yield return new WaitForSeconds((int)levelPlayerCfg["reliveDuration"]);

            AddPlayer();
            EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
            em.MakeAllContivueSearch();
        }

        public GameObject GetPlayer()
        {
            return player;
        }
        //public int GetExsitedDuration()
        //{
        //    return (int)levelPlayerCfg["exsitedDuration"];
        //}

        void AddPlayer()
        {
            LeftPlayerLives--;

            player = ResourcesLoader.LoadActor((string)levelPlayerCfg["name"]);
            player.name = (string)levelPlayerCfg["name"];
            PlayerMove pm = player.GetComponent<PlayerMove>();
            pm.Speed = (double)levelPlayerCfg["speed"];
            pm.ImmediateMoveTo(new Vector2((int)levelPlayerCfg["position"][0], (int)levelPlayerCfg["position"][1]));
            pm.ExpectDirection = new Vector2((int)levelPlayerCfg["direction"][0], (int)levelPlayerCfg["direction"][1]);
        }
    }
}


