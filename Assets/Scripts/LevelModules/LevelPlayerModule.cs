using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    public class LevelPlayerModule : LevelModuleBase
    {
        public static string name = "LevelPlayerCtl";
        public LevelPlayerModule(string _name)
            : base(_name)
        {

        }

        public delegate void PlayerLivesUpdateHandler(int count);
        public event PlayerLivesUpdateHandler EnentPlayerLivesUpdate;

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

        public override void OnLevelLoaded(GameLevel level)
		{
            base.OnLevelLoaded(level);
            int levelIndex = level.Index;

            levelPlayerCfg = level.JsonPlayer;

            leftPlayerLives = (int)levelPlayerCfg["playerCounts"];

            int playerCtlType = 1;
            if (playerCtlType == 0)
            {
                ResourcesLoader.LoadOther("SwipeControl");
            }
            else if (playerCtlType == 1)
            {
                ResourcesLoader.LoadOther("JoystickControl");
            }

            EnentPlayerLivesUpdate(leftPlayerLives);
            AddPlayer();
		}

        public void OnPlayerDead()
        {
            LevelEnemyModule em = LevelModuleManager.Instance.GetModule(LevelEnemyModule.name) as LevelEnemyModule;
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
            yield return new WaitForSeconds((int)levelPlayerCfg["playerReliveDelayTime"]);

            AddPlayer();
            LevelEnemyModule em = LevelModuleManager.Instance.GetModule(LevelEnemyModule.name) as LevelEnemyModule;
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

        public void MakePause(bool b)
        {
            PlayerMove pm = player.GetComponent<PlayerMove>();
            pm.Pause = b;
        }
    }
}


