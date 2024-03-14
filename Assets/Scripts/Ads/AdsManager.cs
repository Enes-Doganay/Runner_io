using UnityEngine;
using GoogleMobileAds.Api;
public class AdsManager : AbstractSingleton<AdsManager>
{
    private const string appID = "ca-app-pub-9115875903348915~6570790894";
#if UNITY_ANDROID
    private const string rewardedID = "ca-app-pub-3940256099942544/5224354917"; //TestID
#elif UNITY_IOS
    // Test ad unit ID: ca-app-pub-3940256099942544/5662855259
    private const string AD_UNIT_ID = "<YOUR_IOS_APPOPEN_AD_UNIT_ID>";
#else
    private const string rewardedID = "ca-app-pub-3940256099942544/1712485313";
    private const string AD_UNIT_ID = "unexpected_platform";
#endif
    RewardedAd rewardedAd;
    public RewardedAd RewardedAd => rewardedAd;

    private void Start()
    {
        //Show coin
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            //Ads initialized
            LoadRewardedAd();
        });
    }
    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        RewardedAd.Load(rewardedID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null && ad == null)
            {
                Debug.Log("Rewarded failed to load" + error);
                return;
            }
            Debug.Log("Rewarded ad loaded" + error);
            rewardedAd = ad;
            RewardedAdEvents(rewardedAd);
        });
    }
    public void ShowRewardedAd(int rewardAmount)
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                //Give reward to player

                CurrencyManager.Instance.AddCurrency(rewardAmount);
                Debug.Log("gived reward " + rewardAmount);
            });
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }
    public void RewardedAdEvents(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log("Rewarded ad paid {0} {1}." +
                adValue.Value +
                adValue.CurrencyCode);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}
