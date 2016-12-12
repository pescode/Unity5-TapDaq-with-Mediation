#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tapdaq {
	public class TDSettings : ScriptableObject {
		public bool showLogs = true;
		public bool testMode = true;
		public string ios_applicationID = "";
		public string ios_clientKey = "";
		public string android_applicationID = "";
		public string android_clientKey = "";
		public int frequency = 2;
		public int duration = 1;

		public AdTags tags;


		#if UNITY_EDITOR
		//[MenuItem ("Assets/Create/Tapdaq Settings")]
		public static void CreateAsset () {
			string path = "Plugins/Scripts/";
			TDSettings asset;
			TDSettings[] assets = Resources.LoadAll<TDSettings>("Tapdaq");

			if (assets != null && assets.Length > 0) {
				asset = assets [0];
			}
			else {
				string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ("Assets/" + path + "Resources/Tapdaq/TapdaqSettings.asset");
				asset = ScriptableObject.CreateInstance<TDSettings> ();

				AssetDatabase.CreateAsset (asset, assetPathAndName);

				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();
			}
			EditorUtility.FocusProjectWindow ();
			Selection.activeObject = asset;
		}

		[MenuItem ("Window/Tapdaq/Edit Settings")]
		public static void FocusOnSettings () {
			CreateAsset ();
		}
		#endif
	}


	[Serializable]
	public class AdOptions {
		public bool large = false;
		public bool medium = false;
		public bool small = false;

		#if UNITY_EDITOR
		public void DrawGUI () {
			large = EditorGUILayout.Toggle ("\tLarge", large, EditorStyles.radioButton);
			medium = EditorGUILayout.Toggle ("\tMedium", medium, EditorStyles.radioButton);
			small = EditorGUILayout.Toggle ("\tSmall", small, EditorStyles.radioButton);
		}
		#endif
	}

	[Serializable]
	public class AdTags {
		public string[] tags = new string[26];

		public Dictionary<TDAdType, string> GetTags () {
			Dictionary<TDAdType, string> toReturn = new Dictionary<TDAdType, string> ();

			for (int i = 1; i < tags.Length; i++) {
				toReturn.Add ((TDAdType)i, tags[i]);
			}

			return toReturn;
		}

		public string GetAdTypesWithTags() {
			Dictionary<TDAdType, string> adTypeTags = GetTags ();

			string adTypeTagsString = "";

			foreach (TDAdType type in adTypeTags.Keys) {
				string value = null;
				if (adTypeTags.TryGetValue (type, out value)) {
					//Format [AdType]-[Placement];
					//Placements are comma seperated before they get here
					adTypeTagsString += String.Format("{0}-{1}",type.ToString (),  value);
				}

				if (adTypeTags.Keys.Last() != type)
					adTypeTagsString += ";"; //add a semi colon to seperate each ad type
			}

			return adTypeTagsString;
		}


		public string GetTagSets () {
			List<string[]> firstPass = new List<string[]> ();
			Dictionary<string, List<int>> secondPass = new Dictionary<string, List<int>> ();
			string finalPass = string.Empty;

			for (int i = 0; i < tags.Length; i++) {
				firstPass.Add (tags[i].Split (','));
			}

			for (int x = 0; x < firstPass.Count; x++) {
				for (int y = 0; y < firstPass[x].Length; y++) {
					if (firstPass [x] [y].Trim () == string.Empty)
						continue;

					List<string[]> adTypes = firstPass.Where (_x => _x.ToList ().Contains (firstPass [x] [y])).ToList ();

					Debug.Log (firstPass [x] [y]);

					for (int z = 0; z < adTypes.Count; z++) {
						List<int> outVaue;
						if (!secondPass.TryGetValue (firstPass [x] [y], out outVaue)) {
							secondPass.Add (firstPass [x] [y], new List<int> ());
						}

						int index = firstPass.IndexOf (adTypes [z]);
						if (!secondPass [firstPass [x] [y]].Contains (index)) {
							secondPass [firstPass [x] [y]].Add (index);
						}
					}
				}
			}

			foreach (var i in secondPass) {
				finalPass += "#####";
				finalPass += i.Key;
				finalPass += "@@@";

				for (int t = 0; t < i.Value.Count; t++) {
					if (t > 0)
						finalPass += "%%%%";

					finalPass += i.Value [t].ToString ();
				}
			}

			finalPass = finalPass.Remove (0, 5);

			//Debug.Log (finalPass);

			return finalPass;
		}

		#if UNITY_EDITOR
		private bool showOther = false;
		private bool show1x1 = false;
		private bool show1x2 = false;
		private bool show2x1 = false;
		private bool show2x3 = false;
		private bool show3x2 = false;
		private bool show1x5 = false;
		private bool show5x1 = false;

		public void DrawGUI () {
			GUIStyle foldout = EditorStyles.foldout;
			foldout.fontStyle = FontStyle.Bold;

			GUILayout.Label ("Ad Types Tags", EditorStyles.boldLabel);
			GUILayout.Label ("--Interstitials & Video Ads--", EditorStyles.boldLabel);
			showOther = EditorGUILayout.Foldout (showOther, "Interstitials & Video Ads", foldout);

			if (showOther) {
				DrawTextArea ((int)TDAdType.TDAdTypeInterstitial);
				DrawTextArea ((int)TDAdType.TDAdTypeVideo);
				DrawTextArea ((int)TDAdType.TDAdTypeRewardedVideo);
			}

			GUILayout.Label ("--Natives--", EditorStyles.boldLabel);
			show1x1 = EditorGUILayout.Foldout (show1x1, "1x1", foldout);

			if (show1x1) {
				DrawTextArea ((int)TDAdType.TDAdType1x1Large);
				DrawTextArea ((int)TDAdType.TDAdType1x1Medium);
				DrawTextArea ((int)TDAdType.TDAdType1x1Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show1x2 = EditorGUILayout.Foldout (show1x2, "1x2", foldout);

			if (show1x2) {
				DrawTextArea ((int)TDAdType.TDAdType1x2Large);
				DrawTextArea ((int)TDAdType.TDAdType1x2Medium);
				DrawTextArea ((int)TDAdType.TDAdType1x2Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show2x1 = EditorGUILayout.Foldout (show2x1, "2x1", foldout);

			if (show2x1) {
				DrawTextArea ((int)TDAdType.TDAdType2x1Large);
				DrawTextArea ((int)TDAdType.TDAdType2x1Medium);
				DrawTextArea ((int)TDAdType.TDAdType2x1Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show2x3 = EditorGUILayout.Foldout (show2x3, "2x3", foldout);

			if (show2x3) {
				DrawTextArea ((int)TDAdType.TDAdType2x3Large);
				DrawTextArea ((int)TDAdType.TDAdType2x3Medium);
				DrawTextArea ((int)TDAdType.TDAdType2x3Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show3x2 = EditorGUILayout.Foldout (show3x2, "3x2", foldout);

			if (show3x2) {
				DrawTextArea ((int)TDAdType.TDAdType3x2Large);
				DrawTextArea ((int)TDAdType.TDAdType3x2Medium);
				DrawTextArea ((int)TDAdType.TDAdType3x2Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show1x5 = EditorGUILayout.Foldout (show1x5, "1x5", foldout);

			if (show1x5) {
				DrawTextArea ((int)TDAdType.TDAdType1x5Large);
				DrawTextArea ((int)TDAdType.TDAdType1x5Medium);
				DrawTextArea ((int)TDAdType.TDAdType1x5Small);
			}

			EditorGUILayout.LabelField (string.Empty, GUI.skin.horizontalSlider);

			show5x1 = EditorGUILayout.Foldout (show5x1, "5x1", foldout);

			if (show5x1) {
				DrawTextArea ((int)TDAdType.TDAdType5x1Large);
				DrawTextArea ((int)TDAdType.TDAdType5x1Medium);
				DrawTextArea ((int)TDAdType.TDAdType5x1Small);
			}
		}

		private void DrawTextArea (int index) {

			tags [index] = EditorGUILayout.TextField (((TDAdType)index).ToString (), tags [index]);

		}
		#endif
	}
}