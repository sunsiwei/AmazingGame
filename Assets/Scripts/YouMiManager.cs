using UnityEngine;
using System.Collections;


namespace PacmanGame
{
	public class YouMiManager
	{
        AndroidJavaClass mJc;
        AndroidJavaObject mJo;


        public void Init()
        {
            // 广告相关
            //mJc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //mJo = mJc.GetStatic<AndroidJavaObject>("currentActivity");
            //mJo.Call("showBanner");
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // 如果开发者使用了插屏广告，那么当按返回键的时候，逻辑应该如下：
                // 1、如果插屏广告在展示时，返回键应该先关闭正在展示的插屏广告，在按一次返回键才执行开发者自己的逻辑（如：退出应用）
                // 2、如果插屏广告没有在展示时，就进行自己的逻辑（如：退出应用等）

                // 当插屏广告已经消失了，就执行后续逻辑（这里为退出应用）
                // Android示例项目中定义0为返回键
                if (mJo.Call<bool>("closeSpot", 0) == true)
                {
                    Application.Quit();
                }
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                // 按Home键时，调用尝试关闭插屏广告的代码，开发者可以实现后续逻辑
                // Android示例项目中定义1为Home键
                if (mJo.Call<bool>("closeSpot", 1) == true)
                {

                }
            }
        }

        private static YouMiManager _Instance = null;
        private YouMiManager() { }
        public static YouMiManager Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new YouMiManager();
                return _Instance;
            }
        }
	}
}
