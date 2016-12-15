/*
Author: Victor Corvalan @pescadon
pescode.wordpress.com

Roshka Studios
roshkastudios.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tapdaq;
using UnityEngine.Analytics;
using System;

public class RshkAds : MonoBehaviour {
	public const string TAG_REWARDED_CONTINUE = "continue_mathathlete";
	public const string TAG_REWARDED_UNLOCK_GAMEMODE = "unlock_challenge";

	public const string TAG_INTERSTITIAL_GAMEOVER = "int_gameover";
	public const string TAG_INTERSTITIAL_CROSSPROMOTION = "int_crosspromotion";

	public const string TAG_VIDEO_GAMEOVER = "video_gameover";

	static RshkAds instance;

	static int InterstitialMinNextAdShow = 0;
	static int InterstitialMaxNextAdShow = 2;
	static int InterstitialNextAdShow = 0;
	static int InterstitialAdCount = 0;

	static int VideoMinNextAdShow = 0;
	static int VideoMaxNextAdShow = 2;
	static int VideoNextAdShow = 0;
	static int VideoAdCount = 0;

	public static bool HasWatchedRewardedAds = false;
	public delegate void actionRewardedCompleted();
	public static event actionRewardedCompleted OnRewardedCompleted;

	//
	static bool isRewardedLoaded = false;
	static bool isInterstitialLoaded = false;
	static bool isVideoLoaded = false;
	static bool isBannerLoaded = false;
	static bool isBannerShowing = false;
	static bool canShowBanner = false;

	// Use this for initialization
	void Start () {
		if (instance) {
			Destroy (gameObject);
		} else {
			Debug.Log ("**********************\n**********************\nINITIALIZING AD SYSTEM");
			instance = this;
			DontDestroyOnLoad (gameObject);
			InterstitialAdCount = PlayerPrefs.GetInt ("InterstitialAdCount", InterstitialAdCount);
			InterstitialNextAdShow = PlayerPrefs.GetInt ("InterstitialNextAdShow", InterstitialNextAdShow);
			VideoAdCount = PlayerPrefs.GetInt ("VideoAdCount", VideoAdCount);
			VideoNextAdShow = PlayerPrefs.GetInt ("VideoNextAdShow", VideoNextAdShow);

			SetupListeners ();

			AdManager.Init();

		}
	}

	static void SetupListeners()
	{
		TDCallbacks.AdAvailable += OnAdAvailable;
		TDCallbacks.AdStarted += OnAdStarted;
		TDCallbacks.AdFinished += OnAdFinished;
		TDCallbacks.AdClicked += OnAdClicked;
		TDCallbacks.AdNotAvailable += OnAdNotAvailable;
		TDCallbacks.AdError += OnAdError;
	}

	public static void ShowInterstitial(string tag = "interstitial")
	{
		Debug.Log ("**********************\n**********************\nSHOW INTERSTITIAL! \n " + (InterstitialAdCount+1) + "=" + InterstitialNextAdShow);
		//if (!IAP.IsAdsRemoved ()) {
			InterstitialAdCount++;
			PlayerPrefs.SetInt ("InterstitialAdCount", InterstitialAdCount);
			if (isInterstitialLoaded) {
				if (InterstitialAdCount >= InterstitialNextAdShow) {
					if (!HasWatchedRewardedAds) {
						InterstitialAdCount = 0;
						InterstitialNextAdShow = UnityEngine.Random.Range (InterstitialMinNextAdShow, InterstitialMaxNextAdShow);
						VideoAdCount = 0;
						PlayerPrefs.SetInt ("InterstitialNextAdShow", InterstitialNextAdShow);
						AdManager.ShowInterstitial (tag);
						Analytics.CustomEvent ("ADS Interstitial", new Dictionary<string, object> {
							{ "Tag", tag }
						});
					}
				}
			}
		//}
	}

	public static void ShowRewarded(string tag = "rewarded")
	{
		Debug.Log ("**********************\n**********************\nSHOW REWARDED! \n ");
		Analytics.CustomEvent("ADS Rewarded", new Dictionary<string, object>
			{
				{ "Tag", tag }
			});
		HasWatchedRewardedAds = true;
		InterstitialAdCount = 0;
		VideoAdCount = 0;
		AdManager.ShowRewardVideo(tag);
	}

	public static void ShowVideo(string tag = "video")
	{
		Debug.Log ("**********************\n**********************\nSHOW INTERSTITIAL! \n " + (VideoAdCount+1) + "=" + VideoNextAdShow);

		//if (!IAP.IsAdsRemoved ()) {
			VideoAdCount++;
			PlayerPrefs.SetInt ("VideoAdCount", VideoAdCount);
			if (isVideoLoaded) {
				if (VideoAdCount >= VideoNextAdShow) {
					InterstitialAdCount = 0;
					VideoAdCount = 0;
					VideoNextAdShow = UnityEngine.Random.Range (VideoMinNextAdShow, VideoMaxNextAdShow);
					PlayerPrefs.SetInt ("VideoNextAdShow", VideoNextAdShow);
					AdManager.ShowVideo (tag);
					Analytics.CustomEvent ("ADS Video", new Dictionary<string, object> {
						{ "Tag", tag }
					});
					
				}	
			}
		//}
	}

	public static void ShowBanner()
	{
		//if (!IAP.IsAdsRemoved ()) {
			if (!isBannerShowing) {
				AdManager.RequestBanner (TDMBannerSize.TDMBannerSmartLandscape);
				canShowBanner = true;
			}
		//}
	}

	public static void HideBanner()
	{
		canShowBanner = false;
		if (isBannerLoaded && isBannerShowing) {
			isBannerShowing = false;
		}
	}

	public static void DestroyBanner()
	{

	}
		
	public static bool IsRewardedAdsAvailable()
	{
		return isRewardedLoaded;
	}

	static IEnumerator InterstitialDone()
	{
		yield return new WaitForSeconds (1f);
		Debug.Log ("-- INTERSTITIAL COMPLETED --");
		AudioListener.pause = false;
		Time.timeScale = 1;			//optional to continue the game
	}

	// We need this to avoid errors on Android
	static IEnumerator RewardedDone()
	{
		yield return new WaitForSeconds (1f);
		Debug.Log ("-- REWARDED COMPLETED --");
		AudioListener.pause = false;
		OnRewardedCompleted();
	}

	//
	// Callback Methods
	//
	static void OnAdAvailable (TDAdEvent e) {
		if (e.adType == "INTERSTITIAL") {
			Debug.Log ("-- Test Log -- Interstitial is available.");
			isInterstitialLoaded = true;
		} else if (e.adType == "VIDEO") {
			Debug.Log ("-- Test Log -- Video ad is available.");
			isVideoLoaded = true;
		} else if (e.adType == "REWARD_AD") {
			Debug.Log ("-- Test Log -- Reward video ad is available.");
			isRewardedLoaded = true;
		} else if (e.adType == "BANNER") {
			Debug.Log ("-- Test Log -- Banner ad is available.");
			isBannerLoaded = true;
			if (canShowBanner) {
				AdManager.ShowBanner ();
				isBannerShowing = true;
			}
		}
	}

	static void OnAdStarted (TDAdEvent e) {
		Debug.Log ("-- Test Log OnAdStarted -- " + e.adType + ": " + e.message);
		if (e.adType != "BANNER") {
			AudioListener.pause = true;
			Time.timeScale = 0.02f;		//	optional to pause the game
		}
	}

	static void OnAdFinished (TDAdEvent e) {
		Debug.Log ("-- Test Log OnAdFinished -- " + e.adType + ": " + e.message);
		if (e.adType == "INTERSTITIAL") {
			instance.StartCoroutine (InterstitialDone ());
		} else if (e.adType == "VIDEO") {
			instance.StartCoroutine (InterstitialDone ());
		} else if (e.adType == "REWARD_AD") {
			instance.StartCoroutine (RewardedDone ());
		}

	}

	static void OnAdClicked (TDAdEvent e) {
		Debug.Log ("-- Test Log OnAdClicked -- " + e.adType + ": " + e.message);
		Analytics.CustomEvent("ADS Clicked", new Dictionary<string, object>
			{
				{ "Type", e.adType },
				{ "Message", e.message }
			});
	}


	static void OnAdNotAvailable (TDAdType adType) {
		Debug.Log ("-- Test Log OnAdNotAvailable -- " + adType + " is not available.");
		if (adType.ToString() == "INTERSTITIAL") {
			isInterstitialLoaded = false;
		}else if (adType.ToString() == "VIDEO") {
			isVideoLoaded = false;
		} else if (adType.ToString() == "REWARD_AD") {
			isRewardedLoaded = false;
		}
	}

	static void OnAdError (TDAdEvent e) {
		Debug.Log ("-- Test Log -- " + e.adType + ": " + e.message);
	}
		
}
