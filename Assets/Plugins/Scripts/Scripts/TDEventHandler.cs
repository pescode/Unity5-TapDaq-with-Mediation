using UnityEngine;

namespace Tapdaq {
	public class TDEventHandler : MonoBehaviour {
		private static TDEventHandler reference;

		public static TDEventHandler instance {
			get {
				if (!reference) {
					TDEventHandler[] references = FindObjectsOfType<TDEventHandler> ();

					if (references.Length > 0) {
						reference = references [0]; 
					} else {
						reference = new GameObject ("TapdaqV1").AddComponent<TDEventHandler> ();
						Debug.Log (":: Ad test ::" + "Spawned Event Handler");
					}
				}

				return reference;
			}
		}

		private void Awake () {
			DontDestroyOnLoad (gameObject);
		}

		// Currently just used as an empty call to make a TDEventHandler GameObject
		public void Init () {

		}

		//
		// Delegates
		//


		// Android

		void _onAvailable (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "static_interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video_interstitial") {
				type = "VIDEO";
			}
			else if (adType == "reward_interstitial") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdAvailable (new TDAdEvent (type, "AVAILABLE"));
			}
		}

		void _onAdClosed (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdClosed (new TDAdEvent (type, "DID_CLOSE"));
			}
		}

		void _onAdFailedToLoad (string message) {
			string[] tempInfoParser = message.Split (new string[] {"----"}, System.StringSplitOptions.None);
			string adType = tempInfoParser [0];
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdError (new TDAdEvent (type, "FAILED_TO_LOAD"));
			}
		}

		void _onAdClick (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdClicked (new TDAdEvent (type, "CLICKED"));
			}
		}

		void _onAdOpened (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdStarted (new TDAdEvent (type, "WILL_DISPLAY"));
			}
		}

		void _onAdLoaded (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdAvailable (new TDAdEvent (type, "LOADED"));
			}
		}

		void _onComplete (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdFinished (new TDAdEvent (type, "AD_ENDED"));
			}
		}

		void _onEngagement (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {

			}
		}

		void _onLimitReached (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdError (new TDAdEvent (type, "VALIDATION_EXCEEDED_QUOTA"));
			}
		}

		void _onRejected (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdError (new TDAdEvent (type, "VALIDATION_REJECTED"));
			}
		}

		void _onFailed (string message) {
			string[] tempInfoParser = message.Split (new string[] {"----"}, System.StringSplitOptions.None);
			string adType = tempInfoParser [0];
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdError (new TDAdEvent (type, tempInfoParser[1]));
			}
		}

		void _onUserDeclined (string adType) {
			string type = string.Empty;

			if (adType == "banner") {
				type = "BANNER";
			}
			else if (adType == "interstitial") {
				type = "INTERSTITIAL";
			}
			else if (adType == "video") {
				type = "VIDEO";
			}
			else if (adType == "reward") {
				type = "REWARD_AD";
			}
			else if (adType == "native") {
				type = "NATIVE_AD";
			}

			if (type != string.Empty) {
				TDCallbacks.instance.OnAdClosed (new TDAdEvent (type, "DECLINED_TO_VIEW"));
			}
		}

		void _onVerified (string message) {
			string[] tempInfoParser = message.Split (new string[] {"||"}, System.StringSplitOptions.None);

			string location = tempInfoParser [0];
			string reward = tempInfoParser [1];
			double value = double.Parse (tempInfoParser [2]);

		}


		// iOS

		// Interstitial

		// If you would like run some code right after an interstitial is loaded, implement the following:
		void _didLoadInterstitial (string msg) {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("INTERSTITIAL", "LOADED"));
		}

		// If you would like run some code before an interstitial is shown, implement the following:
		void _willDisplayInterstitial (string msg) {
			TDCallbacks.instance.OnAdStarted (new TDAdEvent ("INTERSTITIAL", "WILL_DISPLAY"));
		}

		// If you would like to run some code after an interstitial is shown, implement the following:
		void _didDisplayInterstitial (string msg) {
			TDCallbacks.instance.OnAdFinished (new TDAdEvent ("INTERSTITIAL", "DID_DISPLAY"));
		}

		// If you would like to run some code just before an interstitial is closed, implement the following:
		void _willCloseInterstitial () {
			// Your code goes here
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("INTERSTITIAL", "WILL_CLOSE"));
		}

		// If you would like to run some code when an interstitial is closed, implement the following:
		void _didCloseInterstitial (string msg) {
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("INTERSTITIAL", "DID_CLOSE"));
		}

		// If you would like to run some code when an interstitial is clicked, implement the following:
		void _didClickInterstitial (string msg) {
			//callbacks aint firing from android
			TDCallbacks.instance.OnAdClicked (new TDAdEvent ("INTERSTITIAL", "CLICKED"));
		}

		// When an interstitial fails to show
		void _didFailToDisplayInterstitial (string msg) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("INTERSTITIAL", "FAILED_TO_DISPLAY"));
		}

		// When error occurs requesting interstitials from the servers
		void _didFailToLoadInterstitial (string msg) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("INTERSTITIAL", "FAILED_TO_LOAD"));
		}

		// When servers respond back with empty queue of interstitials
		void _hasNoInterstitialsAvailable (string msg) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("INTERSTITIAL", "NONE_AVAILABLE"));
		}

		// When an interstitial is ready to display
		void _hasInterstitialsAvailableForOrientation (string orientation) {
			AdManager.instance._hasInterstitialsAvailableForOrientation (orientation);
		}

		// When interstitial fails to load
		void _didFailToLoadInterstitialForOrientation (string orientation) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("INTERSTITIAL", "FAILED_TO_LOAD"));
		}



		// Banner

		// Called immediately after the banner is loaded
		void _didLoadBanner () {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("BANNER", "LOADED"));
		}

		// Called immediately before the banner is to be displayed to the user
		void _willDisplayBanner () {
			TDCallbacks.instance.OnAdStarted (new TDAdEvent ("BANNER", "WILL_DISPLAY"));
		}

		// Called immediately after the banner is displayed to the user
		void _didDisplayBanner () {
			TDCallbacks.instance.OnAdStarted (new TDAdEvent ("BANNER", "DID_DISPLAY"));
		}

		// Called when, for whatever reason, the banner was not able to be displayed
		void _didFailToDisplayBanner () {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("BANNER", "FAILED_TO_DISPLAY"));
		}

		// Called when the user clicks the banner
		void _didClickBanner () {
			TDCallbacks.instance.OnAdClicked (new TDAdEvent ("BANNER", "CLICKED"));
		}

		// Called when the user clicked banner ad has finished and the user is returned to the app
		void _didFinishHandlingClickBanner () {
			TDCallbacks.instance.OnAdClicked (new TDAdEvent ("BANNER", "FINISH_HANDLING_CLICK"));
		}



		// Video

		// Called immediately after a video ad is available to the user
		void _didLoadVideo () {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("VIDEO", "LOADED"));
		}

		// Called immediately before the video is to be displayed to the user
		void _willDisplayVideo () {
			TDCallbacks.instance.OnAdStarted (new TDAdEvent ("VIDEO", "WILL_DISPLAY"));
		}

		// Called immediately after the video is displayed to the user
		void _didDisplayVideo () {
			TDCallbacks.instance.OnAdFinished (new TDAdEvent ("VIDEO", "DID_DISPLAY"));
		}

		// Called when, for whatever reason, the video was not able to be displayed
		void _didFailToDisplayVideo () {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("VIDEO", "FAILED_TO_DISPLAY"));
		}

		// Called when the user closes video
		void _willCloseVideo () {
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("VIDEO", "WILL_CLOSE"));
		}

		// Called when the user closes the ad shown after a video, either by tapping the close button
		void _willDisplayVideoEndAd () {
			TDCallbacks.instance.OnAdFinished (new TDAdEvent ("VIDEO", "AD_ENDED"));
		}

		// Called when the user closes video
		void _didCloseVideo () {
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("VIDEO", "DID_CLOSE"));
		}

		// Called when the user clicks the video ad
		void _didClickVideo () {
			TDCallbacks.instance.OnAdClicked (new TDAdEvent ("VIDEO", "CLICKED"));
		}



		// Reward Video

		// Called immediately after a rewarded video ad is available to the user
		void _didLoadRewardedVideo () {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("REWARD_AD", "LOADED"));
		}

		// Called when a validation of a reward has succeeded
		void _rewardValidationSuceeded (string rewardName) {
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("REWARD_AD", "VALIDATED:" + rewardName));
		}

		// Called when a validation of a reward has exceeded the quota
		void _rewardValidationExceededQuota () {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("REWARD_AD", "VALIDATION_EXCEEDED_QUOTA"));
		}

		// Called when a validation of a reward has been rejected
		void _rewardValidationRejected () {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("REWARD_AD", "VALIDATION_REJECTED"));
		}

		// Called when a validation of a reward has errored
		void _rewardValidationErrored () {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("REWARD_AD", "FAILED_TO_DISPLAY"));
		}

		// Called when a user declines to watch a rewarded video. Applicable if a pop is displayed
		void _userDeclinedToViewRewardedVideo () {
			TDCallbacks.instance.OnAdClosed (new TDAdEvent ("REWARD_AD", "DECLINED_TO_VIEW"));
		}



		// Native

		// When error occurs requesting native adverts from the servers
		void _didFailToLoadNativeAdverts (string msg) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("NATIVE_AD", "FAILED_TO_LOAD"));
		}

		// When servers respond back with empty queue of native adverts
		void _hasNoNativeAdvertsAvailable (string msg) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("NATIVE_AD", "NONE_AVAILABLE"));
		}


		// OLDER VERSION DELEGATES

		// When native advert is successfully loaded
		void _didLoadNativeAdvert (string nativeAdType) {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("NATIVE_AD", "LOADED"));
		}

		// When native advert fails to load
		void _didFailToLoadNativeAdvertForAdType (string nativeAdType) {
			TDCallbacks.instance.OnAdError (new TDAdEvent ("NATIVE_AD", (TDNativeAdType)int.Parse (nativeAdType)+":NONE_AVAILABLE"));
		}






		//Delegate Events
		public void FetchFailed (string msg) {
			
		}
			

		// Called when the request for natives was successful
		// and 1 or more natives were found
		//NOTE - this event will be called everytime a native is cached and ready to be shown.
		//       which can happen multiple times in a few frames
		public void _hasNativeAdvertsAvailableForAdUnit (string unit) {
			AdManager.instance._hasNativeAdvertsAvailableForAdUnit (unit);
		}

		public void _Android_hasNoNativeAdvertsAvailable (string msg) {
			AdManager.instance._Android_hasNoNativeAdvertsAvailable (msg);
		}

		public void _Android_hasNativeAdvertsAvailableForUnit (string msg) {
			AdManager.instance._Android_hasNativeAdvertsAvailableForUnit (msg);
		}

		public void _FailedToConnectToServer (string msg) {
			AdManager.instance._FailedToConnectToServer (msg);
		}

		public void _UnexpectedErrorHandler (string msg) {
			AdManager.instance._UnexpectedErrorHandler (msg);
		}
	}
}