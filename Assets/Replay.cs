using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class Replay : MonoBehaviour
{
    public static int adcount = 0;
    private InterstitialAd interstitial;
    public Canvas myCanvas;
    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void ReplayGame()
    {
        adcount++;
        if(adcount > 2) {
            adcount = 0;
            RequestInterstitial();
            //When you want call Interstitial show
            StartCoroutine(showInterstitial());
        } else {
            SceneManager.LoadScene("PlayScene");
        }
        
        IEnumerator showInterstitial()
        {
            while(!this.interstitial.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            this.interstitial.Show();
            myCanvas.sortingOrder = -1;
        }
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        SceneManager.LoadScene("PlayScene");
    }
}
