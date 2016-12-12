using System;
using System.Threading;

namespace Tapdaq {
	public class TDCallbacks {
		//
		// Static Fields
		//
		private static TDCallbacks reference;


		//
		// Static Properties
		//
		public static TDCallbacks instance {
			get {
				if (TDCallbacks.reference == null) {
					TDCallbacks.reference = new TDCallbacks ();
				}
				return TDCallbacks.reference;
			}
		}

		//
		// Constructors
		//
		internal TDCallbacks () {}

		//
		// Static Events
		//
		public static event Action<TDAdEvent> AdAvailable;

		public static event Action<TDAdEvent> AdFinished;

		public static event Action<TDAdType> AdNotAvailable;

		public static event Action<TDAdEvent> AdStarted;

		public static event Action<TDAdEvent> AdClicked;

		public static event Action<TDAdEvent> AdClosed;

		public static event Action<TDAdEvent> AdError;

//		public static event Action<string> BannerAdLeftApplication;

//		public static event Action<string> BannerAdLoaded;

//		public static event Action<string> BannerAdWillPresentModalView;

//		public static event Action<string> NativeError;

//		public static event Action<TDAdEvent> RequestFail;

//		public static event Action<string> VirtualCurrencyError;

//		public static event Action<string> VirtualCurrencySuccess;

		//
		// Methods
		//
		public void OnAdAvailable (TDAdEvent ad) {
			if (TDCallbacks.AdAvailable != null)
				TDCallbacks.AdAvailable (ad);
		}

		public void OnAdClicked (TDAdEvent ad) {
			if (TDCallbacks.AdClicked != null)
				TDCallbacks.AdClicked (ad);
		}

		public void OnAdError (TDAdEvent ad) {
			if (TDCallbacks.AdError != null) {
				TDCallbacks.AdError (ad);
			}
		}

		public void OnAdFinished (TDAdEvent result) {
			if (TDCallbacks.AdFinished != null) {
				TDCallbacks.AdFinished (result);
			}
		}

		public void OnAdClosed (TDAdEvent ad) {
			if (TDCallbacks.AdClosed != null) {
				TDCallbacks.AdClosed (ad);
			}
		}

		public void OnAdNotAvailable (TDAdType adFormat) {
			if (TDCallbacks.AdNotAvailable != null) {
				TDCallbacks.AdNotAvailable (adFormat);
			}
		}

		public void OnAdStarted (TDAdEvent ad) {
			if (TDCallbacks.AdStarted != null) {
				TDCallbacks.AdStarted (ad);
			}
		}

	}
}
