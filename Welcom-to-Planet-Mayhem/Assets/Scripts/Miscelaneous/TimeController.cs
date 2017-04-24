
using UnityEngine;
using System.Collections;

public class TimeController: MonoBehaviour
{
	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	static bool inBulletTime;
	TimeController instance;

	#endregion

	//===============================================================================================================================//
	//===================================================== Public Properties =======================================================//
	//===============================================================================================================================//

	#region Public Properties

	public static TimeController Instance;

	public float bulletTimeSpeed = 5f;
	[Range (0f, 100f)] public float bulletTimeScale = 0.02f;


	#endregion

	//===============================================================================================================================//
	//========================================================= Unity Event Methods =================================================//
	//===============================================================================================================================//

	#region Unity Event Methods

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			return;
		}

		Destroy (gameObject);
	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public static void BulletTime (float duration)
	{
		if (!inBulletTime) {
			inBulletTime = true;
			Instance.StartCoroutine (Instance.DoBulletTime (duration, Instance.bulletTimeScale));
		}
	}

	public static void BulletTime (float duration, float timeScale)
	{
		if (!inBulletTime) {
			inBulletTime = true;
			Instance.StartCoroutine (Instance.DoBulletTime (duration, timeScale));
		}
	}

	#endregion

	//===============================================================================================================================//
	//============================================================= Coroutines ======================================================//
	//===============================================================================================================================//

	#region Coroutines

	IEnumerator DoBulletTime (float duration, float scale)
	{
		float originalScale = Time.timeScale;
		inBulletTime = true;

		Time.timeScale = scale;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;

		while (duration > 0.1f) {
			Time.timeScale += (1 / duration) * Time.deltaTime;
			Time.fixedDeltaTime = 0.02f * Time.timeScale;
			duration -= Time.unscaledDeltaTime;
			yield return null;
		}

		Time.timeScale = originalScale;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;

		inBulletTime = false;
	}

	#endregion

	//===============================================================================================================================//
	//============================================================= Debugging =======================================================//
	//===============================================================================================================================//

	#region Debugging

	void OnValidate ()
	{
		if (bulletTimeSpeed < 0.01f)
			bulletTimeSpeed = 0.01f;
	}


	#endregion

}
