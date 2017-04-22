using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	
	#region Public Members

	public GameObject playerPrefab;

	#endregion

	#region Private Members

	GameObject[] allPlayerArr;

	#endregion

	#region Unity Methods

	void Start ()
	{
		GetComponent<MultiPlayerManager> ().OnPlayerJoinEvent += onPlayerSpawn;
		allPlayerArr = new GameObject[4];
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	void onPlayerSpawn (int playerCount)
	{
		GameObject newPlayer = Instantiate (playerPrefab);
		allPlayerArr [playerCount - 1] = newPlayer;

		switch (playerCount) {
		case 1:
			break;
		case 2:
			Camera player1Cam2 = allPlayerArr [0].GetComponentInChildren<Camera> ();
			player1Cam2.rect = new Rect (0, 0, 0.5f, 1);

			Camera player2Cam2 = allPlayerArr [1].GetComponentInChildren<Camera> ();
			player2Cam2.rect = new Rect (0.5f, 0, 0.5f, 1);
			break;
		case 3:
			Camera player1Cam3 = allPlayerArr [0].GetComponentInChildren<Camera> ();
			player1Cam3.rect = new Rect (0, 0.5f, 0.5f, 0.5f);

			Camera player2Cam3 = allPlayerArr [1].GetComponentInChildren<Camera> ();
			player2Cam3.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);

			Camera player3Cam3 = allPlayerArr [2].GetComponentInChildren<Camera> ();
			player3Cam3.rect = new Rect (0.25f, 0, 0.5f, 0.5f);
			break;
		case 4:
			Camera player3Cam4 = allPlayerArr [2].GetComponentInChildren<Camera> ();
			player3Cam4.rect = new Rect (0, 0, 0.5f, 0.5f);

			Camera player4Cam4 = allPlayerArr [3].GetComponentInChildren<Camera> ();
			player4Cam4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);
			break;
		}
	}

	#endregion
}
