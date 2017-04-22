using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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


		FindObjectOfType<RankUIGlobal> ().UpdateGlobalRankUI (FindObjectOfType< RankManager> ().scoreKeeperList.ToArray ());
		gameUI.OpenGameOverPanel ();
	}

	#endregion

	#region Private Methods

	#endregion
}
