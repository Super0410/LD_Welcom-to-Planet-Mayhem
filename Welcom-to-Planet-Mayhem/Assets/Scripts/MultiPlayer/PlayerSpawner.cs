using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	//
	//	#region Public Members
	//
	//	public GameObject playerPrefab;
	//
	//	#endregion
	//
	//	#region Private Members
	//
	//	#endregion
	//
	//	#region Unity Methods
	//
	//	void Start ()
	//	{
	//		MultiPlayerManager.OnPlayerJoinEvent += OnPlayerSpawn;
	//	}
	//
	//	#endregion
	//
	//	#region Public Methods
	//
	//	#endregion
	//
	//	#region Private Methods
	//
	//	static void OnPlayerSpawn (int playerCount)
	//	{
	//		if (!GameManager.SpawnPlayer (playerCount))
	//			return;
	//
	//		switch (playerCount) {
	//		case 1:
	//			break;
	//		case 2:
	//			Camera player1Cam2 = GameManager.GetPlayer (0).GetComponentInChildren<Camera> ();
	//			player1Cam2.rect = new Rect (0, 0, 0.5f, 1);
	//
	//			Camera player2Cam2 = GameManager.GetPlayer (1).GetComponentInChildren<Camera> ();
	//			player2Cam2.rect = new Rect (0.5f, 0, 0.5f, 1);
	//			break;
	//		case 3:
	//			Camera player1Cam3 = GameManager.GetPlayer (0).GetComponentInChildren<Camera> ();
	//			player1Cam3.rect = new Rect (0, 0.5f, 0.5f, 0.5f);
	//
	//			Camera player2Cam3 = GameManager.GetPlayer (1).GetComponentInChildren<Camera> ();
	//			player2Cam3.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
	//
	//			Camera player3Cam3 = GameManager.GetPlayer (2).GetComponentInChildren<Camera> ();
	//			player3Cam3.rect = new Rect (0.25f, 0, 0.5f, 0.5f);
	//			break;
	//		case 4:
	//			Camera player3Cam4 = GameManager.GetPlayer (2).GetComponentInChildren<Camera> ();
	//			player3Cam4.rect = new Rect (0, 0, 0.5f, 0.5f);
	//
	//			Camera player4Cam4 = GameManager.GetPlayer (3).GetComponentInChildren<Camera> ();
	//			player4Cam4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);
	//			break;
	//		}
	//	}
	//
	//	#endregion
}
