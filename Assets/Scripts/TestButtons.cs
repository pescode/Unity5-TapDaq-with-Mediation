using UnityEngine;
using System.Collections;

public class TestButtons : MonoBehaviour {

	void OnEnable()
	{
		RshkAds.OnRewardedCompleted += UserHasBeenRewarded;
	}

	void OnDisable()
	{
		RshkAds.OnRewardedCompleted -= UserHasBeenRewarded;
	}

	public void UserHasBeenRewarded()
	{
		Debug.Log ("User has been rewarded!");
	}

	public void ClickInterstitial()
	{
		RshkAds.ShowInterstitial ();
	}

	public void ClickRewarded()
	{
		RshkAds.ShowRewarded ();
	}

	public void ClickLoadInterstitial()
	{
		
	}

	public void ClickLoadRewarded()
	{

	}

	public void ClickHideBanner()
	{
		RshkAds.HideBanner ();
	}

	public void ClickShowBanner()
	{
		RshkAds.ShowBanner ();
	}

	void Update()
	{

	}
}
