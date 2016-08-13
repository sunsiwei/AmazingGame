using UnityEngine;
using System.Collections;

namespace PacmanGame
{
    public class PaySystem : SystemBase
    {
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
        public void PayReply(string str)
        {
            int code = int.Parse(str);
            if (EventPayReply != null)
                EventPayReply(code);
        }

    }
}


