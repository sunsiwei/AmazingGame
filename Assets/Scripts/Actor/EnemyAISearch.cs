using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

namespace PacmanGame
{

    public enum SearchType
    {
        Direct = 0,//pacman的当前位置为追击目标
        Front = 1,//pacman的当前位置的前进方向的4格位置为追击目标
        Smart = 2,//Blue到pacman的当前位置的前进方向的2格位置的方向再延伸一倍的位置
        Lazy = 3,
    }

    public class EnemyAISearch : BaseMove
    {
        public class PositionSortInfo : IComparable
        {
            public Vector2 pos;
            public float distance;
            public int CompareTo(System.Object obj)
            {
                PositionSortInfo other = obj as PositionSortInfo;
                return this.distance.CompareTo(other.distance);
            }
        }

        void Awake()
        {
            NotificationCenter.DefaultCenter().AddObserver(this, "EventPlayerHitAfraidFood");
        }

        double currentSpeed;
        double initialSpeed;
        public double InitialSpeed
        {
            set {
                initialSpeed = value;
                currentSpeed = initialSpeed;
            }
        }
        double afraidSpeed;
        public double AfraidSpeed
        {
            set { afraidSpeed = value; }
        }
        int reliveDuration;
        public int ReliveDuration
        {
            set { reliveDuration = value; }
        }
        Vector2 initialPosition;
        public Vector2 InitialPosition
        { 
            set 
            { 
                initialPosition = value;
                transform.position = initialPosition;
                nextPos = initialPosition;
            } 
        }
        Vector2 scatterPosition;
        public Vector2 ScatterPosition
        {
            set { scatterPosition = value; }
        }


        SearchType searchType;
        public SearchType SearchType
        {
            get { return searchType; }
            set { searchType = value; }
        }
        BehaviorType behaviorType;
        public BehaviorType BehaviorType
        {
            get { return behaviorType; }
        }
        Vector2[] directionList = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        Vector2 lastPos;
        bool pauseSearch;
        public bool PauseSearch
        {
            set { pauseSearch = value; }
        }
        

        EnemyBehaviorController behaviorController;


        public void SetBehaviors(JsonData behaviorsCfg)
        {
            behaviorController = GetComponent<EnemyBehaviorController>();
            if (behaviorController == null)
                behaviorController = gameObject.AddComponent<EnemyBehaviorController>();
            for (int index = 0; index < behaviorsCfg.Count; index++)
            {
                JsonData behaviorCfg = behaviorsCfg[index];
                behaviorController.AddBehavior((BehaviorType)(int)behaviorCfg["type"], (int)behaviorCfg["duration"]);
            }
            behaviorController.onEnterBehavior = OnEnterBehavior;
            behaviorController.StartBehavior();
        }
        void OnEnterBehavior(BehaviorType _behaviorType)
        {
            behaviorType = _behaviorType;
        }

        public void MakeAfraid()
        {
            PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
            float _afraidDuration = pm.GetExsitedDuration();
            StartCoroutine(CMakeAfraid(_afraidDuration));
        }
        IEnumerator CMakeAfraid(float _afraidDuration)
        {
            behaviorController.Pause = true;

            currentSpeed = afraidSpeed;
            behaviorType = PacmanGame.BehaviorType.Afraid;

            yield return new WaitForSeconds(_afraidDuration);

            behaviorController.Pause = false;

            currentSpeed = initialSpeed;
        }

        public void MakeHome()
        {
            ImmediateMoveTo(initialPosition);

            behaviorController.Pause = true;

            behaviorType = PacmanGame.BehaviorType.Home;
        }

        public void MakeHomeForDead()
        {
            StartCoroutine(CMakeHomeForDead());
        }
        IEnumerator CMakeHomeForDead()
        {
            StopAllCoroutines();
            behaviorController.Pause = true;

            currentSpeed = initialSpeed;
            nextPos = initialPosition;
            behaviorType = PacmanGame.BehaviorType.Home;

            yield return new WaitForSeconds(reliveDuration);

            behaviorController.Pause = false;
        }

        public void ContinueSearch()
        {
            currentSpeed = initialSpeed;
            behaviorController.Pause = false;
        }


        

