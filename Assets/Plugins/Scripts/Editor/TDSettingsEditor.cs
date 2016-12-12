using UnityEngine;
using UnityEditor;
using System;

namespace Tapdaq {
	[CustomEditor (typeof(TDSettings))]
	public class TDSettingsEditor : Editor {

		private TDSettings settings;

		void OnEnable () {
			settings = (TDSettings)target;
		}

		public override void OnInspectorGUI () {
			GUIStyle foldout = EditorStyles.foldout;
			foldout.fontStyle = FontStyle.Bold;

			serializedObject.Update ();

			GUILayout.Label ("You must have an App ID and Client Key to use Tapdaq", EditorStyles.boldLabel);
		
			if (GUILayout.Button ("Visit Tapdaq.com")) {
				Application.OpenURL ("https://tapdaq.com/dashboard/apps");
			}
		
			GUILayout.Label ("Ad Settings", EditorStyles.boldLabel);


			//Logs
			settings.showLogs = EditorGUILayout.Toggle ("Show Additional Logs", settings.showLogs);

			//Test mode
			//settings.testMode = EditorGUILayout.Toggle ("TEST MODE", settings.testMode);
			GUILayout.Label ("", EditorStyles.boldLabel);
		
			//application ID + client key
			settings.ios_applicationID = EditorGUILayout.TextField ("iOS Application ID", settings.ios_applicationID);
			settings.ios_clientKey = EditorGUILayout.TextField ("iOS Client Key", settings.ios_clientKey);
		
			GUILayout.Label ("", EditorStyles.boldLabel);

			settings.android_applicationID = EditorGUILayout.TextField ("Android Application ID", settings.android_applicationID);
			settings.android_clientKey = EditorGUILayout.TextField ("Android Client Key", settings.android_clientKey);
			GUILayout.Label ("", EditorStyles.boldLabel);
		
			//------frequency
			settings.frequency = (int)EditorGUILayout.Slider ("Frequency", settings.frequency, 0, 2000);
		
			//-----duration
			settings.duration = (int)EditorGUILayout.Slider ("Duration", settings.duration, 0, 2000);

			GUILayout.Label ("", EditorStyles.boldLabel);
			//---GROUP TAGS

			settings.tags.DrawGUI ();

			if (GUI.changed)
				EditorUtility.SetDirty (settings);

		}
	}
}