using UnityEngine;
using System.Collections.Generic;
using WelcomeToPlanetMayhem;

public class GameManager : Singleton<GameManager>
{
	enum MenuState
	{
		MainMenu,
		PlayerLobby,
		Options,

	}

	#region Public Members

	[Range (2f, 500f)][SerializeField] float timeToBigBang;
	[SerializeField] string playerObjectID;
	[SerializeField] GameObject playerPrefab;

	#endregion

	#region Private Members

	static GameUI gameUI;
	static bool isPaused;
	static int pausedByPlayer;

	const float GlobalTimeScale = 1f;
	const float FixedDeltaTime = 0.02f;

	static List<PlayerController> players = new List<PlayerController> ();

	#endregion

	#region Unity Methods

	void Start ()
	{
		gameUI = GetComponent<GameUI> ();
		Common.State = Common.GameState.Menu;

		var weapons = Resources.LoadAll<GameObject> ("Weapons/");
		for (int i = 0; i < weapons.Length; i++) {
			ObjectPooler.CreatePool (weapons [i].name, weapons [i], 10);
		}

		var bullets = Resources.LoadAll<GameObject> ("Bullets/");
		for (int i = 0; i < bullets.Length; i++) {
			ObjectPooler.CreatePool (bullets [i].name, bullets [i], 100);
		}

		ObjectPooler.CreatePool (playerObjectID, playerPrefab, 10);

		Planet.Load ();
	}

	void Update ()
	{
		//TODO: Read input from Controller 1

	

	}

	#endregion

	#region Public Methods

	public static void OnGameStart ()
	{
		//TODO
	}

	public static void OnGameOver ()
	{
		//TODO

		RankUIGlobal.UpdateGlobalRankUI (RankManager.scoreKeeperList.ToArray ());
		gameUI.OpenGameOverPanel ();
	}

	public static void Pause (int playerID)
	{
		if (!isPaused) {
			isPaused = true;
			pausedByPlayer = playerID;

			SetTimeScale (0f);
			return;
		}

		if (isPaused && playerID == pausedByPlayer) {
			SetTimeScale (GlobalTimeScale);
			isPaused = false;
			pausedByPlayer = -1;
		}
	}

	public static bool SpawnPlayer (int playerID)
	{
		var player = ObjectPooler.FetchInstance (Instance.playerObjectID);
		if (player != null) {
			
			Vector3 spawnPos = Planet.RandomEnemySpawnPoint;
			Vector3 dir2Plane = (Planet.Instance.transform.position - spawnPos).normalized;
			Quaternion spawnRotation = Quaternion.LookRotation (dir2Plane);
			spawnRotation.eulerAngles = new Vector3 (spawnRotation.eulerAngles.x - 90, spawnRotation.eulerAngles.y, spawnRotation.eulerAngles.z);

			player.transform.position = spawnPos;
			player.transform.rotation = spawnRotation;

			var controller = player.GetComponent<PlayerController> ();
			if (controller != null) {
				controller.PlayerID = playerID;
				controller.Init ();
				players.Add (controller);
				return true;
			}
		}

		return false;
	}

	public static GameObject GetPlayer (int index)
	{
		if (index >= 0 && index < players.Count) {
			return players [index].gameObject;
		}
		return null;
	}

	#endregion

	#region Private Methods

	static void SetTimeScale (float scale)
	{
		Time.timeScale = scale;
		Time.fixedDeltaTime = FixedDeltaTime * Time.timeScale;
	}

	#endregion
}
