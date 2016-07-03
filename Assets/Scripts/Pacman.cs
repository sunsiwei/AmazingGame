using UnityEngine;
using System.Collections;
using LitJson;

namespace PacmanGame
{
    //public class Pacman : MonoBehaviour
    //{

    //    private int _score = 0;
    //    public int Score
    //    {
    //        get { return _score; }
    //        set { 
    //            _score = value;
    //        }
    //    }

    //    public delegate void DelegateReliveCompleted();
    //    public DelegateReliveCompleted ReliveCompleted;

    //    void Start()
    //    {
    //        NotificationCenter.DefaultCenter().AddObserver(this, "EventTriggerSuperPacdot");
    //    }

    //    public void AddScore(int value)
    //    {
    //        _score = _score + value;
    //        Game.Instance.AddScore(value);
    //    }
        
    //    public void Dead()
    //    {
    //        Game.Instance.ReduceLives();
    //        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //        gameObject.GetComponent<PacmanMove>().ExpectDir = Vector2.zero;
    //        gameObject.GetComponent<PacmanMove>().enabled = false;
    //    }

    //    public void Relive(DelegateReliveCompleted d)
    //    {
    //        StartCoroutine(DelayRelive(d));
    //    }

    //    IEnumerator DelayRelive(DelegateReliveCompleted reliveCallback)
    //    {
    //        JsonData baseCfg = ConfigManager.GetGameCfgItem("baseCfg");
    //        yield return new WaitForSeconds((int)baseCfg["pacmanReliveDelayTime"]);
    //        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    //        gameObject.GetComponent<PacmanMove>().enabled = true;
    //        gameObject.GetComponent<PacmanMove>().ExpectDir = Vector2.right;
    //        JsonData levelCfg = Game.Instance.GetLevel().GetLevelCfg();
    //        PacmanMove pm = GetComponent<PacmanMove>();
    //        pm.SetPosition(new Vector2((int)levelCfg["pacmanPos"][0], (int)levelCfg["pacmanPos"][1]));
    //        pm.ExpectDir = new Vector2((int)levelCfg["pacmanDir"][0], (int)levelCfg["pacmanDir"][1]);
    //        pm.Speed = (double)levelCfg["pacmanSpeed"];
    //        reliveCallback();
    //    }

    //    void EventTriggerSuperPacdot(Notification notifi)
    //    {
    //        SuperPacdot sd = notifi.sender as SuperPacdot;
    //        AddScore(sd.score);
    //        StartCoroutine(ChangeSpeed());
    //    }

    //    IEnumerator ChangeSpeed()
    //    {
    //        PacmanMove pm = GetComponent<PacmanMove>();
    //        double originalSpeed = pm.Speed;
    //        pm.Speed = Game.Instance.GetLevel().GetPacmanExsitedSpeed();

    //        int dura = Game.Instance.GetLevel().GetExsitedDuration();
    //        yield return new WaitForSeconds(dura);

    //        pm.Speed = originalSpeed;
    //    }

    //}
}


