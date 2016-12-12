using AOT;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using UnityEngine;

namespace Tapdaq {
	public class AdManager {

		//
		// Static Fields
		//
		private static AdManager reference;


		//
		// Static Properties
		//
		public static AdManager instance {
			get {
				if (AdManager.reference == null) {
					AdManager.reference = new AdManager ();
				}
				return AdManager.reference;
			}
		}

		//
		// Constructors
		//
		internal AdManager () {}

		private const string unsupportedPlatformMessage = "We support iOS and Android platforms only.";

		// Interop Delegate
		public delegate void nativeAdDelegate (string _adObject);

		public delegate void interstitialAdDelegate (string _adObject);

		public delegate void InteropInterstitialDelegateCallBack (string _adObject);

		public delegate void InteropNativeDelegateCallBack (string adType);

		[DllImport ("__Internal")]
		private static extern void _GenerateCallBacks (InteropNativeDelegateCallBack nativeAdsDelegate, InteropInterstitialDelegateCallBack interstitialAdDelegate);

		#if UNITY_IPHONE
		
		//================================= Interstitials ==================================================
		[DllImport ("__Internal")]
		private static extern void _ConfigureTapdaq(string appIdChar, string clientKeyChar, int frequencyCap, int frequencyCapDurationInDays, string enabledAdTypesChar, bool testMode);

		[DllImport ("__Internal")]
		private static extern void _LaunchMediationDebugger();

		[DllImport ("__Internal")]
		private static extern void _AddTestDevices(string adNetwork, string deviceIDs);

		// interstitial
		[DllImport ("__Internal")]
		private static extern void _ShowInterstitial(int pointer);

		[DllImport ("__Internal")]
		private static extern void _ShowInterstitialWithTag(string tag);

		// banner
		[DllImport ("__Internal")]
		private static extern void _RequestBanner(int size);

		[DllImport ("__Internal")]
		private static extern void _RequestBannerWithTag (string tag, int size);

		[DllImport ("__Internal")]
		private static extern void _ShowBanner();

		[DllImport ("__Internal")]
		private static extern void _ShowBannerWithTag (string tag);

		// video
		[DllImport ("__Internal")]
		private static extern void _ShowVideo();

		[DllImport ("__Internal")]
		private static extern void _ShowVideoWithTag (string tag);

		// reward video
		[DllImport ("__Internal")]
		private static extern void _ShowRewardVideo();

		[DllImport ("__Internal")]
		private static extern void _ShowRewardVideoWithTag (string tag);

		//================================== Natives =================================================
		[DllImport ("__Internal")]
		private static extern void _FetchNative(nativeAdDelegate _delegate, int adType); 

		[DllImport ("__Internal")]
		private static extern void _FetchNativeAdWithTag (nativeAdDelegate callback, string tag, int nativeAdType);

		[DllImport ("__Internal")]
		private static extern void _SendNativeClick(int pointer);

		[DllImport ("__Internal")]
		private static extern void _SendNativeImpression(int pointer);

		#endif

		#region Class Variables

		private TDSettings settings;

		private string iosApplicationID = "";
		private string iosClientKey = "";

		private string androidApplicationID = "";
		private string androidClientKey = "";

		private int frequencyCap = 10;
		private int frequencyCapDurationInDays = 2;

		private bool testModeEnabled = true;
		private bool showLogMessages = false;


		//-----------Ad Types//
		Dictionary<TDAdType, string> adTags;

		private static NativeAd thisNativeAd;
		private static TDNativeAd externalNative = new TDNativeAd ();

		#endregion

		public static void Init () {
			instance._Init ();
		}

