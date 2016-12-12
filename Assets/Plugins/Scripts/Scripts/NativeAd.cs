using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;


namespace Tapdaq {
	public class NativeAd {
		public string applicationId { get; private set; }

		public string targetingId { get; private set; }

		public string subscriptionId { get; private set; }

		public string title { get; private set; }
		public string app_name { get; private set; }
		public string description { get; private set; }
		public string cta_text { get; private set; }
		public string developer_name { get; private set; }
		public string age_rating { get; private set; }
		public string app_size { get; private set; }
		public float average_review { get; private set; }
		public int total_reviews { get; private set; }
		public string category { get; private set; }
		public string app_version { get; private set; }
		public float price { get; private set; }
		public string currency { get; private set; }
		public string icon_url { get; private set; }
		public string image_url { get; private set; }

		public Texture2D icon { get; private set; }
		public Texture2D creativeImage { get; private set; }

		public string creativeIdentifier { get; private set; }
		public TDOrientation creativeOrientation { get; private set; } // Can be either `TDOrientationPortrait` or `TDOrientationLandscape
		public string creativeResolution { get; private set; } // Can be `TDResolution1x`, `TDResolution2x` or `TDResolution3x`
		public int creativeAspectRatioWidth { get; private set; }
		public int creativeAspectRatioHeight { get; private set; }

		public int pointer{ get; private set; }

		public NativeAd (string objcAdString) {
			string[] adObject = objcAdString.Split (new[]{ ";" }, System.StringSplitOptions.None);
			if (adObject.Length == 0) {
				AdManager.LogMessage (TDLogSeverity.debug, "this ad Object is empty, SDK has not initialized yet.");
			} else {
				foreach (string k in adObject) {
					if(k.StartsWith("title")) {
						title =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("app_name")) {
						app_name =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("description")) {
						description =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("cta_text")) {
						cta_text =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("developer_name")) {
						developer_name =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("age_rating")) {
						age_rating =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("app_size")) {
						app_size =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("average_review")) {
						average_review = float.Parse(k.Substring(k.IndexOf(":") + 1));
					} else if(k.StartsWith("total_reviews")) {
						total_reviews = Int32.Parse(k.Substring(k.IndexOf(":") + 1));
					} else if(k.StartsWith("category")) {
						category =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("app_version")) {
						app_version =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("price")) {
						price = float.Parse(k.Substring(k.IndexOf(":") + 1));
					} else if(k.StartsWith("currency")) {
						currency =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("icon_url")) {
						icon_url =  k.Substring(k.IndexOf(":") + 1);
					} else if(k.StartsWith("image_url")) {
						image_url =  k.Substring(k.IndexOf(":") + 1);
					}
				}

				creativeImage = PathToTexture (image_url);
			}


		}

		//Read I.O. path and build texture.
		private Texture2D PathToTexture (string path) {
			int width = Screen.width;
			int height = Screen.height;

			Texture2D tex = new Texture2D (width, height, TextureFormat.RGBA32, false);
			byte[] imageBytes;
			if (path != null) {	
				imageBytes = File.ReadAllBytes (path);
				tex.LoadImage (imageBytes);
			}

			imageBytes = null;

			return tex;
		
		}
	}
}