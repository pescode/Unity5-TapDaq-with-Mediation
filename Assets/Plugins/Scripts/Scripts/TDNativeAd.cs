using UnityEngine;
using System;

namespace Tapdaq {
	public class TDNativeAd {

		//This class exposes the tapdaq ad class with a much more accessable constructor

		public string applicationId { get; private set; }
		public string targetingId { get; private set; }
		public string subscriptionId { get; private set; }
		// (optional)
		//

		//--native specific members

		public string appName { get; private set; }
		public string appDescription { get; private set; }
		public string buttonText { get; private set; }
		public string developerName { get; private set; }
		public string ageRating { get; private set; }
		public string appSize { get; private set; }
		public float averageReview { get; private set; }
		public int totalReviews { get; private set; }
		public string category { get; private set; }
		public string appVersion { get; private set; }
		public float price { get; private set; }
		public string currency { get; private set; }
//		public TDNativeAdUnit adUnit { get; private set; }
		// Can be either `TDNativeAdUnitSquare`, `TDNativeAdUnitNewsfeed`, `TDNativeAdUnitFullscreen`, `TDNativeAdUnitStrip`

//		public TDNativeAdSize adSize { get; private set; }
		// Can be either `TDNativeAdSizeSmall`, `TDNativeAdSizeMedium`, `TDNativeAdSizeLarge`

		public string iconUrl { get; private set; }
		public Texture2D icon { get; private set; }
		//---

		public string creativeIdentifier { get; private set; }
		public TDOrientation creativeOrientation { get; private set; }
		// Can be either `TDOrientationPortrait` or `TDOrientationLandscape
		public string creativeResolution { get; private set; }
		// Can be `TDResolution1x`, `TDResolution2x` or `TDResolution3x`

		public int creativeAspectRatioWidth { get; private set; }
		public int creativeAspectRatioHeight { get; private set; }
		public string creativeURL { get; private set; }
		public Texture2D creativeImage { get; private set; }
		public int pointer{ get; private set; }

		public void Reset () {
			applicationId = "";
			targetingId = "";
			subscriptionId = "";

			appName = "This is a test native ad";
			appDescription = "";
			buttonText = "";
			developerName = "";
			ageRating = "";
			appSize = "";
			averageReview = 0;
			totalReviews = 0;
			category = "";
			appVersion = "";
			price = 0;
			currency = "";
//			adUnit = TDNativeAdUnit.square; // Can be either `TDNativeAdUnitSquare`, `TDNativeAdUnitNewsfeed`, `TDNativeAdUnitFullscreen`, `TDNativeAdUnitStrip`
//			adSize = TDNativeAdSize.small; // Can be either `TDNativeAdSizeSmall`, `TDNativeAdSizeMedium`, `TDNativeAdSizeLarge`
			iconUrl = "http://thisisatesticonurl.com";
			icon = null;

			creativeIdentifier = "";
			creativeOrientation = TDOrientation.landscape;
			creativeResolution = "";

			creativeAspectRatioWidth = 1;
			creativeAspectRatioHeight = 1;

			creativeURL = "http://thisisatest.com";
			creativeImage = null;
			pointer = 0;
		}

		public override string ToString () {
			return string.Format ("[TDNativeAd: applicationId={0}, targetingId={1}, subscriptionId={2}, appName={3}, appDescription={4}, buttonText={5}, developerName={6}, ageRating={7}, appSize={8}, averageReview={9}, totalReviews={10}, category={11}, appVersion={12}, price={13}, currency={14}, iconUrl={15}, icon={16}, creativeIdentifier={17}, creativeOrientation={18}, creativeResolution={19}, creativeAspectRatioWidth={20}, creativeAspectRatioHeight={21}, creativeURL={22}, creativeImage={23}, pointer={24}]", applicationId, targetingId, subscriptionId, appName, appDescription, buttonText, developerName, ageRating, appSize, averageReview, totalReviews, category, appVersion, price, currency, iconUrl, icon, creativeIdentifier, creativeOrientation, creativeResolution, creativeAspectRatioWidth, creativeAspectRatioHeight, creativeURL, creativeImage, pointer);
		}

		//Constructor

		public TDNativeAd () {
			Reset ();
		}

		public TDNativeAd Build (NativeAd adObject) {
			applicationId = adObject.applicationId;
			targetingId = adObject.targetingId;
			subscriptionId = adObject.subscriptionId;

			appName = adObject.app_name;
			appDescription = adObject.description;
			buttonText = adObject.cta_text;
			developerName = adObject.developer_name;
			ageRating = adObject.age_rating;
			appSize = adObject.app_size;
			averageReview = adObject.average_review;
			totalReviews = adObject.total_reviews;
			category = adObject.category;
			appVersion = adObject.app_version;
			price = adObject.price;
			currency = adObject.currency;
//			adUnit = adObject.adUnit; // Can be either `TDNativeAdUnitSquare`, `TDNativeAdUnitNewsfeed`, `TDNativeAdUnitFullscreen`, `TDNativeAdUnitStrip`
//			adSize = adObject.adSize; // Can be either `TDNativeAdSizeSmall`, `TDNativeAdSizeMedium`, `TDNativeAdSizeLarge`
			iconUrl = adObject.icon_url;
			icon = adObject.icon;

			creativeIdentifier = adObject.creativeIdentifier;
			creativeOrientation = adObject.creativeOrientation;
			creativeResolution = adObject.creativeResolution;

			creativeAspectRatioWidth = adObject.creativeAspectRatioWidth;
			creativeAspectRatioHeight = adObject.creativeAspectRatioHeight;

			creativeURL = adObject.image_url;
			creativeImage = adObject.creativeImage;
			pointer = adObject.pointer;

			return this;
		}

	}
}