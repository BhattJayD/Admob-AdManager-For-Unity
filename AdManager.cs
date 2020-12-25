using UnityEngine;
using admob;

public class AdManager : MonoBehaviour
{
    	Admob ad;
	string appID="";
	string bannerID="";
	string interstitialID="";
	string videoID="";
	//string nativeBannerID = "";

    private static AdManager _instance;
    public static AdManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance=GameObject.FindObjectOfType<AdManager>();
            }
            return _instance;
        }
    }
    void Awake()
    {
        #if UNITY_IOS
        		// appID="ca-app-pub-3940256099942544~1458002511";
				 //bannerID="ca-app-pub-3940256099942544/2934735716";
				 //interstitialID="ca-app-pub-3940256099942544/4411468910";
				 //videoID="ca-app-pub-3940256099942544/1712485313";
				// nativeBannerID = "ca-app-pub-3940256099942544/3986624511";
#elif UNITY_ANDROID
        		 //appID="";
				 bannerID="";
				 interstitialID="";
				 videoID="";
				// nativeBannerID = "";
#endif
        AdProperties adProperties = new AdProperties();
        adProperties.isTesting(false);
        adProperties.isAppMuted(true);
        adProperties.isUnderAgeOfConsent(false);
        adProperties.appVolume(100);
        adProperties.maxAdContentRating(AdProperties.maxAdContentRating_G);
        string[] keywords = { "diagram", "league", "brambling" };
        adProperties.keyworks(keywords);

        ad = Admob.Instance();
            ad.bannerEventHandler += onBannerEvent;
            ad.interstitialEventHandler += onInterstitialEvent;
            ad.rewardedVideoEventHandler += onRewardedVideoEvent;
            ad.initSDK(adProperties);//reqired,adProperties can been null
    }


    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
    }

    public void ShowInterstitial()
    {
        Debug.Log("touch inst button -------------");
            if (ad.isInterstitialReady())
            {
                ad.showInterstitial();
            }
            else
            {
                ad.loadInterstitial(interstitialID);
            }
    }
    public void ShowBanner()
    {
        Admob.Instance().showBannerRelative(bannerID,AdSize.BANNER, AdPosition.BOTTOM_CENTER);
    }

    public void DestroyBanner()
    {
        Admob.Instance().removeBanner();
    }
    public void ShowRewardedVideo()
    {
        Debug.Log("touch video button -------------");
        if (ad.isRewardedVideoReady())
        {
            ad.showRewardedVideo();
        }
        else
        {
         	ad.loadRewardedVideo(videoID);
        }
    }
}
