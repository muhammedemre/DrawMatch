using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using GameAnalyticsSDK;
using TMPro;

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

    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;


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
            .SetMaxAdContentRating(MaxAdContentRating.G)
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
            .SetSameAppKeyEnabled(true)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => {
            LoadInterstitialAd();
            LoadRewardedAd();
        });
    }

    private void ClearInterstitialAd()
    {
        if (interstitialAd == null) return;

        interstitialAd.Destroy();
        interstitialAd = null;
    }

    private void ClearRewardedAd()
    {
        if (rewardedAd == null) return;

        rewardedAd.Destroy();
        rewardedAd = null;
    }

    private void LoadInterstitialAd()
    {
        ClearInterstitialAd();

        string adUnitId = "";

#if UNITY_ANDROID
        adUnitId = Interstitial_Level_Android;
#elif UNITY_IOS
        adUnitId = Interstitial_Level_IOS;
#endif

        if (isTest)
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/5662855259";
#endif
        }

        interstitialAd = new InterstitialAd(adUnitId);
        Debug.Log("[interstitialAd][adUnitId] " + adUnitId);

        interstitialAd.OnAdLoaded += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "admob", "OnAdLoaded");
        };

        interstitialAd.OnAdOpening += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "admob", "OnAdOpening");
        };

        interstitialAd.OnAdClosed += (object sender, System.EventArgs args) => {
            LoadInterstitialAd();
        };

        interstitialAd.OnAdDidRecordImpression += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "admob", "OnAdDidRecordImpression");
        };

        interstitialAd.OnAdFailedToShow += (object sender, AdErrorEventArgs adErrorEventArgs) => {
            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "admob", adErrorEventArgs.ToString());
            LoadInterstitialAd();
        };

        interstitialAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs adFailedToLoadEventArgs) => {
            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "admob", adFailedToLoadEventArgs.ToString());
            ClearInterstitialAd();
        };

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
        GameAnalytics.NewAdEvent(GAAdAction.Request, GAAdType.Interstitial, "admob", "OnRequested");
    }

    private void LoadRewardedAd() {
        ClearRewardedAd();

        string adUnitId = "";

#if UNITY_ANDROID
        adUnitId = Rewarded_Hint_Android;
#elif UNITY_IOS
        adUnitId = Rewarded_Hint_IOS;
#endif

        if (isTest)
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#endif
        }

        rewardedAd = new RewardedAd(adUnitId);
        Debug.Log("[rewardedAd][adUnitId] " + adUnitId);

        rewardedAd.OnAdLoaded += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.RewardedVideo, "admob", "OnAdLoaded");
        };

        rewardedAd.OnAdOpening += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "admob", "OnAdOpening");
        };

        rewardedAd.OnAdDidRecordImpression += (object sender, System.EventArgs args) => {
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "admob", "OnAdDidRecordImpression");
        };

        rewardedAd.OnAdClosed += (object sender, System.EventArgs args) => {
            LoadRewardedAd();
        };

        rewardedAd.OnAdFailedToShow += (object sender, AdErrorEventArgs adErrorEventArgs) => {
            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "admob", adErrorEventArgs.ToString());
            LoadRewardedAd();
        };

        rewardedAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs adFailedToLoadEventArgs) => {
            GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "admob", adFailedToLoadEventArgs.ToString());
            ClearRewardedAd();
        };

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        GameAnalytics.NewAdEvent(GAAdAction.Request, GAAdType.RewardedVideo, "admob", "OnRequested");
    }

    public void ShowAd(InterstitialAdType adType)
    {
        if (interstitialAd == null)
        {
            LoadInterstitialAd();
            return;
        }

        if (!interstitialAd.IsLoaded()) return;

        interstitialAd.Show();
        
    }

    public void ShowRewardedAd(RewardedInterstitialAdType adType, UnityAction onUserEarnedReward)
    {
        if (rewardedAd == null)
        {
            onUserEarnedReward();
            LoadRewardedAd();
            return;
        }

        if (!rewardedAd.IsLoaded()) {
            onUserEarnedReward();
            return;
        }

        rewardedAd.OnUserEarnedReward += (object sender, Reward reward) => {
            onUserEarnedReward();
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "admob", adType.ToString());
        };

        rewardedAd.Show();
    }
}
