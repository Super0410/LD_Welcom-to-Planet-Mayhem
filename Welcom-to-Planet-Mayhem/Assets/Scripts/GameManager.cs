using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeToPlanetMayhem;
using GameSystems.Patterns;

public class GameManager : Singleton<GameManager>
{
	
	#region Public Members

	#endregion

	#region Private Members

	GameUI gameUI;

	#endregion

	#region Unity Methods

	void Start ()
	{
		gameUI = GetComponent<GameUI> ();
		Common.State = Common.GameState.Playing;
	}

	void Update ()
	{
		
	}

	#endregion

	#region Public Methods

	public void OnGameStart ()
	{
		//TODO
	}

	public void OnGameOver ()
	{
		//TODO

		RankUIGlobal.UpdateGlobalRankUI (RankManager.scoreKeeperList.ToArray ());
		gameUI.OpenGameOverPanel ();
	}

	#endregion

	#region Private Methods

	#endregion
}
