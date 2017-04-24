using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerManager : Singleton<MultiPlayerManager>
{
	
	#region Public Members

	//	public static event System.Action<int> OnPlayerJoinEvent;

	#endregion

	#region Private Members

	//	int playerCount = 0;
	//	int playerAliveCount = 0;

	#endregion

	#region Unity Methods

	//	void Start ()
	//	{
	//	}

	#endregion

	#region Public Methods

	//	public void OnPlayerJoin ()
	//	{
	//		if (playerCount > 3)
	//			return;
	//
	//		playerCount++;
	//		playerAliveCount++;
	//		if (OnPlayerJoinEvent != null)
	//			OnPlayerJoinEvent (playerCount);
	//	}
	//
	//	public void OnPlayerDie ()
	//	{
	//		playerAliveCount--;
	//		if (playerAliveCount <= 0) {
	//			GameManager.OnGameOver ();
	//		}
	//	}

	public static void SplitScreen (int playerCount)
	{
		switch (playerCount) {
		case 1:
			break;
		case 2:
			Camera player1Cam2 = GameManager.GetPlayer (0).GetComponentInChildren<Camera> ();
			player1Cam2.rect = new Rect (0, 0, 0.5f, 1);

			Camera player2Cam2 = GameManager.GetPlayer (1).GetComponentInChildren<Camera> ();
			player2Cam2.rect = new Rect (0.5f, 0, 0.5f, 1);
			break;
		case 3:
			Camera player1Cam3 = GameManager.GetPlayer (0).GetComponentInChildren<Camera> ();
			player1Cam3.rect = new Rect (0, 0.5f, 0.5f, 0.5f);

			Camera player2Cam3 = GameManager.GetPlayer (1).GetComponentInChildren<Camera> ();
			player2Cam3.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);

			Camera player3Cam3 = GameManager.GetPlayer (2).GetComponentInChildren<Camera> ();
			player3Cam3.rect = new Rect (0.25f, 0, 0.5f, 0.5f);
			break;
		case 4:
			Camera player3Cam4 = GameManager.GetPlayer (2).GetComponentInChildren<Camera> ();
			player3Cam4.rect = new Rect (0, 0, 0.5f, 0.5f);

			Camera player4Cam4 = GameManager.GetPlayer (3).GetComponentInChildren<Camera> ();
			player4Cam4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);
			break;
		}
	}

	#endregion
}
