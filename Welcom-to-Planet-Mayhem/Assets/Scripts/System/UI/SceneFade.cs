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

	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return 1 / fadeSpeed;
	}

	void Start ()
	{
		StartCoroutine (EnableCamera ());
	}

	IEnumerator EnableCamera ()
	{
		float time = BeginFade (-1) / 8;
		yield return new WaitForSeconds (time);
	}

}