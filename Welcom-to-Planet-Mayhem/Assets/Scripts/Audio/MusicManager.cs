using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	
	#region Public Members

	public SceneBg[] sceneBgArr;

	#endregion

	#region Private Members

	string activeSceneName;
	Dictionary<string , AudioClip> sceneBgDict = new Dictionary<string, AudioClip> ();

	#endregion

	#region Unity Methods

	void Awake ()
	{
		for (int i = 0; i < sceneBgArr.Length; i++) {
			sceneBgDict.Add (sceneBgArr [i].sceneName, sceneBgArr [i].sceneBg);
		}
	}

	void Start ()
	{
		OnLevelWasLoaded (0);
	}

	void OnLevelWasLoaded (int index)
	{
		string newSceneName = SceneManager.GetActiveScene ().name;

		if (newSceneName != activeSceneName) {
			activeSceneName = newSceneName;
			Invoke ("onChangeMusic", 0.25f);
		}

	}

	#endregion

	#region Private Methods

	void onChangeMusic ()
	{
		if (!sceneBgDict.ContainsKey (activeSceneName))
			return;
		
		AudioClip targetClip = sceneBgDict [activeSceneName];

		if (targetClip != null) {
			AudioManager.PlayMusic (targetClip, 1);
			Invoke ("onChangeMusic", targetClip.length);
		}
	}

	#endregion

	[System.Serializable]
	public struct SceneBg
	{
		public string sceneName;
		public AudioClip sceneBg;
	}
}
