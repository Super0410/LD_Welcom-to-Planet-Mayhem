using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFade : MonoBehaviour
{

	public Texture fadeTexture;
	public float fadeSpeed = 0.3f;

	private float alpha = 1f;
	private int fadeDir = 1;

	void OnGUI ()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp (alpha, 0, 1);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
	}

	public float beginFade (int direction)
	{
//		alpha = 0;
		fadeDir = direction;
		return 1 / fadeSpeed;
	}

	void Start ()//OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
	{
		StartCoroutine (enableCamera ());
//		beginFade (-1);
	}

	IEnumerator enableCamera ()
	{
		float time = beginFade (-1) / 8;
		yield return new WaitForSeconds (time);
	}

}