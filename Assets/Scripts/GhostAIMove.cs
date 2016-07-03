using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace PacmanGame
{
    //enum ModeType
    //{
    //    Scatter,//check sequence: up right down left.
    //    Chase,
    //    Afraid,
    //    Home
    //}
    //struct Mode
    //{

    //    public ModeType type;
    //    public float duration;
    //    public Mode(ModeType _type, float _duration)
    //    {
    //        type = _type;
    //        duration = _duration;
    //    }
    //}
    //public enum GhostType
    //{
    //    Blue = 0,//pacman的当前位置为追击目标
    //    Pink = 1,//pacman的当前位置的前进方向的4格位置为追击目标
    //    Orange = 2//Blue到pacman的当前位置的前进方向的2格位置的方向再延伸一倍的位置
    //}
    

//    public class GhostAIMove : MonoBehaviour
//    {
//        public class PositionSortInfo : IComparable
//        {
//            public Vector2 pos;
//            public float distance;
//            public int CompareTo(System.Object obj)
//            {
//                PositionSortInfo other = obj as PositionSortInfo;
//                return this.distance.CompareTo(other.distance);
//            }
//        }

//        public GhostType type = GhostType.Blue;
//        Transform scatterTarget;
//        public Transform ScatterTarget
//        {
//            set { scatterTarget = value; }
//        }
//        Vector2 ChasePosition;

//        JsonData ghostCfg;
//        double speed = 0.03f;
//        double afraidSpeed = 0.1f;
//        int afraidDuration = 0;

//        ModeType curModeType;
//        Mode[] modeList = { new Mode(ModeType.Scatter, 100.0f), new Mode(ModeType.Chase, 5.0f), new Mode(ModeType.Scatter, 7.0f), new Mode(ModeType.Chase, 5.0f) };

//        Vector2 lastPos;
//        Vector2 nextPos;
//        Vector2[] directionList = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

//        // Use this for initializations
//        void Start()
//        {
//            nextPos = transform.position;
//            StartCoroutine(StartMode());
//        }

//        public void Init(JsonData _ghostCfg)
//        {
//            ghostCfg = _ghostCfg;
//            name = (string)ghostCfg["name"];
//            type = (GhostType)(int)ghostCfg["type"];
//            speed = (double)ghostCfg["speed"];
//            afraidSpeed = (double)ghostCfg["afraidSpeed"];
//            ChasePosition = new Vector2((int)ghostCfg["chasePosition"][0], (int)ghostCfg["chasePosition"][1]);
//            transform.position = new Vector2((int)ghostCfg["position"][0], (int)ghostCfg["position"][1]);
//        }

//        public void MakeAfraid(int _afraidDuration)
//        {
//            curModeType = ModeType.Afraid;
//            afraidDuration = _afraidDuration;
//        }

//        public void MakeHome()
//        {
//            curModeType = ModeType.Home;
//            transform.position = new Vector2((int)ghostCfg["position"][0], (int)ghostCfg["position"][1]);
//            nextPos = transform.position;
//            lastPos = transform.position;
//        }

//        public int GetAfraidDuration()
//        {
//            return afraidDuration;
//        }
        
//// ---------------------------- private function --------------------------
//        void FixedUpdate()
//        {
//            if ((Vector2)transform.position != nextPos)
//            {
//                Vector3 p = Vector3.MoveTowards(transform.position, nextPos, (float)speed);
//                GetComponent<Rigidbody2D>().MovePosition(p);
//            }
//            else
//            {
//                nextPos = GetNextPos();
//                lastPos = (Vector2)transform.position;
//            }
//            Debug.DrawLine(transform.position, destination, Color.yellow);
//        }
//        Vector2 destination;
//        Vector2 GetNextPos()
//        {
//            if (curModeType == ModeType.Scatter || curModeType == ModeType.Chase)
//            {
//                destination = GetDestinationPosition(curModeType);
//                Vector2[] sortList = GetNearSortListToTarget(destination);
                
//                for (int index = 0; index < sortList.Length; index++)
//                {
//                    Vector2 pos = sortList[index];
//                    if (CheckMove(pos) && pos != lastPos)
//                        return pos;
//                }
//            }
//            else if (curModeType == ModeType.Afraid)
//            {
//                Vector2 pos = transform.position;
//                Vector2[] aroundPosList = new Vector2[4];
//                for (int i = 0; i < directionList.Length; i++)
//                {
//                    aroundPosList[i] = pos + directionList[i];
//                }
//                return GetAfraidRandomPos(aroundPosList);
//            }
//            else if (curModeType == ModeType.Home)
//            {
//                Vector2[] posList = { (Vector2)transform.position + Vector2.left, (Vector2)transform.position + Vector2.right };
//                for (int index = 0; index < posList.Length; index++)
//                {
//                    Vector2 pos = posList[index];
//                    if (CheckMove(pos) && pos != lastPos)
//                        return pos;
//                }
//            }
//            return transform.position;
//        }
//        Vector2 GetDestinationPosition(ModeType modeType)
//        {
//            if (modeType == ModeType.Scatter)
//            {
//                if (type == GhostType.Blue)
//                {
//                    return scatterTarget.position;
//                }else if(type == GhostType.Pink)
//                {
//                    PacmanMove pm = scatterTarget.GetComponent<PacmanMove>();
//                    Vector2 dir = pm.GetDirection();
//                    Vector2 targetPos = (Vector2)scatterTarget.position + dir * 4;
//                    return targetPos;
//                }else if(type == GhostType.Orange)
//                {
//                    PacmanMove pm = scatterTarget.GetComponent<PacmanMove>();
//                    Vector2 dir = pm.GetDirection();
//                    Vector2 tempPos = (Vector2)scatterTarget.position + dir * 2;
//                    Vector2 bPos = Game.Instance.GetLevel().GetGhost("Blue").transform.position;
//                    return (tempPos - bPos) * 2 + bPos;
//                }
//            }
//            else if (modeType == ModeType.Chase)
//            {
//                return ChasePosition;
//            }
//            return transform.position;
//        }

//        Vector2[] GetNearSortListToTarget(Vector2 target)
//        {
//            PositionSortInfo[] list = new PositionSortInfo[4];
//            Vector2 pos = transform.position;
//            for (int i = 0; i < directionList.Length; i++ )
//            {
//                PositionSortInfo info = new PositionSortInfo();
//                info.pos = pos + directionList[i];
//                info.distance = Vector2.Distance(info.pos, target);
//                list[i] = info;
//            }

//            Array.Sort(list);

//            Vector2[] sortList = new Vector2[4];
//            for (int i = 0; i < list.Length; i++)
//            {
//                sortList[i] = list[i].pos;
//            }
//            return sortList;
//        }

        
//        Vector2 GetAfraidRandomPos(Vector2[] list)
//        {
//            int randomValue = UnityEngine.Random.Range(0, list.Length);
//            if (CheckMove(list[randomValue]) == false)
//            {
//                List<Vector2> tempList = new List<Vector2>();
//                for (int i = 0; i < list.Length; i++ )
//                {
//                    if (i != randomValue)
//                    {
//                        tempList.Add(list[i]);
//                    }
//                }
//                if (tempList.Count <= 0)
//                {
//                    return transform.position;
//                }
//                return GetAfraidRandomPos(tempList.ToArray());
//            }
//            return list[randomValue];
//        }
        

//        IEnumerator StartMode()
//        {
//            for (int i = 0; i < modeList.Length; i++ )
//            {
//                Debug.LogFormat("enter mode: {0}", modeList[i].type);
//                yield return StartCoroutine(EnterMode(modeList[i]));
//            }
//        }
//        IEnumerator EnterMode(Mode mode)
//        {
//            curModeType = mode.type;
//            ModeType recordModeType = mode.type;
//            JsonData baseCfg = ConfigManager.GetGameCfgItem("baseCfg");

//            for (float t = 0; t <= mode.duration; t += Time.deltaTime)
//            {
//                if (curModeType == ModeType.Afraid)
//                {
//                    yield return new WaitForSeconds((float)afraidDuration);
//                    curModeType = recordModeType;
//                }else
//                if(curModeType == ModeType.Home)
//                {
//                    yield return new WaitForSeconds((int)baseCfg["pacmanReliveDelayTime"]);
//                    curModeType = recordModeType;
//                }
//                yield return 0;
//            }
//        }

//        bool CheckMove(Vector2 targetPos)
//        {
//            Vector2 pos = transform.position;
//            int layerMask = 1 << 9 | 1 << 11;
//            Vector2 dir = (targetPos - pos).normalized;
//            RaycastHit2D circleHit = Physics2D.CircleCast(targetPos + dir * 0.2f, 0.2f, -dir, 1.2f, layerMask);
//            return circleHit.collider == GetComponent<Collider2D>();
//        }

//        void OnTriggerEnter2D(Collider2D co)
//        {
//            if (co.CompareTag("Player"))
//            {
//                if (curModeType == ModeType.Afraid)
//                {
//                    Ghost ghost = GetComponent<Ghost>();
//                    ghost.Dead();
//                    MakeHome();
//                    Pacman man = co.GetComponent<Pacman>();
//                    man.AddScore(ghost.score);
//                }
//                else
//                {
//                    Pacman man = co.GetComponent<Pacman>();
//                    man.Dead();
//                }
//            }
//        }

//    }
    
}


