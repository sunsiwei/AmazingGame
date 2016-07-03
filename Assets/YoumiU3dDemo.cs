using UnityEngine;
using System.Collections;

public class YoumiU3dDemo : MonoBehaviour
{

    AndroidJavaClass mJc;
    AndroidJavaObject mJo;
    private int mPoints;

    // 更新积分，这个方法在Android项目中调用
    void UpdatePoints(string points)
    {
        this.mPoints = int.Parse(points);
    }

    void OnGUI()
    {
        GUILayout.Label("Youmi Unity3d Demo");
        GUILayout.Label("Current Points: " + mPoints);

        // 调用Android工程提供的api——展示插屏广告
        if (GUILayout.Button("Show Spot", GUILayout.Height(100)))
        {
            mJo.Call("showSpot");
        }

        // 调用Android工程提供的api——展示视频广告
        if (GUILayout.Button("Show Video", GUILayout.Height(150)))
        {
            mJo.Call("showVideo");
        }

        // 调用Android工程提供的api——展示全屏积分墙
        if (GUILayout.Button("Show Offers", GUILayout.Height(100)))
        {
            mJo.Call("showOffers");
        }

        // 调用Android工程提供的api——展示对话框积分墙
        if (GUILayout.Button("Show Offers Dialog", GUILayout.Height(100)))
        {
            mJo.Call("showOffersDialog");
        }

        // 调用Android工程提供的api——展查询积分
        if (GUILayout.Button("Query Points", GUILayout.Height(100)))
        {
            this.mPoints = mJo.Call<int>("queryPoints");
        }

        // 调用Android工程提供的api——奖励10积分
        if (GUILayout.Button("Award 10 Points", GUILayout.Height(100)))
        {
            if (mJo.Call<bool>("awardPoints", 10))
            {
                this.mPoints = mJo.Call<int>("queryPoints");
            }
        }

        // 调用Android工程提供的api——消耗5积分
        if (GUILayout.Button("Spend 5 Points", GUILayout.Height(100)))
        {
            if (mJo.Call<bool>("spendPoints", 5))
            {
                this.mPoints = mJo.Call<int>("queryPoints");
            }
        }

        if (GUILayout.Button("Exit", GUILayout.Height(100)))
        {
            Application.Quit();
        }
    }

    void Start()
    {
        mJc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        mJo = mJc.GetStatic<AndroidJavaObject>("currentActivity");
        mJo.Call("showBanner");
    }

    void Update()
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
}