using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystems.Patterns;

public class AudioManager : Singleton<AudioManager>
{
	#region Public Members

	public enum AudioChanelType
	{
		Master,
		Sfx,
		Music
	}

	#endregion

	#region Private Members

	public float masterVolumePercent { get; private set; }

	public float sfxVolumePercent { get; private set; }

	public float musicVolumePercent { get; private set; }

	public AudioLibrary.SoundGroup[] soundGroupArr;

	static AudioSource SoundSource2d;
	static AudioSource[] musicSourceArr;

	static int activeMusicSourceIndex;



	#endregion

	#region Unity Methods

	void Start ()
	{
		AudioLibrary.Populate (soundGroupArr);

		musicSourceArr = new AudioSource[2];
		for (int i = 0; i < 2; i++) {
			GameObject newSource = new GameObject ("Music Source " + (i + 1));
			musicSourceArr [i] = newSource.AddComponent<AudioSource> ();
			newSource.transform.parent = transform;
		}
		GameObject new2dSource = new GameObject ("Sound 2d Source ");
		SoundSource2d = new2dSource.AddComponent<AudioSource> ();
		new2dSource.transform.parent = transform;

		masterVolumePercent = PlayerPrefs.GetFloat ("MasterVolume", 1);
		sfxVolumePercent = PlayerPrefs.GetFloat ("SfxVolume", 1);
		musicVolumePercent = PlayerPrefs.GetFloat ("MusicVolume", 1);

	}

	#endregion

	#region Public Methods

	public void SetVolume (AudioChanelType setType, float targetVolume)
	{
		switch (setType) {
		case AudioChanelType.Master:
			masterVolumePercent = targetVolume;
			break;

		case AudioChanelType.Sfx:
			sfxVolumePercent = targetVolume;
			break;

		case AudioChanelType.Music:
			musicVolumePercent = targetVolume;
			break;
		}

		musicSourceArr [0].volume = musicVolumePercent * masterVolumePercent;
		musicSourceArr [1].volume = musicVolumePercent * masterVolumePercent;

		PlayerPrefs.SetFloat ("MasterVolume", masterVolumePercent);
		PlayerPrefs.SetFloat ("SfxVolume", sfxVolumePercent);
		PlayerPrefs.SetFloat ("MusicVolume", musicVolumePercent);
		PlayerPrefs.Save ();
	}

	public static void PlayMusic (AudioClip clip, float fadeDuration = 1)
	{
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		musicSourceArr [activeMusicSourceIndex].clip = clip;
		musicSourceArr [activeMusicSourceIndex].Play ();

		Instance.StartCoroutine (animateAudio (fadeDuration));
	}

	public void PlaySound2d (string name)
	{
		AudioClip clip = AudioLibrary.GetClipFromName (name);
		if (clip != null)
			SoundSource2d.PlayOneShot (clip, sfxVolumePercent * masterVolumePercent);
	}

	public void PlaySound2d (AudioClip clip)
	{
		if (clip != null)
			SoundSource2d.PlayOneShot (clip, sfxVolumePercent * masterVolumePercent);
	}

	#endregion

	#region Private Methods

	static IEnumerator animateAudio (float duration)
	{
		float percent = 0;
		float speed = 1 / duration;
		float maxVolume = Instance.musicVolumePercent * Instance.masterVolumePercent;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			musicSourceArr [activeMusicSourceIndex].volume = Mathf.Lerp (0, maxVolume, percent);
			musicSourceArr [1 - activeMusicSourceIndex].volume = Mathf.Lerp (maxVolume, 0, percent);
			yield return null;
		}
		musicSourceArr [1 - activeMusicSourceIndex].Stop ();
	}

	#endregion
}