		private void _Init () {
			if (!settings) {
				settings = Resources.LoadAll<TDSettings> ("Tapdaq")[0];
			}

			TDEventHandler.instance.Init ();

			showLogMessages = settings.showLogs;
			testModeEnabled = settings.testMode;

			iosApplicationID = settings.ios_applicationID;
			iosClientKey = settings.ios_clientKey;
			androidApplicationID = settings.android_applicationID;
			androidClientKey = settings.android_clientKey;

			frequencyCap = settings.frequency;
			frequencyCapDurationInDays = settings.duration;

			string adtags = settings.tags.GetAdTypesWithTags ();

			if (showLogMessages) {
				LogMessage (TDLogSeverity.debug, "TapdaqSDK/Test Mode Active? -- " + testModeEnabled);

				#if UNITY_IPHONE
				LogMessage(TDLogSeverity.debug, "TapdaqSDK/Application ID -- " + iosApplicationID);
				LogMessage(TDLogSeverity.debug, "TapdaqSDK/Client Key -- " + iosClientKey);
				#elif UNITY_ANDROID
				LogMessage(TDLogSeverity.debug, "TapdaqSDK/Application ID -- " + androidApplicationID);
				LogMessage(TDLogSeverity.debug, "TapdaqSDK/Client Key -- " + androidClientKey);
				#endif

				LogMessage (TDLogSeverity.debug, "TapdaqSDK/Ad Frequency -- " + frequencyCap);
				LogMessage (TDLogSeverity.debug, "TapdaqSDK/Ad Duration -- " + frequencyCapDurationInDays);


			}


			#if UNITY_IPHONE
			GenerateCallbacks();
			Initialize (iosApplicationID, iosClientKey, frequencyCap, frequencyCapDurationInDays, adtags);
			#elif UNITY_ANDROID
			string tags = settings.tags.GetAdTypesWithTags();
			Initialize (androidApplicationID, androidClientKey, frequencyCap, frequencyCapDurationInDays, tags);
			#endif

			externalNative = new TDNativeAd ();
		}

		private void Initialize (string appID, string clientKey, int freq, int dur, string tags) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			string compressedTags = string.Empty;


			#if UNITY_IPHONE
//			foreach (var t in tags) {
//				if (t.Value == string.Empty)
//					continue;
//
//				compressedTags += (int)t.Key;
//				compressedTags += "-";
//				compressedTags += t.Value;
//				compressedTags += ";";
//			}
//
//			if (compressedTags.Length > 1)
//				compressedTags = compressedTags.Remove (compressedTags.Length-1);

			compressedTags = settings.tags.GetTagSets ();
			string adtags = settings.tags.GetAdTypesWithTags();


			Debug.Log (compressedTags);

			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				LogMessage (TDLogSeverity.debug, "TapdaqSDK/Initializing");

