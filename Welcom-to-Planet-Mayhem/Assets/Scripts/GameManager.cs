//=======================================================================================================================================//
// Product:    	Welcome to Planet Mayhem																								 //
// Developer : 	Pericles Barros																											 //
// Company:    	GameDevTeam																												 //
// Date: 	   	2017/04/22																						 						 //
//=======================================================================================================================================//

using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	//===============================================================================================================================//
	//==================================================== Inspector Variables ======================================================//
	//===============================================================================================================================//

	#region Inspector Variables

	[Range (2f, 500f)][SerializeField] float timeToBigBang;
	[SerializeField] float playerSelectionTime;
	[SerializeField] string playerObjectID;
	[SerializeField] GameObject playerPrefab;

	#endregion

	//===============================================================================================================================//
	//======================================================= Private Fields ========================================================//
	//===============================================================================================================================//

	#region Private Fields

	//	static MenuState Common.State;
	static int pausedByPlayer;

	const float GlobalTimeScale = 1f;
	const float FixedDeltaTime = 0.02f;

	float timer;

	static List<PlayerController> players = new List<PlayerController> (Common.NumPlayers);

	#endregion

	//===============================================================================================================================//
	//==================================================== Unity Event Functions ====================================================//
	//===============================================================================================================================//

	#region Unity Methods

	void Start ()
	{
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
		switch (Common.State) {
		case Common.GameState.PlayerLobby:
			timer -= Time.deltaTime;
			for (int i = 0; i < Common.NumPlayers; i++) {
				if (players [i] == null && Input.GetKeyDown (InputMapping.ButtonID (InputMapping.StartBtn, i)))
					SpawnPlayer (i);
			}
			break;
		}

	}

	#endregion

	//===============================================================================================================================//
	//=========================================================== Public Methods ====================================================//
	//===============================================================================================================================//

	#region Public Methods

	public static void Load ()
	{
		Instance.Start ();
	}

	public static void SetGameState (Common.GameState state)
	{
		switch (state) {
		case Common.GameState.Loading:
			//TODO: Play splash Screen & menu music
			//TODO: SetState(Menu) after animation
			break;
		case Common.GameState.Menu:
			//TODO: Display main Menu (planet, game title, menu)
			//TODO: Player music
			SceneSwitcher.SwitchScene ("MainMenu");
			break;
		case Common.GameState.PlayerLobby:
			SceneSwitcher.SwitchScene ("Lobby");
			break;
		}
	}

	public static void OnGameStart (bool[] _players)
	{		
		Common.State = Common.GameState.Loading;
		GameUI.ShowLoadingScreen ();

		int idx = 0;
		for (int i = 0; i < _players.Length; i++) {		
			if (_players [i]) {				
				SpawnPlayer (i + 1);
				players [idx++].Init ();
			}
		}
	}

	public static void OnGameOver ()
	{
		//TODO
		RankUIGlobal.UpdateGlobalRankUI (RankManager.scoreKeeperList.ToArray ());
		GameUI.OpenGameOverPanel ();
	}

	public static void Pause (int playerID)
	{
		//Pause Game
		if (Common.State == Common.GameState.Playing) {			
			pausedByPlayer = playerID;
			Common.State = Common.GameState.Paused;
			SetTimeScale (0f);
			return;
		}

		//Unpause Game
		if (Common.State == Common.GameState.Paused && playerID == pausedByPlayer) {
			SetTimeScale (GlobalTimeScale);
			pausedByPlayer = -1;
			Common.State = Common.GameState.Playing;
		}
	}

	public static bool SpawnPlayer (int playerID)
	{		
		var player = ObjectPooler.FetchInstance (Instance.playerObjectID);
		print (player);
		if (player != null) {
			player.transform.position = Planet.RandomPlayerSpawnPoint;
			var controller = player.GetComponent<PlayerController> ();
			if (controller != null) {
				controller.PlayerID = playerID;
				players.Add (controller);
				SplitScreen (players.Count);
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

	public static Color GetPlayerColor (int index)
	{
		switch (index) {
		case 1:
			return Color.red;
		case 2:
			return Color.blue;
		case 3:
			return Color.green;
		case 4:
			return Color.yellow;		
		}

		return Color.white;
	}


	#endregion

	//===============================================================================================================================//
	//========================================================== Private  Methods ===================================================//
	//===============================================================================================================================//

	#region Private Methods

	static void SetTimeScale (float scale)
	{
		Time.timeScale = scale;
		Time.fixedDeltaTime = FixedDeltaTime * Time.timeScale;
	}

	static void SplitScreen (int playerCount)
	{
		switch (playerCount) {
		case 1:
			break;
		case 2:
			Camera player1Cam2 = players [0].GetComponentInChildren<Camera> ();
			player1Cam2.rect = new Rect (0, 0, 0.5f, 1);

			Camera player2Cam2 = players [1].GetComponentInChildren<Camera> ();
			player2Cam2.rect = new Rect (0.5f, 0, 0.5f, 1);
			break;
		case 3:
			Camera player1Cam3 = players [0].GetComponentInChildren<Camera> ();
			player1Cam3.rect = new Rect (0, 0.5f, 0.5f, 0.5f);

			Camera player2Cam3 = players [1].GetComponentInChildren<Camera> ();
			player2Cam3.rect = new Rect (0.5f, 0.5f, 0.5f, 0.5f);

			Camera player3Cam3 = players [2].GetComponentInChildren<Camera> ();
			player3Cam3.rect = new Rect (0.25f, 0, 0.5f, 0.5f);
			break;
		case 4:
			Camera player3Cam4 = players [2].GetComponentInChildren<Camera> ();
			player3Cam4.rect = new Rect (0, 0, 0.5f, 0.5f);

			Camera player4Cam4 = players [3].GetComponentInChildren<Camera> ();
			player4Cam4.rect = new Rect (0.5f, 0, 0.5f, 0.5f);
			break;
		}
	}

	#endregion
}
