using UnityEngine;
using UnityEngine.UI;

namespace Tapdaq {
	public static class TDExtensionMethods {
		public static Vector2 ToVector2 (this TDNativeAdType adType) {
			switch (adType) {

			case TDNativeAdType.TDNativeAdType1x1Large:
				return new Vector2 (750,750);
				//break;
			case TDNativeAdType.TDNativeAdType1x1Medium:
				return new Vector2 (375,375);
				//break;
			case TDNativeAdType.TDNativeAdType1x1Small:
				return new Vector2 (150,150);
				//break;

			case TDNativeAdType.TDNativeAdType1x2Large:
				return new Vector2 (900,1800);
				//break;
			case TDNativeAdType.TDNativeAdType1x2Medium:
				return new Vector2 (450,900);
				//break;
			case TDNativeAdType.TDNativeAdType1x2Small:
				return new Vector2 (180,360);
				//break;

			case TDNativeAdType.TDNativeAdType2x1Large:
				return new Vector2 (1800,900);
				//break;
			case TDNativeAdType.TDNativeAdType2x1Medium:
				return new Vector2 (900,450);
				//break;
			case TDNativeAdType.TDNativeAdType2x1Small:
				return new Vector2 (360,180);
				//break;

			case TDNativeAdType.TDNativeAdType2x3Large:
				return new Vector2 (960,1440);
				//break;
			case TDNativeAdType.TDNativeAdType2x3Medium:
				return new Vector2 (480,720);
				//break;
			case TDNativeAdType.TDNativeAdType2x3Small:
				return new Vector2 (192,288);
				//break;

			case TDNativeAdType.TDNativeAdType3x2Large:
				return new Vector2 (1440,960);
				//break;
			case TDNativeAdType.TDNativeAdType3x2Medium:
				return new Vector2 (720,480);
				//break;
			case TDNativeAdType.TDNativeAdType3x2Small:
				return new Vector2 (288,192);
				//break;

			case TDNativeAdType.TDNativeAdType1x5Large:
				return new Vector2 (360,1800);
				//break;
			case TDNativeAdType.TDNativeAdType1x5Medium:
				return new Vector2 (180,900);
				//break;
			case TDNativeAdType.TDNativeAdType1x5Small:
				return new Vector2 (72,360);
				//break;

			case TDNativeAdType.TDNativeAdType5x1Large:
				return new Vector2 (1800,360);
				//break;
			case TDNativeAdType.TDNativeAdType5x1Medium:
				return new Vector2 (900,180);
				//break;
			case TDNativeAdType.TDNativeAdType5x1Small:
				return new Vector2 (360,72);
				//break;


			default:
				return Vector2.zero;
				//break;

			}
		}
	}
}