				_ConfigureTapdaq(appID, clientKey, freq, dur, adtags, testModeEnabled);
			}
			#elif UNITY_ANDROID
			string _path = Application.persistentDataPath.Substring( 0, Application.persistentDataPath.Length - 5 );
			_path = _path.Substring( 0, _path.LastIndexOf( '/' ) );
			_path = Path.Combine( _path, "Documents/" );

//			foreach (var t in tags) {
//				if (t.Value == string.Empty)
//					continue;
//
//				compressedTags += t.Key;
//				compressedTags += "-";
//				compressedTags += t.Value;
//				compressedTags += ";";
//			}
//
//			if (compressedTags.Length > 1)
//				compressedTags = compressedTags.Remove (compressedTags.Length-1);

			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");

					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("SetDataPath", appCtx,_path);
						mHumbleAssistantClass.CallStatic("InitiateTapdaq",act,appID,clientKey,tags, freq, dur, testModeEnabled);
					}
				}
			}
			#endif
		}

		[MonoPInvokeCallback (typeof(InteropInterstitialDelegateCallBack))]
		private static void InterstitialDelegateCallBack (string _orientation) {
			reference._hasInterstitialsAvailableForOrientation (_orientation);
		}

		[MonoPInvokeCallback (typeof(InteropNativeDelegateCallBack))]
		private static void NativeDelegateCallBack (string adType) {
			reference._hasNativeAdvertsAvailableForAdUnit (adType);
		}

		private void GenerateCallbacks () {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				_GenerateCallBacks(NativeDelegateCallBack, InterstitialDelegateCallBack);
			}
			#endif
		}

		// Called when the request for interstitials was successful
		// and 1 or more interstitials were found
		//NOTE - this event will be called everytime an interstitial is cached and ready to be shown.
		//       which can happen multiple times in a few frames
		public void _hasInterstitialsAvailableForOrientation (string orientation) {
			Debug.Log (":: Ad test ::" + "Interstitial available for " + orientation);
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("INTERSTITIAL", "LOADED:" + ((TDOrientation)int.Parse (orientation)).ToString ().ToUpper ()));
		}
	

		// Called when the request for natives was successful
		// and 1 or more natives were found
		//NOTE - this event will be called everytime a native is cached and ready to be shown.
		//       which can happen multiple times in a few frames
		public void _hasNativeAdvertsAvailableForAdUnit (string adType) {
			TDCallbacks.instance.OnAdAvailable (new TDAdEvent ("NATIVE_AD", "LOADED:" + ((TDNativeAdType)int.Parse (adType)).ToString ()));
		}

		public void _Android_hasNoNativeAdvertsAvailable (string msg) {
			TDCallbacks.instance.OnAdNotAvailable (TDAdType.TDAdType1x1Small);
		}

		public void _Android_hasNativeAdvertsAvailableForUnit (string msg) {

			string[] adObject = msg.Split (new[]{ "<>" }, System.StringSplitOptions.None);




//			if (hasNativeAdvertsAvailableForAdUnit != null) {
//				hasNativeAdvertsAvailableForAdUnit (adUnit, adSize, adOrientation);
//			}
		}

		public void _FailedToConnectToServer (string msg) {
			Debug.Log (":: Ad test ::" + "Failed to connect to server");
//			if (didFailToConnectToServer != null) {
//				didFailToConnectToServer (msg);
//			}
		}

		#region Android Listener Class

		#if UNITY_ANDROID
		class NativeAdFetchCallback : AndroidJavaProxy {
			public NativeAdFetchCallback() : base("com.nerd.TapdaqUnityPlugin.TapdaqUnity$NativeAdFetchListener") { }

			void onFetchFinished(string _adObj) {
				BuildAndroidNativeAd(_adObj);
			}
		}
		#endif
		#endregion

		public void _UnexpectedErrorHandler (string msg) {
			Debug.Log (":: Ad test ::" + msg);
			LogMessage (TDLogSeverity.error, msg);
		}

		public static void LogMessage (TDLogSeverity severity, string message) {
			string prefix = "Tapdaq Unity SDK: ";
			if (severity == TDLogSeverity.warning) {
				Debug.LogWarning (prefix + message);
			} else if (severity == TDLogSeverity.error) {
				Debug.LogError (prefix + message);
			} else {
				Debug.Log (prefix + message);
			}

		}

		[MonoPInvokeCallback (typeof(nativeAdDelegate))]
		private static void BuildNativeAd (string adObject) {
			thisNativeAd = new NativeAd (adObject);
			externalNative.Build (thisNativeAd);			
		}

		public static void BuildAndroidNativeAd (string adObject) {
			thisNativeAd = new NativeAd (adObject);
			externalNative.Build (thisNativeAd);

		}

		public void FetchFailed (string msg) {
			Debug.Log (msg);
			LogMessage (TDLogSeverity.debug, "unable to fetch more ads");
		}

		public static void LaunchMediationDebugger () {
			#if UNITY_IPHONE
			_LaunchMediationDebugger ();
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowMediationDebugger",act);
					}
				}
			}
			#endif
		}

		public static void AddTestDevicesForAdMob (params string[] testDeviceIDs) {
			if (testDeviceIDs.Length > 0) {
				AddTestDevices ("AdMob", testDeviceIDs);
			}
		}

		public static void AddTestDevicesForFacebook (params string[] testDeviceIDs) {
			if (testDeviceIDs.Length > 0) {
				AddTestDevices ("Facebook", testDeviceIDs);
			}
		}

		private static void AddTestDevices (string adNetwork, params string[] testDeviceIDs) {
			string compressedIDs = string.Empty;

			for (int i = 0; i < testDeviceIDs.Length; i++) {
				if (i != 0)
					compressedIDs += "<!@#$%$#@!>";

				compressedIDs += testDeviceIDs [i];
			}

			#if UNITY_IPHONE
			_AddTestDevices (adNetwork, compressedIDs);
			#elif UNITY_ANDROID

			#endif
		}

		public static void ShowInterstitial () {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowInterstitial(0);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowInterstitial",act);
					}
				}
			}
			#endif
		}

		public static void ShowInterstitial (string tag) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowInterstitialWithTag(tag);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowInterstitialWithTag",act,tag);
					}
				}
			}
			#endif
		}

		public static void RequestBanner (TDMBannerSize size) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_RequestBanner ((int)size);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("LoadBannerOfType",act, "MEDIUM_RECT");
					}
				}
			}
			#endif
		}

		public static void RequestBanner (TDMBannerSize size, string tag) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_RequestBannerWithTag (tag, (int)size);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("LoadBannerOfType",act, "MEDIUM_RECT");
					}
				}
			}
			#endif
		}

		public static void ShowBanner () {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowBanner ();
				}
			}
			#elif UNITY_ANDROID

			#endif
		}

		public static void ShowBanner (string tag) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowBannerWithTag (tag);
				}
			}
			#elif UNITY_ANDROID

			#endif
		}

		public static void ShowVideo () {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowVideo ();
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowVideo",act);
					}
				}
			}
			#endif
		}

		public static void ShowVideo (string tag) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowVideoWithTag (tag);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowVideoWithTag",act,tag);
					}
				}
			}
			#endif
		}

		public static void ShowRewardVideo () {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowRewardVideo ();
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowRewardAd",act);
					}
				}
			}
			#endif
		}

		public static void ShowRewardVideo (string tag) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.warning, unsupportedPlatformMessage);
			}

			#if UNITY_IPHONE
			if(Application.platform == RuntimePlatform.IPhonePlayer) {
				//fire off event
				if(AdManager.instance) {
					_ShowRewardVideoWithTag (tag);
				}
			}
			#elif UNITY_ANDROID
			if(Application.platform == RuntimePlatform.Android) {
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("ShowRewardAdWithTag",act,tag);
					}
				}
			}
			#endif
		}

		public static TDNativeAd GetNativeAd (TDNativeAdType adType) {
			#if UNITY_IPHONE
			_FetchNative(BuildNativeAd,(int)adType);
			#elif UNITY_ANDROID
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
					mHumbleAssistantClass.CallStatic("FetchNativeAd", 1, adType.ToString (),new NativeAdFetchCallback());
				}
			}
			#endif

			return externalNative;
		}

		public static TDNativeAd GetNativeAd (TDNativeAdType adType, string tag) {
			#if UNITY_IPHONE
			_FetchNativeAdWithTag(BuildNativeAd,tag,(int)adType);
			#elif UNITY_ANDROID
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
					mHumbleAssistantClass.CallStatic("FetchNativeAdWithTag", 1, adType.ToString (),tag,new NativeAdFetchCallback());
				}
			}
			#endif

			return externalNative;
		}

		public static void SendNativeImpression (TDNativeAd ad) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.debug, "Tapdaq Dummy: Send Native Impression");
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_IPHONE
				_SendNativeImpression(ad.pointer);
				#endif

			} else if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("SendNativeImpression",ad.pointer,appCtx);
					}
				}
				#endif
			}
		}

		public static void SendNativeClick (TDNativeAd ad) {
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
				LogMessage (TDLogSeverity.debug, "Tapdaq Dummy: Send Native Click");
			} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
				#if UNITY_IPHONE
				_SendNativeClick(ad.pointer);
				#endif

			} else if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
					AndroidJavaObject act = jc.GetStatic<AndroidJavaObject>("currentActivity");
					AndroidJavaObject appCtx = act.Call<AndroidJavaObject>("getApplicationContext");
					using (AndroidJavaClass mHumbleAssistantClass = new AndroidJavaClass("com.nerd.TapdaqUnityPlugin.TapdaqUnity")) {
						mHumbleAssistantClass.CallStatic("SendNativeClick",ad.pointer,appCtx);
					}
				}
				#endif
			}
		}

		public static implicit operator bool (AdManager item) {
			return item != null;
		}
	}
}