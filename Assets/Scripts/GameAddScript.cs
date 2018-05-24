using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GameAddScript : MonoBehaviour {
    const string InterstitialAdUnitId = "ca-app-pub-1549259004910051/3006350927";
    const string VideoAdUnitId = "ca-app-pub-1549259004910051/6099418127";
    InterstitialAd interstitial;
    bool rewardedBool;
    int PlaysCount;
    RewardBasedVideoAd rewardBasedVideo;
    

    string CurrentSceneName;

    // Use this for initialization
    void Start() {

        rewardBasedVideo = RewardBasedVideoAd.Instance;
        InterstitialEventSub();

    }

    public void CheckForAdd(string SceneName)       
    {
        VideoIsLoaded();

        CurrentSceneName = SceneName;
        PlaysCount += 1;
        if (PlaysCount % 5 == 1)
        {
            RequestInterstitial();              // Инициализация рекламы при первом запуске
        }
        if (PlaysCount % 5 == 0)
        {
            if (interstitial.IsLoaded())
            {
                print("Showing interstitial add");
                interstitial.Show();
                return;
            }

        }
        BackToGame();
    }

    private void RequestInterstitial()
    {
        // Create an empty ad request.
        interstitial = new InterstitialAd(InterstitialAdUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {


        rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, VideoAdUnitId);
    }


    public void ShowRewVideo()
    {
        if (rewardBasedVideo.IsLoaded())
            rewardBasedVideo.Show();
        rewardBasedVideo = RewardBasedVideoAd.Instance;
    }

    public bool VideoIsLoaded()
    {
        if (rewardBasedVideo.IsLoaded() == false)
        {
            RequestRewardBasedVideo();
            return false;
        }
        else return true;
        
    }

    private void InterstitialEventSub()
    {
        InterstitialAd InterstitialView = new InterstitialAd(InterstitialAdUnitId);
        // Called when an ad request has successfully loaded.
        //InterstitialView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        //InterstitialView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        //InterstitialView.OnAdOpened += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        InterstitialView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //InterstitialView.OnAdLeavingApplication += HandleOnAdLeavingApplication;


        // is opened.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // has rewarded the user.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;



    }

    public void HandleRewardBasedVideoOpened(object sender, System.EventArgs args)
    {
        rewardedBool = false;
    }

    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        Invoke("LooooooooseBlock", 1f);
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        BackToGame();
    }

    public void HandleRewardBasedVideoRewarded(object sender, System.EventArgs args)
    {
        rewardedBool = true;
        CancelInvoke();
        StartCoroutine(GameObject.Find("WhiteHouse").GetComponent<HouseScript>().Disappeare(true));
        
    }

    void BackToGame()
    {
        if (CurrentSceneName == "MenuScene")
            GameObject.Find("Manager").GetComponent<MenuScript>().JumpIntoGame();
        else
        {
            if (CurrentSceneName == "MainScene")
                GameObject.Find("EndGroup").GetComponent<EndGameInterfaceScript>().SecondStepRestart();
        }
    }

    void LooooooooseBlock()
    {
        if(!rewardedBool)
            GameObject.Find("WhiteHouse").GetComponent<HouseScript>().EvadedAdd();
    }

}
