using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame
{
    public class WaresData
    {
        public string name = "";
        public int id = 0;
        public int diamondNum = 0;
    }
    public class PaySystem : SystemBase
    {
        Dictionary<int, WaresData> waresList;

        public static string name = "PaySystem";

        public PaySystem(string _name)
            :base(_name)
        {
        }

        AndroidJavaClass mJc;
        AndroidJavaObject mJo;

        public delegate void PayReplyHandler(int code);
        public event PayReplyHandler EventPayReply;

        public override void Create()
        {
            base.Create();
            InitWarsesList();

        }

        public Dictionary<int, WaresData> GetWaresList()
        {
            return waresList;
        }

        public void Pay()
        {
            string appuserid = "1064874807@qq.com";
            string cpprivateinfo = "cpprivateinfo123456";
            int waresid = 1;
            int price = 1;

            mJc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            mJo = mJc.GetStatic<AndroidJavaObject>("currentActivity");
            mJo.Call("Pay",
                appuserid,
                cpprivateinfo,
                waresid,
                price
                );
        }
        // 0 支付失败， 其他数字是商品id
        public void PayReply(string str)
        {
            int code = int.Parse(str);
            if (code == 0)
            {
                // pay fail
            }
            else
            {
                DiamondSystem ds = SystemManager.Instance.GetSystem(DiamondSystem.name) as DiamondSystem;
                ds.AddDiamond(waresList[code].diamondNum);
            }

            if (EventPayReply != null)
                EventPayReply(code);
        }

        void InitWarsesList()
        {
            waresList = new Dictionary<int, WaresData>();
            WaresData a = new WaresData();
            a.name = "一小包宝石";
            a.id = 1;
            a.diamondNum = 5;
            waresList.Add(1, a);
            WaresData b = new WaresData();
            b.name = "一大包宝石";
            b.id = 2;
            b.diamondNum = 10;
            waresList.Add(2, b);
        }

    }
}


