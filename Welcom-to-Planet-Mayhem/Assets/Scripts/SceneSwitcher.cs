using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//using GameSystems.Patterns;

public class SceneSwitcher : Singleton<SceneSwitcher>
{
	
	#region Public Members

	public Texture fadeTexture;
	public float fadeSpeed = 0.8f;

	#endregion

	#region Private Members

	float alpha = 1f;
	int fadeDir = 1;

	#endregion

	#region Unity Methods

	void Start ()
	{
		OnLevelWasLoaded (0);
	}

	void OnLevelWasLoaded (int index)
	{

		beginFade (-1);
	}

	void OnGUI ()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp (alpha, 0, 1);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
	}

	#endregion

	#region Public Methods

	public void SwitchScene (string sceneName)
	{
		StartCoroutine (switchWithDelay (sceneName));
	}

	#endregion

	#region Private Methods

	float beginFade (int direction)
	{
		fadeDir = direction;
		return 1 / fadeSpeed;
	}

	IEnumerator switchWithDelay (string sceneName)
	{
		beginFade (1);
		float time = beginFade (1) / 2;
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (sceneName);
	}

	#endregion

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			SwitchScene ("Menu");
		}
		if (Input.GetMouseButtonDown (1)) {
			SwitchScene ("Game");
		}
	}
}
