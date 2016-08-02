using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PacmanGame
{
    public class MainPage : PageBase
    {
        int score = 0;
        int lives = 0;
        int levelIndex = 0;
        Vector2 arrowDirection;

        public MainPage(UIHierarchy _hierarchy, string _path)
            :base(_hierarchy, _path)
        {
            PlayerScoreModule sm = ModuleManager.Instance.GetModule(PlayerScoreModule.name) as PlayerScoreModule;
            sm.EventScoreUpdate += EventScoreUpdate;

            PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
            pm.EnentPlayerLivesUpdate += EventLivesUpdate;
            pm.EventPlayerExpectDirectionUpdate += EventPlayerExpectDirectionUpdate;

            GameLevelManager.Instance.EventLevelLoaded += EventLevelLoaded;
        }

        Text txtScore;
        Text txtLives;
        Text txtLevelIndex;
        Transform imgArrowLeft;
        Transform imgArrowRight;
        protected override void Awake(GameObject go)
        {
            Button btnMenu = transform.Find("BtnMenu").GetComponent<Button>();
            btnMenu.onClick.AddListener(OnBtnMenuClick);
            txtScore = transform.Find("TxtScore").GetComponent<Text>();
            txtLives = transform.Find("TxtLives").GetComponent<Text>();
            txtLevelIndex = transform.Find("TxtLevelIndex").GetComponent<Text>();
            imgArrowLeft = transform.Find("ImgArrowLeft");
            imgArrowRight = transform.Find("ImgArrowRight");
        }

        protected override void Refresh()
        {
            if (gameObject == null)
                return;
            txtScore.text = "score: " + score;
            txtLives.text = "lives: " + lives;
            txtLevelIndex.text = "level:" + (levelIndex + 1);

            if (arrowDirection == Vector2.up)
            {
                imgArrowLeft.eulerAngles = new Vector3(0, 0, 180);
                imgArrowRight.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (arrowDirection == Vector2.right)
            {
                imgArrowLeft.eulerAngles = new Vector3(0, 0, 90);
                imgArrowRight.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (arrowDirection == Vector2.down)
            {
                imgArrowLeft.eulerAngles = new Vector3(0, 0, 0);
                imgArrowRight.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (arrowDirection == Vector2.left)
            {
                imgArrowLeft.eulerAngles = new Vector3(0, 0, -90);
                imgArrowRight.eulerAngles = new Vector3(0, 0, -90);
            }
        }

        void OnBtnMenuClick()
        {
            

            PageManager.Instance.ShowPage("UIMenu");
            //EnemyAISearch a = UnityEngine.GameObject.Find("Orange2").GetComponent<EnemyAISearch>();
            //if (a.PauseSearch)
            //    a.PauseSearch = false;
            //else
            //    a.PauseSearch = true;
        }

        void EventScoreUpdate(int _score)
        {
            score = _score;
            Refresh();
        }
        void EventLivesUpdate(int _lives)
        {
            lives = _lives;
            Refresh();
        }
        
        void EventLevelLoaded(int _levelIndex)
        {
            levelIndex = _levelIndex;
            Refresh();
        }

        void EventPlayerExpectDirectionUpdate(Vector2 expectDir)
        {
            arrowDirection = expectDir;
            Refresh();
        }
    }
}


