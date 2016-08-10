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
            level.MakePause(true);
            if (LeftPlayerLives <= 0)
            {
                Debug.Log("Game over!!!");

                PageManager.Instance.ShowPage("UIGameOver");
                return;
            }

            AmazingGame.Instance.StartCoroutine(DelayRelive());
        }

        public void Relive()
        {
            DiamondSystem ds = SystemManager.Instance.GetSystem(DiamondSystem.name) as DiamondSystem;
            if (ds.DiamondAmount > 0)
            {
                ds.ReduceDiamond(1);
                AmazingGame.Instance.StartCoroutine(DelayRelive());
            }
            else
            {
                PromptSystem ps = SystemManager.Instance.GetSystem(PromptSystem.name) as PromptSystem;
                ps.Prompt("diamond is not enough.");
            }
        }

        IEnumerator DelayRelive()
        {
            level.MakePause(false);
            LevelEnemyModule em = LevelModuleManager.Instance.GetModule(LevelEnemyModule.name) as LevelEnemyModule;
            em.MakeAllHome();

            yield return new WaitForSeconds((int)levelPlayerCfg["playerReliveDelayTime"]);

            AddPlayer();
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


