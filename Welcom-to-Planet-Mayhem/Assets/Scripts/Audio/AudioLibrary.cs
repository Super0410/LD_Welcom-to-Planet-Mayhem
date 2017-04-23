using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioLibrary
{
	#region Private Members

	static Dictionary<string,AudioClip[]> soundDic = new Dictionary<string, AudioClip[]> ();

	#endregion

	#region Contructor

	public static void Populate (SoundGroup[] soundGroupArr)
	{
		for (int i = 0; i < soundGroupArr.Length; i++) {
			soundDic.Add (soundGroupArr [i].groupId, soundGroupArr [i].groupAudioArr);
		}
	}

	#endregion

	#region Public Methods

	public static AudioClip GetClipFromName (string name)
	{
		if (soundDic.ContainsKey (name)) {
			AudioClip[] tempClipArr = soundDic [name];
			return tempClipArr [Random.Range (0, tempClipArr.Length)];
		}
		return null;
	}

	#endregion

	[System.Serializable]
	public struct SoundGroup
	{
		public string groupId;
		public AudioClip[] groupAudioArr;
	}
}