        void FixedUpdate()
        {
            if (pauseSearch)
                return;

            if ((Vector2)transform.position != nextPos)
            {
                Vector3 p = Vector3.MoveTowards(transform.position, nextPos, (float)currentSpeed);
                GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else
            {
                nextPos = GetNextPos();
                lastPos = (Vector2)transform.position;
            }
            Debug.DrawLine(transform.position, destination, Color.yellow);
        }

        Vector2 destination;
        Vector2 GetNextPos()
        {

            if (behaviorType == BehaviorType.Scatter || behaviorType == BehaviorType.Chase)
            {
                destination = GetDestinationPosition(behaviorType);
                Vector2[] sortList = GetNearSortListToTarget(destination);

                for (int index = 0; index < sortList.Length; index++)
                {
                    Vector2 pos = sortList[index];
                    if (CheckMove(pos) && pos != lastPos)
                        return pos;
                }
            }
            else if (behaviorType == BehaviorType.Afraid)
            {
                Vector2 pos = transform.position;
                Vector2[] aroundPosList = new Vector2[4];
                for (int i = 0; i < directionList.Length; i++)
                {
                    aroundPosList[i] = pos + directionList[i];
                }
                return GetAfraidRandomPos(aroundPosList);
            }
            else if (behaviorType == BehaviorType.Home)
            {
                Vector2[] posList = { (Vector2)transform.position + Vector2.left, (Vector2)transform.position + Vector2.right };
                for (int index = 0; index < posList.Length; index++)
                {
                    Vector2 pos = posList[index];
                    if (CheckMove(pos) && pos != lastPos)
                        return pos;
                }
            }
            return transform.position;
        }
        Vector2 GetDestinationPosition(BehaviorType bType)
        {
            if (bType == BehaviorType.Chase)
            {
                if (searchType == SearchType.Direct)
                {
                    return ChaseTarget.position;
                }
                else if (searchType == SearchType.Front)
                {
                    Transform obj = ChaseTarget;
                    PlayerMove pm = obj.GetComponent<PlayerMove>();
                    Vector2 dir = pm.CurDirection;
                    Vector2 targetPos = (Vector2)ChaseTarget.position + dir * 4;
                    return targetPos;
                }
                else if (searchType == SearchType.Smart)
                {
                    PlayerMove pm = ChaseTarget.GetComponent<PlayerMove>();
                    Vector2 dir = pm.CurDirection;
                    Vector2 tempPos = (Vector2)ChaseTarget.position + dir * 2;
                    EnemyModule em = ModuleManager.Instance.GetModule(EnemyModule.name) as EnemyModule;
                    GameObject blue = em.GetEnemy(SearchType.Direct);
                    Vector2 bPos = blue.transform.position;
                    return (tempPos - bPos) * 2 + bPos;
                }
            }
            else if (bType == BehaviorType.Scatter)
            {
                return scatterPosition;
            }
            return transform.position;
        }
        Transform ChaseTarget
        {
            get 
            {
                PlayerModule pm = ModuleManager.Instance.GetModule(PlayerModule.name) as PlayerModule;
                GameObject player = pm.GetPlayer();
                if (player == null)
                    return null;
                return player.transform;
            }
        }
        

        Vector2[] GetNearSortListToTarget(Vector2 target)
        {
            PositionSortInfo[] list = new PositionSortInfo[4];
            Vector2 pos = transform.position;
            for (int i = 0; i < directionList.Length; i++)
            {
                PositionSortInfo info = new PositionSortInfo();
                info.pos = pos + directionList[i];
                info.distance = Vector2.Distance(info.pos, target);
                list[i] = info;
            }

            Array.Sort(list);

            Vector2[] sortList = new Vector2[4];
            for (int i = 0; i < list.Length; i++)
            {
                sortList[i] = list[i].pos;
            }
            return sortList;
        }


        Vector2 GetAfraidRandomPos(Vector2[] list)
        {
            int randomValue = UnityEngine.Random.Range(0, list.Length);
            if (CheckMove(list[randomValue]) == false)
            {
                List<Vector2> tempList = new List<Vector2>();
                for (int i = 0; i < list.Length; i++)
                {
                    if (i != randomValue)
                    {
                        tempList.Add(list[i]);
                    }
                }
                if (tempList.Count <= 0)
                {
                    return transform.position;
                }
                return GetAfraidRandomPos(tempList.ToArray());
            }
            return list[randomValue];
        }

        bool CheckMove(Vector2 nextPos)
        {
            Vector2 pos = transform.position;
            int layerMask = 1 << AmazingGame.MapLayer | 1 << AmazingGame.EnemyLayer;
            Vector2 dir = (nextPos - pos).normalized;
            RaycastHit2D circleHit = Physics2D.CircleCast(nextPos + dir * 0.2f, 0.2f, -dir, 1.2f, layerMask);
            return circleHit.collider == GetComponent<Collider2D>();
        }

        void EventPlayerHitAfraidFood(Notification noti)
        {
            MakeAfraid();
        }
    }
}


