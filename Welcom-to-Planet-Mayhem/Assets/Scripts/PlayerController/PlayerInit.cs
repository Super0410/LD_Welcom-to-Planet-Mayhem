using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
	
	#region Public Members

	#endregion

	#region Private Members

	#endregion

	#region Unity Methods

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	#endregion

	#region Public Methods

	public void OnPlayerInit (int playerIndex)
	{
		ScoreKeeperForPlayer scoreKeeper = GetComponentInChildren<ScoreKeeperForPlayer> ();
		scoreKeeper.playerIndex = playerIndex;
		RankManager.AddScoreListener (scoreKeeper);
	}

	#endregion

	#region Private Methods

	#endregion
}
