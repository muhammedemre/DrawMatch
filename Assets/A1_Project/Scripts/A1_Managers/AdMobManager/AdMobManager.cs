using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance;

    public enum RewardedInterstitialAdType
    {
        Hint,
    }

    public enum InterstitialAdType
    {
        Level
    }

    private bool isTest = false;

    #region Android UNIT IDs
    private const string Rewarded_Hint_Android = "ca-app-pub-4426570100779360/5038356060";
    private const string Interstitial_Level_Android = "ca-app-pub-4426570100779360/7707067227";
    #endregion
    #region IOS UNIT IDs
    private const string Rewarded_Hint_IOS = "ca-app-pub-4426570100779360/3798508154";
    private const string Interstitial_Level_IOS = "ca-app-pub-4426570100779360/7546181473";
    #endregion


    private void Awake()
    {
        SingletonCheck();
        Init();
    }

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    void Init()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);

        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
        });
    }

    public void ShowAd(InterstitialAdType adType)
    {
        string adUnitId = "";

        if (adType == InterstitialAdType.Level)
        {
#if UNITY_ANDROID
            adUnitId = Interstitial_Level_Android;
#elif UNITY_IOS
            adUnitId = Interstitial_Level_IOS;
#endif
        }

        if (isTest)
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/5662855259";
#endif
        }

        Debug.Log("[admobmanager][showad] " + adUnitId);


        if (adUnitId == "")
        {
            Debug.LogError("[admobmanager][showad] adUnitId does not exist");
            return;
        }

        InterstitialAd ad = new InterstitialAd(adUnitId);
        ad.OnAdLoaded += (object sender, System.EventArgs args) => {
            ad.Show();
        };
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }

    public void ShowRewardedAd(RewardedInterstitialAdType adType, UnityAction onUserEarnedReward)
    {
        string adUnitId = "";

        if (adType == RewardedInterstitialAdType.Hint)
        {
#if UNITY_ANDROID
            adUnitId = Rewarded_Hint_Android;
#elif UNITY_IOS
            adUnitId = Rewarded_Hint_IOS;
#endif
        }

        if (isTest)
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif
        }

        Debug.Log("[admobmanager][showrewardedad] " + adUnitId);

        if (adUnitId == "")
        {
            Debug.LogError("[admobmanager][showrewardedad] adUnitId does not exist");
            return;
        }


        RewardedAd ad = new RewardedAd(adUnitId);
        ad.OnUserEarnedReward += (object sender, Reward args) =>
        {
            onUserEarnedReward();
        };
        ad.OnAdLoaded += (object sender, System.EventArgs args) => {
            ad.Show();
        };
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
    }
}
