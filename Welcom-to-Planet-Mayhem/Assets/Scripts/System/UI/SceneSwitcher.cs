using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

		BeginFade (-1);
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

	public static void SwitchScene (string sceneName)
	{
		Instance.StartCoroutine (Instance.SwitchWithDelay (sceneName));
	}

	#endregion

	#region Private Methods

	float BeginFade (int direction)
	{
		fadeDir = direction;
		return 1 / fadeSpeed;
	}

	IEnumerator SwitchWithDelay (string sceneName)
	{
		float time = BeginFade (1) / 2;
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
